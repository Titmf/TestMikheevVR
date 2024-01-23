using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class SceneSelector : MonoBehaviour
    {
        public void OpenGameScene()
        {
            SceneManager.LoadScene("Main");
        }
    
        public void OpenMenuScene()
        {
            SceneManager.LoadScene("Menu");
        }
    }
}