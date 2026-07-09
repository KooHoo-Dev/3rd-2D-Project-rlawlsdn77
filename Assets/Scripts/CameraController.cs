using UnityEngine;

namespace SHIFTER
{
    public class CameraController : MonoBehaviour
    {
        public Transform player;     // 따라갈 플레이어의 Transform
        public float smoothing = 5f;  // 카메라가 따라오는 속도

        [Header("Horizontal Dead Zone")] // 좌우 데드존 크기
        public float horizontalDeadZone = 4.0f;

        [Header("Vertical Dead Zone")] // 상하 데드존 크기
        public float deadZoneUp = 3.5f;
        public float deadZoneDown = 1.0f;

        private Vector3 offset = new Vector3(0, 0, -10);

        private float targetX;
        private float targetY;

        void Start()
        {
            if (player != null)
            {
                targetX = player.position.x;
                targetY = player.position.y;

                Vector3 initPos = new Vector3(targetX, targetY, 0f) + offset;
                transform.position = initPos;
            }
        }

        void LateUpdate()
        {
            if (player == null) return;

            // 좌우 데드존 계산
            float deltaX = player.position.x - targetX;
            if (Mathf.Abs(deltaX) > horizontalDeadZone)
            {
                targetX += deltaX - (Mathf.Sign(deltaX) * horizontalDeadZone);
            }

            // 상하 데드존 계산
            float deltaY = player.position.y - targetY;

            if (deltaY > 0.0f) // 플레이이어가 카메라 위로 갈때
            {
                if (deltaY > deadZoneUp)
                {   
                    // 타겟 위치 올림
                    targetY += deltaY - deadZoneUp;
                }
            }
            else // 플레이어가 카메라 아래로 갈때
            {
                if (Mathf.Abs(deltaY) > deadZoneDown)
                {
                    // 타겟 위치 내림
                    targetY += deltaY + deadZoneDown;
                }
            }

            // 최종 이동
            Vector3 targetPosition = new Vector3(targetX, targetY, 0f) + offset;
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing * Time.deltaTime);
        }
        
        // 에디터에서 먼저 확인하기
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            
            Vector3 center = new Vector3(targetX, targetY, 0f);
            
            float boxHeight = deadZoneUp + deadZoneDown;
            float boxCenterY = targetY + (deadZoneUp - deadZoneDown) / 2f;
            Vector3 visualCenter = new Vector3(targetX, boxCenterY, 0f);
            Vector3 size = new Vector3(horizontalDeadZone * 2f, boxHeight, 0.1f);

            Gizmos.DrawWireCube(visualCenter, size);
        }
    }
}