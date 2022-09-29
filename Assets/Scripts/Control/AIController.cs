using RPG.Combat;
using UnityEngine;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;
        private Fighter fighter;
        private GameObject player;

        private void Start()
        {
            fighter = GetComponent<Fighter>();
            player = GameObject.FindWithTag("Player");
        }

        private void Update()
        {
            
            if (InAttackRangeOf(player) && fighter.CanAttack(player))
            {
                fighter.Attack(player);
            }
            else
            {
                StopAttack();
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
    }
}
