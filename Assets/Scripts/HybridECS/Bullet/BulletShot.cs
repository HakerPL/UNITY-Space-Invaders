using UnityEngine;

namespace Assets.Scripts.HybridECS
{
    public class BulletShot : MonoBehaviour
    {
        public GameObject BulletPrefab;

        public void CreateInstance(Transform transform)
        {
            Instantiate(BulletPrefab, transform.position, transform.rotation);
        }
    }
}
