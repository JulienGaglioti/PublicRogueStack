using System;
using System.Collections;
using System.Collections.Generic;
// using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

public class Location : Card
{
    public List<CardGeneration> MacroSequence;
    // [ShowInInspector] 
    private int _index;

    protected override void InitializeStats()
    {
        base.InitializeStats();
        foreach (var cardGen in MacroSequence)
        {
            cardGen.InitializeGenerator();
        }
    }

    // [Button("Pick a Card")]
    public List<Card> GenerateCard()
    {
        List<Card> pickedCards = MacroSequence[_index].PickCards();
        CurrentSequenceCheck();
        return pickedCards;
    }

    private void CurrentSequenceCheck()
    {
        if (MacroSequence[_index].ThisSequenceIsOver)
        {
            _index++;
        }

        if (_index == MacroSequence.Count)
        {
            Stack.SplitStack(1);
            Destroy(Stack.gameObject);
        }
    }
}

[Serializable]
public class CardGeneration
{
    public Type GenerationType;
    [HideInInspector] public bool ThisSequenceIsOver;

    // [ShowIf("GenerationType", Type.WeightedRandom)]
    public List<CardJoin> WeightedPicker = new List<CardJoin>();
    // [ShowIf("GenerationType", Type.WeightedRandom)]
    public List<CardProbability> WeightedRandomCards;
    // [ShowIf("GenerationType", Type.WeightedRandom)]
    public int InsideWeightedRandomIndex;

    // [ShowIf("GenerationType", Type.Sequence)]
    public int InsideSequenceIndex;
    // [ShowIf("GenerationType", Type.Sequence)]
    public List<CardJoin> SequencePicker;

    // [ShowIf("GenerationType", Type.RandomOrder)]
    public List<CardJoin> RandomOrderPicker;

    public List<Card> PickCards()
    {
        List<Card> cardsToReturn = new List<Card>();
        bool keepLooking = true;
        switch (GenerationType)
        {
            case Type.WeightedRandom:
                do
                {
                    int randIndex = Random.Range(0, WeightedPicker.Count);
                    CardJoin chosenCard = WeightedPicker[randIndex];
                    cardsToReturn.Add(chosenCard.GeneratedCard);
                    keepLooking = chosenCard.Join;
                    if (InsideWeightedRandomIndex > 0)
                    {
                        InsideWeightedRandomIndex--;
                    }
                    if (InsideWeightedRandomIndex == 0)
                    {
                        keepLooking = false;
                        ThisSequenceIsOver = true;
                    }
                } while (keepLooking);
                break;

            case Type.Sequence:
                do
                {
                    CardJoin chosenCard = SequencePicker[InsideSequenceIndex];
                    InsideSequenceIndex++;
                    cardsToReturn.Add(chosenCard.GeneratedCard);
                    keepLooking = chosenCard.Join;
                    if (InsideSequenceIndex >= SequencePicker.Count)
                    {
                        keepLooking = false;
                        ThisSequenceIsOver = true;
                    }
                } while (keepLooking);
                break;

            case Type.RandomOrder:
                do
                {
                    int randIndex = Random.Range(0, RandomOrderPicker.Count);
                    CardJoin chosenCard = RandomOrderPicker[randIndex];
                    cardsToReturn.Add(chosenCard.GeneratedCard);
                    keepLooking = chosenCard.Join;
                    RandomOrderPicker.Remove(chosenCard);
                    if (RandomOrderPicker.Count == 0)
                    {
                        keepLooking = false;
                        ThisSequenceIsOver = true;
                    }
                } while (keepLooking);
                break;
        }
        return cardsToReturn;
    }

    public void InitializeGenerator()
    {
        switch (GenerationType)
        {
            case Type.WeightedRandom:
                foreach (var card in WeightedRandomCards)
                {
                    for (int i = 0; i < card.Probability; i++)
                    {
                        WeightedPicker.Add(card.Card);
                    }
                }
                break;

            case Type.Sequence:

                break;

            case Type.RandomOrder:

                break;
        }
    }

    [Serializable]
    public enum Type
    {
        WeightedRandom,
        Sequence,
        RandomOrder
    }

    [Serializable]
    public class CardProbability
    {
        public CardJoin Card;
        public int Probability;
    }

    [Serializable]
    public class CardJoin
    {
        public Card GeneratedCard;
        public bool Join;
    }

}
