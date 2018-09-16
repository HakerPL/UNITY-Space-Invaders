using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    public float LiveTime = 3f;
	// Use this for initialization
	void Start ()
    {
        Destroy(gameObject, LiveTime);
    }
}
