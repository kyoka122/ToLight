namespace Game.Hero
{
    public class OnPullingChairState:BaseHeroStateHandlerHelper,IHeroState
    {
        //private bool _first=true;
        //private IHeroHandleItem _pullingChairObj;
        public OnPullingChairState(in StateConditions.Delegates heroBehaviourDelegates) : base(in heroBehaviourDelegates)
        {
        }
        
        protected override void RegisterDelegateInOrder(in StateConditions.Delegates conditionDelegates)
        {
            _delegate.Add(conditionDelegates.selectStateFalling);
            _delegate.Add(conditionDelegates.selectStateRunning);
            _delegate.Add(conditionDelegates.selectStatePullingChair);
            _delegate.Add(conditionDelegates.selectStateWalking);
            _delegate.Add(conditionDelegates.selectStateIdle);
            _delegate.Add(conditionDelegates.selectStateCrouching);
        }

        public override IHeroState SelectNextStateByInput(IHero hero)
        {
            foreach (var func in _delegate)
            {
                IHeroState heroState = func(hero);
                if (heroState == null)
                {
                    continue;
                }
                if (heroState.GetType()!=typeof(PullingChairState))
                {
                    hero.SetHandlingItem(null);
                }
                return heroState;
            }
            return null;
        }

        public void OnAction(IHero hero)
        {
            hero.SetHandlingItem(hero.handleableItem);
            //Debug.Log($"HeroState: OnPullingChair");
        }
        
        public void RefreshThisState(IHero hero)
        {
            //Debug.Log($"HeroState: Finish: OnPullingChair");
        }
        
    }
}