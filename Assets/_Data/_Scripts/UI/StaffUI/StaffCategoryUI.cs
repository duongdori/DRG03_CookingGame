using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StaffCategoryUI : MyMonobehaviour
{
    [SerializeField] private List<RecruitmentPopup> recruitmentPopups = new();

    protected override void Awake()
    {
        base.Awake();
        UpdateStaffCategory();
        foreach (var recruitmentPopup in recruitmentPopups)
        {
            recruitmentPopup.Load();
        }
        gameObject.SetActive(false);
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        if(recruitmentPopups.Count > 0) return;
        recruitmentPopups = GetComponentsInChildren<RecruitmentPopup>().ToList();
    }

    private void OnEnable()
    {
        UpdatePopup();
    }

    public void UpdateStaffCategory()
    {
        foreach (var popup in recruitmentPopups)
        {
            popup.Setup();
        }
    }

    public void UpdatePopup()
    {
        foreach (var popup in recruitmentPopups)
        {
            popup.UpdateRecruitButton();
        }
    }

    public void Save()
    {
        foreach (var recruitmentPopup in recruitmentPopups)
        {
            recruitmentPopup.Save();
        }
    }
}
