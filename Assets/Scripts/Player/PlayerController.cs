using Audio;
using Managers;
using Pooling;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float speed = 5f;
        [SerializeField] private float rotationSpeed = 1f;
        [SerializeField] private float bulletKnockbackForce = 20f;
        [SerializeField] private Transform weaponHolder;
        [SerializeField] private Transform bulletSpawnPoint;
        [SerializeField] private Transform bodyTransform;
        [SerializeField] private ParticleSystem muzzleFlash;
        [SerializeField] private AudioClip shootSound;

        public PoolSpawner poolSpawner;
        private Rigidbody2D rb;
        private Animator anim;
        private float horizontal => Input.GetAxisRaw("Horizontal");
        private float vertical => Input.GetAxisRaw("Vertical");

        private Vector3 mousePosition;
        private Vector3 lookDirection;
        private float angle;

        public static PlayerController instance;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
        }

        private void FixedUpdate()
        {
            MovePlayer();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                GameManager.instance.TogglePauseGame();
            }

            if (Time.timeScale == 0) return;

            SetCharacterLookDirection();
            RotateWeapon();

            if (Input.GetMouseButtonDown(0))
            {
                if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject()) return;
                Shoot();
                GetObjectNameWithCast();
            }
        }

        // Move o personagem na direção do input.
        private void MovePlayer()
        {
            Vector2 direction = rb.transform.up * vertical + rb.transform.right * horizontal;
            direction.Normalize();
            rb.transform.Translate(direction * speed * Time.fixedDeltaTime);
            
            anim.SetBool("IsWalking", vertical != 0f || horizontal != 0f);

        }

        // Atira um projetil e empurra o personagem para trás.
        private void Shoot()
        {
            // Instancia um projetil da pool e o ativa.
            poolSpawner.SpawnFromPool("Bullet", bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            muzzleFlash.Play();
            AudioManager.instance.PlayOneShot(shootSound);
            KnockbackPlayer();
        }

        // Empurra o personagem para trás quando atira na direção oposta a do mouse.
        private void KnockbackPlayer()
        {
            rb.AddForce(-lookDirection * bulletKnockbackForce, ForceMode2D.Impulse);
        }

        // Corpo do personagem rotaciona para a direção que anda.
        private void SetCharacterLookDirection()
        {
            Vector3 lookDirection = new Vector3(horizontal, vertical, 0);
            if (lookDirection != Vector3.zero)
            {
                float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
                Quaternion targetRotation = Quaternion.Euler(0, 0, angle - 90f);
                bodyTransform.rotation = Quaternion.Lerp(bodyTransform.rotation, Quaternion.Euler(0, 0, angle - 90f), rotationSpeed);
            }
        }

        // Raycast para pegar o nome do objeto que o jogador está olhando.
        private void GetObjectNameWithCast()
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null)
            {
                Debug.Log(hit.collider.gameObject.name);
            }

        }

        // Rotaciona a arma do personagem para a direção do mouse
        private void RotateWeapon()
        {
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            lookDirection = (mousePosition - weaponHolder.position).normalized;
            angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
            weaponHolder.rotation = Quaternion.Euler(0, 0, angle - 90f);
        }
    }
}