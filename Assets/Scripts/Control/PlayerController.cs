using System;
using RPG.Combat;
using RPG.Movement;
using UnityEngine;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {

        private void Update()
        {
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

                if (Input.GetMouseButtonDown(0))
                {
                    GetComponent<Fighter>().Attack(combatTarget);
                }

                return true;
            }

            // if (Input.GetMouseButtonDown(0))
            // {
            //     GetComponent<Fighter>().StopAttack();
            // }

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