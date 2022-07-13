using Game.Hero;
using Game.Stage;
using UnityEngine;

namespace Game.StageItem.Roof.Interface
{
    public interface IBoss
    {
        public Vector3 position { get; }
        public void Init(IStage stage,IHero hero);
        public void StartBattle();
        public Vector3 GetPosition();
        public void AddDamage(float damage);
        public void ResetParameter();
    }
}