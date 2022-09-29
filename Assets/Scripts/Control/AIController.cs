using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        // Start is called before the first frame update
        [SerializeField] float chaseDistance = 5f;

        private void Update()
        {
            if (DistanceToPlayer() < chaseDistance)
            {
                print($"{gameObject.name}: chase the player!");
            }
        }

        private float DistanceToPlayer()
        {
            var player = GameObject.FindWithTag("Player");
            var distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            return distanceToPlayer;
        }
    }
}
