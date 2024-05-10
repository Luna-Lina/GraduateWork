using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace StarVelocity.Controllers
{
    public class Transition : MonoBehaviour
    {
        [SerializeField] private GameObject loadingScreen;

        public void LoadScene(string sceneName)
        {
            loadingScreen.SetActive(true);

            SceneManager.LoadScene(sceneName);
        }
    }
}