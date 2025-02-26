using UnityEngine;
using System.Collections;

public class DollarSpawn : MonoBehaviour
{
    public float floatSpeed = 1f; // Speed of floating upwards
    public float lifetime = 2f;   // Time before destroying the object

    private void Start()
    {
        StartCoroutine(FloatUpwards());
    }

    private IEnumerator FloatUpwards()
    {
        float elapsedTime = 0f;

        while (elapsedTime < lifetime)
        {
            transform.position += new Vector3(0, floatSpeed * Time.deltaTime, 0);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }
}
