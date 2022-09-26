using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{
    [SerializeField]
    Transform target;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            MoveToCursor();
        }

        // if(Input.GetMouseButtonUp(0))
        // {
        //     // Stop movement when the mouse button is released
        //     GetComponent<NavMeshAgent>().destination = transform.position;
        // }

        UpdateAnimator();
        //Debug.DrawRay(lastRay.origin, lastRay.direction * 1000);
    }

    private void MoveToCursor()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        bool hasHit = Physics.Raycast(ray, out var hitInfo);
        if(hasHit)
        {
            GetComponent<NavMeshAgent>().destination = hitInfo.point;
        }
    }

    private void UpdateAnimator()
    {
        Vector3 velocity = GetComponent<NavMeshAgent>().velocity;

        // Convert this to the local value relative to the character - taking from global converting to local
        Vector3 localVelocity = transform.InverseTransformDirection(velocity);

        float speed = localVelocity.z;

        // Set the animators blend valut to be equal to our desired forward speed (on the Z axis)
        GetComponent<Animator>().SetFloat("forwardSpeed", speed);
    }
}
