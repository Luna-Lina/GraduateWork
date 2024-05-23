using UnityEngine;
using UnityEngine.Advertisements;

namespace StarVelocity.Controllers
{
    public class AdManager : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
    {
        [SerializeField] private string _androidID;
        [SerializeField] private string _iOsID;
        [SerializeField] private string _adUnitId = "Rewarded_Android";
        [SerializeField] private bool _testMode = true;

        private string _gameId;

        void Start()
        {
#if UNITY_EDITOR
            _gameId = _androidID;
#endif

            if (Application.platform == RuntimePlatform.Android)
            {
                _gameId = _androidID;
            }
            else if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                _gameId = _iOsID;
            }

            if (!Advertisement.isInitialized)
            {
                Advertisement.Initialize(_gameId, _testMode, this);
            }
        }

        public void OnInitializationComplete()
        {
            LoadAdd();
        }

        public void OnInitializationFailed(UnityAdsInitializationError error, string message)
        {
            Debug.Log($"Unity Ads Initialization Failed: {error} - {message}");
        }

        private void LoadAdd()
        {
            if (Advertisement.isInitialized)
            {
                Advertisement.Load(_adUnitId, this);
            }
        }

        public void ShowRewardedAd()
        {
            if (Advertisement.isInitialized)
            {
                Advertisement.Show(_adUnitId, this);
            }
        }

        public void OnUnityAdsAdLoaded(string placementId)
        {
            Debug.Log("Unity Ads Loaded" + placementId);
        }

        public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
        {
            Debug.Log($"Unity Ads Failed to Load: {error} - {message}");
            LoadAdd();
        }

        public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
        {
            Debug.Log($"Unity Ads Show Failure: {error} - {message}");
            Time.timeScale = 1;
            LoadAdd();
        }

        public void OnUnityAdsShowStart(string placementId)
        {
            Time.timeScale = 0;
            Debug.Log("Unity Ads Show Start");
        }

        public void OnUnityAdsShowClick(string placementId)
        {
            Time.timeScale = 0;
            Debug.Log("Unity Ads Show Click");
        }

        public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
        {
            Time.timeScale = 0;

            switch (showCompletionState)
            {
                case UnityAdsShowCompletionState.SKIPPED:
                    break;
                case UnityAdsShowCompletionState.COMPLETED:
                    break;
                case UnityAdsShowCompletionState.UNKNOWN:
                    break;
                default:
                    break;
            }

            LoadAdd();
        }
    }
}