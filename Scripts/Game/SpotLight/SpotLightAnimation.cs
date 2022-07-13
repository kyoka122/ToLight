using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Hero;
using UnityEngine;

namespace Game.SpotLight
{
    public class SpotLightAnimation
    {
        private const float HeroAboveRange = 0.001f;
        
        private readonly IHero _hero;
        private readonly ISpotLight _spotLight;
        private readonly Transform _lightTransform;
        private readonly float _forceMoveSpeed;
        
        public SpotLightAnimation(IHero hero,ISpotLight spotLight,Transform lightTransform,float forceMoveSpeed)
        {
            _hero = hero;
            _spotLight = spotLight;
            _lightTransform = lightTransform;
            _forceMoveSpeed = forceMoveSpeed;
        }
        
        public async UniTask ForceMoveNewStageTask(CancellationToken token)
        {
            bool isAboveHeroPos = false;
            bool isFrontOfDown = false;
            bool firstLoop = true;

            float moveDistanceX = _hero.pos.x - _lightTransform.position.x;
            float prevPosX = _lightTransform.position.x;
            float maxDegrees = 0;

            while (!(isAboveHeroPos && isFrontOfDown))
            {
                if (!isAboveHeroPos)
                {
                    var newPos = GetNewPosLerpLightToHeroPos(_lightTransform.position);

                    if (firstLoop)
                    {
                        var moveRate = (newPos.x - prevPosX) / moveDistanceX;
                        maxDegrees = _lightTransform.rotation.eulerAngles.z * moveRate;
                        firstLoop = false;
                    }

                    prevPosX = newPos.x;
                    _spotLight.ForcePosition(newPos);
                }

                if (!isFrontOfDown)
                {
                    var newRot = GetRotationToDown(_lightTransform.rotation, maxDegrees);
                    //var newRot = GetRotationToDown(lightTransform.rotation, forceMoveSpeed);
                    _spotLight.ForceRotation(newRot);
                }

                isAboveHeroPos = IsAboveHeroPos( _lightTransform.position);
                isFrontOfDown = IsFrontOfDown(_lightTransform.up);
                await UniTask.DelayFrame(1, cancellationToken: token);
            }

            _spotLight.ForceRotation(Quaternion.identity);
        }

        private bool IsAboveHeroPos(Vector3 lightPos)
        {
            float lightToHero = Vector2.Distance(new Vector2(_hero.pos.x, _hero.pos.z),
                new Vector2(lightPos.x, lightPos.z));
            return Mathf.Abs(lightToHero)<HeroAboveRange ;
        }

        private bool IsFrontOfDown(Vector3 lightVec)
        {
            var lightRotAngle = Vector3.SignedAngle(-lightVec, Vector3.down, -Vector3.forward);
            return Mathf.Abs(lightRotAngle) <= 0;
        }

        private Vector3 GetNewPosLerpLightToHeroPos(Vector3 lightPos)
        {
            var destination = new Vector3(_hero.pos.x, lightPos.y, _hero.pos.z);
            var newPos = Vector3.Lerp(lightPos, destination, Time.deltaTime * _forceMoveSpeed);
            return newPos;
        }

        private Quaternion GetRotationToDown(Quaternion lightRot, float maxDegrees)
        {
            var newRot =
                Quaternion.RotateTowards(lightRot, Quaternion.LookRotation(Vector3.forward), maxDegrees);
            return newRot;

        }
    }
}