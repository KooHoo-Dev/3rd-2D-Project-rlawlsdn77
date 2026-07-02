using UnityEngine;

namespace SHIFTER
{

    // Red 타일에 플레이어가 올라갔을 때 damageInterval 간격으로 damageAmount 만큼의 데미지를 주는 스크립트
    public class Red : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private GameObject player;

        [Header("데미지 세팅")]
        [SerializeField] private float damageInterval = 1.0f; // 데미지 간격 (초 단위)
        [SerializeField] private float damageAmount = 2f;  // 데미지 값

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