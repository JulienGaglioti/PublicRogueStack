using System.Collections;
using System.Collections.Generic;
// using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

public class FillCombinations : MonoBehaviour
{
    public List<CombinationSO> Combinations;

    // [Button]
    public void Fill()
    {

        foreach (var combination in Combinations)
        {
            foreach (var card in combination.CardTagsSO)
            {
                // TODO: maybe change this later since it checks duplicates multiple times
                EditorUtility.SetDirty(card);
                card.Combinations.Clear();
            }
        }

        List<CardTagSO> checkedCards = new List<CardTagSO>();
        foreach (var combination in Combinations)
        {
            checkedCards.Clear();
            foreach (var card in combination.CardTagsSO)
            {
                if (!checkedCards.Contains(card))
                {
                    card.Combinations.Add(combination);
                    checkedCards.Add(card);
                }
            }
        }
        AssetDatabase.SaveAssets();
    }
}
