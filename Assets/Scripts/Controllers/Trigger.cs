using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace StarVelocity
{
    public class Trigger : ObstacleController
    {
        public UnityEvent Act;

        protected override void KillPlayer(IPlayer player)
        {
            Act?.Invoke();
        }
    }
}