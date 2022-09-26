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
            InteractWithCombat();
            InteractWithMovement();
        }

        private void InteractWithCombat()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var hits = Physics.RaycastAll(GetMouseRay());
                foreach (var hit in hits)
                {
                    var combatTarget = hit.transform.GetComponent<CombatTarget>();
                    if (combatTarget != null)
                    {
                        GetComponent<Fighter>().Attack(combatTarget);
                        break;
                    }
                }
            }
        }

        private void InteractWithMovement()
        {
            if (Input.GetMouseButton(0))
            {
                MoveToCursor();
            }
        }

        private void MoveToCursor()
        {
            bool hasHit = Physics.Raycast(GetMouseRay(), out var hitInfo);
            if (hasHit)
            {
                GetComponent<Mover>().MoveTo(hitInfo.point);
            }
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}