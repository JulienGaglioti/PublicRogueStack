using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardStateController : MonoBehaviour
{
    public bool IsInCombat;
    public bool IsInAuto;
    public bool IsInCombination;

    public Action OnCombatEnter;
    public Action OnCombatExit;
    public Action OnAutoEnter;
    public Action OnAutoExit;
    public Action OnCombinationEnter;
    public Action OnCombinationExit;

    public void DisengageEverything()
    {
        IsInCombat = false;
        IsInCombination = false;
        IsInAuto = false;
        OnCombatExit?.Invoke();
        OnCombinationExit?.Invoke();
        OnAutoExit?.Invoke();
    }

    public void EnterCombat()
    {
        ExitAutoProgress();
        ExitCombinationProgress();
        IsInCombat = true;
        OnCombatEnter?.Invoke();
    }

    public void ExitCombat()
    {
        IsInCombat = false;
        OnCombatExit?.Invoke();
    }

    public void EnterCombinationProgress()
    {
        ExitAutoProgress();
        IsInCombination = true;
        OnCombinationEnter?.Invoke();
    }

    public void ExitCombinationProgress()
    {
        IsInCombination = false;
        OnCombinationExit?.Invoke();
    }

    public void EnterAutoProgress()
    {
        ExitCombinationProgress();
        IsInAuto = true;
        OnAutoEnter?.Invoke();
    }

    public void ExitAutoProgress()
    {
        IsInAuto = false;
        OnAutoExit?.Invoke();
    }
}
