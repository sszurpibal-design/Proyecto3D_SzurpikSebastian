using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyController : MonoBehaviour
{
    public float speed = 3f;
    public float chaseDistance = 10f; 
    
    private Transform player;
    private Rigidbody rb;

    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");

        player = playerObj.transform;

        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;    }

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
                }
            }
            else
            {
                rb.velocity = new Vector3(0, rb.velocity.y, 0);
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
    }
}