using UnityEngine;

namespace SHIFTER
{
    public class BulletCountStart : MonoBehaviour
    {
        public GameObject[] Launchers;
        [SerializeField] private GameObject CountPivot;
        private bool StartGame = false;


        private void OnTriggerEnter2D(Collider2D collision)
        {
            for (int i = 0; i < Launchers.Length; i++)
            {
                Launchers[i].SetActive(true);
            }
            CountPivot.SetActive(true);
            StartGame = true;
        }
    }
}
