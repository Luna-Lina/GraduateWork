using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace StarVelocity
{
    public class LosingPanel : MonoBehaviour
    {
        [SerializeField] private Button _repeatButton;
        [SerializeField] private Button _menuButton;
        [SerializeField] private AudioMixerSnapshot _miss;
        [SerializeField] private AudioMixerSnapshot _normal;
        private string previousScene;

        private void Start()
        {
            _repeatButton.onClick.AddListener(RestartLevel);
            _menuButton.onClick.AddListener(ReturnToMenu);
        }

        private void OnEnable()
        {
            Time.timeScale = 0f;
            _miss.TransitionTo(0.5f);
        }

        private void OnDisable()
        {
            Time.timeScale = 1f;
            _normal.TransitionTo(0.5f);
        }

        private void RestartLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        private void ReturnToMenu()
        {
            previousScene = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene("Start");
        }
    }
}