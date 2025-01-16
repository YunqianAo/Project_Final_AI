using UnityEngine;

public class DestroyOnCollision : MonoBehaviour
{
    [SerializeField]
    private string targetTag = "Enemy"; // Tag que debe tener el otro objeto para activar la destrucción

    private void OnCollisionEnter(Collision collision)
    {
        // Comprueba si el objeto que colisionó tiene el tag especificado
        if (collision.gameObject.CompareTag(targetTag))
        {
            // Destruye ambos objetos
            Destroy(collision.gameObject); // Destruye el objeto que colisionó
            Destroy(gameObject); // Destruye el objeto que tiene este script
        }
    }
}