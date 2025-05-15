using UnityEngine;
using System.Collections;

public class PlayerWinOnCollision : MonoBehaviour
{
    [SerializeField] private PhysicsMaterial winBarrierMaterial;

    private bool hasWon = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (hasWon) return;

        foreach (ContactPoint contact in collision.contacts)
        {
            var hitMaterial = contact.otherCollider.sharedMaterial;
            var myMaterial = contact.thisCollider.sharedMaterial;

            if (hitMaterial == winBarrierMaterial || myMaterial == winBarrierMaterial)
            {
                hasWon = true;

                Debug.Log($"{gameObject.name} WINS!");
                GetComponent<FreeFallCharacter>()?.MarkAsWinner();

                StartCoroutine(DestroyAfterShortDelay());

                return;
            }
        }
    }
    //For multiple winners 
    private IEnumerator DestroyAfterShortDelay()
    {
        yield return new WaitForSeconds(0.3f); 
        gameObject.SetActive(false);
    }
}
