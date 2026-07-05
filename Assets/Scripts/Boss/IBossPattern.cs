using System.Collections;
using UnityEngine;

namespace SHIFTER
{
    // 보스 패턴 인터페이스
    public interface IBossPattern
    {
        void StartPattern();
        void EndPattern();
    }
}
