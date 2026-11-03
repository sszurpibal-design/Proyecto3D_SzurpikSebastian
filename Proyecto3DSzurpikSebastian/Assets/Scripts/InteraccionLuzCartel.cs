using UnityEngine;
using TMPro;

public class InteraccionLuzCartel : MonoBehaviour {
    public GameObject mensaje;
    public Light luzInteractiva;
    private TextMeshProUGUI componenteTexto;
    private bool enRango = false;

    void Start() {
        if (luzInteractiva != null) {
            luzInteractiva.enabled = false;
        }
        
        if (mensaje != null) {
            componenteTexto = mensaje.GetComponent<TextMeshProUGUI>();
        }
    }

    void Update() {
        if (enRango && mensaje != null && componenteTexto != null) {
            if (luzInteractiva.enabled) {
                componenteTexto.text = "J para apagar la luz";
            } else {
                componenteTexto.text = "J para encender";
            }

            if (Input.GetKeyDown(KeyCode.J)) {
                luzInteractiva.enabled = !luzInteractiva.enabled;
            }
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            enRango = true;
            if (mensaje != null) {
                mensaje.SetActive(true);
            }
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            enRango = false;
            if (mensaje != null) {
                mensaje.SetActive(false);
            }
        }
    }
}