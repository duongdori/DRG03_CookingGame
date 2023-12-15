using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }
    
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private GameObject mainMenuScreen;

    [SerializeField] private Slider loadingSlider;
    [SerializeField] private TextMeshProUGUI loadingProgressText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadLevel(string levelToLoad)
    {
        mainMenuScreen.SetActive(!mainMenuScreen.activeSelf);
        loadingScreen.SetActive(true);

        StartCoroutine(LoadLevelAsync(levelToLoad));

    }

    private IEnumerator LoadLevelAsync(string levelToLoad)
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(levelToLoad);

        while (!loadOperation.isDone)
        {
            float progressValue = Mathf.Clamp01(loadOperation.progress / 0.9f);
            loadingSlider.value = progressValue;
            loadingProgressText.text = ((int)(progressValue * 100)).ToString();
            yield return null;
        }
        loadingScreen.SetActive(false);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
