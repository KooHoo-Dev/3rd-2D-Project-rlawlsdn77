using UnityEngine;

namespace SHIFTER
{
    public class Bullet : MonoBehaviour
    {
        private float speed; 
        private Vector3 startPosition;
        private float maxDistance;
        private float damage;

        // 발사 시 발사기로부터 정보를 받아옴
        public void Setup(float distance, float damageValue, float speedValue)
        {
            startPosition = transform.position;
            maxDistance = distance;
            damage = damageValue;
            speed = speedValue; // 전달받은 속도 적용
        }

        void Update()
        {
            // 발사기에서 설정한 speed로 이동
            transform.Translate(Vector2.right * speed * Time.deltaTime);

            // endPivot 거리만큼 이동하면 삭제
            if (Vector3.Distance(startPosition, transform.position) > maxDistance)
            {
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            // 총알 태그와 부딪힌 대상의 태그가 일치할 때만 작동
            if (collision.CompareTag(gameObject.tag))
            {
                ITakeDamage target = collision.GetComponent<ITakeDamage>();
                if (target != null)
                {
                    target.TakeDamage(damage);
                    Destroy(gameObject);
                }
            }
        }
    }
}