using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    [RequireComponent(typeof(AudioSource))]
    public class ButtonManager : MonoBehaviour
    {
        public AudioClip HoverMouseSound;
        public AudioClip SelectSound;

        private AudioSource _audioSource;

        void Start()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        public void ExitGame()
        {
            StartCoroutine(ExitCoroutine());
        }

        public void StartGame()
        {
            StartCoroutine(StartGameCoroutine());
        }

        public void OnHover()
        {
            if(_audioSource != null)
                _audioSource.PlayOneShot(HoverMouseSound);
        }

        private IEnumerator StartGameCoroutine()
        {
            if (_audioSource != null)
                _audioSource.PlayOneShot(SelectSound);

            yield return new WaitForSeconds(1f);

            SceneManager.LoadScene("Game");
        }

        private IEnumerator ExitCoroutine()
        {
            if (_audioSource != null)
                _audioSource.PlayOneShot(SelectSound);

            yield return new WaitForSeconds(1f);


            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif

        }
    }
}
