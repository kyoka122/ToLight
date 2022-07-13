using UnityEngine;

namespace Game.Hero
{
    public class OnHangingStickState:BaseHeroStateHandlerHelper,IHeroState
    {
        public OnHangingStickState(in StateConditions.Delegates heroBehaviourDelegates) : base(in heroBehaviourDelegates)
        {
        }
        
        protected override void RegisterDelegateInOrder(in StateConditions.Delegates conditionDelegates)
        {
            _delegate.Add(conditionDelegates.selectStateHangingStick);
            _delegate.Add(conditionDelegates.selectStateRunning);
            _delegate.Add(conditionDelegates.selectStateIdle);
            _delegate.Add(conditionDelegates.selectStateCrouching);
        }

        public override IHeroState SelectNextStateByInput(IHero hero)
        {
            foreach (var func in _delegate)
            {
                IHeroState heroState = func(hero);
                //MEMO: OnHangingStickStateからHangingStickStateに遷移しない場合、操作しているItemの情報を削除
                if (heroState != null)
                {
                    if (heroState.GetType()!=typeof(HangingStickState))
                    {
                        hero.SetHandlingItem(null);
                    }
                    return heroState;
                }
            }
            return null;
        }

        public void OnAction(IHero hero)
        {
            Debug.Log($"HeroState: OnHangingStickState");
            hero.SetHandlingItem(hero.handleableItem);
        }
        
        public void RefreshThisState(IHero hero)
        {
            //Debug.Log($"HeroState: Finish: OnHangingStickState");
        }
    }
}