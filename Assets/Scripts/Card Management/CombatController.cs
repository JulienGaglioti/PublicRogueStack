using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class CombatController : MonoBehaviour
{
    public CombatUnit Unit;
    public CombatBox Combat;
    private float _currentTime;

    private void Update()
    {
        _currentTime += Time.deltaTime;
        if (_currentTime >= Unit.AttackTime)
        {
            Attack();
            _currentTime = 0;
        }
    }

    private void Attack()
    {
        CombatUnit target = SelectTarget();
        target.ModifyHP(-Unit.Power);
        Unit.ModifyMana(Unit.Spirit);

        CombatAnimation(target);
    }

    public void ActivateSkill()
    {
        // temporary simple effect
        CombatUnit target = SelectTarget();
        target.ModifyHP(-Unit.Power * 4);
        Unit.SetMana(0);

        CombatAnimation(target);
    }

    private CombatUnit SelectTarget()
    {
        return Combat.GetRandomOpponent(Unit);
    }

    private void CombatAnimation(CombatUnit target)
    {
        Vector3 startingPosition = transform.localPosition;
        Vector3 direction = (target.transform.localPosition - transform.localPosition).normalized;
        Vector3 tweenPosition = startingPosition + direction * 0.8f;
        transform.DOLocalMove(tweenPosition, 0.15f)
            .OnComplete(() => transform.DOLocalMove(Unit.LocalCombatPosition, 0.35f));
    }
}
