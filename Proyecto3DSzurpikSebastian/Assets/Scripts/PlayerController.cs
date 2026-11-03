using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float velocidad = 5f;
    public float jumpForce = 5f;
    public float sensibilidadRaton = 1f; 
    
    private Animator ani;
    private Rigidbody rb;
    private Collider col;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        if (rb == null) {
            rb = gameObject.AddComponent<Rigidbody>();
        }
        
        rb.freezeRotation = true; 
        ani = GetComponent<Animator>();
        col = GetComponent<Collider>();
    }

    void Update()
    {
        float distanciaSuelo = col.bounds.extents.y + 0.1f;
        isGrounded = Physics.Raycast(col.bounds.center, Vector3.down, distanciaSuelo);

        float mouseX = Input.GetAxis("Mouse X") * sensibilidadRaton;
        transform.Rotate(0, mouseX, 0);

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = transform.right * moveHorizontal + transform.forward * moveVertical;
        
        rb.velocity = new Vector3(movement.x * velocidad, rb.velocity.y, movement.z * velocidad);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }

        Vector3 velHorizontal = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        
        if (ani != null) {
            ani.SetFloat("speed", velHorizontal.magnitude);
            ani.SetBool("enSuelo", isGrounded);
        }
    }
}