using System.Collections;
using UnityEngine;

namespace BaseGameLogic.Managers
{
    [DisallowMultipleComponent]
    public sealed class CoroutineManager : MonoBehaviour
    {
        public new void StartCoroutine(IEnumerator coroutine)
        {
            base.StartCoroutine(coroutine);
        }

        public new void StopCoroutine(IEnumerator coroutine)
        {
            if (coroutine == null)
                return;

            base.StopCoroutine(coroutine);
        }

        public void StartCoroutine(IEnumerator coroutine, ref Coroutine routine)
        {
            routine = base.StartCoroutine(coroutine);
        }

        public void StopCoroutine(ref Coroutine routine)
        {
            if (routine == null)
                return;

            StopCoroutine(routine);
            routine = null;
        }
    }
}