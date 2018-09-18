using UnityEngine;

namespace Assets.Scripts.GameControler
{
    public class SpawnLifes : MonoBehaviour
    {
        public GameObject LifeIconPrefab;
        public float DefaultWidthSizeIcon = 25;

        private int _children;

        public int GetCountLifes()
        {
            return _children;
        }

        public void Spawn(int playerLife)
        {
            _children = playerLife;

            for (int i = 0; i < _children; i++)
            {
                float width = DefaultWidthSizeIcon;

                var rectTransform = LifeIconPrefab.GetComponent<RectTransform>();
                if (rectTransform != null)
                    width = rectTransform.rect.width;

                Vector2 position = new Vector2();
                position.x += width * i;

                var instance = Instantiate(LifeIconPrefab, position, transform.rotation);
                instance.transform.SetParent(transform, false);
            }
        }

        public void DestroyLife()
        {
            if (_children == 0)
                return;

            _children--;

            Destroy(transform.GetChild(_children).gameObject);
        }
    }
}
