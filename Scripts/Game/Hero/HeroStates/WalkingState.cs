using UnityEngine;

namespace Game.Hero
{
    public class WalkingState:BaseHeroStateHandlerHelper,IHeroState
    {
        
        public WalkingState(in StateConditions.Delegates conditionDelegates) : base(in conditionDelegates)
        {
        }

        protected override void RegisterDelegateInOrder(in StateConditions.Delegates conditionDelegates)
        {
            _delegate.Add(conditionDelegates.selectStateFalling);
            _delegate.Add(conditionDelegates.selectStateOnJumping);
            //_delegate.Add(conditionDelegates.selectStateGoingUpStairs);
            _delegate.Add(conditionDelegates.selectStateRunning);
            _delegate.Add(conditionDelegates.onSelectStatePullingChair);
            _delegate.Add(conditionDelegates.selectStateOpeningDoor);
            _delegate.Add(conditionDelegates.selectStateFlashingBoss);
            _delegate.Add(conditionDelegates.selectStateWalking);
            _delegate.Add(conditionDelegates.selectStateIdle);
            _delegate.Add(conditionDelegates.selectStateCrouching);
        }

        public void OnAction(IHero hero)
        {
            hero.Animate(AnimationStrings.Walk);
            hero.Walk();
            //Debug.Log($"HeroState: Walking");
        }

        public void RefreshThisState(IHero hero)
        {
            //Debug.Log($"HeroState: Finish: Walking");
        }
    }
}