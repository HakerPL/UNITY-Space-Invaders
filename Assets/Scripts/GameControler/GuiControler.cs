using System.Collections;
using Unity.Mathematics;
using UnityEngine;

namespace Assets.Scripts.GameControler
{
    class GuiControler : MonoBehaviour
    {
        public GameObject ParentGui;

        public GameObject Start;
        public GameObject NewWave;
        public GameObject Dead;
        public GameObject GameOver;

        public float TimeShow = 2;

        public bool IsShow => _isShow;
        private bool _isShow = false;

        private readonly float3 _position = new float3(0, 0, 0);

        public IEnumerator ShowStart()
        {
            _isShow = true;
            var instance = Instantiate(Start, _position, Start.transform.rotation);
            instance.transform.SetParent(ParentGui.transform);

            yield return new WaitForSeconds(TimeShow);

            Destroy(instance);
            _isShow = false;
        }

        public IEnumerator ShowNewWave()
        {
            _isShow = true;
            var instance = Instantiate(NewWave, _position, Start.transform.rotation);
            instance.transform.SetParent(ParentGui.transform);

            yield return new WaitForSeconds(TimeShow);

            Destroy(instance);
            _isShow = false;
        }

        public IEnumerator ShowDead()
        {
            _isShow = true;
            var instance = Instantiate(Dead, _position, Start.transform.rotation);
            instance.transform.SetParent(ParentGui.transform);

            yield return new WaitForSeconds(TimeShow);

            Destroy(instance);
            _isShow = false;
        }

        public IEnumerator ShowGameOver()
        {
            _isShow = true;
            var instance = Instantiate(GameOver, _position, Start.transform.rotation);
            instance.transform.SetParent(ParentGui.transform);

            yield return new WaitForSeconds(TimeShow);

            Destroy(instance);
            _isShow = false;
        }
    }
}
