using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Effects/Extract Cards")]
public class ExtractCardsESO : CombinationEffectSO
{
    public List<int> indexes;
    private int numberOfCards;
    public override void ActivateEffect(StackOfCards stackOfCards)
    {
        foreach (var i in indexes)
        {
            Card cardToExtract = stackOfCards.Cards[i];
            stackOfCards.ExtractCard(i);
            cardToExtract.Stack.transform.position =
                stackOfCards.transform.position + Vector3.down * (numberOfCards * 4);
            numberOfCards++; // TODO: FIX! DOESNT WORK CAUSE THE NUMBER KEEPS GETTING ADDED TO THE SCRIPTABLE OBJECT
        }
    }
}
