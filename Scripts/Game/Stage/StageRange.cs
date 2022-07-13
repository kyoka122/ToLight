using System;
using UnityEngine;
using Utility.CustomEditor;

namespace Game.Stage
{
    [Serializable]
    public class MoveRange
    {
        public float RightLimit;
    }
    
    public class StageRange:MonoBehaviour
    {
        [SerializeField,EnumIndex(typeof(StageEnum))]
        private MoveRange[] stage;

        public MoveRange[] Stage => stage;
    }
}