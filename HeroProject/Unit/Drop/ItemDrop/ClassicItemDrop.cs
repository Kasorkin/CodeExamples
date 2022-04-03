using UnityEngine;

using BaseGameLogic.Managers;

namespace BaseGameLogic.Drop
{
    [System.Serializable]
    public sealed class ClassicItemDrop : ItemDrop
    {
        [Header("Конкретный предмет")]
        [SerializeField]
        private GameObject _item;

        protected sealed override void Dropping()
        {
            Debug.Log("Происходит спавн");
            GameManager.Singleton.InstantiateManager.Create(_item.gameObject, _dropPos);
        }
    }
}