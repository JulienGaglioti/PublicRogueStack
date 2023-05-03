using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

[CreateAssetMenu(menuName = "Effects/Create Card")]
public class CreateSpecificCardESO : CombinationEffectSO
{
    public Card CardPrefab;
    public Vector3 Offset;

    public override void ActivateEffect(StackOfCards stackOfCards)
    {
        Instantiate(CardPrefab, stackOfCards.transform.position + Offset, quaternion.identity);
    }
}
