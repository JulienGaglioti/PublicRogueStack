using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Effects/Single/Attack PartyMember")]
public class AttackPartyMemberESO : CombinationEffectSO
{
    public override void ActivateEffect(StackOfCards stackOfCards)
    {
        CombatUnit thisCard = (CombatUnit)stackOfCards.Cards[0];
        CombatBox closestCombat = ObjectReferences.Instance.GetClosestCombat(thisCard.transform.position, 20);
        CombatUnit allyToAttack =  ObjectReferences.Instance.GetClosestPartyMemberNotInCombat(thisCard.transform.position, Single.PositiveInfinity);

        if (closestCombat != null)
        {
            closestCombat.AddMember(thisCard);
        }
        else if (allyToAttack != null)
        {
            CombatBox combatBox = Instantiate(ObjectReferences.Instance.CombatBoxPrefab);
            combatBox.Initialize(allyToAttack.Stack.transform.position);
            combatBox.AddMember(allyToAttack);
            combatBox.AddMember(thisCard);
        }
        else
        {
            CombatBox anyCombat = ObjectReferences.Instance.GetClosestCombat(thisCard.transform.position, Single.PositiveInfinity);
            if (anyCombat != null)
            {
                anyCombat.AddMember(thisCard);
            }
        }
    }
}
