using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Effects/Start Combat")]
public class StartCombatESO : CombinationEffectSO
{
    public override void ActivateEffect(StackOfCards stackOfCards)
    {
        CombatUnit firstCard = (CombatUnit)stackOfCards.Cards[0];
        CombatUnit secondCard = (CombatUnit)stackOfCards.Cards[1];
        CombatBox combatBox = Instantiate(ObjectReferences.Instance.CombatBoxPrefab);
        
        combatBox.Initialize(firstCard.Stack.transform.position);
        combatBox.AddMember(firstCard);
        combatBox.AddMember(secondCard);
    }
}
