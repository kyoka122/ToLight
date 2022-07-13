using System;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Utility
{
    //CHANGED: 経過時間がほしい場合はこのクラスで実装
    /*public class Timer
    {
        private Task _timerTask;
        
        private void Counter(CancellationToken token,float UntilCrouchingTime,Action continueWithMethod)
        {
            _timerTask = Task.Run(async () =>await CountDown(UntilCrouchingTime), token)
                .ContinueWith(_ =>
                {
                    continueWithMethod();
                }, token);
        }
        
        
        public async UniTask CountDown(float time,CancellationToken token)
        {
            Debug.Log($"TimerFinish");
        }
        
        public bool IsCompleted()
        {
            return _timerTask.IsCompleted;
        }

        public void Refresh()
        {
            _timerTask = null;
        }
        
    }*/
}