using UnityEngine;

public class BigBigScript : MonoBehaviour {
    
    public GameObject rocket;
    private Transform playerTransform;
    private Rigidbody2D _rb;

    public Transform firePoint;

    private float time = 0f;
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
            moveSpeed += 1000;
            bulletHitCount++;
        }
    }

    private void Update() {
        time += Time.deltaTime;
        float angle = Mathf.Atan2((transform.position.y - playerTransform.position.y), (transform.position.x - playerTransform.position.x)) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle + 90f);
        _rb.velocity = transform.up * (moveSpeed * Time.deltaTime);
        if (time >= 2f) {
            GameObject bullet1 = Instantiate(rocket, firePoint.position, firePoint.rotation);
            bullet1.GetComponent<Rigidbody2D>().AddForce(firePoint.transform.up * 1500f);
            Destroy(bullet1, 5f);
            time = 0f;
        }
        
    }
}
