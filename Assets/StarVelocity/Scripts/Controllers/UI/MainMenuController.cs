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
        [SerializeField] private HelpPanel _helpPanel;
        [SerializeField] private Button _play;
        [SerializeField] private Button _settings;
        [SerializeField] private Button _exit;
        [SerializeField] private Button _start;
        [SerializeField] private Button _backOptionPanel;
        [SerializeField] private GameObject _optionPanel;
        [SerializeField] private GameObject _startScene;
        [SerializeField] private AudioMixerSnapshot _normal;
        [SerializeField] private AudioMixerSnapshot _pause;
        [SerializeField] private Button _playGame;
        [SerializeField] private TMP_InputField _userName;
        [SerializeField] private GameObject _loginPanel;

        private int _currentScore = 0;
        
        private void Start()
        {
            _play.onClick.AddListener(Play);
            _settings.onClick.AddListener(OpenOptionPanel);
            _backOptionPanel.onClick.AddListener(CloseOptionPanel);
            _exit.onClick.AddListener(ExitGame);
            _playGame.onClick.AddListener(PlayGame);

            StartCoroutine(StartScene());
        }

        private IEnumerator StartScene()
        {
            yield return new WaitForSeconds(1f);
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
                if (!string.IsNullOrEmpty(_userName.text) && _userName.text.Length >= 4)
                {
                    FirebaseWrapper.SaveData(_userName.text, _currentScore.ToString());
                    PlayerPrefs.SetString("User", _userName.text);
                    _helpPanel.gameObject.SetActive(true);
                    StartCoroutine(StartGameWithDelay());
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(_userName.text) && _userName.text.Length >= 4)
                {
                    PlayerPrefs.SetString("User", _userName.text);
                }

                _helpPanel.gameObject.SetActive(true);
                StartCoroutine(StartGameWithDelay());
            }
        }

        private IEnumerator StartGameWithDelay()
        {
            yield return new WaitForSeconds(1f);

            _helpPanel.LoadScene("Game");
        }

        private void OpenOptionPanel()
        {
            _optionPanel.SetActive(true);
            _pause.TransitionTo(0.5f);
        }

        private void CloseOptionPanel()
        {
            _optionPanel.SetActive(false);
            _normal.TransitionTo(0.5f);
            Time.timeScale = 1f;
        }

        private void ExitGame()
        {
            Debug.Log("Exiting...");
            Application.Quit();
        }
    }
}