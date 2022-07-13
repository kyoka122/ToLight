using Game.StageItem;
using UnityEngine;

namespace Game.Hero
{
    public static class AnimationStrings
    {
        public const string Idle = "Idle";
        public const string Walk = "Walk";
        public const string Run = "Run";
        public const string Falling = "Falling";
        public const string HangingStick = "HangingStick";
        public const string JumpingUp = "JumpingUp";
        public const string JumpingDown = "JumpingDown";
        public const string Landing = "Landing";
        public const string PullingChair = "PullingChair";
        public const string Slipping = "Slipping";
        public const string OpeningDoor = "OpeningDoor";
        public const string FlashingBoss = "FlashingBoss";
        public const string Crouching = "Crouching";
    }

    public class HeroAnimation : MonoBehaviour
    {
        private Animator _animator;

        //TODO: StageItemDataからではなく、動的な取得に変更
        private Transform _rightHandSetPoint;
        private Transform _leftHandSetPoint;

        private string _pervString="";
        private bool _hadInit;
        public void Init(Animator animator)
        {
            _animator = animator;
            _hadInit = true;
        }
        
        /// <summary>
        /// 主人公が物を操作するアニメーションでIKを使用する場合
        /// </summary>
        /// <param name="animString"></param>
        /// <param name="rightHandSetPoint"></param>
        /// <param name="leftHandSetPoint"></param>
        public void HandleAnimation(string animString, Transform rightHandSetPoint, Transform leftHandSetPoint)
        {
            if (_pervString.Equals(animString))
            {
                return;
            }
            _rightHandSetPoint = rightHandSetPoint;
            _leftHandSetPoint = leftHandSetPoint;
            TriggerAnimation(animString);
            _pervString = animString;
        }
        
        public void TriggerAnimation(string animString)
        {
            if (_pervString.Equals(animString))
            {
                return;
            }
            _animator.SetTrigger(animString);
            _pervString = animString;
        }

        /// <summary>
        /// イベント関数
        /// </summary>
        private void OnAnimatorIK()
        {
            if (!_hadInit)
            {
                return;
            }
            if (_animator.GetCurrentAnimatorStateInfo(0).IsName(AnimationStrings.PullingChair))
            {
                OnPullingChairPullIK();
            }

            if (_animator.GetCurrentAnimatorStateInfo(0).IsName(AnimationStrings.HangingStick))
            {
                OnHangingChairPullIK();
            }
        }

        /// <summary>
        /// 椅子を引っ張る時のアニメーションのIK
        /// </summary>
        private void OnPullingChairPullIK()
        {
            //Debug.Log($"OnPullingChairPullIK");
            _animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
            _animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0);
            _animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
            _animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0);
            _animator.SetIKPosition(AvatarIKGoal.RightHand, _rightHandSetPoint.position);
            _animator.SetIKRotation(AvatarIKGoal.RightHand, _rightHandSetPoint.rotation);
            _animator.SetIKPosition(AvatarIKGoal.LeftHand, _leftHandSetPoint.position);
            _animator.SetIKRotation(AvatarIKGoal.LeftHand, _leftHandSetPoint.rotation);
        }

        /// <summary>
        /// 木の棒に捕まる時のアニメーションのIK
        /// </summary>
        private void OnHangingChairPullIK()
        {
            Debug.Log($"OnHangingChairPullIK");
        }
    }
}