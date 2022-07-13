using Game.Hero;
using Game.Stage;
using UnityEngine;

namespace Game.Camera
{
    public interface IHeroCamera
    {
        public void Init(Vector3 heroInitPos, IHero hero);
        public void StartUpdateCamera();
        public void MoveNextStage(StageEnum nextStage);
        public void Reset(StageEnum resetStage);
    }
}