using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game.Hero
{
    public class RunningState:BaseHeroStateHandlerHelper,IHeroState
    {
        private readonly TimeSpan _canRunningTime = TimeSpan.FromSeconds(6f);

        private bool _timerOn = false;
        private UniTask _timerTask;
        private CancellationTokenSource _tokenSource;
        
        private int _runningIndex;
        private Func<IHero,IHeroState> _selectStateRunning;
        
        public RunningState(in StateConditions.Delegates heroBehaviourDelegates) : base(in heroBehaviourDelegates)
        {
        }

        protected override void RegisterDelegateInOrder(in StateConditions.Delegates conditionDelegates)
        {
            _delegate.Add(conditionDelegates.selectStateFalling);
            _delegate.Add(conditionDelegates.selectStateOnJumping);
            //_delegate.Add(conditionDelegates.selectStateGoingUpStairs);
            _runningIndex = 2;
            _selectStateRunning = conditionDelegates.selectStateRunning;
            _delegate.Add(_selectStateRunning);
            _delegate.Add(conditionDelegates.selectStateWalking);
            _delegate.Add(conditionDelegates.selectStateIdle);
            _delegate.Add(conditionDelegates.selectStateCrouching);
        }

        public void OnAction(IHero hero)
        {
            //Debug.Log($"HeroState: Running");
            hero.Run();
            hero.Animate(AnimationStrings.Run);
            //TODO: 基底クラスで用意する
            if (!_timerOn)
            {
                _timerOn = true;
                _tokenSource = new CancellationTokenSource();
                _timerTask=TimeCanRunning(_tokenSource.Token);
                return;
            }
            
            if (_timerTask.Status==UniTaskStatus.Succeeded)
            {
                _delegate.RemoveAt(_runningIndex);
                _timerOn = false;
            }
        }
        private async UniTask TimeCanRunning(CancellationToken token)
        {
            await UniTask.Delay(_canRunningTime,cancellationToken:token);
        }

        public void RefreshThisState(IHero hero)
        {
            //Debug.Log($"HeroState: Finish: Running");
            if (!_timerOn)
            {
                _delegate.Insert(_runningIndex,_selectStateRunning);
            }

            if (_timerOn)
            {
                _timerOn = false;
            }
            if(_tokenSource.Token.CanBeCanceled)
            {
                _tokenSource.Cancel();
            }
        }
    }
}