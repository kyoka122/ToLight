using System;
using UnityEngine;
using Utility.CustomEditor;

namespace Game.StageItem.Roof
{
    /// <summary>
    /// お化けが画面外から入る、出る時の目標点
    /// </summary>
    [Serializable]
    public class BossMovePointAtStateChange
    {
        public Vector3 entryStart;
        public Vector3 entryEnd;
        public Vector3 exitEnd;
    }
    
    [Serializable]
    public class BossStateParameter
    {
        public BossMovePointAtStateChange movePos;
        public float hp;
    }
}