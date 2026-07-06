using UnityEngine;

namespace SHIFTER
{
    // 플랫폼 왕복이동
    public class MovingPlatform : MonoBehaviour
    {
        [SerializeField] private Vector2 moveDirection = Vector2.up; // 왕복 이동 방향
        [SerializeField] private float moveDistance = 2f; // 시작 위치 기준 왕복 거리
        [SerializeField] private float moveSpeed = 1f; // 이동 속도

        private Vector3 startPos;

        private void Awake()
        {
            startPos = transform.position;
        }

        private void Update()
        {
            // -moveDistance ~ +moveDistance 사이를 부드럽게 왕복
            float offset = Mathf.PingPong(Time.time * moveSpeed, moveDistance * 2f) - moveDistance;
            transform.position = startPos + (Vector3)(moveDirection.normalized * offset);
        }
    }
}
