using UnityEngine;
using UnityEngine.UI;

public class PointControler : MonoBehaviour
{
    private Text _pointObject;

    public PointControler(Text pointObject)
    {
        _pointObject = pointObject;
    }

    public void UpdatePoint(int newValuePoint)
    {
        _pointObject.text = newValuePoint.ToString();
    }
}
