using UnityEngine;

namespace StarVelocity.Data
{
    [CreateAssetMenu(fileName = "PlayerControllerSettings", menuName = "StarVelocity/PlayerControllerSettings")]
    public class PlayerControllerSettings : ScriptableObject
    {
        public float Speed;
        public float MoveDistance = 1.4f;
        public float ReturnDelay = 1.3f;
    }
}