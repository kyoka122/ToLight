using System;
using Game.StageItem.Interface;
using UnityEngine;

namespace Game.Hero
{
    public class HangingStickState:BaseHeroStateHandlerHelper, IHeroState
    {
        private const float FallingStartDistance=0.1f;

        private Func<IHero, IHeroState> _selectStateHangingStick;
        private int _hangingIndex;
        private bool _first=true;
        private IHeroHandleItem _stick;
        private float _hangingStartPosY;
        
        
        public HangingStickState(in StateConditions.Delegates heroBehaviourDelegates) : base(in heroBehaviourDelegates)
        {
        }

        protected override void RegisterDelegateInOrder(in StateConditions.Delegates conditionDelegates)
        {
            _delegate.Add(conditionDelegates.selectStateRunning);
            
            _hangingIndex = 1;
            _selectStateHangingStick = conditionDelegates.selectStateHangingStick;
            _delegate.Add(_selectStateHangingStick);
            
            _delegate.Add(conditionDelegates.selectStateFallingHavingStick);
            
            _delegate.Add(conditionDelegates.selectStateFalling);
            _delegate.Add(conditionDelegates.selectStateIdle);
            _delegate.Add(conditionDelegates.selectStateCrouching);
        }

        public void OnAction(IHero hero)
        {
            Debug.Log($"HeroState: HangingStick");
            if (_first)
            {
                _stick = hero.handlingItem;

                //Debug.Log($"_stick:{_stickHeroHandleItem}");
                 hero.Animate(AnimationStrings.HangingStick, _stick.HeroRightHandSetPoint, _stick.HeroLeftHandSetPoint);
                hero.SetItemJoint(_stick);
                _stick.FreeFreezingPosition();
                _stick.OnSelectedLight();
                _hangingStartPosY = hero.pos.y;
                _first = false;
            }

            if (_hangingStartPosY - FallingStartDistance > hero.pos.y)
            {
                _delegate.RemoveAt(_hangingIndex);
                _stick.RemoveJoint();
                return;
            }

            hero.BackWalk();
        }

        public void RefreshThisState(IHero hero)
        {
            //Debug.Log($"HeroState: Finish: HangingStick");
            _delegate.Insert(_hangingIndex,_selectStateHangingStick);
            hero.ReleaseItemJoint(_stick);
            hero.SetHandlingItem(null);
            _stick.OffSelectedLight();
            _first = true;
        }
    }
}