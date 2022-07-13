using System;

namespace Game.UI.Interface
{
    public class UIController:IUIController
    {
        private readonly RespawnAnimator _respawnAnimator;
        
        public UIController(RespawnAnimator respawnAnimator)
        {
            _respawnAnimator = respawnAnimator;
        }
        
        public void StartFadein(Action reset,Action restart)
        {
            _respawnAnimator.StartFadein(reset,restart);
        }
        
    }
}