using RPG.Core;
using RPG.Movement;
using UnityEngine;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float weaponRange = 2f;
        [SerializeField] float timeBetweenAttacks = 1f;
        [SerializeField] float weaponDamage = 5f;

        private Health target;
        float timeSinceLastAttack = 0;

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            if (target == null || target.IsDead())
            {
                return;
            }

            var mover = GetComponent<Mover>();

            // Check distance of the target and the fighter.
            // If it's close enough, then we need to stop the movement!

            if (!IsInRange())
            {
                mover.MoveTo(target.transform.position);
            }
            else
            {
                mover.Cancel();

                AttackBehaviour();
            }
        }

        private void AttackBehaviour()
        {
            transform.LookAt(target.transform);

            if (timeSinceLastAttack > timeBetweenAttacks)
            {
                TriggerAttack();
                timeSinceLastAttack = 0;
            }
        }

        private void TriggerAttack()
        {
            GetComponent<Animator>().ResetTrigger("stopAttack");
            // This will trigger the Hit() event
            GetComponent<Animator>().SetTrigger("attack");
        }

        // Handling the animation hit event
        void Hit()
        {
            if(target == null)
            {
                return;
            }
            
            target.TakeDamage(weaponDamage);
        }

        private bool IsInRange()
        {
            var distance = Vector3.Distance(transform.position, target.transform.position);
            var isNotInRange = distance < weaponRange;
            return isNotInRange;
        }

        public void Attack(CombatTarget combatTarget)
        {
            // Move towards the target and attack it.
            // Keep in mind that the enemy might be also moving!

            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.GetComponent<Health>();
        }

        public bool CanAttack(CombatTarget combatTarget)
        {
            return combatTarget != null && !combatTarget.GetComponent<Health>().IsDead();
        }

        public void Cancel()
        {
            target = null;
            TriggerStopAttack();
        }

        private void TriggerStopAttack()
        {
            GetComponent<Animator>().SetTrigger("stopAttack");
            GetComponent<Animator>().ResetTrigger("attack");
        }
    }
}