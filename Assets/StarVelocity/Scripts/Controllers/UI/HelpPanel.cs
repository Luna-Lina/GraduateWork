using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace StarVelocity.Controllers
{
    public class HelpPanel : MonoBehaviour
    {
        [SerializeField] private GameObject loadingScreen;

        public void LoadScene(string sceneName)
        {
            loadingScreen.SetActive(true);

            SceneManager.LoadScene(sceneName);
        }
    }
}