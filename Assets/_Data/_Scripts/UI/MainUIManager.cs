using System;
using Assets.SimpleLocalization.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainUIManager : MonoBehaviour
{
    public static MainUIManager Instance { get; private set; }
    
    public StaffCategoryUI staffCategoryUI;
    public ShopCategoryUI shopCategoryUI;

    public PopupTutorial popupTutorial;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    private void Start()
    {
        shopCategoryUI.gameObject.SetActive(false);
    }

    public void SetupPopupTutorial()
    {
        if(popupTutorial == null) return;
        popupTutorial.gameObject.SetActive(true);
        popupTutorial.SetupText(LocalizationManager.Localize("Introduce.Text4"));
        popupTutorial.count = 1;
    }

    private void OnSceneUnloaded(Scene scene)
    {
        staffCategoryUI.Save();
        shopCategoryUI.Save();
    }

    private void OnApplicationQuit()
    {
        staffCategoryUI.Save();
        shopCategoryUI.Save();
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            staffCategoryUI.Save();
            shopCategoryUI.Save();
        }
    }
}
