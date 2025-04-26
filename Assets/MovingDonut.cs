using UnityEngine;

public class MovingDonut : MonoBehaviour
{
    [SerializeField] private float speed = 4.5f;
    [SerializeField] private float minX = -6.7f;
    [SerializeField] private float maxX = 81f;

    private bool movingRight = true;

    void Update()
    {
        float moveStep = speed * Time.deltaTime;
        Vector3 pos = transform.position;

        if (movingRight)
        {
            pos.x += moveStep;
            if (pos.x >= maxX)
            {
                pos.x = maxX;
                movingRight = false;
            }
        }
        else
        {
            pos.x -= moveStep;
            if (pos.x <= minX)
            {
                pos.x = minX;
                movingRight = true;
            }
        }

        transform.position = pos;
    }
}
