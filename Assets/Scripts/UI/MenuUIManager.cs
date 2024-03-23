using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace UI
{
    public class MenuUIManager : MonoBehaviour
    {
        [Header("Menu Containers")]
        [SerializeField] private GameObject mainMenuContainer;
        [SerializeField] private GameObject settingsMenuContainer;
        [SerializeField] private GameObject controllsMenuContainer;
        [SerializeField] private GameObject creditsMenuContainer;

        [Header("Main Menu Buttons")]
        [SerializeField] private Button settingsButton;
        [SerializeField] private Button controllsButton;
        [SerializeField] private Button creditsButton;

        [Header("Settings Buttons")]
        [SerializeField] private Slider volumeSlider;
        [SerializeField] private Toggle fullScreenToggle;
        [SerializeField] private TMP_Dropdown resolutionDropDown;
        [SerializeField] private Button settingsBackButton;
        [SerializeField] private AudioMixer audioMixer;

        [Header("Controlls Buttons")]
        [SerializeField] private Button controllsBackButton;

        [Header("Credits Buttons")]
        [SerializeField] private Button creditsBackButton;

        private void Start()
        {
            settingsButton.onClick.AddListener(OpenSettingsMenu);
            controllsButton.onClick.AddListener(OpenControllsMenu);
            creditsButton.onClick.AddListener(OpenCreditsMenu);

            creditsBackButton.onClick.AddListener(OpenMainMenu);
            controllsBackButton.onClick.AddListener(OpenMainMenu);
            settingsBackButton.onClick.AddListener(OpenMainMenu);

            StartResolutionDropdown();
            Screen.fullScreen = PlayerPrefs.GetInt("fullScreen", 1) == 1;
            audioMixer.SetFloat("volume", Mathf.Log10(PlayerPrefs.GetFloat("volumePref")) * 20);

            fullScreenToggle.isOn = Screen.fullScreen;
            volumeSlider.value = PlayerPrefs.GetFloat("volumePref", 0.3f);

            resolutionDropDown.onValueChanged.AddListener(ChangeResolution);
            fullScreenToggle.onValueChanged.AddListener(ToggleFullScreen);
            volumeSlider.onValueChanged.AddListener(ChangeVolume);
        }

        private void OnDestroy()
        {
            settingsButton.onClick.RemoveListener(OpenSettingsMenu);
            controllsButton.onClick.RemoveListener(OpenControllsMenu);
            creditsButton.onClick.RemoveListener(OpenCreditsMenu);

            creditsBackButton.onClick.RemoveListener(OpenMainMenu);
            controllsBackButton.onClick.RemoveListener(OpenMainMenu);
            settingsBackButton.onClick.RemoveListener(OpenMainMenu);

            resolutionDropDown.onValueChanged.RemoveListener(ChangeResolution);
            fullScreenToggle.onValueChanged.RemoveListener(ToggleFullScreen);
            volumeSlider.onValueChanged.RemoveListener(ChangeVolume);
        }

        // Método para alterar a resolução do jogo
        private void ChangeResolution(int dropdownIndex)
        {
            Resolution[] resolution = Screen.resolutions;
            Screen.SetResolution(resolution[dropdownIndex].width, resolution[dropdownIndex].height, Screen.fullScreen);
            PlayerPrefs.SetInt("resolutionIndex", dropdownIndex);
        }

        // Método para alternar entre tela cheia e janela
        private void ToggleFullScreen(bool isFullScreen)
        {
            Screen.fullScreen = isFullScreen;
            PlayerPrefs.SetInt("fullScreen", isFullScreen ? 1 : 0);
        }

        // Método para iniciar o dropdown de resolução
        private void StartResolutionDropdown()
        {
            resolutionDropDown.ClearOptions();
            List<string> resolutions = new List<string>();
            Resolution[] resolution = Screen.resolutions;
            int currentResolutionIndex = 0;

            for (int i = 0; i < resolution.Length; i++)
            {
                string resolutionString = resolution[i].width + " x " + resolution[i].height;
                resolutions.Add(resolutionString);

                if (resolution[i].width == Screen.currentResolution.width && resolution[i].height == Screen.currentResolution.height)
                {
                    currentResolutionIndex = i;
                }
            }

            resolutionDropDown.AddOptions(resolutions);
            if (PlayerPrefs.HasKey("resolutionIndex"))
            {
                currentResolutionIndex = PlayerPrefs.GetInt("resolutionIndex");
            }
            resolutionDropDown.value = currentResolutionIndex;
            resolutionDropDown.RefreshShownValue();
        }

        // Método para alterar o volume do jogo
        private void ChangeVolume(float volume)
        {
            PlayerPrefs.SetFloat("volumePref", volumeSlider.value);
            audioMixer.SetFloat("volume", Mathf.Log10(PlayerPrefs.GetFloat("volumePref")) * 20);
        }

        // Métodos para abrir ou fechar os menus
        private void OpenCreditsMenu()
        {
            CloseMainMenu();
            creditsMenuContainer.SetActive(true);
        }

        private void OpenControllsMenu()
        {
            CloseMainMenu();
            controllsMenuContainer.SetActive(true);
        }

        private void OpenSettingsMenu()
        {
            CloseMainMenu();
            settingsMenuContainer.SetActive(true);
        }

        public void OpenMainMenu()
        {
            mainMenuContainer.SetActive(true);
            settingsMenuContainer.SetActive(false);
            controllsMenuContainer.SetActive(false);
            creditsMenuContainer.SetActive(false);
        }

        public void CloseMainMenu()
        {
            mainMenuContainer.SetActive(false);
        }
    }
}