using TMPro;
using UnityEngine;

public class Key : MonoBehaviour
{

    public bool selected = false;
    public bool active = false;

    TMP_Text text;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text = gameObject.GetComponentInChildren<TMP_Text>();


    }

    // Update is called once per frame
    void Update()
    {
        if (selected)
        {
            text.color = Color.blue;
        }
        else
        {
            text.color = Color.white;
        }
    }
}
