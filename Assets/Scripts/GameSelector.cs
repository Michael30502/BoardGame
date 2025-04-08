using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameSelector : MonoBehaviour
{
    [SerializeField] private Button[] gameButtons;
    [SerializeField] private SceneAsset[] sceneAssets; // FolderArray af minigames
    public string[] sceneNames; // Converted scene name

    [SerializeField] private float spinDuration = 2f; // Time of spinbutton
    [SerializeField] private int minSpin = 6, maxSpin = 30; // Spicy Range for randomizatiopn
    [SerializeField] private Color highlightColor = Color.yellow; 
    [SerializeField] private Color defaultColor = Color.white;

    private int chosenIndex = 0;
    private TextMeshProUGUI[] buttonTexts;

    private void Awake()
    {
        //debuger Så lortet ikke bugger.
        if (sceneAssets.Length != gameButtons.Length)
        {
            Debug.LogError("Scene assets array must match the length of game buttons!");
            return;
        }

        // Convert SceneAsset[] to sceneNames[] for unity Scenebuilder and editor!
        sceneNames = new string[sceneAssets.Length];
        for (int i = 0; i < sceneAssets.Length; i++)
        {
            sceneNames[i] = GetSceneName(sceneAssets[i]);
            Debug.Log($"Scene {i}: {sceneNames[i]}"); 
        }

        // Yoink text from textmeshpro buttons instead of generating aligning text fields to each button ligting up.
        buttonTexts = new TextMeshProUGUI[gameButtons.Length];
        for (int i = 0; i < gameButtons.Length; i++)
        {
            buttonTexts[i] = gameButtons[i].GetComponentInChildren<TextMeshProUGUI>();
        }
    }

#if UNITY_EDITOR
    private string GetSceneName(SceneAsset sceneAsset)
    {
        return sceneAsset != null ? sceneAsset.name : "Unnamed Scene";
    }
#endif



    private void OnEnable()
    {
        StartSpin();
    }

    private void StartSpin()
    {
        int spinCount = Random.Range(minSpin, maxSpin);
        chosenIndex = spinCount % gameButtons.Length;

        StartCoroutine(SpinEffect(spinCount));
    }

    private IEnumerator SpinEffect(int totalSpins)
    {
        int currentIndex = 0;
        float delay = 0.1f;
        float elapsed = 0f;

        foreach (var text in buttonTexts)
        {
            text.color = defaultColor;
        }

        for (int i = 0; i < totalSpins; i++)
        {
            int previousIndex = (currentIndex - 1 + gameButtons.Length) % gameButtons.Length;
            currentIndex = (currentIndex + 1) % gameButtons.Length;

            buttonTexts[previousIndex].color = defaultColor;
            buttonTexts[currentIndex].color = highlightColor;

            yield return new WaitForSeconds(delay);
            elapsed += delay;
            delay = Mathf.Lerp(0.05f, 0.3f, elapsed / spinDuration);
        }

        // Highlight the selected button
        foreach (var text in buttonTexts)
        {
            text.color = defaultColor;
        }
        buttonTexts[currentIndex].color = highlightColor;

        chosenIndex = currentIndex;
        string selectedScene = sceneNames[chosenIndex];

        Debug.Log("Selected Game: " + selectedScene);
        Debug.Log("Can load scene: " + Application.CanStreamedLevelBeLoaded(selectedScene));

        // 3-second delay for sceneexcitement
        yield return new WaitForSeconds(3f);

        // Load the scene from Build Settings
        if (Application.CanStreamedLevelBeLoaded(selectedScene))
        {
            Debug.Log("Loading scene: " + selectedScene);
            SceneManager.LoadScene(selectedScene);
        }
        else
        {
            Debug.LogError("Scene is not in Build Settings: " + selectedScene);
        }
    }
}