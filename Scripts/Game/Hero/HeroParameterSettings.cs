using UnityEngine;

namespace Game.Hero
{
    public class HeroParameterSettings : MonoBehaviour
    {
        [Range(0, 0.5f)] [SerializeField] private float idleRange;
        public float IdleRange => idleRange;

        [Tooltip("この範囲を超えると走り始める"), Range(0, 0.5f)] [SerializeField]
        private float walkRange;

        public float WalkRange => walkRange;

        [Range(0, 0.5f)] [SerializeField] private float walkAcceleration;
        public float WalkAcceleration => walkAcceleration;
        
        [Range(0, 0.5f)] [SerializeField] private float sideWalkAcceleration;
        public float SideWalkAcceleration => sideWalkAcceleration;

        [Range(0, 0.5f)] [SerializeField] private float runAcceleration;
        public float RunAcceleration => runAcceleration;

        [Range(0, 1)] [SerializeField] private float decelerationRate;
        public float DecelerationRate => decelerationRate;

        [Range(0, 1)] [SerializeField] private float rotAngleWhilePullingChair;
        public float RotAngleWhilePullingChair => rotAngleWhilePullingChair;
        
        /*[Range(0, 500f)] [SerializeField] private float upStairsAcceleration;
        public float UpStairsAcceleration => upStairsAcceleration;*/

        [Range(5, 40)] [SerializeField] private float jumpAcceleration;
        public float JumpAcceleration => jumpAcceleration;

        [Tooltip("最低限地面から離れる為に必要なジャンプ力"), Range(0, 40)] [SerializeField]
        private float firstJumpAcceleration;

        public float FirstJumpAcceleration => firstJumpAcceleration;

        [Tooltip("オブジェクトより高い位置に飛んだあとどれだけ余分にジャンプするか"), Range(0, 40)] [SerializeField]
        private float jumpExcessAcceleration;

        public float JumpExcessAcceleration => jumpExcessAcceleration;

        [Range(0, 3)] [SerializeField] private float jumpingGravity;
        public float JumpingGravity => jumpingGravity;

        [Range(0, 1)] [SerializeField] private float maxJumpHeight;
        public float MaxJumpHeight => maxJumpHeight;

        [Range(0, 5)] [SerializeField] private float attack;
        public float Attack => attack;

        //CHANGED: FacedObjのRayを少し高い位置にしたため、必要なし
        //[Tooltip("ジャンプする為に必要な、目の前のオブジェクトの高さ")]
        //[SerializeField] private float minimalObjHeightToJump ;
    }
}