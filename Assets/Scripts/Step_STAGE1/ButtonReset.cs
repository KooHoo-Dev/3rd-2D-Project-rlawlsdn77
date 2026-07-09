using UnityEngine;

namespace SHIFTER
{
    // STAGE 1 STEP 2 퍼즐 (버튼 초기화 or 퍼즐 성공 시 이벤트 매니징)
    public class ButtonReset : MonoBehaviour
    {
        [SerializeField] private GameObject[] Light;
        [SerializeField] private GameObject Door2;

        private SpriteRenderer[] LightColor;
        private int AllGreen;

        private void Awake()
        {
            LightColor = new SpriteRenderer[Light.Length];
            AllGreen = Light.Length;

            for (int i = 0; i < Light.Length; i++)
            {
                LightColor[i] = Light[i].GetComponent<SpriteRenderer>();
            }
        }

        private void Update()
        {
            if (Door2.activeSelf)
            {
                CheckAllGreen();
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            ResetLights();
        }

        // 전구 초기화 로직 분리
        private void ResetLights()
        {
            for (int i = 0; i < LightColor.Length; i++)
            {
                LightColor[i].color = Color.white;
            }
        }

        private void CheckAllGreen()
        {
            int countGreen = 0;

            for (int i = 0; i < LightColor.Length; i++)
            {
                if (LightColor[i].color == Color.green)
                {
                    countGreen++;
                }
            }

            if (AllGreen == countGreen)
            {
                Door2.SetActive(false);
            }
        }
    }
}