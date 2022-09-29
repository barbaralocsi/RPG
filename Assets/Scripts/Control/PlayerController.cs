using System;
using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using UnityEngine;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {

        private Health health;

        private void Start()
        {

            health = GetComponent<Health>();
        }

        private void Update()
        {
            if (health.IsDead())
            {
                return;
            }

            if (InteractWithCombat())
            {
                return;
            }

            if (InteractWithMovement())
            {
                return;
            }

            print("Nothing to do.");
        }

        private bool InteractWithCombat()
        {
            var hits = Physics.RaycastAll(GetMouseRay());
            foreach (var hit in hits)
            {
                var combatTarget = hit.transform.GetComponent<CombatTarget>();
                if (combatTarget == null)
                {
                    continue;
                }

                var targetGameObject = combatTarget.gameObject;
                var fighter = GetComponent<Fighter>();

                if (!fighter.CanAttack(targetGameObject))
                {
                    continue;
                }

                if (Input.GetMouseButton(0))
                {
                    fighter.Attack(targetGameObject);
                }

                return true;
            }

            return false;
        }

        private bool InteractWithMovement()
        {
            bool hasHit = Physics.Raycast(GetMouseRay(), out var hitInfo);
            if (hasHit)
            {
                if (Input.GetMouseButton(0))
                {
                    GetComponent<Mover>().StartMoveAction(hitInfo.point);
                }
                return true;
            }

            return false;
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}