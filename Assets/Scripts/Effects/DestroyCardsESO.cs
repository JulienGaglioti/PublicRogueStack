using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Effects/Destroy All Cards")]
public class DestroyCardsESO : CombinationEffectSO
{
    public override void ActivateEffect(StackOfCards stackOfCards)
    {
        Destroy(stackOfCards.gameObject);
    }
}
