using RPG.Combat;
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
                Attack();
            }
            else
            {
                StopAttack();
            }
        }

        private void StopAttack()
        {
            var fighter = GetComponent<Fighter>();
            fighter.Cancel();
        }

        private void Attack()
        {
            print($"{gameObject.name}: chase the player!");
            var targetGameObject = GameObject.FindWithTag("Player");
            var fighter = GetComponent<Fighter>();

            if (!fighter.CanAttack(targetGameObject))
            {
                return;
            }

            fighter.Attack(targetGameObject);
        }

        private float DistanceToPlayer()
        {
            var player = GameObject.FindWithTag("Player");
            var distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            return distanceToPlayer;
        }
    }
}
