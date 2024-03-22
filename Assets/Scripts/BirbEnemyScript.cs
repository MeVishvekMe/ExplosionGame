using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirbEnemyScript : MonoBehaviour {
    public GameObject bullet;
    private Transform playerTransform;
    private Rigidbody2D _rb;

    public Transform firePoint;

    private float time = 0f;

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
            ParticleSystem ps = Instantiate(destroyPS, transform.position, Quaternion.identity);
            ps.Play();
            Destroy(ps, 2f);
            PlayerMechanics.score++;
            enemyDestroyPlayer.playAudio();
            Destroy(other.gameObject);
            PlayerMechanics.canDash = true;
            Destroy(gameObject);
        }
    }

    private void Update() {
        time += Time.deltaTime;
        float angle = Mathf.Atan2((transform.position.y - playerTransform.position.y), (transform.position.x - playerTransform.position.x)) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle + 90f);
        _rb.velocity = transform.up * (moveSpeed * Time.deltaTime);
        if (time >= 2f) {
            GameObject bullet1 = Instantiate(bullet, firePoint.transform.position, firePoint.transform.rotation);
            bullet1.GetComponent<Rigidbody2D>().AddForce(firePoint.transform.up * 2000f);
            Destroy(bullet1, 5f);
            time = 0f;
        }
        
    }
}
