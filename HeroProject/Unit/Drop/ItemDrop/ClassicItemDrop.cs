using UnityEngine;

using BaseGameLogic.Managers;

namespace BaseGameLogic.Drop
{
    [System.Serializable]
    public sealed class ClassicItemDrop : ItemDrop
    {
        [Header("���������� �������")]
        [SerializeField]
        private GameObject _item;

        protected sealed override void Dropping()
        {
            Debug.Log("���������� �����");
            GameManager.Singleton.InstantiateManager.Create(_item.gameObject, _dropPos);
        }
    }
}