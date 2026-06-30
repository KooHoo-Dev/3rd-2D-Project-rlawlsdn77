using UnityEngine;

namespace SHIFTER
{
    public class Red : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private GameObject player;
        [SerializeField] private float damageInterval = 1.0f;
        [SerializeField] private float damageAmount = 2f;

        private Collider2D[] childColliders;
        private Collider2D playerCollider;
        private ITakeDamage playerDamageable;
        private float damageTimer = 0f;

        private void Awake()
        {
            childColliders = GetComponentsInChildren<Collider2D>();

            if (player != null)
            {
                playerCollider = player.GetComponent<Collider2D>();
                playerDamageable = player.GetComponent<ITakeDamage>();
            }
        }

        private void Update()
        {
            CheckPlayerOnTile();
        }

        private void CheckPlayerOnTile()
        {
            if (playerCollider == null || playerDamageable == null) return;

            bool isStandingOnRed = false;

            foreach (var col in childColliders)
            {
                if (col.enabled && col.IsTouching(playerCollider))
                {
                    isStandingOnRed = true;
                    break;
                }
            }

            if (isStandingOnRed)
            {
                damageTimer += Time.deltaTime;
                if (damageTimer >= damageInterval)
                {
                    playerDamageable.TakeDamage(damageAmount);
                    damageTimer = 0f;
                }
            }
            else
            {
                damageTimer = 0f;
            }
        }
    }
}