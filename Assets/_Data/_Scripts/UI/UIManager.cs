using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    
    [SerializeField] private Button settingButton;
    [SerializeField] private Button continueButton;
    [SerializeField] private Button newGameButton;
    [SerializeField] private PopupIntroduceUI popupIntroduceUI;
    [SerializeField] private bool isNewGame;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        
        continueButton.gameObject.SetActive(ES3.FileExists(ES3Settings.defaultSettings));
        continueButton.onClick.AddListener(ContinueGame);
        newGameButton.onClick.AddListener(NewGame);
        
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode)
    {
        if (settingButton == null) return;
        
        string currentSceneName = scene.name;
        if (currentSceneName == "MainMenu")
        {
            settingButton.gameObject.SetActive(false);
        }
        else if (currentSceneName == "MainScene")
        {
            settingButton.gameObject.SetActive(true);
            if (isNewGame)
            {
                popupIntroduceUI.gameObject.SetActive(true);
            }
        }
    }

    private void NewGame()
    {
        isNewGame = true;
        if (ES3.FileExists(ES3Settings.defaultSettings.path))
        {
            ES3.DeleteFile(ES3Settings.defaultSettings.path);
        }
        LevelManager.Instance.LoadLevel("MainScene");
    }

    private void ContinueGame()
    {
        isNewGame = false;
        Debug.Log("Continue Game");
        LevelManager.Instance.LoadLevel("MainScene");
    }
}
