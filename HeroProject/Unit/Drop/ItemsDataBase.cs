using System.Collections.Generic;
using UnityEngine;

namespace BaseGameLogic.Drop
{
    [CreateAssetMenu(fileName = "New itemsDataBase", menuName = "Drop/ItemsDataBase")]
    //[System.Serializable]
    public sealed class ItemsDataBase : ScriptableObject
    {
        [Header("Список предметов")]
        [SerializeField]
        private List<GameObject> _items = new List<GameObject>();

        public GameObject GetRandomItem()
        {
            int index = Random.Range(0, _items.Count - 1);
            return _items[index];
        }
    }
}