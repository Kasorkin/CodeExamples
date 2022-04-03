using UnityEngine;

namespace BaseGameLogic
{
    public interface IDestroyible
    {
        public void Destroy(in Transform obj);
    }
}