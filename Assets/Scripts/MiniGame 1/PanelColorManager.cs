using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PanelColorManager : MonoBehaviour
{
    public List<GameObject> cubeGreens;
    public List<GameObject> cubeReds;
    public List<GameObject> cubeYellows;
    public List<GameObject> cubeBlacks;

    public Image colorIndicatorUI;
    public float intervalSeconds = 2.5f;

    private class Panel
    {
        public string colorName;
        public Collider collider;
        public Renderer renderer;
    }

    private Dictionary<string, List<Panel>> panelsByColor = new Dictionary<string, List<Panel>>();
    private string[] colorKeys = { "green", "red", "yellow", "black" };
    private int previousIndex = -1;

    void Start()
    {
        AddPanels(cubeGreens, "green");
        AddPanels(cubeReds, "red");
        AddPanels(cubeYellows, "yellow");
        AddPanels(cubeBlacks, "black");

        StartCoroutine(PanelLoop());
    }

    private void AddPanels(List<GameObject> sourceList, string color)
    {
        if (!panelsByColor.ContainsKey(color))
            panelsByColor[color] = new List<Panel>();

        foreach (var obj in sourceList)
        {
            if (obj == null)
            {
                Debug.LogWarning($"Null GameObject in {color} list");
                continue;
            }

            var col = obj.GetComponent<Collider>();
            var rend = obj.GetComponent<Renderer>();

            if (col == null || rend == null)
            {
                Debug.LogWarning($"Missing Collider or Renderer on {obj.name} ({color})");
                continue;
            }

            var panel = new Panel
            {
                colorName = color,
                collider = col,
                renderer = rend
            };

            panel.renderer.material = new Material(panel.renderer.material); // isolate
            panelsByColor[color].Add(panel);
        }
    }

    IEnumerator PanelLoop()
    {
        while (true)
        {
            
            foreach (var panelGroup in panelsByColor.Values)
            {
                foreach (var panel in panelGroup)
                {
                    panel.collider.enabled = true;
                    panel.renderer.material.color = GetColorByName(panel.colorName);
                }
            }

            
            int index;
            do
            {
                index = Random.Range(0, colorKeys.Length);
            } while (index == previousIndex);
            previousIndex = index;

            string selectedColor = colorKeys[index];
            var selectedGroup = panelsByColor[selectedColor];

            foreach (var panel in selectedGroup)
            {
                panel.collider.enabled = false;
                panel.renderer.material.color = GetColorByName(panel.colorName) * 1.5f;
            }

            if (colorIndicatorUI != null)
                colorIndicatorUI.color = GetColorByName(selectedColor);

            yield return new WaitForSeconds(intervalSeconds);
        }
    }

    private Color GetColorByName(string name)
    {
        switch (name.ToLower())
        {
            case "green": return Color.green;
            case "red": return Color.red;
            case "yellow": return Color.yellow;
            case "black": return Color.black;
            default: return Color.white;
        }
    }
}
