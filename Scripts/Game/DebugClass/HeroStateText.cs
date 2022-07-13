using UnityEngine;
using UnityEngine.UI;

namespace Game.DebugClass
{
    public class HeroStateText:MonoBehaviour
    {
        private Text _heroStateText;

        private void Start()
        {
            _heroStateText = GetComponent<Text>();
        }

        public void SetHeroStateText(string state)
        {
            _heroStateText.text = $"HeroState: {state}";
        }
    }
}