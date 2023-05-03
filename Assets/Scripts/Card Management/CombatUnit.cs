using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;


public class CombatUnit : Card
{
    public CombatBox Combat;
    public CombatController Controller;
    public Vector3 LocalCombatPosition;
    
    // Stats
    public int CurrentHP;
    public int MaxHP;
    public int CurrentMana;
    public int MaxMana;
    public float AttackTime;
    public float AttacksPerSecond => 1 / AttackTime;
    public int Power;
    public float DamagePerSecond => AttacksPerSecond * Power;
    public int Spirit;
    public float ManaPerSecond => AttacksPerSecond * Spirit;

    public Action OnHPChanged;
    public Action OnManaChanged;

    protected override void InitializeStats()
    {
        base.InitializeStats();
        CombatUnitSO combatInfo = (CombatUnitSO)CardInfo;
        MaxHP = combatInfo.MaxHP;
        CurrentHP = MaxHP;
        AttackTime = combatInfo.AttackTime;
        Power = combatInfo.Atk;
        Spirit = combatInfo.Mph;
        CurrentMana = 0;
        MaxMana = 15;
        ModifyHP(0);
        ModifyMana(0);
    }

    public void StartCombatBehaviour(CombatBox combat)
    {
        Controller = gameObject.AddComponent<CombatController>();
        Controller.Combat = combat;
        Combat = combat;
        Controller.Unit = this;
        Stack.RemoveCardAndCheckCombinations(this);
        Stack = null;
        
        StateController.EnterCombat();
    }

    public void StopCombatBehaviour()
    {
        Destroy(Controller);
        Combat = null;
        InitializeStack();
        
        StateController.ExitCombat();
    }

    public void ModifyHP(int amount)
    {
        CurrentHP += amount;
        CurrentHP = Mathf.Clamp(CurrentHP, 0, MaxHP);
        if (CurrentHP <= 0)
        {
            Death();
        }
        
        OnHPChanged?.Invoke();
    }

    public void ModifyMana(int amount)
    {
        CurrentMana += amount;
        CurrentMana = Mathf.Clamp(CurrentMana, 0, MaxMana);
        CheckSkillActivation();
        
        OnManaChanged?.Invoke();
    }

    public void SetMana(int value)
    {
        CurrentMana = value;
        CurrentMana = Mathf.Clamp(CurrentMana, 0, MaxMana);
        CheckSkillActivation();

        OnManaChanged?.Invoke();
    }

    private void CheckSkillActivation()
    {
        if (CurrentMana >= MaxMana)
        {
            Controller.ActivateSkill();
        }
    }

    private void Death()
    {
        Combat.RemoveMember(this);
        Destroy(gameObject);
    }
}
