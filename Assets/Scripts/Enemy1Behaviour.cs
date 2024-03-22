using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1Behaviour : MonoBehaviour {

    private Transform playerTransform;
    private Rigidbody2D _rb;

    private int bulletHitCount = 0;

    public float moveSpeed;
    public ParticleSystem destroyPS;

    private EnemyDestroyPlayer enemyDestroyPlayer;
    
    private void Start() {
        playerTransform = GameObject.FindWithTag("Player").transform;
        _rb = GetComponent<Rigidbody2D>();
        enemyDestroyPlayer = GameObject.Find("EnemyDestroyAudioPlayer").GetComponent<EnemyDestroyPlayer>();
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Bullet")) {
            if (bulletHitCount == 2) {
                ParticleSystem ps = Instantiate(destroyPS, transform.position, Quaternion.identity);
                ps.Play();
                Destroy(ps, 2f);
                PlayerMechanics.score++;
                enemyDestroyPlayer.playAudio();
                PlayerMechanics.canDash = true;
                Destroy(gameObject);
            }
            Destroy(other.gameObject);
            transform.localScale += new Vector3(0.5f, 0.5f, 0.5f);
            bulletHitCount++;
        }
    }

    private void Update() {
        float angle = Mathf.Atan2((transform.position.y - playerTransform.position.y), (transform.position.x - playerTransform.position.x)) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle + 90f);
        _rb.velocity = transform.up * (moveSpeed * Time.deltaTime);
    }
}
