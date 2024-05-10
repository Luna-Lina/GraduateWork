using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using StarVelocity.Data;
using Zenject;

namespace StarVelocity.Controllers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Player _player;
        [SerializeField] private TMP_Text _score;
        [SerializeField] private Button _openScoreList;
        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private ScoreItem _scoreItem;
        [SerializeField] private RectTransform _scoreList;
        [SerializeField] private GameObject _endScreen;
        [SerializeField] private AudioSource _backgroundMusic;
        [SerializeField] private AudioClip[] _musicTracks;

        private AdManager _adManager;
        private FirebaseWrapper _firebaseWrapper;
        private MainThreadDispatcher _mainThreadDispatcher;

        private List<ScoreItem> _scoreItems = new List<ScoreItem>();
        private int _currentScore = 0;
        
        [Inject]
        public void Construct(FirebaseWrapper firebaseWrapper, MainThreadDispatcher mainThreadDispatcher, Player player, AdManager adManager)
        {
            _firebaseWrapper = firebaseWrapper;
            _mainThreadDispatcher = mainThreadDispatcher;
            _player = player;
            _adManager = adManager;
            Debug.Log("GameController constructed");
        }

        private void Start()
        {
            if (_musicTracks.Length > 0 && _backgroundMusic != null)
            {
                int randomIndex = Random.Range(0, _musicTracks.Length);
                _backgroundMusic.clip = _musicTracks[randomIndex];
                _backgroundMusic.Play();
            }
        }

        private void OnEnable()
        {
            _player.OnKilled += OnKilledPlayer;
            _player.OnCurrentScore += OnCurrentScore;
            _openScoreList.onClick.AddListener(ShowScoreList);
            _firebaseWrapper.OnDataLoaded += OnDataLoaded;
            _player.OnKilled += _adManager.ShowRewardedAd;
        }

        private void OnDisable()
        {
            _player.OnKilled -= OnKilledPlayer;
            _player.OnCurrentScore -= OnCurrentScore;
        }

        private void OnKilledPlayer()
        {
            _endScreen.SetActive(true);
        }

        private void OnCurrentScore(int score)
        {
            _currentScore++;
            UpdateCurrentScoreUI();
        }

        private void UpdateCurrentScoreUI()
        {
            if (_scoreText != null)
            {
                _scoreText.text = _currentScore.ToString();
            }
        }

        public void LoadScene(int sceneNumber)
        {
            SceneManager.LoadScene(sceneNumber);
        }

        private void OnDataLoaded(List<FirebaseWrapper.PlayerData> list)
        {
            Debug.Log("Data loaded " + list.Count);

            _mainThreadDispatcher.Dispatch(() =>
            {
                ResetPlayerList(list);
            });
        }

        private void ResetPlayerList(List<FirebaseWrapper.PlayerData> list)
        {
            foreach (var item in _scoreItems)
            {
                Destroy(item.gameObject);
            }

            _scoreItems.Clear();

            for (int i = 0; i < list.Count; i++)
            {
                var scoreItem = Instantiate(_scoreItem, _scoreList);
                scoreItem.SetData(list[i]);
                _scoreItems.Add(scoreItem);
            }
        }

        private void ShowScoreList()
        {
            _firebaseWrapper.LoadData();
        }
    }
}