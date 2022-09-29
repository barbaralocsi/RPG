using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using UnityEngine;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;

        Fighter fighter;
        GameObject player;
        Health health;
        Mover mover;

        Vector3 guardPosition;

        private void Start()
        {
            fighter = GetComponent<Fighter>();
            health = GetComponent<Health>();
            mover = GetComponent<Mover>();

            player = GameObject.FindWithTag("Player");

            guardPosition = transform.position;
        }

        private void Update()
        {
            if (health.IsDead())
            {
                return;
            }

            if (InAttackRangeOf(player) && fighter.CanAttack(player))
            {
                fighter.Attack(player);
            }
            else
            {
                StopAttack();
                mover.StartMoveAction(guardPosition);
            }
        }

        private void StopAttack()
        {
            fighter.Cancel();
        }

        private bool InAttackRangeOf(GameObject target)
        {
            return DistanceToTarget(target) < chaseDistance;
        }

        private float DistanceToTarget(GameObject target)
        {
            return Vector3.Distance(transform.position, target.transform.position);
        }

        // Called by Unity
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }
    }
}
