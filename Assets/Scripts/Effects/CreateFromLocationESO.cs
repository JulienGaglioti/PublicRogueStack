using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(menuName = "Effects/Create from Location")]
public class CreateFromLocationESO : CombinationEffectSO
{
    public override void ActivateEffect(StackOfCards stackOfCards)
    {
        Location location = (Location)stackOfCards.Cards[0];
        List<Card> cards = location.GenerateCard();
        for (int i = 0; i < cards.Count; i++)
        {
            Instantiate(cards[i], stackOfCards.transform.position + Vector3.right * (i+1) * 4 + Vector3.up * Random.Range(0, 0.5f), quaternion.identity);
        }
    }
}
