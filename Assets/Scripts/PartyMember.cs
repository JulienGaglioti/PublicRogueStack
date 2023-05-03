using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyMember : MonoBehaviour
{
    private CombatUnit _thisCombatUnit;
    
    
    private void Start()
    {
        _thisCombatUnit = GetComponent<CombatUnit>();
        ObjectReferences.Instance.CurrentPartyMembers.Add(_thisCombatUnit);
    }

    private void OnDisable()
    {
        ObjectReferences.Instance.CurrentPartyMembers.Remove(_thisCombatUnit);
    }
}
