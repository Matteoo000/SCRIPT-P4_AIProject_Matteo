using System;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (player != null)
        {
            navMeshAgent.SetDestination(player.position);
        }

    }
        #region diffrent AI script
        /*
        public float health = 50f;
        private NavMeshAgent navMeshAgent;
        private Transform player;

        public event Action OnDeath; // Declare the OnDeath event

        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            navMeshAgent = GetComponent<NavMeshAgent>();
        }

        void Update()
        {
            if (player != null)
            {
                navMeshAgent.SetDestination(player.position);
            }
        }

        public void TakeDamage(float damage)
        {
            health -= damage;
            if (health <= 0)
            {
                Die();
            }
        }

        void Die()
        {
            OnDeath?.Invoke(); // Invoke the OnDeath event
            EnemyManager.Instance.RemoveEnemy();
            Destroy(gameObject);
        }
        */
        #endregion
    }
