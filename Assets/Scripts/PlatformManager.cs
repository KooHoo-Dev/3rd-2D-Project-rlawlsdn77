using UnityEngine;
using System.Collections.Generic;

namespace SHIFTER
{
    public class PlatformManager : MonoBehaviour
    {
        [SerializeField] private GameObject player;

        // 레벨에 미리 배치된 타일(자식 계층)뿐 아니라, 보스전 중 런타임에 Instantiate()되는
        // 발판까지 전부 관리하기 위해 "자식 캐싱" 대신 매 프레임 태그를 기준으로 다시 찾습니다.
        private static readonly string[] ManagedTags = { "Common", "White", "Black", "Red" };

        private readonly List<BoxCollider2D> allColliders = new List<BoxCollider2D>();
        private readonly List<SpriteRenderer> allSprites = new List<SpriteRenderer>();

        private void Update()
        {
            RefreshTargets();
            ColliderControll();
            AlphaControll();
        }

        // 태그(Common/White/Black/Red)를 기준으로 관리 대상 콜라이더/스프라이트를 갱신합니다.
        // 계층 구조와 무관하게 동작하므로 보스전 중 새로 생성된 발판도 즉시 포함됩니다.
        private void RefreshTargets()
        {
            allColliders.Clear();
            allSprites.Clear();

            foreach (string tag in ManagedTags)
            {
                GameObject[] objs = GameObject.FindGameObjectsWithTag(tag);
                foreach (GameObject obj in objs)
                {
                    // 보스 패턴이 발사하는 운석/레이저/탄환 등은 Rigidbody2D를 가진 이동체이며,
                    // 발판(플랫폼)이 아니므로 관리 대상에서 제외합니다. (색상/충돌은 각 공격 스크립트가 직접 관리)
                    if (obj.GetComponent<Rigidbody2D>() != null) continue;

                    BoxCollider2D col = obj.GetComponent<BoxCollider2D>();
                    if (col != null) allColliders.Add(col);

                    SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
                    if (sr != null) allSprites.Add(sr);
                }
            }
        }

        /// <summary>
        /// collider On / Off 코드
        /// </summary>
        private void ColliderControll()
        {
            foreach (BoxCollider2D col in allColliders)
            {
                GameObject obj = col.gameObject;

                // 태그 조건 검사 (기존 로직 유지)
                if (obj.CompareTag("Common") || obj.CompareTag("Red"))
                {
                    col.enabled = true;
                }
                else if (obj.CompareTag(player.tag))
                {
                    col.enabled = true;
                }
                else
                {
                    col.enabled = false;
                }
            }
        }

        /// <summary>
        /// color.a 변경 코드
        /// </summary>
        private void AlphaControll()
        {
            string playerTag = player.tag;

            foreach (SpriteRenderer sprite in allSprites)
            {
                if (sprite == null) continue;

                GameObject obj = sprite.gameObject;
                Color color = sprite.color;


                // 다른 태그일때 알파값 0.5
                if ((playerTag == "Black" && obj.CompareTag("White")) || (playerTag == "White" && obj.CompareTag("Black")))
                {
                    color.a = 0.5f;
                }
                // 그 외는 수정 X
                else
                {
                    color.a = 1.0f;
                }

                // 변경된 컬러 적용
                sprite.color = color;
            }
        }
    }
}
