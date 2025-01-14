using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class ToMenu : MonoBehaviour
{
    public static ToMenu instance;

    [SerializeField]
    private GameObject SetActiveSlots;
    
    [SerializeField]private GameController gameController;
    private int currentSceneIndex;

    [SerializeField]
    private Button BackFromFightScreenButton;

    private void Awake()
    {
        instance = this;
        BackFromFightScreenButton.onClick.AddListener(() => { LoadMainMenu(); });
    }

    public void LoadMainMenu()
    {
        var settings = SettingsController.Instance;
        SetActiveSlots.SetActive(false);
        var slots = gameController.slots;
        
        settings._userSettingsSO.UserSettings.itemId = new List<int>();
        settings._userSettingsSO.UserSettings.slotId = new List<int>();
        settings._userSettingsSO.UserSettings.totalGold = UIManager.current.Gold;
        
        foreach (var slot in slots)
        {
            if (slot.currentItem == null)
            {
                continue;
            }
            
            var itemId = slot.currentItem.id;
            var slotId = slot.id;
            settings._userSettingsSO.UserSettings.itemId.Add(itemId);
            settings._userSettingsSO.UserSettings.slotId.Add(slotId);

            if (itemId%2 == 0)
            {
                settings._userSettingsSO.UserSettings.archerCounter = gameController.ArcherCost;
            }
            else
            {
                settings._userSettingsSO.UserSettings.meleeCounter = gameController.MeleeCost;
            }
        }
        settings.SaveSettings();

        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt("SavedScene", currentSceneIndex);
        SceneManager.LoadScene(0);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
