using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{
    [SerializeField] Transform targetPos;
    [SerializeField] float speed = 5f;
    NavMeshAgent agent;
    Ray lastRay;
    Camera cam;
    Animator animator;

    int animSpeedId;

    private void Start()
    {
        animator = GetComponent<Animator>();
        animSpeedId = Animator.StringToHash("Speed");

        cam = Camera.main;
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
    }

    void Update()
    {
        transform.LookAt(targetPos);

        if (Input.GetMouseButtonDown(0))
        {
            MoveToCursor();
        }
        // Debug.DrawRay(lastRay.origin, lastRay.direction * 100);
        UpdateAnimator();
    }

    private void UpdateAnimator()
    {
        // Get the global velocity from Nav Mesh Agent
        Vector3 veloctiy = agent.velocity;
        // Convert this into a local value relative to the character
        Vector3 localVelocity = transform.InverseTransformDirection(veloctiy); // our Animator only wants to know if we run forward or not

        // Set the Animators blend value to our desired Speed (Z Axis)
        float speed = localVelocity.z;
        animator.SetFloat(animSpeedId, speed);
    }

    void MoveToCursor()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            agent.SetDestination(hit.point);
        }
    }
}
