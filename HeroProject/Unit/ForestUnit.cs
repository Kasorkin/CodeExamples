using UnityEngine;

namespace BaseGameLogic
{
    [DisallowMultipleComponent, RequireComponent(typeof(Health))]
    public class ForestUnit : MonoBehaviour
    {
        [Header("Тип")]
        [SerializeField]
        private UnitType _unitType;

        [Header("Воскрешение")]
        [SerializeField]
        private bool _isRespawn;
        [SerializeField]
        private bool _isUniqueTime;
        [SerializeField]
        private float _uniqueTime;

        [Header("Настройки агрессивности")]
        [SerializeField]
        private UnitAgressionType _agressionType = UnitAgressionType.Agressive;

        private void Start()
        {
            Init();
        }

        private void Init()
        {
            if (_isRespawn == false)
                return;

            IDestroyible destroyible;
            if (_isUniqueTime)
            {
                destroyible = new RespawnDestoy(_uniqueTime);
            }
            else
            {
                if (_unitType == UnitType.Boss)
                    destroyible = new RespawnDestoy(Consts.TimeToCreepBossRespawn);
                else
                    destroyible = new RespawnDestoy(Consts.TimeToCreepRespawn);
            }

            GetComponent<Health>().Death.SetDestroyible(destroyible);
        }
    }
}