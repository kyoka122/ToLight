using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Game.Hero
{
    public class CrouchingState:BaseHeroStateHandlerHelper,IHeroState
    {
        private readonly TimeSpan _crouchingTime = TimeSpan.FromSeconds(5f);
        
        private bool _timerOn = false;
        private UniTask _timerTask;
        private CancellationTokenSource _tokenSource;
        
        public CrouchingState(in StateConditions.Delegates heroBehaviourDelegates) : base(in heroBehaviourDelegates)
        {
        }

        protected override void RegisterDelegateInOrder(in StateConditions.Delegates conditionDelegates)
        {
            _delegate.Add(conditionDelegates.selectStateFalling);
            _delegate.Add(conditionDelegates.selectStateCrouching);
            _delegate.Add(conditionDelegates.selectStateWalking);
            _delegate.Add(conditionDelegates.selectStateIdle);
        }

        public void OnAction(IHero hero)
        {
            //Debug.Log($"HeroState: Crouching");
            if (!_timerOn)
            {
                _timerOn = true;
                _tokenSource = new CancellationTokenSource();
                _timerTask=TimeCanCrouching(_tokenSource.Token);
                hero.Stay();
                hero.Animate(AnimationStrings.Crouching);
                return;
            }
            if (_timerTask.Status==UniTaskStatus.Succeeded)
            {
                hero.Defeat();
                _timerOn = false;
            }
        }
        
        private async UniTask TimeCanCrouching(CancellationToken token)
        {
            await UniTask.Delay(_crouchingTime,cancellationToken:token);
        }
        
        public void RefreshThisState(IHero hero)
        {
            //Debug.Log($"HeroState: Finish: Crouching");

            if (_timerOn)
            {
                _timerOn = false;
            }
            if (_tokenSource.Token.CanBeCanceled)
            {
                _tokenSource.Cancel();
            }
        }
    }
}