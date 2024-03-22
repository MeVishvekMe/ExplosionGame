using System;
using System.Collections;
using Cinemachine;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Weapon : MonoBehaviour {
    private SpriteRenderer _spriteRenderer;
    private String currentWeapon = "pistol";

    public Sprite pistolSprite;
    public Sprite shotgunSprite;
    public Sprite pistolAmmoSprite;
    public Sprite shotgunShellSprite;

    public GameObject bulletPrefab;
    public GameObject firePoint;
    public Image uiImage;
    
    private bool fire = true;
    public float bulletSpeed;
    private Animator _animator;
    private Quaternion originalRotationFirePoint;
    private bool canSwitch = true;
    private int pistolAmmo = 6;
    private int shotgunAmmo = 10;
    private bool isReloading = false;

    public CameraManager cameraManager;
    private float cameraShakeAmplitude;
    private float cameraShakeFrequency;

    public AudioSource source;
    public AudioClip pistolClip;
    public AudioClip shotgunClip;

    public TextMeshProUGUI ammoText;
    private void Start() {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        originalRotationFirePoint = Quaternion.Euler(0f ,0f ,0f);
        ammoText.text = "" + pistolAmmo;
    }

    private void Update() {
        if (canSwitch) {
            if (Input.GetKeyDown(KeyCode.Alpha1)) {
                currentWeapon = "pistol";
                isReloading = true;
                fire = false;
                canSwitch = false;
                ammoText.text = "" + pistolAmmo;
                uiImage.sprite = pistolAmmoSprite;
                weaponControl();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2)) {
                currentWeapon = "shotgun";
                isReloading = true;
                fire = false;
                canSwitch = false;
                ammoText.text = "" + shotgunAmmo;
                uiImage.sprite = shotgunShellSprite;
                weaponControl();
            }
        }
        
        if (Input.GetKeyDown(KeyCode.R) && isReloading == false) {
            fire = false;
            canSwitch = false;
            isReloading = true;
            reload();
        }

        
        if (fire) {
            if (currentWeapon == "pistol") {
                if (Input.GetKeyDown(KeyCode.Mouse0) && pistolAmmo > 0) {
                    firePoint.transform.localRotation = Quaternion.Euler(0f ,0f ,0f);
                    GameObject bullet = Instantiate(bulletPrefab, firePoint.transform.position, firePoint.transform.rotation);
                    bullet.GetComponent<Rigidbody2D>().AddForce(firePoint.transform.up * bulletSpeed);
                    source.clip = pistolClip;
                    source.Play();
                    cameraShakeAmplitude = 10f;
                    cameraShakeFrequency = 2f;
                    StartCoroutine(cameraShake());
                    Destroy(bullet, 5f);
                    pistolAmmo--;
                    ammoText.text = "" + pistolAmmo;
                }
            }
            else if (currentWeapon == "shotgun") {
                if (Input.GetKeyDown(KeyCode.Mouse0) && shotgunAmmo > 0) {
                    int count = 0;
                    source.clip = shotgunClip;
                    source.Play();
                    while (count < 5) {
                        float randomAngle = Random.Range(-15f, 15f);
                        firePoint.transform.localRotation = Quaternion.Euler(0f ,0f ,randomAngle);
                        GameObject bullet1 = Instantiate(bulletPrefab, firePoint.transform.position, firePoint.transform.rotation);
                        bullet1.GetComponent<Rigidbody2D>().AddForce(firePoint.transform.up * bulletSpeed);
                        cameraShakeAmplitude = 10f;
                        cameraShakeFrequency = 4f;
                        StartCoroutine(cameraShake());
                        Destroy(bullet1, 0.25f);
                        count++;
                    }
                    shotgunAmmo -= 5;
                    ammoText.text = "" + shotgunAmmo;
                }
            }
        }
    }

    private void reload() {
        StopCoroutine(reloadAnimation());
        StartCoroutine(reloadAnimation());
    }

    IEnumerator cameraShake() {
        StopCoroutine(cameraShake());
        cameraManager.idleCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;
        cameraManager.idleCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 0;
        cameraManager.movingCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;
        cameraManager.movingCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 0;
        cameraManager.idleCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = cameraShakeAmplitude;
        cameraManager.idleCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain =
            cameraShakeFrequency;
        cameraManager.movingCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = cameraShakeAmplitude;
        cameraManager.movingCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain =
            cameraShakeFrequency;
        yield return new WaitForSeconds(0.1f);
        cameraManager.idleCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;
        cameraManager.idleCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 0;
        cameraManager.movingCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;
        cameraManager.movingCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 0;
        StopCoroutine(cameraShake());
    }

    IEnumerator reloadAnimation() {
        if (currentWeapon == "shotgun") {
            _animator.speed = 1.5f;
            _animator.Play("ReloadAnimation");
            yield return new WaitForSeconds(2f);
            shotgunAmmo = 10;
            ammoText.text = "" + shotgunAmmo;
        }
        else if (currentWeapon == "pistol") {
            _animator.speed = 1f;
            _animator.Play("ReloadAnimation");
            yield return new WaitForSeconds(3f);
            pistolAmmo = 6;
            ammoText.text = "" + pistolAmmo;
        }
        
        _animator.speed = 1f;
        fire = true;
        canSwitch = true;
        isReloading = false;
    }

    private void weaponControl() {
        StopCoroutine(equipAnimation());
        StartCoroutine(equipAnimation());
    }

    IEnumerator equipAnimation() {
        _animator.Play("EquipAnimation");
        yield return new WaitForSeconds(0.2f);
        if (currentWeapon == "pistol") {
            _spriteRenderer.sprite = pistolSprite;
        }
        else if (currentWeapon == "shotgun") {
            _spriteRenderer.sprite = shotgunSprite;
        }
        yield return new WaitForSeconds(1f);
        fire = true;
        canSwitch = true;
        isReloading = false;
    }
}
