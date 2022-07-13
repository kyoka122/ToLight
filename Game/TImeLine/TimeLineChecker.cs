using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.Playables;


public class TimeLineChecker : MonoBehaviour
{
    // Start is called before the first frame update

    public ReactiveProperty<bool> IsFinish = new ReactiveProperty<bool>();
   
    [SerializeField]PlayableDirector _startTimeLine;
   
    public bool IsDone()
    {
        return _startTimeLine.time >= _startTimeLine.duration;
    }

    public void PlayTimeLine()
    {
        _startTimeLine.time = 0;
        _startTimeLine.Play();
    }
    public void IsStart()
    {
        IsFinish.Value = false;
    }

    // Update is called once per frame
    public void IsFinished()
    {
        IsFinish.Value = true;
    }
    void Update()
    {
        if(Input.GetKey(KeyCode.Space)){
             _startTimeLine.Stop();
             IsFinish.Value = true;

        }
    }
}
