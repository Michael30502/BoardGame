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

#if UNITY_EDITOR
    [SerializeField] private SceneAsset[] sceneAssets; // FolderArray af minigames

#endif
    public string[] sceneNames; // Converted scene name

    [SerializeField] private float spinDuration = 2f; // Time of spinbutton
    [SerializeField] private int minSpin = 6, maxSpin = 30; // Spicy Range for randomizatiopn
    [SerializeField] private Color highlightColor = Color.yellow; 
    [SerializeField] private Color defaultColor = Color.white;
    [SerializeField] private int riggedDiceRoll = -1;
    [SerializeField] private GameObject sceneParent;
    private int chosenIndex = 0;
    private TextMeshProUGUI[] buttonTexts;

    private void Awake()
    {
        //debuger Så lortet ikke bugger.
        if (sceneNames.Length != gameButtons.Length)
        {
            Debug.LogError("Scene assets array must match the length of game buttons!");
            return;
        }

        // Yoink text from textmeshpro buttons instead of generating aligning text fields to each button ligting up.
        buttonTexts = new TextMeshProUGUI[gameButtons.Length];
        for (int i = 0; i < gameButtons.Length; i++)
        {
            buttonTexts[i] = gameButtons[i].GetComponentInChildren<TextMeshProUGUI>();
        }
    }





    private void OnEnable()
    {
        StartSpin();
    }

    private void StartSpin()
    {
        int spinCount = Random.Range(minSpin, maxSpin);
        if (riggedDiceRoll != -1) {
            spinCount = riggedDiceRoll-1;
        }
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
            SceneManager.LoadScene(selectedScene, LoadSceneMode.Additive);
            gameObject.SetActive(false);
        }
        else
        {
            Debug.LogError("Scene is not in Build Settings: " + selectedScene);
        }
    }
}