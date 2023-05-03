using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Card : MonoBehaviour
{
    public StackOfCards Stack { get; protected set; }
    public int PositionOnStack;
    public CardSO CardInfo;
    
    public bool IsAlly;

    public CardStateController StateController;
    public ProgressTimer Timer;

    private void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        InitializeStack();
        InitializeStats();
    }

    protected virtual void InitializeStats()
    {
        
    }

    public void AddToStack(StackOfCards stack)
    {
        transform.SetParent(stack.transform);
        
        Stack = stack;

        if (Stack.Cards.Count == 0)
        {
            transform.position = stack.transform.position;
        }
        else
        {
            transform.position = stack.Cards[stack.Cards.Count - 1].transform.position + Vector3.back + Vector3.down;
        }
        

        PositionOnStack = stack.Cards.Count;
        stack.AddCard(this);
    }

    public void InitializeStack()
    {
        transform.DOKill();
        transform.SetParent(null);
        StackOfCards newStack = Instantiate(ObjectReferences.Instance.StackPrefab);
        // Vector3 newPos = new Vector3(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y), 0);
        // newStack.transform.position = newPos;
        newStack.transform.position = transform.position; 
        
        transform.SetParent(newStack.transform);
        Stack = newStack;
        PositionOnStack = 0;
        newStack.AddCard(this);
        Invoke("ResetLocal", 0.25f);
    }

    public void ResetLocal()
    {
        transform.position = Stack.transform.position;
    }
    
    public void AdjustPosition()
    {
        transform.position = Stack.Cards[PositionOnStack - 1].transform.position + Vector3.back + Vector3.down;
    }

    public virtual void SetUI()
    {
        // TODO: set side panel UI properly
        UIManager.Instance.SetUI(this);
    }
}
