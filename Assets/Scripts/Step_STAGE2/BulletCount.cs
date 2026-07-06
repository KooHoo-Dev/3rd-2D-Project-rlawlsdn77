using UnityEngine;

namespace SHIFTER
{
    public class BulletCount : MonoBehaviour
    {
        public int CountBlack { get; private set; } = 0;
        public int CountWhite { get; private set; } = 0;
        public int CountBullet { get; private set; } = 0;

        private BulletLauncher BulletLauncher;

        private BulletCountStart Step_2_1;

        private void Update()
        {

        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Black")) CountBlack++;
            if (collision.CompareTag("White")) CountWhite++;

            if (CountBlack + CountWhite == Step_2_1.Launchers.Length) CountBullet++;
        }
    }
}
