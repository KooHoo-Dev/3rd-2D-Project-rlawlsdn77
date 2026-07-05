using UnityEngine;

namespace SHIFTER
{
    // 보스 패턴 공격 오브젝트의 태그 / 색상 관리
    // 속성 3종: Common(회색) / White(흰색) / Black(검은색)

    public static class BossAttributeUtil
    {
        public const string COMMON = "Common";
        public const string WHITE = "White";
        public const string BLACK = "Black";

        // 전체 속성 목록
        public static readonly string[] AllTags = { COMMON, WHITE, BLACK };

        // 실제 플레이어 변환 속성 목록 
        public static readonly string[] ShiftableTags = { WHITE, BLACK };

        // 태그에 대응하는 색상 반환 (black 태그이면 검정색, white 태그이면 흰색)
        public static Color GetColor(string tag)
        {
            switch (tag)
            {
                case BLACK:
                    return Color.black;
                case WHITE:
                    return Color.white;
                case COMMON:
                default:
                    return new Color(0.745283f, 0.745283f, 0.745283f, 1f);
            }
        }

        // 전체 속성 태그 랜덤 (3개 중 1개)
        public static string RandAllTag()
        {
            return AllTags[Random.Range(0, AllTags.Length)];
        }

        // 플레이어 변환 태그 랜덤 (2개 중 1개)
        public static string RandShiftTag()
        {
            return ShiftableTags[Random.Range(0, ShiftableTags.Length)];
        }

        // 태그에 맞는 색상 적용
        public static void Apply(GameObject obj, string tag)
        {
            obj.tag = tag;

            SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
            sr.color = GetColor(tag);
        }
    }
}
