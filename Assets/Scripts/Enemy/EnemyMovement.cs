using System.Collections;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float MoveSpeed = 1f;
    [SerializeField] private RangedEnemy rangedEnemy;
    public Weapon_Enemy weapon;
    private Vector3 previousPosition;
    
    public float idleDuration = 1f;
    Rigidbody2D myRigidbody2D;
    Animator myAnimator;
    public bool isDead = false;
    private bool movingLeft = true;
    private bool isIdling = true;
    

    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        myRigidbody2D = GetComponent<Rigidbody2D>();
        // previousPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            if (!rangedEnemy.PlayerInSight())
            {
                if (!isIdling)
                {
                    if (movingLeft)
                    {
                        moveLeft();
                    }
                    else
                    {
                        moveRight();
                    }
                    UpdateAnimator();
                }
            }
            else
            {
                StartCoroutine(IdleAtEndpoint());
                isIdling = true;
                myAnimator.SetBool("IsMoving", false);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "changeDetectionPoint")
        {
            StartCoroutine(IdleAtEndpoint());
            movingLeft = !movingLeft;
        }
    }

    void UpdateAnimator()
    {
        myAnimator.SetBool("IsMoving", Mathf.Abs(myRigidbody2D.velocity.x) > 0);
    }

    void FlipEnemyFacing(bool faceRight)
    {
        Vector3 newScale = transform.localScale;
        newScale.x = Mathf.Abs(newScale.x) * (faceRight ? 1 : -1);
        transform.localScale = newScale;
    }

    void moveRight()
    {
        myRigidbody2D.velocity = new Vector2(MoveSpeed, myRigidbody2D.velocity.y);
        FlipEnemyFacing(true);
    }

    void moveLeft()
    {
        myRigidbody2D.velocity = new Vector2(-MoveSpeed, myRigidbody2D.velocity.y);
        FlipEnemyFacing(false);
    }

    IEnumerator IdleAtEndpoint()
    {
        isIdling = true;
        myAnimator.SetBool("IsMoving", false);
        yield return new WaitForSeconds(idleDuration);
        myAnimator.SetBool("IsMoving", true);
        isIdling = false;
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
