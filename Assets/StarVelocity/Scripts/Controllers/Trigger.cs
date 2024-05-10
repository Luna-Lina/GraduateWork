using UnityEngine.Events;

namespace StarVelocity.Controllers
{
    public class Trigger : ObstacleController
    {
        public UnityEvent Act;

        protected override void KillPlayer(IPlayer player)
        {

            if (player != null)
            {
                player.MakeDamage();
            }
        }
    }
}