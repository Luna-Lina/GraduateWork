using UnityEngine;

namespace StarVelocity.Controllers
{
    public class Buffs : Food
    {
        protected override void EatFood(IPlayer player)
        {
            Destroy(gameObject);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.GetComponent<IPlayer>() is IPlayer player)
            {
                player.CurrentScore();
                Destroy(gameObject);
            }
        }
    }
}