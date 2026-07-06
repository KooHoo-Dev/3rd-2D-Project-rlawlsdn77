using UnityEngine;

namespace SHIFTER
{
    public class BulletLauncher : MonoBehaviour
    {
        public GameObject bulletPrefab;
        public Transform firePivot;
        public Transform endPivot;
        public float fireRate = 0.5f;

        [Header("총알 설정")]
        public float bulletDamage = 1f; // 데미지 조절
        public float bulletSpeed = 10f;  // 속도 조절 

        private float nextFireTime;

        void Update()
        {
            if (Time.time >= nextFireTime)
            {
                Fire();
                nextFireTime = Time.time + fireRate;
            }
        }

        void Fire()
        {
            if (bulletPrefab == null || firePivot == null || endPivot == null) return;

            Vector2 direction = (endPivot.position - firePivot.position).normalized;
            GameObject bulletObj = Instantiate(bulletPrefab, firePivot.position, Quaternion.identity);
            bulletObj.transform.right = direction;

            // 태그 무작위 설정 및 색상 변경
            string selectedTag = (Random.Range(0, 2) == 0) ? "Black" : "White";
            bulletObj.tag = selectedTag;
            SpriteRenderer sr = bulletObj.GetComponent<SpriteRenderer>();
            if (sr != null) sr.color = (selectedTag == "Black") ? Color.black : Color.white;

            // 거리, 데미지, 속도 정보를 총알에게 전달
            float distanceLimit = Vector3.Distance(firePivot.position, endPivot.position);
            Bullet bullet = bulletObj.GetComponent<Bullet>();
            if (bullet != null)
            {
                // Setup 함수에 속도(bulletSpeed) 인자 추가
                bullet.Setup(distanceLimit, bulletDamage, bulletSpeed);
            }
        }
    }
}