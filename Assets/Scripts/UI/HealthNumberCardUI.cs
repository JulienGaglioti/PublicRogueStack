using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthNumberCardUI : MonoBehaviour
{
    [SerializeField] private CombatUnit _unit;
    [SerializeField] private TextMeshPro _hpNumber;

    private void Start()
    {
        _unit.OnHPChanged += UpdateBar;
    }

    private void UpdateBar()
    {
        _hpNumber.SetText(_unit.CurrentHP.ToString());
    }
}
