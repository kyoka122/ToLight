using System.Collections.Generic;
using Game.Hero;
using Game.Stage;
using UnityEngine;

namespace Game.SpotLight
{
    public interface ISpotLight
    {
        public bool isOnLight { get; }
        public void Init(IStage stage,IHero hero);
        public void StartUpdateSpotLight();
        public void SetLightSize();
        public void Move(Vector3 moveVec);
        public void ForcePosition(Vector3 newPos);
        public void Rotate(Vector3 moveVec);
        public void ForceRotation(Quaternion newRot);
        public void ForceVelocityX(float forcedVelocityX);
        public void ForceAngularVelocityZ(float forcedVelocityZ);
        public void SetActiveModel(bool active);
        public void SetActionLight(bool isAction);
        public bool IsOnActionLight();
        public bool IsOnMoveLight();
        public List<RaycastHit> SearchInSpotLight();
        public bool TryGetLightedPoint(out Vector3 lightedPoint);
        public Transform GetLightOriginTransform();
        public List<GameObject> GetObjectsInSpotLight();
        public void UpdateNextStage();
        public void Reset(Vector3 restartPos);
        public void ReStart();
    }
}