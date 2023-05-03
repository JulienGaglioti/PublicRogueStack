using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class StackOfCards : MonoBehaviour
{
    public List<Card> Cards = new List<Card>();

    [SerializeField] private BoxCollider _collider;
    private CardSO _cardToCheck;
    private List<CombinationSO> _combinationsToCheck;
    private CombinationSO _correctCombination;
    private Vector3 _colliderStartingSize;
    private Vector3 _colliderStartingCenter;

    private void Awake()
    {
        _colliderStartingCenter = _collider.center;
        _colliderStartingSize = _collider.size;
    }

    public void AddCard(Card cardToAdd)
    {
        Cards.Add(cardToAdd);
        if (Cards.Count == 1)
        {
            name = cardToAdd.CardInfo.Name;
        }
        CancelCombinationTimer();
        ResizeCollider();
    }

    public void RemoveCardAndCheckCombinations(Card cardToRemove)
    {
        Cards.Remove(cardToRemove);
        if (Cards.Count == 0)
        {
            Destroy(gameObject);
            return;
        }
        CancelCombinationTimer();
        CheckForCombinations();
        ResizeCollider();
    }

    public void ExtractCard(int index)
    {
        Cards[index].InitializeStack();
    }

    public void SplitStack(int startingIndex)
    {
        StackOfCards newStack = Instantiate(ObjectReferences.Instance.StackPrefab);
        newStack.transform.position = new Vector3(Cards[startingIndex].transform.position.x, Cards[startingIndex].transform.position.y, 0);
        for (int i = startingIndex; i < Cards.Count; i++)
        {
            Cards[i].AddToStack(newStack);
        }

        int numberOfCardsToRemove = Cards.Count - startingIndex;
        for (int i = 0; i < numberOfCardsToRemove; i++)
        {
            Cards.RemoveAt(Cards.Count - 1);
        }

        CheckForCombinations();
        ResizeCollider();
    }
    public void CheckForCombinations()
    {
        // Cards[0].CardInfo.Combinations[0].OnCombineEnd(this);
        if (Cards.Count == 0)
        {
            return;
        }

        _combinationsToCheck = new List<CombinationSO>();
        int lowestCount = 1000;
        int iterations = 0;
        _cardToCheck = Cards[0].CardInfo;

        // Take the card with the smallest amount of combinations
        foreach (var card in Cards)
        {
            foreach (var tag in card.CardInfo.Tags)
            {
                iterations++;
                if (tag.Combinations.Count < lowestCount && tag.Combinations.Count != 0)
                {
                    lowestCount = tag.Combinations.Count;
                    _cardToCheck = card.CardInfo;
                }
            }
        }

        // Take the combinations that have the same amount of cards as this stack
        for (int i = 0; i < _cardToCheck.Tags.Count; i++)
        {
            for (int j = 0; j < _cardToCheck.Tags[i].Combinations.Count; j++)
            {
                iterations++;
                if (_cardToCheck.Tags[i].Combinations[j].CardTagsSO.Count == Cards.Count)
                {
                    _combinationsToCheck.Add(_cardToCheck.Tags[i].Combinations[j]);
                }
            }
        }

        for (int i = 0; i < _combinationsToCheck.Count; i++)
        {
            List<CardTagSO> tempList = new List<CardTagSO>();

            foreach (var cardTag in _combinationsToCheck[i].CardTagsSO)
            {
                tempList.Add(cardTag);
            }

            foreach (var cardOnStack in Cards)
            {
                iterations++;

                foreach (var cardOnStackTag in cardOnStack.CardInfo.Tags)
                {
                    if (tempList.Contains(cardOnStackTag))
                    {
                        tempList.Remove(cardOnStackTag);
                    }
                }

                if (tempList.Count == 0)
                {
                    // print(_combinationsToCheck[i].name);
                    _correctCombination = _combinationsToCheck[i];
                    StartCombination();
                    return;
                }
            }
        }

        // print(iterations);
    }

    private void StartCombination()
    {
        SortCards();
        if (_correctCombination.TimeNeeded > 0)
        {
            Card firstCard = Cards[0];
            firstCard.Timer.InitializeCombination(this, _correctCombination.TimeNeeded);
            firstCard.StateController.EnterCombinationProgress();
        }
        else
        {
            OnTimerEnd();
        }
    }

    private void ResizeCollider()
    {
        _collider.size = new Vector3(_colliderStartingSize.x, _colliderStartingSize.y + (Cards.Count - 1), _colliderStartingSize.z);
        _collider.center = new Vector3(_colliderStartingCenter.x, -0.5f * (Cards.Count - 1), _colliderStartingCenter.z);
    }

    private void SortCards()
    {
        List<Card> sortedList = new List<Card>();
        Vector3 firstCardPosition = Vector3.zero;

        foreach (var cardTagSO in _correctCombination.CardTagsSO)
        {
            foreach (var card in Cards)
            {
                if (card.CardInfo.Tags.Contains(cardTagSO))
                {
                    if (card.PositionOnStack == 0)
                    {
                        firstCardPosition = card.transform.position;
                    }
                    sortedList.Add(card);
                    Cards.Remove(card);
                    break;
                }
            }
        }

        Cards = sortedList;
        for (int i = 0; i < Cards.Count; i++)
        {
            Cards[i].PositionOnStack = i;
            if (i == 0)
            {
                Cards[i].transform.position = firstCardPosition;
            }
            else
            {
                Cards[i].AdjustPosition();
            }
        }
    }

    public void CancelCombinationTimer()
    {
        Cards[0].StateController.ExitCombinationProgress();
    }

    public void OnTimerEnd()
    {
        Cards[0].StateController.ExitCombinationProgress();
        transform.position = new Vector3(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y), 0);
        _correctCombination.OnCombineEnd(this);
        CheckForCombinations();
    }

    private void OnTriggerEnter(Collider other)
    {
        Vector3 direction = transform.position - other.transform.position;
        transform.position += direction.normalized * Time.deltaTime * 3;
    }

    private void OnTriggerStay(Collider other)
    {
        Vector3 direction = transform.position - other.transform.position;
        transform.position += direction.normalized * Time.deltaTime * 3;
    }
}
