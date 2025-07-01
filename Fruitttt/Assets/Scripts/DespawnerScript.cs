using UnityEngine;

public class DespawnerScript : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        // Destroy any object that enters this trigger zone
        // We'll refine this later if we need to prevent destroying certain things
        Destroy(other.gameObject);
        Debug.Log("Despawned: " + other.gameObject.name); // For testing
    }
}