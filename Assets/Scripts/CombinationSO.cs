using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CombinationSO : ScriptableObject
{
    public float TimeNeeded;
    public List<CardTagSO> CardTagsSO;

    public List<CombinationEffectSO> Effects;

    public void OnCombineStart(StackOfCards stack)
    {
        
    }

    public void OnCombineEnd(StackOfCards stack)
    {
        foreach (var effect in Effects)
        {
            effect.ActivateEffect(stack);
        }
    }
}
