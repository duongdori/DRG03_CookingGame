using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingUI : MonoBehaviour
{
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    [SerializeField] private Button exitButton;

    private void OnEnable()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        if (currentSceneName == "MainMenu")
        {
            exitButton.gameObject.SetActive(false);
        }
        else if (currentSceneName == "MainScene")
        {
            exitButton.gameObject.SetActive(true);
        }
    }

    private void Start()
    {
        SoundManager.Instance.ChangeMusicVolume(musicSlider.value);
        SoundManager.Instance.ChangeSfxVolume(sfxSlider.value);
        
        musicSlider.onValueChanged.AddListener(val => SoundManager.Instance.ChangeMusicVolume(val));
        sfxSlider.onValueChanged.AddListener(val => SoundManager.Instance.ChangeSfxVolume(val));

        if (exitButton != null)
        {
            exitButton.onClick.AddListener((ExitGame));
        }
    }
    
    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
