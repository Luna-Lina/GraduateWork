using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;


namespace StarVelocity
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Player _player;
        [SerializeField] private GameObject _endScreen;
        [SerializeField] TMP_Text RatingText;

        private void OnEnable()
        {
            _player.OnKilled += OnKilledPlayer;
        }

        private void OnDisable()
        {
            _player.OnKilled -= OnKilledPlayer;
        }

        private void OnKilledPlayer()
        {
            _endScreen.SetActive(true);
        }

        public void LoadScene(int sceneNumber)
        {
            SceneManager.LoadScene(sceneNumber);
        }
    }
}