using UnityEngine;
using UnityEngine.Rendering;

public class Door : MonoBehaviour
{

   public Key key = null;
    public bool selected = false;
    [SerializeField] Material whiteMaterial;
    [SerializeField] Material blueMaterial;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (selected)
        {
            gameObject.GetComponentInChildren<Renderer>().material = blueMaterial;
        }
        else {
            gameObject.GetComponentInChildren<Renderer>().material = whiteMaterial;

        }

    }
}
