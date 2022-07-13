using System;

namespace Game.UI.Interface
{
    public interface IUIController
    {
        public void StartFadein(Action reset,Action restart);
    }
}