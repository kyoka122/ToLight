using UnityEngine;

namespace Game.Presentation
{
    public class DebugMonoBehaviour:MonoBehaviour
    {
        [SerializeField] private GameObject chairLeft;
        [SerializeField] private GameObject chairRight;

        public GameObject ChairRight => chairRight;
        public GameObject ChairLeft => chairLeft;
    }
}