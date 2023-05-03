using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CombatUnitSO : CardSO
{
    public int MaxHP;
    public float AttackTime;
    public int Atk; // damage dealt with attacks
    public int Mph; // mana per hit
}
