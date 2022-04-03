using UnityEngine;

namespace BaseGameLogic.Managers
{
    [DisallowMultipleComponent]
    public sealed class InstantiateManager : MonoBehaviour
    {
        public void Create(GameObject obj, Vector2 pos)
        {
            Instantiate(obj, pos, Quaternion.identity);
        }
    }
}