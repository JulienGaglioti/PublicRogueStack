using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class CombatBox : MonoBehaviour
{
    public List<CombatUnit> Allies = new List<CombatUnit>();
    public List<CombatUnit> Enemies = new List<CombatUnit>();
    public SpriteRenderer ThisSprite;
    public BoxCollider ThisCollider;
    public BoxCollider2D ThisCollider2D;

    private int _highestCharacterCount;

    public void Initialize(Vector3 position)
    {
        transform.position = new Vector3(Mathf.RoundToInt(position.x), Mathf.RoundToInt(position.y) + 3, 0);
        ObjectReferences.Instance.CurrentCombats.Add(this);
    }

    public void AddMember(CombatUnit card)
    {
        card.transform.SetParent(transform);
        if (card.IsAlly)
        {
            Allies.Add(card);
        }
        else
        {
            Enemies.Add(card);
        }

        card.StartCombatBehaviour(this);
        _highestCharacterCount = Mathf.Max(Allies.Count, Enemies.Count);
        UpdateBoxSize();
        UpdateCardPositions();
    }

    public void RemoveMember(CombatUnit card)
    {
        if (card.IsAlly)
        {
            Allies.Remove(card);
        }
        else
        {
            Enemies.Remove(card);
        }

        _highestCharacterCount = Mathf.Max(Allies.Count, Enemies.Count);
        
        UpdateBoxSize();
        UpdateCardPositions();
        CheckCombatEnd();
    }

    public void CheckCombatEnd()
    {
        if (Allies.Count == 0)
        {
            foreach (var unit in Enemies)
            {
                unit.StopCombatBehaviour();
            }
            Destroy(gameObject);
            return;
        }

        if (Enemies.Count == 0)
        {
            foreach (var unit in Allies)
            {
                unit.StopCombatBehaviour();
            }
            Destroy(gameObject);
        }
    }

    public CombatUnit GetRandomOpponent(CombatUnit source)
    {
        CombatUnit target;
        if (source.IsAlly)
        {
            target = Enemies[Random.Range(0, Enemies.Count)];
            
        }
        else
        {
            target = Allies[Random.Range(0, Allies.Count)];
        }

        return target;
    }

    private void UpdateBoxSize()
    {
        int xSize = _highestCharacterCount * 3 + (_highestCharacterCount - 1) + 2;
        ThisSprite.size = new Vector2(xSize, 11);
        ThisCollider.size = new Vector3(xSize, 11, 0.2f);
        ThisCollider2D.size = new Vector2(xSize, 11);
    }

    private void UpdateCardPositions()
    {
        float startingX = (-ThisSprite.size.x/2) + 2.5f;
        for (int i = 0; i < Allies.Count; i++)
        {
            CombatUnit unit = Allies[i];
            Vector3 position = new Vector3(startingX + i * 4f, -3, -0.5f);
            unit.transform.DOKill();
            unit.LocalCombatPosition = position;
            unit.transform.localPosition = position;
        }

        startingX = (-ThisSprite.size.x / 2) + 2.5f;
        for (int i = 0; i < Enemies.Count; i++)
        {
            CombatUnit unit = Enemies[i];
            Vector3 position = new Vector3(startingX + i * 4f, 2, -0.5f);
            unit.transform.DOKill();
            unit.LocalCombatPosition = position;
            unit.transform.localPosition = position;
        }
    }

    private void OnDestroy()
    {
        ObjectReferences.Instance.CurrentCombats.Remove(this);
    }
}
