namespace Game.Hero
{
    public class FlashingBossState:BaseHeroStateHandlerHelper,IHeroState
    {
        private const int ConsumePower=1;

        public FlashingBossState(in StateConditions.Delegates heroBehaviourDelegates) : base(in heroBehaviourDelegates)
        {
        }

        protected override void RegisterDelegateInOrder(in StateConditions.Delegates conditionDelegates)
        {
            _delegate.Add(conditionDelegates.selectStateFalling);
            _delegate.Add(conditionDelegates.selectStateRunning);
            _delegate.Add(conditionDelegates.selectStateFlashingBoss);
            _delegate.Add(conditionDelegates.selectStateWalking);
            _delegate.Add(conditionDelegates.selectStateIdle);
            _delegate.Add(conditionDelegates.selectStateCrouching);
        }

        public void OnAction(IHero hero)
        {
            hero.Stay();
            hero.LookAtBoss();
            hero.AttackBoss();//TODO: 一定時間…の処理に変える？
            hero.SetBattlePower(-ConsumePower);
            hero.Animate(AnimationStrings.FlashingBoss);
        }

        public void RefreshThisState(IHero hero)
        {
            
        }
    }
}