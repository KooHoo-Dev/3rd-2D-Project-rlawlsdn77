using UnityEngine;

namespace SHIFTER
{
    public class RedTileDamageManager : MonoBehaviour
    {
        [SerializeField] private GameObject player;
        private Shifter playerScript;
        private Collider2D playerCollider;
        private Collider2D[] childColliders;
        private float damageTimer = 0f;

        private void Awake()
        {
            // 1. 하위의 모든 red tile 콜라이더를 미리 캐싱합니다 [1].
            childColliders = GetComponentsInChildren<Collider2D>();

            if (player != null)
            {
                playerScript = player.GetComponent<Shifter>();
                playerCollider = player.GetComponent<Collider2D>();
            }
        }

        private void Update()
        {
            if (playerScript == null || playerCollider == null) return;

            bool isStandingOnRed = false;

            // 2. 자식 콜라이더 중 하나라도 플레이어와 접촉 중인지 확인합니다 [2].
            foreach (var col in childColliders)
            {
                // PlatformManager에 의해 활성화된 타일만 데미지를 줍니다.
                if (col.enabled && col.IsTouching(playerCollider))
                {
                    isStandingOnRed = true;
                    break;
                }
            }

            // 3. 접촉 중이라면 1초마다 2의 피해를 입힙니다 [3].
            if (isStandingOnRed)
            {
                damageTimer += Time.deltaTime;
                if (damageTimer >= 1.0f)
                {
                    playerScript.Hp -= 2;
                    damageTimer = 0f;
                    Debug.Log($"레드 타일 데미지 발생! 현재 HP: {playerScript.Hp}");
                }
            }
            else
            {
                damageTimer = 0f; // 타일에서 벗어나면 타이머 초기화
            }
        }
    }
}
