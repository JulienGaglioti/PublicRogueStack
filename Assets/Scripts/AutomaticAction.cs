using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CardStateController))]
[RequireComponent(typeof(ProgressTimer))]
public class AutomaticAction : MonoBehaviour
{
    [SerializeField] private float _timeNeeded;
    [SerializeField] private CombinationEffectSO _automaticActionEffect;
    private CardStateController _stateController;
    private ProgressTimer _progressTimer;

    private void Awake()
    {
        _stateController = GetComponent<CardStateController>();
        _progressTimer = GetComponent<ProgressTimer>();
    }

    private void Start()
    {
        _stateController.EnterAutoProgress();
        _progressTimer.InitializeAuto(this, _timeNeeded);
    }

    public void OnTimerEnd()
    {
        _stateController.ExitAutoProgress();
    }
}
