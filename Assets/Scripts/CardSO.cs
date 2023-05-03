using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CardSO : ScriptableObject
{
    public string Name;
    public List<CardTagSO> Tags;
}
