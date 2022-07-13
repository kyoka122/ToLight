namespace Game.Hero
{
    public class IdleState :BaseHeroStateHandlerHelper, IHeroState
    {
        public IdleState(in StateConditions.Delegates heroBehaviourDelegates) : base(in heroBehaviourDelegates)
        {
        }

        protected override void RegisterDelegateInOrder(in StateConditions.Delegates conditionDelegates)
        {
            _delegate.Add(conditionDelegates.selectStateFalling);
            _delegate.Add(conditionDelegates.selectStateOnJumping);
            _delegate.Add(conditionDelegates.onSelectStatePullingChair);
            _delegate.Add(conditionDelegates.selectStateOpeningDoor);
            _delegate.Add(conditionDelegates.selectStateFlashingBoss);
            _delegate.Add(conditionDelegates.selectStateRunning);
            _delegate.Add(conditionDelegates.selectStateWalking);
            _delegate.Add(conditionDelegates.selectStateIdle);
            _delegate.Add(conditionDelegates.selectStateCrouching);
        }

        public void OnAction(IHero hero)
        {
            hero.Stay();
            hero.Animate(AnimationStrings.Idle);
        }

        public void RefreshThisState(IHero hero)
        {
            
        }
    }

}