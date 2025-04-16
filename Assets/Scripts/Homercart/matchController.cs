using UnityEngine;
using UnityEngine.InputSystem;


public class matchController : MonoBehaviour
{
    [SerializeField] public car_controller[] cars;
    
    

         public void setAllGamepads()
    {
        for( int i = 0; i < cars.Length; i++) {
            if (Gamepad.all.Count >i)
            {
                cars[i].gamepad = Gamepad.all[i];
            }
        }
}


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        setAllGamepads();
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
