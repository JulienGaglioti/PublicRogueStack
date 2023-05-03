using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectReferences : MonoBehaviourSingleton<ObjectReferences>
{
    public ProgressTimer ActionBarPrefab;
    public CombatBox CombatBoxPrefab;
    public StackOfCards StackPrefab;

    public List<CombatBox> CurrentCombats;
    public List<CombatUnit> CurrentPartyMembers;

    public CombatBox GetClosestCombat(Vector3 position, float maxDistance)
    {
        float shortestDistance = Single.PositiveInfinity;
        CombatBox closestCombat = null;

        foreach (var combatToCheck in CurrentCombats)
        {
            float distance = Vector3.Distance(combatToCheck.transform.position, position);
            if (distance <= shortestDistance && distance < maxDistance)
            {
                closestCombat = combatToCheck;
                shortestDistance = distance;
            }
        }

        return closestCombat;
    }

    public CombatUnit GetClosestPartyMember(Vector3 position, float maxDistance)
    {
        float shortestDistance = Single.PositiveInfinity;
        CombatUnit closestMember = null;

        foreach (var memberToCheck in CurrentPartyMembers)
        {
            float distance = Vector3.Distance(memberToCheck.transform.position, position);
            if (distance <= shortestDistance && distance < maxDistance)
            {
                closestMember = memberToCheck;
                shortestDistance = distance;
            }
        }

        return closestMember;
    }

    public CombatUnit GetClosestPartyMemberNotInCombat(Vector3 position, float maxDistance)
    {
        float shortestDistance = Single.PositiveInfinity;
        CombatUnit closestMember = null;

        foreach (var memberToCheck in CurrentPartyMembers)
        {
            float distance = Vector3.Distance(memberToCheck.transform.position, position);
            if (!memberToCheck.StateController.IsInCombat && distance <= shortestDistance && distance < maxDistance)
            {
                closestMember = memberToCheck;
                shortestDistance = distance;
            }
        }

        return closestMember;
    }
}
