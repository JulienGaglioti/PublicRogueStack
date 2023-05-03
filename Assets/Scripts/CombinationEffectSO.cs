using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class CombinationEffectSO : ScriptableObject
{
    public abstract void ActivateEffect(StackOfCards stackOfCards);
}
