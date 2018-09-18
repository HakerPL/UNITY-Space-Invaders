using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.GameControler
{
    public class PointControler : MonoBehaviour
    {
        private readonly Text _pointObject;

        public PointControler(Text pointObject)
        {
            _pointObject = pointObject;
        }

        public void UpdatePoint(int newValuePoint)
        {
            _pointObject.text = newValuePoint.ToString();
        }
    }
}
