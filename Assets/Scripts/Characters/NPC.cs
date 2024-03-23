using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character
{
    // A classe NPC é uma classe base que contém os comportamentos que são comuns a todos os NPCs.
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class NPC : MonoBehaviour
    {
        [HideInInspector] public Rigidbody2D rb;

        public NPCData data;
        public Vector2 direction { get; private set; }

        private Vector2 startingPosition;
        private float currentTurnDistance = 1f;
        private float currentIdleTime = 1f;
        private bool isIdle = false;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            startingPosition = rb.transform.position;
            currentTurnDistance = UnityEngine.Random.Range(data.stoppingDistance.min, data.stoppingDistance.max);
            currentIdleTime = UnityEngine.Random.Range(data.idleTime.min, data.idleTime.max);
            direction = data.direction;
        }

        private void FixedUpdate()
        {
            TryMoveNPC();
        }

        private void Update()
        {
            CheckTurningDistance();
        }

        // Verifica se o NPC atingiu a distância de virada.
        private void CheckTurningDistance()
        {
            if (isIdle) return;

            if ((Mathf.Abs(rb.position.x - startingPosition.x) + Mathf.Abs(rb.position.y - startingPosition.y) > currentTurnDistance))
            {
                currentTurnDistance = UnityEngine.Random.Range(data.stoppingDistance.min, data.stoppingDistance.max);
                direction *= -1;
                startingPosition = rb.position;
                isIdle = true;
                StartCoroutine(Idle());
            }
        }

        // Tenta mover o NPC caso não esteja Idle.
        private void TryMoveNPC()
        {
            if (isIdle) return;

            MoveNPC();
        }

        // Classe abstrata que deve ser implementada nas classes filhas para movimentar o NPC.
        public abstract void MoveNPC();

        // Coroutine que faz o NPC ficar parado por um tempo aleatório.
        private IEnumerator Idle()
        {
            // Espera por um tempo aleatório entre min e max.
            yield return new WaitForSeconds(currentIdleTime);
            currentIdleTime = UnityEngine.Random.Range(data.idleTime.min, data.idleTime.max);
            isIdle = false;
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(startingPosition, currentTurnDistance);
        }
    }
}