using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 3f;
    public float chaseDistance = 10f; 
    
    private Transform player;
    private Rigidbody rb;
    private Animator animator;

    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");

        if (playerObj != null)
        {
            player = playerObj.transform;
        }

        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;    
        animator = GetComponentInChildren<Animator>();
    }

    void FixedUpdate() 
    {
        if (player != null)
        {
            float distance = Vector3.Distance(transform.position, player.position);

            if (distance <= chaseDistance)
            {
                Vector3 targetPosition = new Vector3(player.position.x, transform.position.y, player.position.z);
                Vector3 direction = (targetPosition - transform.position).normalized;

                if (direction != Vector3.zero)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(direction);
                    rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, 5f * Time.fixedDeltaTime));

                    Vector3 moveVelocity = direction * speed;
                    rb.velocity = new Vector3(moveVelocity.x, rb.velocity.y, moveVelocity.z);

                    if (animator != null)
                    {
                        animator.SetFloat("speed", speed);
                    }
                }
            }
            else
            {
                rb.velocity = new Vector3(0, rb.velocity.y, 0);

                if (animator != null)
                {
                    animator.SetFloat("speed", 0f);
                }
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Si toca al jugador, lo destruye (muere)
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(collision.gameObject);
        }
    }
}