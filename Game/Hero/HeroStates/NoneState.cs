using UnityEngine;

namespace Game.Hero
{
    public class NoneState:BaseHeroStateHandlerHelper, IHeroState
    {
        public NoneState(in StateConditions.Delegates heroBehaviourDelegates) : base(in heroBehaviourDelegates)
        {
        }

        protected override void RegisterDelegateInOrder(in StateConditions.Delegates conditionDelegates)
        {
            //TODO: あとで厳選
            _delegate.Add(conditionDelegates.selectStateIdle);
            _delegate.Add(conditionDelegates.selectStateRunning);
            _delegate.Add(conditionDelegates.onSelectStatePullingChair);
            _delegate.Add(conditionDelegates.selectStatePullingChair);
            _delegate.Add(conditionDelegates.selectStateOnJumping);
            _delegate.Add(conditionDelegates.selectStateJumping);
            _delegate.Add(conditionDelegates.selectStateWalking);
            _delegate.Add(conditionDelegates.selectStateFalling);
            _delegate.Add(conditionDelegates.selectStateFallingHavingStick);
            _delegate.Add(conditionDelegates.selectStateSlipping);
            _delegate.Add(conditionDelegates.selectStateOnHangingStick);
            _delegate.Add(conditionDelegates.selectStateHangingStick);
            _delegate.Add(conditionDelegates.selectStateFlashingBoss);
            _delegate.Add(conditionDelegates.selectStateCrouching);
            _delegate.Add(conditionDelegates.selectStateNone);
        }

        public void OnAction(IHero hero)
        {
            hero.Stay();
            //hero.Animate(AnimationStrings.Idle);
        }

        public void RefreshThisState(IHero hero)
        {
            
        }
    }
}