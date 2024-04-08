using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PlayerMechanics : MonoBehaviour {
    private Rigidbody2D rb;
    private Vector2 inputVector;
    private Vector3 mousePos;
    private Vector3 worldPos;
    public Camera mainCam;
    public float moveSpeed;
    public Animator _animator;
    public float dashSpeed;

    public AudioSource source;
    public AudioClip dashClip;

    public static bool isMoving = false;
    
    public static bool canDash = true;
    public ParticleSystem dashParticleSystem;
    public GameObject dashUI;

    public TextMeshProUGUI scoreText;

    public static int score = 0;
    public static float health = 100;
    public Slider healthSlider;

    

    private float time = 0f;
    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    public void Update() {
        healthSlider.value = health;
        inputVector.x = Input.GetAxis("Horizontal");
        inputVector.y = Input.GetAxis("Vertical");
        transform.position += new Vector3(inputVector.x * moveSpeed * Time.deltaTime, inputVector.y * moveSpeed * Time.deltaTime, 0f);
        mousePos = Input.mousePosition;
        worldPos = mainCam.ScreenToWorldPoint(mousePos);
        float angle = Mathf.Atan2((transform.position.y - worldPos.y), (transform.position.x - worldPos.x)) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle + 90f);
        
        if (inputVector.x == 0f && inputVector.y == 0f) {
            isMoving = false;
        }
        else {
            isMoving = true;
        }

        if (canDash) {
            dashUI.SetActive(true);
        }
        else {
            dashUI.SetActive(false);
        }
        
        if (Input.GetKeyDown(KeyCode.Mouse1) && canDash) {
            StartCoroutine(playerDash());
        }
        if (Input.GetKeyDown(KeyCode.F)) {
            _animator.Play("SwordSlashAnimation");
        }
        scoreText.text = "Score : " + score;

        if (health <= 0) {
            if (score > DBManager.newHighScore) {
                DBManager.shouldChange = true;
                DBManager.newHighScore = score;
                SceneManager.LoadScene(1);
            }
        }

    }

    private void OnCollisionStay2D(Collision2D other) {
        if (other.collider.CompareTag("Enemy")) {
            health -= 5f * Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("EnemyBullet")) {
            health -= 5;
            Destroy(other.gameObject);
        }
    }


    IEnumerator playerDash() {
        rb.AddForce(transform.up * dashSpeed, ForceMode2D.Impulse);
        dashParticleSystem.gameObject.transform.position = transform.position;
        dashParticleSystem.gameObject.transform.localRotation = Quaternion.Euler(transform.eulerAngles.z + 90f, -90f, 90f);
        source.clip = dashClip;
        source.Play();
        dashParticleSystem.Play();
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
        yield return new WaitForSeconds(0.2f);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        dashParticleSystem.Stop();
        canDash = false;
    }
}
