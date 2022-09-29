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
            for (int i = 0; i < transform.childCount; i++)
            {
                var current = GetWayPointPosition(i);
                var next = GetWayPointPosition(GetNextIndex(i));

                DrawWayPoint(current);
                DrawPathBetween(current, next);
            }
        }

        private int GetNextIndex(int i)
        {
            return (i + 1) % transform.childCount;
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
