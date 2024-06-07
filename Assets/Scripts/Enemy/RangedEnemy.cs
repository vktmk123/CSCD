using UnityEngine;

public class RangedEnemy : MonoBehaviour
{

    [SerializeField] private float rangex;// tầm nhìn của enemy
    [SerializeField] private float rangey;

    [Header("Collider Parameters")]
    [SerializeField] private float colliderDistance;
    [SerializeField] private float colliderOffset;
    [SerializeField] private BoxCollider2D boxCollider;

    [Header("Player Layer")]
    [SerializeField] private LayerMask playerLayer;// lớp của player để xác định xem player có trong tầm nhìn của enemy không  

    private void Update()
    {
        // if (PlayerInSight())
        // {
        //     // Debug.Log("Player in sight");
        //     // enemy isIdling = true;
        // }    
    }

    public bool PlayerInSight()// kiểm tra xem player có trong tầm nhìn của enemy không
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * rangex * transform.localScale.x * 
                        colliderDistance + transform.up * rangey * transform.localScale.y * colliderOffset,
            new Vector3(boxCollider.bounds.size.x * rangex, boxCollider.bounds.size.y * rangey, boxCollider.bounds.size.z),
            0, Vector2.left, 0, playerLayer);
        return hit.collider != null;
    }
    private void OnDrawGizmos() // vẽ hình chữ nhật để xác định tầm nhìn của enemy
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * rangex * transform.localScale.x * 
                            colliderDistance + transform.up * rangey * transform.localScale.y * colliderOffset,
            new Vector3(
            boxCollider.bounds.size.x * rangex, 
            boxCollider.bounds.size.y * rangey, 
            boxCollider.bounds.size.z));
    }
}