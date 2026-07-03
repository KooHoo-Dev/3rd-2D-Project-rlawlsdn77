using System;
using UnityEngine;

namespace SHIFTER
{
    // Step 1
    // Box 오브젝트를 BoxGoal에 넣으면 Box 오브젝트가 사라지고 door_1이 열리도록 하는 스크립트
    public class BoxGoal : MonoBehaviour
    {
        [SerializeField] private GameObject door_1; 
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Box"))
            {
                Destroy(collision.gameObject);
                door_1.SetActive(false);
            }
        }
    }
}
