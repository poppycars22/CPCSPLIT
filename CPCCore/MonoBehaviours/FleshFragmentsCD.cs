using CPCCore.Extensions;
using UnityEngine;

namespace CPCCore.MonoBehaviours
{
    public class FleshFragmentsCD : MonoBehaviour
    {
        public float duration = 0;
        public void Update()
        {
            if (!(duration <= 0))
            {
                duration -= TimeHandler.deltaTime;
            }
        }

    }
}
