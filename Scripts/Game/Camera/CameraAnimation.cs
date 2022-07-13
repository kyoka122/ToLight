namespace Game.Camera
{
    public class CameraAnimation
    {
            
       /* 
        //TODO: _transformに&&別クラスに
        public async UniTask ForceMoveNewStageTask(IHero hero,Transform lightTransform,float forceMoveSpeed,CancellationToken token)
        {
            bool isAboveHeroPos=false;
            bool isFrontOfDown=false;
            bool firstLoop = true;

            float moveDistanceX = hero.posX - lightTransform.position.x;
            float prevPosX = lightTransform.position.x;
            float maxDegrees = 0;
            
            while (!(isAboveHeroPos&&isFrontOfDown))
            {
                if (!isAboveHeroPos)
                {
                    var newPos = GetNewPosLerpLightToHeroPos(hero, lightTransform.position,
                        forceMoveSpeed);

                    if (firstLoop)
                    {
                        var moveRate = (newPos.x - prevPosX) / moveDistanceX;
                        maxDegrees = lightTransform.rotation.eulerAngles.z * moveRate;
                        firstLoop = false;
                    }
                    
                    prevPosX = newPos.x;
                    _spotLight.ForcePosition(newPos);
                }

                if (!isFrontOfDown)
                {
                    var newRot = GetRotationToDown(lightTransform.rotation, maxDegrees);
                    //var newRot = GetRotationToDown(lightTransform.rotation, forceMoveSpeed);
                    _spotLight.ForceRotation(newRot);
                }
                
                isAboveHeroPos = IsAboveHeroPos(hero, lightTransform.position);
                isFrontOfDown = IsFrontOfDown(lightTransform.up);
                await UniTask.DelayFrame(1, cancellationToken: token);
            }
            _spotLight.ForceRotation(Quaternion.identity);
        }

        private bool IsAboveHeroPos(IHero hero,Vector3 lightPos)
        {
            return Vector2.Distance(new Vector2(hero.posX, hero.posZ),
                new Vector2(lightPos.x, lightPos.z)) < HeroAbovePosRange;
        }

        private bool IsFrontOfDown(Vector3 lightVec)
        {
            var lightRotAngle=Vector3.SignedAngle( -lightVec,Vector3.down , -Vector3.forward);
            return Mathf.Abs(lightRotAngle) <= 0;
        }

        private Vector3 GetNewPosLerpLightToHeroPos(IHero hero,Vector3 lightPos,float forceMoveSpeed)
        {
            var destination = new Vector3(hero.posX, lightPos.y, hero.posZ);
            var newPos = Vector3.Lerp(lightPos, destination, Time.deltaTime * forceMoveSpeed);
            return newPos;
        }
        
        private Quaternion GetRotationToDown(Quaternion lightRot,float maxDegrees)
        {
            var newRot = Quaternion.RotateTowards(lightRot,Quaternion.LookRotation(Vector3.forward),  maxDegrees);
            return newRot;

        }*/
    }
}