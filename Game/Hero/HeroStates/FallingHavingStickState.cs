namespace Game.Hero
{
    public class FallingHavingStickState:BaseHeroStateHandlerHelper,IHeroState
    {
        public FallingHavingStickState(in StateConditions.Delegates heroBehaviourDelegates) : base(in heroBehaviourDelegates)
        {
        }
        protected override void RegisterDelegateInOrder(in StateConditions.Delegates conditionDelegates)
        {
            _delegate.Add(conditionDelegates.selectStateSlipping);
            _delegate.Add(conditionDelegates.selectStateFallingHavingStick);
            _delegate.Add(conditionDelegates.selectStateIdle);
            _delegate.Add(conditionDelegates.selectStateCrouching);
        }
        public void OnAction(IHero hero)
        {
            hero.Animate(AnimationStrings.JumpingDown);
            //Debug.Log($"HeroState: Falling");
        }

        public void RefreshThisState(IHero hero)
        {
            
        }
    }
}