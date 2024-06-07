using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float MoveSpeed = 1f;
    public float idleDuration = 1f; // Thời gian nghỉ tại mỗi điểm đầu cuối

    [Header("Components")]
    private Rigidbody2D myRigidbody2D;
    private Animator myAnimator;
    private bool movingLeft = true; // Ban đầu, enemy di chuyển về bên trái
    private bool isIdling = false; // Kiểm tra xem enemy đang nghỉ tại điểm cuối hay không

    [Header("Edge Detection")]
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;
    [SerializeField] private RangedEnemy rangedEnemy;
    [Header("Health")]
    public bool isDead = false;
    public Weapon_Enemy weapon;

    void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!isDead)
        {
            if (!rangedEnemy.PlayerInSight()) // Kiểm tra xem người chơi có trong tầm nhìn của enemy không
            {
                if (!isIdling) // Kiểm tra xem enemy có đang nghỉ tại điểm cuối hay không
                {
                    if (movingLeft)
                    {
                        MoveLeft();
                        if (transform.position.x <= leftEdge.position.x)
                        {
                            StartCoroutine(IdleAtEndpoint()); // Tạm dừng trước khi di chuyển lại
                            movingLeft = false; // Đổi hướng di chuyển sang bên phải
                        }
                    }
                    else
                    {
                        MoveRight();
                        if (transform.position.x >= rightEdge.position.x)
                        {
                            StartCoroutine(IdleAtEndpoint()); // Tạm dừng trước khi di chuyển lại
                            movingLeft = true; // Đổi hướng di chuyển sang bên trái
                        }
                    }
                    UpdateAnimator();
                }
            }
            else
            {
                StartCoroutine(IdleAtEndpoint()); // Tạm dừng trước khi di chuyển lại
                isIdling = true; // Người chơi trong tầm nhìn của enemy, vô hiệu hóa hành động di chuyển
                myAnimator.SetBool("IsMoving", false);
            }

        }

    }


    void MoveLeft()
    {
        if (!isDead)
        {
            myRigidbody2D.velocity = new Vector2(-MoveSpeed, myRigidbody2D.velocity.y);
            FlipEnemyFacing(false);
        }

    }

    void MoveRight()
    {
        if (!isDead)
        {
            myRigidbody2D.velocity = new Vector2(MoveSpeed, myRigidbody2D.velocity.y);
            FlipEnemyFacing(true);
        }

    }

    IEnumerator IdleAtEndpoint()
    {
        isIdling = true; // Bắt đầu nghỉ tại điểm cuối
        myRigidbody2D.velocity = Vector2.zero;
        yield return new WaitForSeconds(idleDuration);
        isIdling = false; // Kết thúc nghỉ tại điểm cuối
    }

    void UpdateAnimator()
    {
        myAnimator.SetBool("IsMoving", Mathf.Abs(myRigidbody2D.velocity.x) > 0);
    }

    void FlipEnemyFacing(bool faceRight) // Đổi hướng của enemy
    {
        float xScale = Mathf.Abs(transform.localScale.x) * (faceRight ? 1 : -1);
        transform.localScale = new Vector3(xScale, transform.localScale.y, transform.localScale.z);
    }

    public void Die()
    {
        isDead = true;
        myAnimator.SetBool("IsDie", true);
        
        Weapon_Enemy weaponEnemy = weapon.GetComponent<Weapon_Enemy>();

        if (weaponEnemy != null)
        {
            weaponEnemy.enabled = false;
        }

    }


}
