using System;
using Game.Stage;
using Game.StageItem;
using UnityEngine;

namespace Game.Hero
{
    /// <summary>
    /// ステートが遷移するにあたって、他のステートに影響されない条件
    /// </summary>
    public class StateConditions
    {
        public Delegates delegates { get; }
        
        private readonly NoneState _noneState;
        private readonly RunningState _runningState;
        private readonly OnPullingChairState _onPullingChairState;
        private readonly PullingChairState _pullingChairState;
        private readonly OnJumpingState _onJumpingState;
        private readonly JumpingState _jumpingState;
        private readonly WalkingState _walkingState;
        private readonly FallingState _fallingState;
        private readonly FallingHavingStickState _fallingHavingStickState;
        private readonly SlippingState _slippingState;
        private readonly IdleState _idleState;
        private readonly OnHangingStickState _onHangingStickState;
        private readonly HangingStickState _hangingStickState;
        private readonly OpeningDoorState _openingDoorState;
        private readonly CrouchingState _crouchingState;
        private readonly FlashingBossState _flashingBossState;
        //private readonly UpStairsState _goingUpStairs;

        private IStage _stage;
        public class Delegates
        {
            public Func<IHero, IHeroState> selectStateNone { get; }
            public Func<IHero, IHeroState> selectStateRunning { get; }
            public Func<IHero, IHeroState> onSelectStatePullingChair { get; }
            public Func<IHero, IHeroState> selectStatePullingChair { get; }
            public Func<IHero, IHeroState> selectStateOnJumping { get; }
            public Func<IHero, IHeroState> selectStateJumping { get; }
            public Func<IHero, IHeroState> selectStateWalking { get; }
            public Func<IHero, IHeroState> selectStateFalling { get; }
            public Func<IHero, IHeroState> selectStateFallingHavingStick { get; }
            public Func<IHero, IHeroState> selectStateSlipping { get; }
            public Func<IHero, IHeroState> selectStateIdle { get; }
            public Func<IHero, IHeroState> selectStateOnHangingStick { get; }
            public Func<IHero, IHeroState> selectStateHangingStick { get; }
            public Func<IHero, IHeroState> selectStateOpeningDoor { get; }
            public Func<IHero, IHeroState> selectStateCrouching { get; }
            public Func<IHero, IHeroState> selectStateFlashingBoss { get; }
            //public Func<IHero, IHeroState> selectStateGoingUpStairs { get; }
            

            public Delegates(StateConditions stateConditions)
            {
                selectStateNone = stateConditions.SelectStateNone;
                selectStateRunning = stateConditions.SelectStateRunning;
                onSelectStatePullingChair = stateConditions.OnSelectStatePullingChair;
                selectStatePullingChair = stateConditions.SelectStatePullingChair;
                selectStateOnJumping = stateConditions.SelectStateOnJumping;
                selectStateJumping = stateConditions.SelectStateJumping;
                selectStateWalking = stateConditions.SelectStateWalking;
                selectStateFalling = stateConditions.SelectStateFalling;
                selectStateFallingHavingStick = stateConditions.SelectStateFallingHavingStick;
                selectStateSlipping = stateConditions.SelectStateSlipping;
                selectStateIdle = stateConditions.SelectIdleState;
                selectStateOnHangingStick = stateConditions.SelectStateOnHangingStick;
                selectStateHangingStick = stateConditions.SelectStateHangingStick;
                selectStateOpeningDoor = stateConditions.SelectStateOpeningDoor;
                selectStateCrouching = stateConditions.SelectStateCrouching;
                selectStateFlashingBoss = stateConditions.SelectStateFlashBoss;
                //selectStateGoingUpStairs = stateConditions.SelectStateGoingUpStairs;
            }
        }

        public StateConditions(IStage stage)
        {
            _stage = stage;
            delegates = new Delegates(this);
            _noneState = new NoneState(delegates);
            _runningState = new RunningState(delegates);
            _onPullingChairState = new OnPullingChairState(delegates);
            _pullingChairState = new PullingChairState(delegates);
            _onJumpingState = new OnJumpingState(delegates);
            _jumpingState = new JumpingState(delegates);
            _walkingState = new WalkingState(delegates);
            _fallingState = new FallingState(delegates);
            _fallingHavingStickState = new FallingHavingStickState(delegates);
            _slippingState = new SlippingState(delegates);
            _idleState = new IdleState(delegates);
            _onHangingStickState = new OnHangingStickState(delegates);
            _hangingStickState = new HangingStickState(delegates);
            _openingDoorState = new OpeningDoorState(delegates);
            _crouchingState = new CrouchingState(delegates);
            _flashingBossState = new FlashingBossState(delegates);
            //_goingUpStairs = new UpStairsState(delegates);
        }

        /// <summary>
        /// このステートは条件なし
        /// </summary>
        /// <param name="hero"></param>
        /// <returns></returns>
        private IHeroState SelectStateNone(IHero hero)
        {
            return _noneState;
        }
        
        private IHeroState SelectStateRunning(IHero hero)
        {
            bool onStandable = hero.onStandable;
            bool outOfLight = !hero.isInLight && hero.isOnLight;
            return (onStandable && outOfLight) ? _runningState : null;
        }

        // MEMO: 主人公が引っ張る物の候補が増えてきたら別にまとめる。 
        private IHeroState SelectStatePullingChair(IHero hero)
        {
            if (hero.handlingItem==null)
            {
                return null;
            }

            bool handlingChair=hero.handlingItem.ItemEnum == HeroHandleItemEnum.Chair;
            return (hero.isOnActionLight && handlingChair) ? _pullingChairState : null;
        }
        private IHeroState OnSelectStatePullingChair(IHero hero)
        {
            if (hero.handleableItem==null)
            {
                return null;
            }
            bool canGetChair = hero.handleableItem.ItemEnum==HeroHandleItemEnum.Chair;
            bool onActionLight=hero.isOnActionLight;
            return (onActionLight && canGetChair) ? _onPullingChairState : null;
        }

        /// <summary>
        /// ジャンプし始めるタイミングの条件
        /// </summary>
        /// <param name="hero"></param>
        /// <returns></returns>
        private IHeroState SelectStateOnJumping(IHero hero)
        {
            bool isCanJumpSpot = hero.facedHavingJumpableHeightObj && hero.onStandable;
            return isCanJumpSpot && !hero.isOnActionLight ? _onJumpingState : null;
        }
        private IHeroState SelectStateJumping(IHero hero)
        {
            return !hero.onStandable ? _jumpingState : null;
        }

        private IHeroState SelectStateWalking(IHero hero)
        {
            bool outOfIdleRange = !hero.inIdleRange;
            bool inLight = hero.isInLight && hero.isOnLight;
            return hero.isOnLight && outOfIdleRange && inLight ? _walkingState : null;
        }

        //MEMO: 物をつかんで落ちる時や高いところから落ちる時。ジャンプして落ちるときはSelectStateOnJumping()
        private IHeroState SelectStateFalling(IHero hero)
        {
            return !hero.onStandable ? _fallingState : null;
        }
        
        private IHeroState SelectStateFallingHavingStick(IHero hero)
        {
            return !hero.onStandable ? _fallingHavingStickState : null;
        }

        private IHeroState SelectStateSlipping(IHero hero)
        {
            Debug.Log($"Slipping?:{hero.onStandable}");
            return hero.onStandable ? _slippingState : null;
        }
        
        private IHeroState SelectIdleState(IHero hero)
        {
            bool inIdleRange = hero.inIdleRange;
            return hero.isOnLight && inIdleRange ? _idleState : null;
        }

        private IHeroState SelectStateOnHangingStick(IHero hero)
        {
            var item = hero.handleableItem;
            var itemIsStick = item?.ItemEnum == HeroHandleItemEnum.Stick;
            return itemIsStick && hero.isOnActionLight ? _onHangingStickState : null;
        }
        private IHeroState SelectStateHangingStick(IHero hero)
        {
            var item = hero.handlingItem;
            var itemIsStick = item?.ItemEnum == HeroHandleItemEnum.Stick;
            return itemIsStick && hero.isOnActionLight ? _hangingStickState : null;
        }

        private IHeroState SelectStateCrouching(IHero hero)
        {
            return (!hero.isOnLight || !hero.isInLight) ? _crouchingState : null;
        }

        private IHeroState SelectStateOpeningDoor(IHero hero)
        {
            if (hero.handleableItem==null)
            {
                return null;
            }
            bool canGetDoor = hero.handleableItem.ItemEnum==HeroHandleItemEnum.Door;
            return (hero.isOnActionLight && canGetDoor) ? _openingDoorState : null;
        }

        private IHeroState SelectStateFlashBoss(IHero hero)
        {
            var isRoofStage = _stage.currentStage == StageEnum.Roof;
            return (hero.isOnActionLight && isRoofStage) ? _flashingBossState : null;
        }

        /*private IHeroState SelectStateGoingUpStairs(IHero hero)
        {
            bool isCanGoUpStairsSpot = hero.facedUpStairsObj && hero.onStandable;
            return isCanGoUpStairsSpot  ? _goingUpStairs : null;
        }*/

    }
}