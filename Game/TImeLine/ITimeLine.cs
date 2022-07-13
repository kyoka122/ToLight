using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Game.TimeLine
{
   public interface ITimeLine
   {
    public void OpeningTask(Action action);

    public void EndingTask(Action action);
   }
}