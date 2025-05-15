using TMPro;
using UnityEngine;

[ExecuteAlways]
[RequireComponent(typeof(TMP_Text))]
public class CurvedText : MonoBehaviour
{
    public float curveStrength = 0.005f; 

    private TMP_Text tmpText;

    void Awake()
    {
        tmpText = GetComponent<TMP_Text>();
    }

    void LateUpdate()
    {
        tmpText.ForceMeshUpdate();
        var mesh = tmpText.mesh;
        var vertices = mesh.vertices;

        var bounds = tmpText.textBounds;
        var center = bounds.center;

        for (int i = 0; i < vertices.Length; i++)
        {
            Vector3 v = vertices[i];
            float x = v.x - center.x;

            
            v.y -= Mathf.Pow(x * curveStrength, 2);

            vertices[i] = v;
        }

        mesh.vertices = vertices;
        tmpText.canvasRenderer.SetMesh(mesh);
    }
}
