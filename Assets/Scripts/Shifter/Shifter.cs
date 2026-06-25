using UnityEngine;
using System.Collections.Generic;

namespace SHIFTER
{
    public class Shifter : MonoBehaviour
    {
        private const int MaxHp = 100;
        public int Hp;

        private const string TAG_COMMON = "Common";
        private const string TAG_WHITE = "White";
        private const string TAG_BLACK = "Black";
        private const string TAG_RED = "red";

        public string currentTag { get; private set; }

        private void Start()
        {
            Hp = MaxHp;
            gameObject.tag = TAG_COMMON;
        }

        public void ShiftTag()
        {
            if (gameObject.CompareTag(TAG_COMMON) || gameObject.CompareTag(TAG_BLACK)) gameObject.tag = TAG_WHITE;
            else gameObject.tag = TAG_BLACK;

            currentTag = gameObject.tag;
        }
    } 
}
