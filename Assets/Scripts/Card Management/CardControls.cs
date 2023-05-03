using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CardControls : MonoBehaviour
{
    [SerializeField] private Transform _stackCenter;
    [SerializeField] private LayerMask _cardAndCombatLayerMask;
    [SerializeField] private Card thisCard;
    
    private Camera _camera;
    private Vector3 _offset;
    private bool _isMovingMultiple;
    private bool _dragInvalid;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void OnMouseDown()
    {
        _offset = transform.position - GetMousePosition() + Vector3.back * 100;
        if (thisCard.StateController.IsInCombat)
        {
            CombatUnit combatCard = (CombatUnit)thisCard;
            if (combatCard.Combat.Allies.Count > 1)
            {
                combatCard.Combat.RemoveMember(combatCard);
                combatCard.StopCombatBehaviour();
            }
            else
            {
                _dragInvalid = true;
            }
        }
        else
        {
            _dragInvalid = false;

            if (thisCard.PositionOnStack != 0)
            {
                thisCard.Stack.CancelCombinationTimer();
                thisCard.Stack.SplitStack(thisCard.PositionOnStack);
            }

            if (thisCard.Stack.Cards.Count > 1)
            {
                _isMovingMultiple = true;
            }
        }
    }

    private void OnMouseDrag()
    {
        if(_dragInvalid)
            return;
        
        thisCard.Stack.transform.position = GetMousePosition() + _offset;
    }

    private void OnMouseUp()
    {
        if (_dragInvalid)
            return;
        
        GetComponent<Collider2D>().enabled = false;
        
        RaycastHit2D hitInfo = Physics2D.BoxCast(_stackCenter.position, Vector2.one/2, 0, Vector2.zero, 3, _cardAndCombatLayerMask);
        if (hitInfo)
        {
            if(hitInfo.transform.TryGetComponent(out Card hitCard))
            {
                InteractWithCard(hitCard);
            }
            else if (hitInfo.transform.parent.TryGetComponent(out CombatBox combatBox))
            {
                InteractWithCombat(combatBox);
            }
        }
        else
        {
            PutCardDown();
        }
        
        GetComponent<Collider2D>().enabled = true;
        _isMovingMultiple = false;
    }


    private void OnMouseEnter()
    {
        thisCard.SetUI();
    }

    private void InteractWithCard(Card hitCard)
    {
        if (_isMovingMultiple)
        {
            StackOfCards oldStack = thisCard.Stack;
            oldStack.CancelCombinationTimer();
            
            foreach (var cardInStack in oldStack.Cards)
            {
                cardInStack.AddToStack(hitCard.Stack);
            }

            Destroy(oldStack.gameObject);
            hitCard.Stack.CheckForCombinations();
        }
        else
        {
            if (hitCard.StateController.IsInCombat)
            {
                // Join combat

                if (thisCard.GetType() == typeof(CombatUnit))
                {
                    CombatUnit hitCombatCard = (CombatUnit)hitCard;
                    CombatUnit thisCombatCard = (CombatUnit)thisCard;
                    hitCombatCard.Combat.AddMember(thisCombatCard);
                }
                else
                {
                    thisCard.Stack.transform.position = new Vector3(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y), 0);
                }
            }
            else
            {
                StackOfCards previousStack = thisCard.Stack;
                thisCard.AddToStack(hitCard.Stack);
                hitCard.Stack.CheckForCombinations();
                Destroy(previousStack.gameObject);
            }
        }
    }

    private void InteractWithCombat(CombatBox combatBox)
    {
        if (!_isMovingMultiple)
        {
            if (thisCard.GetType() == typeof(CombatUnit))
            {
                CombatUnit thisCombatCard = (CombatUnit)thisCard;
                combatBox.AddMember(thisCombatCard);
            }
            else
            {
                thisCard.Stack.transform.position = new Vector3(Mathf.RoundToInt(transform.position.x),
                    Mathf.RoundToInt(transform.position.y), 0);
            }
        }
    }

    private void PutCardDown()
    {
        thisCard.Stack.transform.position = new Vector3(Mathf.RoundToInt(transform.position.x),
            Mathf.RoundToInt(transform.position.y), 0);
        for (int i = 1; i < thisCard.Stack.Cards.Count; i++)
        {
            thisCard.Stack.Cards[i].AdjustPosition();
        }

        if (thisCard.PositionOnStack != 0)
        {
            thisCard.Stack.CheckForCombinations();
        }
    }

    private Vector3 GetMousePosition()
    {
        var mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        return mousePosition;
    }
}
