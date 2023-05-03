using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarCardUI : MonoBehaviour
{
    [SerializeField] private CombatUnit _unit;
    [SerializeField] private Transform FillBar;

    private void Start()
    {
        _unit.OnHPChanged += UpdateBar;
    }

    private void UpdateBar()
    {
        float fraction = ((float)_unit.CurrentHP / (float)_unit.MaxHP);
        FillBar.localScale = Vector3.one - Vector3.right + Vector3.right * fraction;
    }
}
