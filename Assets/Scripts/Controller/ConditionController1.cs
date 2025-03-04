using UnityEngine;

public class PlayerDeletionOnCollision : MonoBehaviour
{
    [SerializeField] private GameObject[] players; // Serialized array for players

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // Check if the collided object has the "DeathBarrier" tag
        if (hit.gameObject.CompareTag("DeathBarrier"))
        {
            Debug.Log($"Collision with DeathBarrier detected by: {hit.gameObject.name}");

            // Check if the collided object is one of the players
            for (int i = 0; i < players.Length; i++)
            {
                if (hit.gameObject == players[i])
                {
                    Debug.Log($"{players[i].name} has been deleted!");
                    Destroy(players[i]);
                    players[i] = null;  // Optional: Remove reference to deleted player
                    enabled = false;     // Disable script after deletion to save performance
                    break;
                }
            }
        }
    }
}
