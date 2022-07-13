using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game.Hero
{
    public class SlippingState:BaseHeroStateHandlerHelper,IHeroState
    {
        private readonly TimeSpan _slipAnimationTime = TimeSpan.FromSeconds(4f);

        private bool _timerOn = false;
        private UniTask _timerTask;
        private CancellationTokenSource _tokenSource;
        
        private int _slippingIndex;
        private Func<IHero,IHeroState> _selectStateSlipping;

        public SlippingState(in StateConditions.Delegates heroBehaviourDelegates) : base(in heroBehaviourDelegates)
        {
        }

        protected override void RegisterDelegateInOrder(in StateConditions.Delegates conditionDelegates)
        {
            _delegate.Add(conditionDelegates.selectStateFalling);
            
            _slippingIndex = 1;
            _selectStateSlipping = conditionDelegates.selectStateSlipping;
            _delegate.Add(_selectStateSlipping);
            
            _delegate.Add(conditionDelegates.selectStateRunning);
            _delegate.Add(conditionDelegates.selectStateWalking);
            _delegate.Add(conditionDelegates.selectStateIdle);
            _delegate.Add(conditionDelegates.selectStateCrouching);
        }

        public void OnAction(IHero hero)
        {
            Debug.Log($"HeroState: Slipping");
            hero.Stay();
            if (!_timerOn)
            {
                _timerOn = true;
                _tokenSource = new CancellationTokenSource();
                _timerTask=TimeSlippingAnimation(_tokenSource.Token);
                //hero.Animate(AnimationStrings.Slipping); //TODO: アニメーション出来たら切り替え
                hero.Animate(AnimationStrings.Idle);
                return;
            }

            if (_timerTask.Status == UniTaskStatus.Succeeded)
            {
                Debug.Log($"Remove");
                _delegate.RemoveAt(_slippingIndex);
                _timerOn = false;
            }
            
        }

        //TODO: 後でアニメーションの終了通知に変更
        private async UniTask TimeSlippingAnimation(CancellationToken token)
        {
            await UniTask.Delay(_slipAnimationTime,cancellationToken:token);
        }

        public void RefreshThisState(IHero hero)
        {
            //Debug.Log($"HeroState: Finish: Slipping");
            if (!_timerOn)
            {
                _delegate.Insert(_slippingIndex,_selectStateSlipping);
            }
            
            if (_timerOn)
            {
                _timerOn = false;
            }
            if (!_tokenSource.Token.CanBeCanceled)
            {
                Debug.Log($"Canceled!!");
                _tokenSource.Cancel();
            }
            
            
            
        }
    }
}