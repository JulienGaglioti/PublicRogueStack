using UnityEngine;

public class ProgressTimer : MonoBehaviour
{
    public bool TrackTime;
    public float ProgressRate;
    public float TimerToReach;
    public CardStateController StateController;
    
    private float _currentTime;
    private StackOfCards _stack;
    private AutomaticAction _autoAction;
    private bool _isCombination;

    private void OnEnable()
    {
        StateController.OnCombinationEnter += TrackTimer;
        StateController.OnAutoEnter += TrackTimer;
        StateController.OnCombinationExit += StopTracking;
        StateController.OnAutoExit += StopTracking;
    }

    private void OnDisable()
    {
        StateController.OnCombinationEnter -= TrackTimer;
        StateController.OnAutoEnter -= TrackTimer;
        StateController.OnCombinationExit -= StopTracking;
        StateController.OnAutoExit -= StopTracking;
    }

    private void TrackTimer()
    {
        TrackTime = true;
    }

    private void StopTracking()
    {
        TrackTime = false;
        _currentTime = 0;
    }
    
    private void Update()
    {
        if(!TrackTime)
            return;
        
        _currentTime += Time.deltaTime;
        if (_currentTime > TimerToReach)
        {
            _stack.OnTimerEnd();
            /*if (_isCombination)
            {
                _stack.OnTimerEnd();
            }
            else
            {
                _autoAction.OnTimerEnd();
            }*/
        }
        else
        {
            ProgressRate = _currentTime / TimerToReach;
        }
    }

    public void InitializeCombination(StackOfCards stack, float timeNeeded)
    {
        TrackTime = true;
        TimerToReach = timeNeeded;
        _stack = stack;
    }

    public void InitializeAuto(AutomaticAction autoAction, float timeNeeded)
    {
        TrackTime = true;
        TimerToReach = timeNeeded;
        _autoAction = autoAction;
    }
}
