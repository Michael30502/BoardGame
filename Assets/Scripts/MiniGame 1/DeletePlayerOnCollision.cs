using UnityEngine;

public class PlayerDeathOnCollision : MonoBehaviour
{
    [SerializeField] private PhysicsMaterial deathBarrierMaterial;

   

    private void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            var hitMaterial = contact.otherCollider.sharedMaterial;
            var myMaterial = contact.thisCollider.sharedMaterial;

            if (hitMaterial == deathBarrierMaterial || myMaterial == deathBarrierMaterial)
            {
                Debug.Log("Du ramte et rør makker");
                Destroy(gameObject);
                return;
            }
        }
    }
}
