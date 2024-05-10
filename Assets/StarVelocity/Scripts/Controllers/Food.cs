using UnityEngine;

namespace StarVelocity.Controllers
{
    public class Food : MonoBehaviour
    {
        [SerializeField] private float _speed;

        protected virtual void EatFood(IPlayer player)
        {

        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.GetComponent<IPlayer>() is IPlayer player)
            {
                EatFood(player);
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.GetComponent<IPlayer>() is IPlayer player)
            {
                EatFood(player);
            }
        }

        private void Update()
        {
            Vector3 newPosition = transform.position + Vector3.down * _speed * Time.deltaTime;
            transform.position = newPosition;
        }
    }
}
