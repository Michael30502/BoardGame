using UnityEngine;

public class PlayerDeathOnTagCollision : MonoBehaviour
{
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("DeathBarrier"))
        {
            
            Destroy(gameObject);
        }
    }
}
