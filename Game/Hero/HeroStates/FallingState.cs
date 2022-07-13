namespace Game.Hero
{
    public class FallingState:BaseHeroStateHandlerHelper,IHeroState
    {

        public FallingState(in StateConditions.Delegates heroBehaviourDelegates) : base(in heroBehaviourDelegates)
        {
        }

        protected override void RegisterDelegateInOrder(in StateConditions.Delegates conditionDelegates)
        {
            _delegate.Add(conditionDelegates.selectStateOnHangingStick);
            _delegate.Add(conditionDelegates.selectStateFalling);
            _delegate.Add(conditionDelegates.selectStateRunning);
            _delegate.Add(conditionDelegates.selectStateWalking);
            _delegate.Add(conditionDelegates.selectStateIdle);
            _delegate.Add(conditionDelegates.selectStateCrouching);
        }
        
        public void OnAction(IHero hero)
        {
            hero.Animate(AnimationStrings.Falling);
            //Debug.Log($"HeroState: Falling");
        }
        
        public void RefreshThisState(IHero hero)
        {
            //Debug.Log($"HeroState: Finish: Falling");
        }
    }
}