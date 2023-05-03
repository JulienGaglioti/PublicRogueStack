using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviourSingleton<UIManager>
{
    public TextMeshProUGUI Name;

    public void SetUI(Card card)
    {
        Name.SetText(card.CardInfo.Name);
    }
}
