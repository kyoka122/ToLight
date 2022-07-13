using System;
using Game.SpotLight;
using Game.Stage;
using Game.StageItem.Interface;
using UnityEngine;

namespace Game.Hero
{
    //TODO: もう少し分離する?
    public interface IHero
    {
        public void Init(ISpotLight spotLight,IStage stage,Action gameOver);
        public void StartUpdateHero();
        
        //public float posX { get; }
        //public float posY { get; }
        //public float posZ { get; }
        public Vector3 pos { get; }
        public bool onStandable { get; }
        public bool isInLight { get; }
        public bool inIdleRange { get; }
        public bool facedHavingJumpableHeightObj { get; }
        //public bool facedUpStairsObj { get;}
        public IHeroHandleItem handleableItem { get; }
        public IHeroHandleItem handlingItem { get; }
        public bool isOnLight { get;}
        public bool isOnMoveLight { get; }
        public bool isOnActionLight { get; }
        public float maxJumpHeightValue { get; }
        public float jumpStartPosY { get; }

        public void Walk();
        public void BackWalk();
        public void SideWalk();
        public void Walk(float walkSpeed);
        public void Run();
        public void Stay();
        public void Jump();
        public void OnJump();
        public void JumpExcess();
        public void LookAtBoss();
        public void Defeat();
        public void Animate(string animString);
        public void Animate(string animString, Transform rightHandSetPoint, Transform leftHandSetPoint);
        public void SetHandlingItem(IHeroHandleItem item);
        public void SetItemJoint(IHeroHandleItem item);
        public void ReleaseItemJoint(IHeroHandleItem item);
        public int GetBattlePowerPoint();
        public void SetBattlePower(int addPoint);
        public void Pose(bool on);
        public void AttackBoss();
        public void Reset(Vector3 newPos);
        public void ReStart();
    }
}