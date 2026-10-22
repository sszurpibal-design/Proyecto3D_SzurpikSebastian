using UnityEngine;

public class MoverPersonaje : MonoBehaviour
{
    public float velocidad = 5f;
    private Rigidbody rb;

    void Start()
    {
        // Cogemos el componente Rigidbody de nuestra cápsula al empezar
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Leemos las teclas de movimiento
        float moverX = Input.GetAxis("Horizontal");
        float moverZ = Input.GetAxis("Vertical");

        // Creamos la dirección del movimiento respetando la física de la gravedad en Y
        Vector3 movimiento = new Vector3(moverX, 0f, moverZ) * velocidad;
        rb.velocity = new Vector3(movimiento.x, rb.velocity.y, movimiento.z);
    }
}   