using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Control
{
    public class PatrolPath : MonoBehaviour
    {
        const float wayPointGizmoRadius = 0.3f;

        private void OnDrawGizmos()
        {
            var first = transform.GetChild(0);
            var current  = first;
            DrawWayPoint(current.position);

            for (int i = 1; i < transform.childCount; i++)
            {
                var previous = current;
                current = transform.GetChild(i);

                DrawWayPoint(current.transform.position);
                DrawPathBetween(previous, current);
            }

            DrawPathBetween(first, current);
        }

        private void DrawWayPoint(Vector3 position)
        {
            Gizmos.DrawSphere(position, wayPointGizmoRadius);
        }

        private void DrawPathBetween(Transform from, Transform to)
        {
            Gizmos.DrawLine(from.position, to.position);
        }
    }
}
