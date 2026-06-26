using UnityEngine;
using Unity;

namespace SHIFTER
{
    public class Boss : MonoBehaviour
    {
        [field: SerializeField] private int CurrentBossCount = 1;
        private const int MaxBossCount = 3;

        private int rd;

        [field: SerializeField] private Shifter shifter;

        private void Awake()
        {
            CurrentBossCount = MaxBossCount;
            
        }

        private void Update()
        {
            
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            rd = Random.Range(1, 10);
            shifter.Hp -= rd;
            Debug.Log($"{collision.gameObject.name}이 보스에 피격당하여 {rd} 만큼의 데미지를 입었습니다!");
        }

        

    }
}
