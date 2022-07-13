namespace Game.Hero
{
    public class JumpingState : BaseHeroStateHandlerHelper, IHeroState
    {
        private bool _hadForcedUp = false;
        
        public JumpingState(in StateConditions.Delegates heroBehaviourDelegates) : base(
            in heroBehaviourDelegates)
        {
        }

        protected override void RegisterDelegateInOrder(in StateConditions.Delegates conditionDelegates)
        {
            _delegate.Add(conditionDelegates.selectStateOnHangingStick);
            _delegate.Add(conditionDelegates.selectStateJumping);
            _delegate.Add(conditionDelegates.selectStateRunning);
            _delegate.Add(conditionDelegates.selectStateWalking);
            _delegate.Add(conditionDelegates.selectStateIdle);
            _delegate.Add(conditionDelegates.selectStateCrouching);
        }

        public void OnAction(IHero hero)
        {
            //Debug.Log($"HeroState: Jumping");
            hero.Walk();

            //MEMO: ジャンプできなくなる＝落ち始める
            if (_hadForcedUp)
            {
                return;
            }
            
            //ジャンプ距離上限に達したかどうかの判定
            float jumpedHeight = hero.pos.y - hero.jumpStartPosY;

            if (jumpedHeight >= hero.maxJumpHeightValue)
            {
                //Debug.Log($"MaxHeight");
                _hadForcedUp = true;
                hero.Animate(AnimationStrings.JumpingDown);
                return;
            }

            //目の前からオブジェクトがなくなったかどうかの判定
            if (!hero.facedHavingJumpableHeightObj)
            {
                //Debug.Log($"JumpExcess");
                _hadForcedUp = true;
                hero.Animate(AnimationStrings.JumpingDown);
                hero.JumpExcess();
                return;
            }
            if (!_hadForcedUp)
            {
                hero.Jump();
            }
        }

        public void RefreshThisState(IHero hero)
        {
            _hadForcedUp = false;
            hero.Animate(AnimationStrings.Landing);
            //Debug.Log($"HeroState: Finish: Jumping");
        }

    }
}