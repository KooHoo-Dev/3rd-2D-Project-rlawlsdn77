using UnityEngine;

namespace SHIFTER
{
    // Button 오브젝트에 삽입되는 스크립트로,
    // 플레이어가 버튼을 누를 시 각 Button에 해당되는 Light 오브젝트의 색상이 White이면 Green / Green이면 White로 변환
    public class ButtontoLight : MonoBehaviour
    {
        [SerializeField] private GameObject[] Light;
        private SpriteRenderer[] LightColor;

        private void Awake()
        {
            LightColor = new SpriteRenderer[Light.Length];

            for (int i = 0; i < Light.Length; i++)
            {
                LightColor[i] = Light[i].GetComponent<SpriteRenderer>();
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            for (int i = 0; i < LightColor.Length; i++)
            {
                if (LightColor[i] == null) continue;

                // 현재 색상이 초록색이면 하얀색으로, 아니면 초록색으로 변경
                LightColor[i].color = (LightColor[i].color == Color.green) ? Color.white : Color.green;
            }
        }
    }
}