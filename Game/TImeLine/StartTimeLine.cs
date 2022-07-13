using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;

namespace Game.TimeLine{
public class StartTimeLine : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]PlayableDirector _startTimeLine;
   
    public bool IsDone()
    {
        return _startTimeLine.time >= _startTimeLine.duration;
    }

    public void PlayTimeLine()
    {
        _startTimeLine.Play();
    }
}
}

