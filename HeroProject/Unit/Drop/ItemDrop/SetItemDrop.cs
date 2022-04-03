using UnityEngine;

using BaseGameLogic.Managers;

namespace BaseGameLogic.Drop
{
    [System.Serializable]
    public sealed class SetItemDrop : ItemDrop
    {
        [Header("Сет предметов")]
        [SerializeField]
        private ItemsDataBase _dataBase;

        protected sealed override void Dropping()
        {
            GameManager.Singleton.InstantiateManager.Create(_dataBase.GetRandomItem(), _dropPos);
        }
    }
}