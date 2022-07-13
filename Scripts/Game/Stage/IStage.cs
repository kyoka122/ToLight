using Game.Camera;
using Game.Hero;
using Game.SpotLight;
using Game.SpotLight.Struct;
using UnityEngine;

namespace Game.Stage
{
    public interface IStage
    {
        public StageEnum currentStage { get; }

        public void Init(LightMoveLimiter lightMoveLimiter,StageRange stageRange, IHero hero, ISpotLight spotLight,IHeroCamera heroCamera);
        public void StartUpdate();
        public MoveLimitStruct GetSpotLightMoveLimit();
        public void ClearRoofStage();
        public void ResetStage();
        public Vector3 GetCheckPoint(StageEnum stageEnum);
        //public PosAndRot GetLeftMoveLimit();
    }
}