using Game.StageItem.Interface;

namespace Game.Hero
{
    public class PullingChairState:BaseHeroStateHandlerHelper,IHeroState
    {
        private bool _first=true;
        private IHeroHandleItem _chair;
        public PullingChairState(in StateConditions.Delegates heroBehaviourDelegates) : base(in heroBehaviourDelegates)
        {
        }

        protected override void RegisterDelegateInOrder(in StateConditions.Delegates conditionDelegates)
        {
            _delegate.Add(conditionDelegates.selectStateFalling);
            _delegate.Add(conditionDelegates.selectStateRunning);
            _delegate.Add(conditionDelegates.selectStatePullingChair);
            //_delegate.Add(conditionDelegates.selectStateOpeningDoor);
            _delegate.Add(conditionDelegates.selectStateWalking);
            _delegate.Add(conditionDelegates.selectStateIdle);
            _delegate.Add(conditionDelegates.selectStateCrouching);
        }

        public void OnAction(IHero hero)
        {
            if (_first)
            {
                _chair = hero.handlingItem;
                hero.Animate(AnimationStrings.PullingChair, _chair.HeroRightHandSetPoint,
                    _chair.HeroLeftHandSetPoint);
                hero.SetItemJoint(_chair);
                _chair.FreeFreezingPosition();
                _chair.OnSelectedLight();
                _first = false;
            }
            hero.BackWalk();
            //Debug.Log($"HeroState: PullingChair");
        }

        public void RefreshThisState(IHero hero)
        {
            hero.ReleaseItemJoint(_chair);
            hero.SetHandlingItem(null);
            _chair.FreezePosition();
            _chair.OffSelectedLight();
            _first = true;
            //Debug.Log($"HeroState: Finish: PullingChair");
        }
    }
}