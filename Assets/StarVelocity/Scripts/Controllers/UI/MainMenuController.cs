using StarVelocity.Data;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace StarVelocity.Controllers
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
        [SerializeField] private Button _playGame;
        [SerializeField] private TMP_InputField _userName;
        [SerializeField] private GameObject _loginPanel;

        private int _currentScore = 0;
        private float transitionDelay = 1.3f;
        
        private void Start()
        {
            _start.onClick.AddListener(StartScene);
            _play.onClick.AddListener(Play);
            _settings.onClick.AddListener(OpenSettings);
            _back.onClick.AddListener(CloseSettings);
            _exit.onClick.AddListener(ExitGame);
            _playGame.onClick.AddListener(PlayGame);
        }

        private void StartScene()
        {
            _startScene.SetActive(false);
        }

        private void Play()
        {
            _loginPanel.SetActive(true);
        }

        private void PlayGame()
        {
            if (!PlayerPrefs.HasKey("User"))
            {
                if (!string.IsNullOrEmpty(_userName.text) && _userName.text.Length > 4)
                {
                    FirebaseWrapper.SaveData(_userName.text, _currentScore.ToString());
                    PlayerPrefs.SetString("User", _userName.text);
                    _transition.gameObject.SetActive(true);
                    StartCoroutine(StartGameWithDelay());
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(_userName.text) && _userName.text.Length > 4)
                {
                    PlayerPrefs.SetString("User", _userName.text);
                }

                _transition.gameObject.SetActive(true);
                StartCoroutine(StartGameWithDelay());
            }
        }

        private IEnumerator StartGameWithDelay()
        {
            yield return new WaitForSeconds(transitionDelay);

            _transition.LoadScene("Game");
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