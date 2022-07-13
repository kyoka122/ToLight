using Game.StageItem.Interface;

namespace Game.Hero
{
    public class OpeningDoorState:BaseHeroStateHandlerHelper,IHeroState
    {
        private bool _first=true;
        private IHeroHandleItem _door;
        public OpeningDoorState(in StateConditions.Delegates heroBehaviourDelegates) : base(in heroBehaviourDelegates)
        {
        }

        protected override void RegisterDelegateInOrder(in StateConditions.Delegates conditionDelegates)
        {
            _delegate.Add(conditionDelegates.selectStateFalling);
            _delegate.Add(conditionDelegates.selectStateRunning);
            _delegate.Add(conditionDelegates.selectStateOpeningDoor);
            _delegate.Add(conditionDelegates.selectStateWalking);
            _delegate.Add(conditionDelegates.selectStateIdle);
            _delegate.Add(conditionDelegates.selectStateCrouching);
        }

        public void OnAction(IHero hero)
        {
            if (_first)
            {
                _door = hero.handleableItem;
                hero.SetHandlingItem(_door);
                hero.Animate(AnimationStrings.OpeningDoor, _door.HeroRightHandSetPoint, _door.HeroLeftHandSetPoint);
                hero.SetItemJoint(_door);
                _door.FreeFreezingPosition();
                _door.OnSelectedLight();
                _first = false;
            }
            hero.SideWalk();
        }

        public void RefreshThisState(IHero hero)
        {
            hero.ReleaseItemJoint(_door);
            hero.SetHandlingItem(null);
            _door.FreezePosition();
            _door.OffSelectedLight();
            _first = true;
            //Debug.Log($"HeroState: Finish: PullingChair");
        }
    }
}