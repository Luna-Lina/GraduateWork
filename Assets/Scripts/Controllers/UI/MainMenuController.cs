using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace StarVelocity
{
    public class MainMenuController : MonoBehaviour
    {
        [SerializeField] private Transition _transition;
        [SerializeField] private Button _play;
        [SerializeField] private Button _settings;
        [SerializeField] private Button _exit;
        [SerializeField] private Button _start;
        [SerializeField] private Button _back;
        [SerializeField] private GameObject _settingsMenu;
        [SerializeField] private GameObject _startScene;
        [SerializeField] private AudioMixerSnapshot _normal;
        [SerializeField] private AudioMixerSnapshot _pause;
        [SerializeField] private Animator _animator;

        private float transitionDelay = 1.3f;

        private void Start()
        {
            _play.onClick.AddListener(Play);
            _settings.onClick.AddListener(OpenSettings);
            _back.onClick.AddListener(CloseSettings);
            _exit.onClick.AddListener(ExitGame);
            _start.onClick.AddListener(StartScene);
        }

        private void Play()
        {
            _transition.gameObject.SetActive(true);

            StartCoroutine(StartGameWithDelay());
        }

        private IEnumerator StartGameWithDelay()
        {
            yield return new WaitForSeconds(transitionDelay);

            _transition.LoadScene("Game");
        }

        private IEnumerator DisableStartScene(float delay)
        {
            yield return new WaitForSeconds(delay);
            _startScene.SetActive(false);
        }

        private void StartScene()
        {
            _animator.enabled = true;
            _startScene.SetActive(false);
        }

        private void OpenSettings()
        {
            _settingsMenu.SetActive(true);
            _pause.TransitionTo(0.5f);
        }

        private void CloseSettings()
        {
            _settingsMenu.SetActive(false);
            _normal.TransitionTo(0.5f);
        }

        private void ExitGame()
        {
            Debug.Log("Exiting...");
            Application.Quit();
        }
    }
}