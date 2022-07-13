using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;
using Game.TimeLine;


public class TestTimeLine : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        ITimeLine a = FindObjectOfType<TimeLine>();
        a.OpeningTask(GameStart);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GameStart(){}
}
