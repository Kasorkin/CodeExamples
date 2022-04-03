using UnityEngine;

namespace BaseGameLogic.Drop
{
    public abstract class ItemDrop
    {
        private enum ChanceDropType { Single, Common };

        [Header("Общие настройки")]
        [SerializeField, Min(1)]
        private int _count;
        [SerializeField]
        private ChanceDropType _chanceDropType = ChanceDropType.Single;
        [SerializeField, Range(0, 100)]
        private int _chance;

        protected Vector2 _dropPos;

        protected abstract void Dropping();

        public void Drop(in Vector2 pos)
        {
            _dropPos = pos;

            if (_chanceDropType == ChanceDropType.Single)
                SingleDrop();
            else
                CommonDrop();
        }

        private void SingleDrop()
        {
            for (int i = 0; i < _count; ++i)
            {
                if (IsChanceSuccessfull())
                {
                    Dropping();
                }
            }
        }

        private void CommonDrop()
        {
            if (IsChanceSuccessfull())
            {
                for (int i = 0; i < _count; ++i)
                {
                    Dropping();
                }
            }
        }

        private bool IsChanceSuccessfull()
        {
            return Random.Range(1, 100) <= _chance;
        }
    }
}