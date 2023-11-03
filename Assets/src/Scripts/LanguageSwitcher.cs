using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class LanguageSwitcher : MonoBehaviour
{
    [SerializeField] private Button switchButton;

    private List<Locale> availableLocales;
    private int currentLocaleIndex;

    private void Awake()
    {
        availableLocales = LocalizationSettings.AvailableLocales.Locales;
        currentLocaleIndex = 0;
    }
    
    private void OnEnable()
    {
        switchButton.onClick.AddListener(SwitchLanguage);
    }

    private void OnDisable()
    {
        switchButton.onClick.RemoveListener(SwitchLanguage);
    }

    private void SwitchLanguage()
    {
        currentLocaleIndex = (currentLocaleIndex + 1) % availableLocales.Count;
        LocalizationSettings.SelectedLocale = availableLocales[currentLocaleIndex];
    }
}
