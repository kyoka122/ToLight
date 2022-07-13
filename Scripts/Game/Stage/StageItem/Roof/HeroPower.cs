using System;
using Game.Hero;
using UnityEngine;

namespace Game.StageItem.Roof
{
    public class HeroPower:MonoBehaviour
    {
        private bool _hadInit;
        private event Action<IHero> GotPowerCallBack;
        
        public void Init(Action<IHero> heroManagerActions)
        {
            GotPowerCallBack += heroManagerActions;
            _hadInit = true;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (!_hadInit)
            {
                return;
            }
            if (other.TryGetComponent(out IHero hero))
            {
                GotPowerCallBack?.Invoke(hero);
                Destroy(gameObject);
            }
        }
    }
}