using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardUI : MonoBehaviour
{
    public GameObject Progressbar;
    public Transform FillBar;
    public CardStateController StateController;
    public TextMeshPro NameText;
    public TextMeshPro TagsText;

    private Card _thisCard;
    private bool _trackingProgressBar;

    private void OnEnable()
    {
        _thisCard = GetComponent<Card>();
        SetCardUI();
        StateController.OnCombinationEnter += TrackTimer;
        StateController.OnCombinationExit += StopTracking;
        StateController.OnAutoEnter += TrackTimer;
        StateController.OnAutoExit += StopTracking;
    }

    private void OnDisable()
    {
        StateController.OnCombinationEnter -= TrackTimer;
        StateController.OnCombinationExit -= StopTracking;
        StateController.OnAutoEnter -= TrackTimer;
        StateController.OnAutoExit -= StopTracking;
    }

    private void SetCardUI()
    {
        NameText.SetText(_thisCard.CardInfo.Name);
        string tagsString = "";
        foreach (var tag in _thisCard.CardInfo.Tags)
        {
            tagsString += tag.name + "\n";
        }

        TagsText.SetText(tagsString);
    }

    private void TrackTimer()
    {
        SetProgressBar(true);
        _trackingProgressBar = true;
    }

    private void StopTracking()
    {
        SetProgressBar(false);
        _trackingProgressBar = false;
    }

    public void SetProgressBar(bool b)
    {
        Progressbar.SetActive(b);
    }

    private void Update()
    {
        if (_trackingProgressBar)
        {
            FillBar.localScale = Vector3.one - Vector3.right + Vector3.right * (_thisCard.Timer.ProgressRate);
        }
    }
}
