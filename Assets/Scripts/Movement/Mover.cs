using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Combat;
using RPG.Core;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour
    {
        [SerializeField]
        Transform target;

        NavMeshAgent navMeshAgent;

        private void Start() {
            navMeshAgent = GetComponent<NavMeshAgent>();
        }

        void Update()
        {
            UpdateAnimator();
        }

        public void StartMoveAction(Vector3 destination)
        {
            GetComponent<Fighter>().Cancel();
            GetComponent<ActionScheduler>().StartAction(this);
            MoveTo(destination);
        }

        public void MoveTo(Vector3 destination)
        {
            navMeshAgent.destination = destination;
            navMeshAgent.isStopped = false;
        }

        public void Stop()
        {
            navMeshAgent.isStopped = true;
        }

        private void UpdateAnimator()
        {
            Vector3 velocity = navMeshAgent.velocity;

            // Convert this to the local value relative to the character - taking from global converting to local
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);

            float speed = localVelocity.z;

            // Set the animators blend valut to be equal to our desired forward speed (on the Z axis)
            GetComponent<Animator>().SetFloat("forwardSpeed", speed);
        }
    }
}