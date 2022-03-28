using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Combat;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour
    {
        [SerializeField] Transform targetPos;
        [SerializeField] float speed = 5f;
        NavMeshAgent agent;
        Ray lastRay;
        Camera cam;
        Animator animator;

        Fighter combat;

        int animSpeedId;


        private void Start()
        {


            combat = GetComponent<Fighter>();
            animator = GetComponent<Animator>();
            animSpeedId = Animator.StringToHash("Speed");

            cam = Camera.main;
            agent = GetComponent<NavMeshAgent>();
            agent.speed = speed;
        }

        void Update()
        {
            UpdateAnimator();
        }

        public void Stop()
        {
            agent.isStopped = true;
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

        public void StartMoveAction(Vector3 _destination)
        {
            combat.Cancel();
            MoveTo(_destination);
        }



        public void MoveTo(Vector3 _destination)
        {

            agent.isStopped = false;
            agent.SetDestination(_destination);
        }
    }

}