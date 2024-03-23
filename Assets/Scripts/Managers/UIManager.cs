using Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameObject returnToMenuButton;
        [SerializeField] private GameObject exitGameButtonPopup;
        [SerializeField] private Button pauseMenuButton;
        [SerializeField] private Button exitGameButton;
        [SerializeField] private Button cancelExitButton;

        //Adicionando listeners para os eventos de pausa e despausa do jogo e para os botões de sair e cancelar saída
        private void Start()
        {
            GameManager.instance.OnGamePaused += ShowMenuButtons;
            GameManager.instance.OnGameUnpaused += HideMenuButtons;

            pauseMenuButton.onClick.AddListener(GameManager.instance.TogglePauseGame);
            exitGameButton.onClick.AddListener(ShowExitGameConfirmationPopup);
            cancelExitButton.onClick.AddListener(HideExitGameConfirmationPopup);
        }

        private void OnDestroy()
        {
            GameManager.instance.OnGamePaused -= ShowMenuButtons;
            GameManager.instance.OnGameUnpaused -= HideMenuButtons;

            pauseMenuButton.onClick.RemoveListener(GameManager.instance.PauseGame);
            exitGameButton.onClick.RemoveListener(ShowExitGameConfirmationPopup);
            cancelExitButton.onClick.RemoveListener(HideExitGameConfirmationPopup);
        }

        //Métodos para mostrar e esconder o botão de sair do jogo
        private void ShowMenuButtons()
        {
            exitGameButton.gameObject.SetActive(true);
            returnToMenuButton.SetActive(true);
        }

        private void HideMenuButtons()
        {
            exitGameButton.gameObject.SetActive(false);
            returnToMenuButton.SetActive(false);
        }

        public void ShowExitGameConfirmationPopup()
        {
            exitGameButtonPopup.SetActive(true);
        }

        public void HideExitGameConfirmationPopup()
        {
            exitGameButtonPopup.SetActive(false);
        }
    }
}