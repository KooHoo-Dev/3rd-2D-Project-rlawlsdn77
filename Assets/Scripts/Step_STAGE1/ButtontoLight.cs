using UnityEngine;

namespace SHIFTER
{
    // STAGE 1 STEP 2 버튼 퍼즐 (버튼 밟을 시 해당 버튼에 할당된 전구 빛 점등)
    // 이미 켜진 전등은 꺼짐
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