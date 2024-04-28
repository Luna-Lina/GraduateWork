using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StarVelocity
{
    public class ObstacleController : MonoBehaviour
    {
        [SerializeField] private float _speed;
        protected virtual void KillPlayer(IPlayer player)
        {
            player.MakeDamage();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.GetComponent<IPlayer>() is IPlayer player)
            {
                KillPlayer(player);
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.GetComponent<IPlayer>() is IPlayer player)
            {
                KillPlayer(player);
            }
        }

        private void Update()
        {
            Vector3 newPosition = transform.position + Vector3.down * _speed * Time.deltaTime;
            transform.position = newPosition;
        }
    }
}