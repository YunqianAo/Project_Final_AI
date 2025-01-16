using UnityEngine;

public class DestroyOnCollision : MonoBehaviour
{
    [SerializeField]
    private string targetTag = "Enemy"; // Tag que debe tener el otro objeto para activar la destrucci�n

    private void OnCollisionEnter(Collision collision)
    {
        // Comprueba si el objeto que colision� tiene el tag especificado
        if (collision.gameObject.CompareTag(targetTag))
        {
            // Destruye ambos objetos
            Destroy(collision.gameObject); // Destruye el objeto que colision�
            Destroy(gameObject); // Destruye el objeto que tiene este script
        }
    }
}