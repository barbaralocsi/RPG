using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using UnityEngine;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;
        [SerializeField] float suspicionTime = 2f;

        Fighter fighter;
        GameObject player;
        Health health;
        Mover mover;

        Vector3 guardPosition;
        float timeSinceLastSawPlayer = Mathf.Infinity;

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
                timeSinceLastSawPlayer = 0;
                fighter.Attack(player);
            }
            else
            {
                timeSinceLastSawPlayer += Time.deltaTime;
                if (timeSinceLastSawPlayer > suspicionTime)
                {
                    // Starting a move action automatically cancels the attack action.
                    mover.StartMoveAction(guardPosition);
                }
                else
                {
                    mover.StartMoveAction(transform.position);
                }
            }
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
