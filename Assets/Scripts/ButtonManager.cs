using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class ButtonManager : MonoBehaviour
    {
        public void ExitGame()
        {
            Application.Quit();
        }

        public void StartGame()
        {
            SceneManager.LoadScene("Game");
        }
    }
}
