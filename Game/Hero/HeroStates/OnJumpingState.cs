using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game.Hero
{
    public class OnJumpingState:BaseHeroStateHandlerHelper,IHeroState
    {
        private readonly TimeSpan _jumpInterval = TimeSpan.FromSeconds(1f);
        
        private bool _passedJumpInterval;
        private UniTask _timerTask;
        private CancellationTokenSource _tokenSource;
        public OnJumpingState(in StateConditions.Delegates heroBehaviourDelegates) : base(in heroBehaviourDelegates)
        {
        }

        protected override void RegisterDelegateInOrder(in StateConditions.Delegates conditionDelegates)
        {
            //_delegate.Add(conditionDelegates.selectStateOnJumping);
            _delegate.Add(conditionDelegates.selectStateJumping);
            _delegate.Add(conditionDelegates.selectStateRunning);
            _delegate.Add(conditionDelegates.selectStateWalking);
            _delegate.Add(conditionDelegates.selectStateIdle);
            _delegate.Add(conditionDelegates.selectStateCrouching);
        }

        public void OnAction(IHero hero)
        {
            //Debug.Log($"HeroState: OnJumping");
            if (_passedJumpInterval)
            {
                _passedJumpInterval = false;
                _tokenSource = new CancellationTokenSource();
                _timerTask=TimeJumpInterval(_tokenSource.Token);
            }

            if (_timerTask.Status==UniTaskStatus.Succeeded)
            {
                _passedJumpInterval = true;
            }

            if (_passedJumpInterval)
            {
                hero.Animate(AnimationStrings.JumpingUp);
                hero.OnJump();
            }
            hero.Walk();
        }
        
        private async UniTask TimeJumpInterval(CancellationToken token)
        {
            await UniTask.Delay(_jumpInterval,cancellationToken:token);
        }

        public void RefreshThisState(IHero hero)
        {
            //Debug.Log($"HeroState: Finish: OnJumping");
        }
    }
}