using TMPro;
using UnityEngine;

public class Key : MonoBehaviour
{

    public bool selected = false;
    public bool active = false;
    [SerializeField] Material originalMaterial;
    [SerializeField] Material blueMaterial;

    TMP_Text text;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        if (selected)
        {
            if (gameObject.GetComponentInChildren<Renderer>().material != blueMaterial)
            gameObject.GetComponentInChildren<Renderer>().material = blueMaterial;
        }
        else if (gameObject.GetComponentInChildren<Renderer>().material!= originalMaterial)
        {
            gameObject.GetComponentInChildren<Renderer>().material = originalMaterial;

        }
    }
}
