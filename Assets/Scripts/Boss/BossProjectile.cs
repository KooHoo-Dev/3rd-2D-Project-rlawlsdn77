using UnityEngine;

namespace SHIFTER
{
    // 보스 패턴에서 발사하는 프로젝타일 (레이저, 운석, 탄환 등)
    // 데미지는 각 패턴 스크립트에서 Initialize() / SetDamage()로 조정
    public class BossProjectile : MonoBehaviour
    {
        private const float LIFETIME = 10f; // 생존 시간 고정

        private Vector3 direction = Vector3.right;
        private float speed = 10f;
        private float damage;
        private Rigidbody2D rb;
        private float elapsedTime = 0f;
        private Shifter shifter;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            if (rb == null)
            {
                rb = gameObject.AddComponent<Rigidbody2D>();
                rb.gravityScale = 0;
            }
        }

        private void Start()
        {
            shifter = FindFirstObjectByType<Shifter>();

            // 태그 없을때 랜덤 속성 부여
            if (gameObject.CompareTag("Untagged"))
            {
                SetAttribute(BossAttributeUtil.RandShiftTag());
            }
        }

        private void Update()
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= LIFETIME)
            {
                Destroy(gameObject);
            }
        }

        // 패턴 스크립트에서 호출하여 방향/속도/피해량/속성 초기화
        public void Initialize(Vector3 dir, float spd, float dmg, string attributeTag = null)
        {
            direction = dir.normalized;
            speed = spd;
            damage = dmg;

            rb.linearVelocity = direction * speed;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            SetAttribute(string.IsNullOrEmpty(attributeTag) ? BossAttributeUtil.RandShiftTag() : attributeTag);
        }

        public void SetAttribute(string tag)
        {
            BossAttributeUtil.Apply(gameObject, tag);
        }

        // 데미지 설정
        public void SetDamage(float value)
        {
            damage = value;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.name == "Shifter")
            {
                // 속성(태그)이 같을 때만 피해를 주고 사라짐
                if (collision.CompareTag(gameObject.tag))
                {
                    shifter.TakeDamage(damage);
                    Destroy(gameObject);
                }
            }
            else if (collision.CompareTag("Common"))
            {
                Destroy(gameObject);
            }
        }
    }
}
