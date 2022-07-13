using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Playables;
using UniRx;

namespace Game.TimeLine{
public class TimeLine : MonoBehaviour, ITimeLine
{
    private Action _openingAction;
    private Action _endingAction;
     [SerializeField]TimeLineChecker timeLineChecker;
     void Start()
     {
        timeLineChecker.IsFinish.AsObservable().Subscribe(_=>{
            if(timeLineChecker.IsFinish.Value)
            {
                _endingAction.Invoke();
            }
            else
            {
                _openingAction.Invoke();
            }
        }
            
        ).AddTo(this);
     }
    public void OpeningTask(Action action)
    {
        _openingAction = action;
    }
    
     
   
   

    

    public void EndingTask(Action action)
    {
        _endingAction = action;
    }
  
}
}
