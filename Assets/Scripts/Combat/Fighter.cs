using RPG.Movement;
using UnityEngine;
namespace RPG.Combat
{
    public class Fighter : MonoBehaviour
    {
        [SerializeField] float weaponRange = 2f;

        private Transform target;

        private void Update()
        {
            if (target == null)
            {
                return;
            }

            var mover = GetComponent<Mover>();

            // Check distance of the target and the fighter.
            // If it's close enough, then we need to stop the movement!

            if (!IsInRange())
            {
                mover.MoveTo(target.position);
            }
            else
            {
                mover.Stop();
                print("Attacking!");
            }

        }

        private bool IsInRange()
        {
            var distance = Vector3.Distance(transform.position, target.position);
            var isNotInRange = distance < weaponRange;
            return isNotInRange;
        }

        public void Attack(CombatTarget combatTarget)
        {
            // Move towards the target and attack it.
            // Keep in mind that the enemy might be also moving!

            target = combatTarget.transform;
        }

        public void Cancel()
        {
            target = null;
        }
    }
}