using UnityEngine;
using UnityEngine.Advertisements;

namespace StarVelocity.Controllers
{
    public class AdManager : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
    {
        [SerializeField] private string _androidID;
        [SerializeField] private string _iOsID;
        [SerializeField] private string _adUnitId = "Rewarded_Android";
        //[SerializeField] private string _adBannerId = "Banner_Android";
        [SerializeField] private bool _testMode = true;

        //private string _bottomBanner;

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

            //Advertisement.Initialize(_gameId, _testMode);

            //Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
            //Advertisement.Banner.Show(_bottomBanner);
        }


        public void OnInitializationComplete()
        {
            LoadAdd();
        }

        public void OnInitializationFailed(UnityAdsInitializationError error, string message)
        {
            Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
        }

        private void LoadAdd()
        {
            if (Advertisement.isInitialized)
            {
                Advertisement.Load(_adUnitId, this);
            }

            //if (Advertisement.isInitialized)
            //{
            //    Advertisement.Load(_adBannerId, this);
            //}
        }

        public void ShowRewardedAd()
        {
            if (Advertisement.isInitialized)
            {
                Advertisement.Show(_adUnitId, this);
            }

            //if (Advertisement.isInitialized)
            //{
            //    Advertisement.Show(_adBannerId, this);
            //}
        }

        public void OnUnityAdsAdLoaded(string placementId)
        {
            Debug.Log("Unity Ads Loaded" + placementId);
        }

        public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
        {
            Debug.Log($"Unity Ads Failed to Load: {error.ToString()} - {message}");
        }

        public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
        {
            Debug.Log($"Unity Ads Show Failure: {error.ToString()} - {message}");
            Time.timeScale = 1;
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
        }
    }
}