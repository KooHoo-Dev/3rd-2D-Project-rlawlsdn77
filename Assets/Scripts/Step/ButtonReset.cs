using UnityEngine;

namespace SHIFTER
{
    // ResetButton에 삽입되는 스크립트로,
    // 이 버튼을 누를 시 모든 전구가 초기화된다.
    // 또한 전구가 모두 Green일 시 문을 여는 역할도 담당한다.
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
            if (collision != null)
            {
                ResetLights();
            }
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
                Debug.Log("모든 전구가 Green입니다. 문이 열립니다.");
            }
        }
    }
}