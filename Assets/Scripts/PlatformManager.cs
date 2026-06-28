using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private BoxCollider2D[] allColliders;
    private SpriteRenderer[] allSprites;

    private void Awake()
    {
        // 자식 오브젝트들의 콜라이더와 스프라이트 렌더러를 미리 캐싱합니다.
        allColliders = GetComponentsInChildren<BoxCollider2D>(true);
        allSprites = GetComponentsInChildren<SpriteRenderer>(true);
    }

    private void Update()
    {
        ColliderControll();
        AlphaControll(); 
    }

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

    private void AlphaControll()
    {
        string playerTag = player.tag;

        foreach (SpriteRenderer sprite in allSprites)
        {
            if (sprite == null) continue;

            GameObject obj = sprite.gameObject;
            Color color = sprite.color;


            // 다른 태그일때 알파값 0.3
            if ((playerTag == "Black" && obj.CompareTag("White")) || (playerTag == "White" && obj.CompareTag("Black")))
            {
                color.a = 0.3f;
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