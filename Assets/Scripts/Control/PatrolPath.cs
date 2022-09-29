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
            var first = GetWayPointPosition(0);
            var current  = first;
            DrawWayPoint(current);

            for (int i = 1; i < transform.childCount; i++)
            {
                var previous = current;
                current = GetWayPointPosition(i);

                DrawWayPoint(current);
                DrawPathBetween(previous, current);
            }

            DrawPathBetween(first, current);
        }

        private Vector3 GetWayPointPosition(int i)
        {
            return transform.GetChild(i).position;
        }

        private void DrawWayPoint(Vector3 position)
        {
            Gizmos.DrawSphere(position, wayPointGizmoRadius);
        }

        private void DrawPathBetween(Vector3 from, Vector3 to)
        {
            Gizmos.DrawLine(from, to);
        }
    }
}
