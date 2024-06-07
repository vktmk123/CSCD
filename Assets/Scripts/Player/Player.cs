// Player.cs

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public ShopSaveScriptable shopData;
    private int currentIndex = 0;
    private int selectedINdex = 0;
    public SpriteRenderer characterSR;
    public bool canMove = true;
    Rigidbody2D myRigidbody;

    Vector3 moveInput;
    BoxCollider2D myFeetCollider;
    Animator myAnimator;
    // settingGame settingGame;
    GameManager gameManager;
    public Weapon_Player weapon;
    // PlayerHealth playerHealth;

    public bool isDead = false;

    void Start()
    {
        selectedINdex = shopData.selectedIndex;
        currentIndex = selectedINdex;

        gameManager = FindObjectOfType<GameManager>();
        myRigidbody = GetComponent<Rigidbody2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();
        myAnimator = GetComponent<Animator>();
        // playerHealth = GetComponent<PlayerHealth>(); 
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
            if (!isDead && canMove)
            {
                FlipSprite();
                Run();
            }
            Debug.Log("move" + shopData.shopItems[currentIndex].playerLevel[shopData.shopItems[currentIndex].unlockedLevel].moveSpeed);
            Debug.Log("jump" + shopData.shopItems[currentIndex].playerLevel[shopData.shopItems[currentIndex].unlockedLevel].jumpSpeed);

        }
    }

    void FlipSprite()
    {
        if (!isDead && moveInput.x != 0)
        {
            if (moveInput.x > 0) { characterSR.transform.localScale = new Vector3(1, 1, 0); }
            else if (moveInput.x < 0) { characterSR.transform.localScale = new Vector3(-1, 1, 0); }
        }
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        if (IsGamePaused())
        {
            return;
        }
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (IsGamePaused())
        {
            return;
        }
        else
        {
            float valuejumpSpeed = shopData.shopItems[currentIndex].playerLevel[shopData.shopItems[currentIndex].unlockedLevel].jumpSpeed;

            var nameLayer = LayerMask.GetMask("Ground", "Enemy", "Building_1", "Trap", "Box", "Building_2");

            if (!isDead && context.performed && myFeetCollider.IsTouchingLayers(nameLayer))
            {
                myRigidbody.velocity += new Vector2(0f, valuejumpSpeed);// công thức là v = u + at (vận tốc = vận tốc ban đầu + gia tốc * thời gian) 
            }
        }

    }

    public void Run()
    {
        float valuemoveSpeed = shopData.shopItems[currentIndex].
        playerLevel[shopData.shopItems[currentIndex].unlockedLevel].moveSpeed; // lấy giá trị moveSpeed từ shopData

        if (canMove && SceneManager.GetActiveScene().name != "Menu_Main")
        {
            if (!isDead && (moveInput.x != 0 || moveInput.y != 0))
            {
                myAnimator.SetBool("IsMove", true);
            }
            else
            {
                myAnimator.SetBool("IsMove", false);
            }

            if (!isDead)
            {
                myRigidbody.velocity = new Vector2(moveInput.x * valuemoveSpeed, myRigidbody.velocity.y); 
            }
        }

    }

    public void Die()
    {
        isDead = true;
        myAnimator.SetBool("IsDie", true);

        Weapon_Player weaponPlayer = weapon.GetComponent<Weapon_Player>();
        if (weaponPlayer != null)
        {
            weaponPlayer.enabled = false;
        }

    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Menu_Main")
        {
            canMove = false;
        }
        else
        {
            canMove = true;
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
