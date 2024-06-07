using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Weapon_Player : MonoBehaviour
{
   
    public bool canFire = true;
    public bool autoFire = false;

    public float TimeBtwFire = 20f; 
    public float bulletForce = 20f; 
    private float timeBtwFire = 0f; 
    public float rotationSpeed = 100f;
    
    public GameObject bulletPrefab;
    public Transform firePoint;
    public GameObject fireEffect;
    public GameObject muzzle;
    public AudioClip fireSound;
    GameManager gameManager;
    Vector3 gunRotate;


    void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsGamePaused())
        {
            return;
        }
        else
        {
            if (canFire)
            {
                if (gameManager.mobileGame)
                {
                    joystickRotateGun(gunRotate);
                }
                else
                {
                    RotateGun();
                    timeBtwFire -= Time.deltaTime;
                    if (autoFire)
                    {
                        if (Input.GetMouseButton(0) && timeBtwFire <= 0)
                        {
                            FireBullet();
                            timeBtwFire = TimeBtwFire;
                        }
                    }
                    else
                    {
                        if (Input.GetMouseButtonDown(0) && timeBtwFire <= 0)
                        {
                            FireBullet();
                        }
                    }
                }
            }
        }
    }

    public void OnRotate(InputAction.CallbackContext context)
    {
        gunRotate = context.ReadValue<Vector2>();
        joystickRotateGun(gunRotate);
    }

    public void OnFireButton(InputAction.CallbackContext context)
    {
        if (IsGamePaused() || !context.performed)
        {
            return;
        }
        else
        {
            if (context.performed)
            {
                FireBullet();
            }
        }
    }


    public void joystickRotateGun(Vector2 rotateInput)
    {
        if (rotateInput != Vector2.zero)
        {
            float angle = Mathf.Atan2(rotateInput.y, rotateInput.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            if (transform.eulerAngles.z > 90 && transform.eulerAngles.z < 270)
            {
                transform.localScale = new Vector3(1, -1, 1);
            }
            else
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }

    public void RotateGun()
    {
        if (canFire && SceneManager.GetActiveScene().name != "Menu_Main")
        {
            //c1
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); // lấy vị trí chuột
            Vector2 lookDir = mousePos - transform.position; // lấy vị trí chuột trừ vị trí súng
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg; // tính góc giữa 2 điểm (vị trí chuột và vị trí súng) 

            Quaternion rotation = Quaternion.Euler(0, 0, angle); // tạo ra 1 quaternion với góc vừa tính. quaternion là 1 kiểu dữ liệu dùng để lưu trữ góc quay
            transform.rotation = rotation; // xoay súng theo góc vừa tính 

            if (transform.eulerAngles.z > 90 && transform.eulerAngles.z < 270)
            {
                transform.localScale = new Vector3(1, -1, 0);
            }
            else
            {
                transform.localScale = new Vector3(1, 1, 0);
            }
        }

    }


    public void FireBullet()
    {
        if (canFire && SceneManager.GetActiveScene().name != "Menu_Main")
        {
            timeBtwFire = TimeBtwFire;
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation); // tạo ra 1 viên đạn

            Instantiate(fireEffect, firePoint.position, firePoint.rotation); // tạo hiệu ứng bắn
            Instantiate(muzzle, firePoint.position, firePoint.rotation); // tạo hiệu ứng súng
            AudioSource.PlayClipAtPoint(fireSound, Camera.main.transform.position); // phát âm thanh bắn

            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>(); // lấy rigidbody của viên đạn
            rb.AddForce(firePoint.right * bulletForce, ForceMode2D.Impulse); // thêm lực cho viên đạn

        }
    }
    bool IsGamePaused()
    {
        if (gameManager != null)
        {
            return gameManager.isPaused;
        }
        return false;
    }

}
