using UnityEngine;

namespace StarVelocity.Controllers
{
    public class Debuffs : Food
    {
        [SerializeField] private float speedIncreaseAmount = 2f;

        protected override void EatFood(IPlayer player)
        {
            player.IncreaseSpeed(speedIncreaseAmount);

            Destroy(gameObject);
        }
    }
}