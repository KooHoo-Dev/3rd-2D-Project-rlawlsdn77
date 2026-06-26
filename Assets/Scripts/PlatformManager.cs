using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private BoxCollider2D[] allColliders;

    private void Awake()
    {
        allColliders = GetComponentsInChildren<BoxCollider2D>(true);
    }

    private void Update()
    {
        ColliderControll();
    }

    private void ColliderControll()
    {
        foreach (BoxCollider2D col in allColliders)
        {
            GameObject obj = col.gameObject;
 

            // 태그 조건 검사
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
}