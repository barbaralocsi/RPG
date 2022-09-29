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
                var current = GetWaypointPosition(i);
                var next = GetWaypointPosition(GetNextIndex(i));

                DrawWayPoint(current);
                DrawPathBetween(current, next);
            }
        }

        public int GetNextIndex(int i)
        {
            return (i + 1) % transform.childCount;
        }

        public Vector3 GetWaypointPosition(int i)
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
