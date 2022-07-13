using Game.Hero;
using Game.StageItem.Roof.Interface;
using UnityEngine;

namespace Game.StageItem.Roof
{
    public class BossCondition
    {
        private Boss _boss;
        public BossCondition(Boss boss)
        {
            _boss = boss;
        }

        public float GetTargetDistanceToHero(float maxDistanceToHero,float currentTargetDistanceToHero,float speed)
        {
            if (maxDistanceToHero<=currentTargetDistanceToHero)
            {
                return maxDistanceToHero;
            }

            float newTargetDistance = currentTargetDistanceToHero-speed;
            return newTargetDistance;
        }
        
        public void TryMove(bool damaged,IHero hero)
        {
            if (damaged)
            {
                LeaveHero(hero);
                return;
            }
            GetCloserToHero(hero);
            
        }

        public void TryUpdateNextState(BossStateEnum currentState,float currentHp,float stateHp)
        {
            if (currentHp<0)
            {
                _boss.Defeated();
            }
            if (currentHp<stateHp)
            {
                _boss.UpdateState(++currentState);
            }
        }

        private void LeaveHero(IHero hero)
        {
            /*if (Vector3.Distance(heroPos, _boss.position) < chaseDistance)
            {
                _boss.Move(Vector3.zero);
                return;
            }*/
            
            _boss.LeaveStage();
        }

        private void GetCloserToHero(IHero hero)
        {
            /*var heroPos = new Vector3(hero.posX, hero.posY, hero.posZ);
            if (Vector3.Distance(heroPos, _boss.position) < chaseDistance)
            {
                _boss.Move(Vector3.zero);
                return;
            }

            //var newPos = Vector3.Lerp(_boss.position, heroPos, Time.deltaTime * speed);
            var newVec = heroPos - _boss.position;
            _boss.Move(newVec);*/
            Vector3 newVec = new Vector3(hero.pos.x - _boss.position.x, 0, hero.pos.z - _boss.position.z);
            _boss.GetCloser(newVec);
        }

    }
}