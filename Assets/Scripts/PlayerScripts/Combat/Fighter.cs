using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour
    {
        [SerializeField] float weaponRange = 2f;

        Transform target;

        Mover mover;

        private void Awake()
        {
            mover = GetComponent<Mover>();
        }
        private void Update()
        {
            if (target != null)
            {
                float distance = Vector3.Distance(this.transform.position, target.position);
                if (distance >= weaponRange)
                {

                    mover.MoveTo(target.position);
                    Debug.Log("Is moving");

                }
                else
                {
                    mover.Stop();
                    Debug.Log("is Stopped");
                }
            }
        }
        public void Attack(CombatTarget _combatTarget)
        {
            target = _combatTarget.transform;
        }

        public void Cancel()
        {
            target = null;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(this.transform.position, weaponRange);
        }
    }



}

