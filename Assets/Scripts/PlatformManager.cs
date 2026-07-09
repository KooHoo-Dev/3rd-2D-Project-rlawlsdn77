using UnityEngine;
using System.Collections.Generic;

namespace SHIFTER
{
    public class PlatformManager : MonoBehaviour
    {
        [SerializeField] private GameObject player;

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
        private void RefreshTargets()
        {
            allColliders.Clear();
            allSprites.Clear();

            foreach (string tag in ManagedTags)
            {
                GameObject[] objs = GameObject.FindGameObjectsWithTag(tag);
                foreach (GameObject obj in objs)
                {
                    if (obj.GetComponent<Rigidbody2D>() != null) continue;

                    BoxCollider2D col = obj.GetComponent<BoxCollider2D>();
                    if (col != null) allColliders.Add(col);

                    SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
                    if (sr != null) allSprites.Add(sr);
                }
            }
        }

        // 콜라이더 On/Off
        private void ColliderControll()
        {
            foreach (BoxCollider2D col in allColliders)
            {
                GameObject obj = col.gameObject;

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

        // 태그 불일치 시 반투명 처리
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
