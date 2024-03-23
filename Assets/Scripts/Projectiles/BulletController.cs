using Player;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Projectile
{
    public class BulletController : MonoBehaviour
    {
        [SerializeField] private ProjectileData projectileData;
        private float timer = 0f;

        private void FixedUpdate()
        {
            // Mover o projetil até o tempo de despawn, então retornar para a pool.
            timer += Time.fixedDeltaTime;
            transform.Translate(Vector2.up * projectileData.speed * Time.fixedDeltaTime);
            if (timer >= projectileData.despawnTime)
            {
                DestroyBullet();
            }
        }

        // Retorna o projetil para a pool.
        private void DestroyBullet()
        {
            timer = 0f;
            PlayerController.instance.poolSpawner.ReturnToPool("Bullet", gameObject);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            // Se o projetil colidir com um inimigo, causar dano e retornar para a pool.
            if (other.gameObject.CompareTag("NPC"))
            {
                ShowNPCText();
            }
            else if (other.gameObject.CompareTag("Damagable"))
            {
                Destroy(other.gameObject);
            }
            else if(other.gameObject.CompareTag("Push"))
            {
                other.gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * projectileData.pushForce, ForceMode2D.Impulse);
            }
            else if(other.gameObject.CompareTag("Scalable"))
            {
                other.gameObject.transform.localScale += new Vector3(0.5f, 0.5f, 0.5f);
            }
        }

        private void ShowNPCText()
        {
            Debug.Log("Ai, para :(");
        }
    }
}