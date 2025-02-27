using UnityEngine;

public class Camera : MonoBehaviour
{
    internal static GameObject main;
    public Transform player;
    public TurnController turnController;
    void Update()
    {
        if (player == null) return;
  
        
        
        transform.position = player.position + new Vector3(-1.5f, 4, 2);
        transform.rotation = Quaternion.Euler(40, -215, 0);
    }
}
