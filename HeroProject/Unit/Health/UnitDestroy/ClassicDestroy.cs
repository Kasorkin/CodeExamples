using UnityEngine;

namespace BaseGameLogic
{
    public class ClassicDestroy : IDestroyible
    {
        void IDestroyible.Destroy(in Transform obj)
        {
            Object.Destroy(obj.gameObject);
        }
    }
}