using System;
using UnityEngine;

using BaseGameLogic.Drop;
using BaseGameLogic.Player;

namespace BaseGameLogic.Managers
{
    [DisallowMultipleComponent, RequireComponent(typeof(CoroutineManager), typeof(InstantiateManager))]
    public sealed class GameManager : MonoBehaviour
    {
        public event Action GlobalRegeneration;

        public static GameManager Singleton { get; private set; }

        public CoroutineManager CoroutineManager { get; private set; }
        public PlayerManager PlayerManager { get; private set; }
        public InstantiateManager InstantiateManager { get; private set; }

        private void Awake()
        {
            PlayerManager = new PlayerManager();
            CoroutineManager = GetComponent<CoroutineManager>();
            InstantiateManager = GetComponent<InstantiateManager>();
            Singleton = this;
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            Distribution();
        }

        private void FixedUpdate()
        {
            GlobalRegeneration?.Invoke();
        }

        private void Distribution()
        {
            UnitData[] units = FindObjectsOfType<UnitData>();

            foreach(var k in units)
            {
                PlayerManager.Distributor(k);
            }
        }
    }
}