using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaBarCardUI : MonoBehaviour
{
    [SerializeField] private CombatUnit _unit;
    [SerializeField] private Transform _fillBar;
    [SerializeField] private GameObject _manaBar;

    private void Start()
    {
        _unit.OnManaChanged += UpdateBar;
        _unit.StateController.OnCombatEnter += () => _manaBar.SetActive(true);
        _unit.StateController.OnCombatExit += () => _manaBar.SetActive(false);
    }

    private void UpdateBar()
    {
        float fraction = ((float)_unit.CurrentMana / (float)_unit.MaxMana);
        _fillBar.localScale = Vector3.one - Vector3.right + Vector3.right * fraction;
    }
}
