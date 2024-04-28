using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

namespace StarVelocity
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField] private GameObject _pauseMenuUI;
        [SerializeField] private GameObject _optionsMenuUI;
        [SerializeField] private Button _continueButton;
        [SerializeField] private Button _repeatButton;
        [SerializeField] private Button _optionButton;
        [SerializeField] private Button _menuButton;
        [SerializeField] private Button _backButton;
        [SerializeField] private Button _pauseButton;

        [SerializeField] private AudioMixerSnapshot _normal;
        [SerializeField] private AudioMixerSnapshot _pause;

        [SerializeField] private BGController _bgController;

        private string previousScene;

        private void Start()
        {
            _continueButton.onClick.AddListener(ResumeGame);
            _repeatButton.onClick.AddListener(RestartLevel);
            _optionButton.onClick.AddListener(OpenOptions);
            _menuButton.onClick.AddListener(ReturnToMenu);
            _backButton.onClick.AddListener(CloseOptions);
            _pauseButton.onClick.AddListener(OpenPauseMenu);
        }

        private void OnEnable()
        {
            Time.timeScale = 0f;
            _pause.TransitionTo(0.5f);
        }

        private void OnDisable()
        {
            Time.timeScale = 1f;
            _normal.TransitionTo(0.5f);
        }

        public void ResumeGame()
        {
            _pauseMenuUI.SetActive(false);
            _bgController.enabled = true;
        }

        public void OpenPauseMenu()
        {
            _pauseMenuUI.SetActive(true);
            _bgController.enabled = false;
        }

        private void RestartLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        private void OpenOptions()
        {
            _optionsMenuUI.SetActive(true);
            _pauseMenuUI.SetActive(false);
        }

        private void CloseOptions()
        {
            _optionsMenuUI.SetActive(false);
            _pauseMenuUI.SetActive(true);
        }

        private void ReturnToMenu()
        {
            previousScene = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene("Start");
        }
    }
}