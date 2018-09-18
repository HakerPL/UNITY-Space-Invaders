using UnityEngine;

namespace Assets.Scripts
{
    public class ShieldLife : MonoBehaviour
    {
        public float Live = 10;

        private float _decreaseSpriteAlpha;
        private SpriteRenderer _spriteRenderer;
        // Use this for initialization
        void Start ()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _decreaseSpriteAlpha = _spriteRenderer.color.a / Live;
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag != "EnemyBullet" && other.tag != "PlayerBullet")
                return;

            Live--;

            Destroy(other.gameObject);

            if (Live <= 0)
                Destroy(gameObject);

            _spriteRenderer.color = new Color(_spriteRenderer.color.r, _spriteRenderer.color.g, 
                _spriteRenderer.color.b, _spriteRenderer.color.a - _decreaseSpriteAlpha);
        }
    }
}
