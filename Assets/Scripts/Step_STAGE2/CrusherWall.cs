using System.Collections.Generic;
using Study.Utilities;
using UnityEngine;

namespace SHIFTER
{
    // white tile / black tile 프리팹 중 하나를 랜덤으로 뽑아 일정 간격으로 계속 생성해서 밀어붙입니다.
    // 벽이 그 프레임에 움직인 만큼만 그대로 따라 밀리게 만듭니다.
    public class CrusherWall : MonoBehaviour
    {
        [SerializeField] private GameObject whiteTilePrefab;
        [SerializeField] private GameObject blackTilePrefab;
        [SerializeField] private Transform pivot; // 벽이 여기 도달하면 사라짐
        [SerializeField] private float moveSpeed = 3f;
        [SerializeField] private float spawnInterval = 0.7f; // 벽 생성 간격
        [SerializeField] private float startDelay = 0f; // 최초 생성 전 대기 시간

        [SerializeField] private GameObject player;

        private CharacterController2D playerController;
        private Collider2D playerCollider;

        // 동시에 여러 벽이 존재할 수 있어서 하나씩 묶어서 관리
        private class Wall
        {
            public GameObject obj;
            public SpriteRenderer renderer;
        }

        private readonly List<Wall> walls = new List<Wall>();
        private float spawnTimer;

        private void Awake()
        {
            if (player == null) player = FindFirstObjectByType<Shifter>().gameObject;
            playerController = player.GetComponent<CharacterController2D>();
            playerCollider = player.GetComponent<Collider2D>();

            spawnTimer = startDelay;
        }

        private void FixedUpdate()
        {
            spawnTimer -= Time.fixedDeltaTime;
            if (spawnTimer <= 0f)
            {
                SpawnWall();
                spawnTimer = spawnInterval;
            }

            for (int i = walls.Count - 1; i >= 0; i--)
            {
                Wall wall = walls[i];

                // 태그가 다르면 막힘 취급 + 반투명하게 표시 (같으면 정상 색)
                bool blocked = !player.CompareTag(wall.obj.tag);
                Color color = wall.renderer.color;
                color.a = blocked ? 0.5f : 1f;
                wall.renderer.color = color;

                Vector3 beforeMove = wall.obj.transform.position;
                wall.obj.transform.position = Vector3.MoveTowards(beforeMove, pivot.position, moveSpeed * Time.fixedDeltaTime);
                Vector3 wallDelta = wall.obj.transform.position - beforeMove;

                // 겹쳐있고 태그가 다르면, 플레이어가 앞으로 움직이려 해도 이번 프레임 입력은 무시하고
                // 벽이 움직인 만큼만 그대로 따라 밀리게 함
                if (blocked && wall.renderer.bounds.Intersects(playerCollider.bounds))
                {
                    playerController.SetMoveInput(0f);
                    playerController.AddExternalMovement(wallDelta);
                }

                if (Vector3.Distance(wall.obj.transform.position, pivot.position) < 0.05f)
                {
                    Destroy(wall.obj);
                    walls.RemoveAt(i);
                }
            }
        }

        private void SpawnWall()
        {
            GameObject prefab = (Random.value < 0.5f) ? whiteTilePrefab : blackTilePrefab;
            // 타일 프리팹이 가로로 긴 발판 모양이라, Z축으로 90도 돌려서 세로 벽 형태로 생성
            GameObject obj = Instantiate(prefab, transform.position, Quaternion.Euler(0f, 0f, 90f));

            // 콜라이더를 켜두면 Tile 레이어라 플레이어 자신의 이동 판정과 겹쳐 얼어붙으므로 꺼둠
            BoxCollider2D col = obj.GetComponent<BoxCollider2D>();
            col.enabled = false;

            // Rigidbody2D가 있어야 PlatformManager가 이 오브젝트를 관리 대상에서 제외함
            // (없으면 PlatformManager가 태그 기준으로 콜라이더를 다시 켜버려서 위 설정과 충돌함)
            Rigidbody2D rb = obj.AddComponent<Rigidbody2D>();
            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.gravityScale = 0f;

            walls.Add(new Wall { obj = obj, renderer = obj.GetComponent<SpriteRenderer>() });
        }
    }
}
