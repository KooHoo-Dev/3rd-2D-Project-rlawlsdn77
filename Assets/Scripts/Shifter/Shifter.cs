using UnityEngine;
using UnityEngine.UI;
using System.Collections; // 코루틴 사용을 위해 추가

namespace SHIFTER
{
    // 플레이어인 Shifter의 체력 관리 및 태그 전환을 담당하는 스크립트
    // 피해를 입을 시 피해를 입는 이펙트 발생

    public class Shifter : MonoBehaviour, ITakeDamage
    {
        [SerializeField] private float MaxHp = 100f;
        [SerializeField] private float hp;

        [SerializeField] private Image HpBar;
        [SerializeField] private GameObject GameoverUI;
        private SpriteRenderer spriteRenderer; // 피격 이펙트를 위한 렌더러

        public float Hp
        {
            get => hp;
            set
            {
                hp = Mathf.Clamp(value, 0, MaxHp);
                if (HpBar != null) HpBar.fillAmount = hp / MaxHp;
                if (hp <= 0) Die();
            }
        }

        private const string TAG_COMMON = "Common";
        private const string TAG_WHITE = "White";
        private const string TAG_BLACK = "Black";

        public string currentTag { get; private set; }

        private void Start()
        {
            Hp = MaxHp;
            gameObject.tag = TAG_COMMON;
            currentTag = TAG_COMMON;
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }

        public void TakeDamage(float damage)
        {
            Hp -= damage;
            Debug.Log($"{gameObject.name} 피격! 현재 HP: {Hp}");

            // 피격 이펙트 코루틴 실행
            if (gameObject.activeInHierarchy)
            {
                StartCoroutine(DamageEffectRoutine());
            }
        }

        // 피격 시 빨간색으로 깜빡이는 이펙트 코루틴
        private IEnumerator DamageEffectRoutine()
        {
            if (spriteRenderer != null)
            {
                for (int i = 0; i < 3; i++)
                {
                    spriteRenderer.color = Color.red;
                    yield return new WaitForSeconds(0.1f);
                    spriteRenderer.color = Color.white;
                    yield return new WaitForSeconds(0.1f);
                }
            }
        }

        public void ShiftTag()
        {
            gameObject.tag = (gameObject.CompareTag(TAG_WHITE)) ? TAG_BLACK : TAG_WHITE;
            currentTag = gameObject.tag;
        }

        private void Die()
        {
            Debug.Log($"{gameObject.name}가 사망하였습니다.");
            if (GameoverUI != null) GameoverUI.SetActive(true);
            Time.timeScale = 0.0f; // 게임 일시정지
        }
    }
}