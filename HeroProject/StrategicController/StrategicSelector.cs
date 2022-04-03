using System.Collections.Generic;
using UnityEngine;

using BaseGameLogic.Player;

namespace StrategicManagement
{
    //TODO : После добавления предметов заменить заглушку предмета
    public sealed class StrategicSelector
    {
        public GameObject FindItemInBox(in Vector2 origin, in Vector2 size)
        {
            List<GameObject> foundObjects = FindObjectsInBox(origin, size);

            return FindItemObject(foundObjects);
        }

        public GameObject FindItemObject(in List<GameObject> foundObjects)
        {
            foreach(var k in foundObjects)
            {
                //если юнит не предмет, то выбросить из списка
            }

            return null;
        }

        public List<UnitController> FindUnitsInBox(in Vector2 origin, in Vector2 size)
        {
            List<GameObject> foundObjects = FindObjectsInBox(origin, size);

            return FindUnits(foundObjects);
        }

        public List<UnitController> FindUnitsInBoxForOwner(in Vector2 origin, in Vector2 size, in PlayerData owner)
        {
            List<UnitController> allUnits = FindUnitsInBox(origin, size);

            /*for (int i = allUnits.Count - 1; i >= 0; --i)
            {
                if (allUnits[i].Owner != owner)
                    allUnits.RemoveAt(i);
            }*/

            return allUnits;
        }

        public List<UnitController> FindUnits(in List<GameObject> gameObjects)
        {
            List<UnitController> units = new List<UnitController>();

            foreach (var k in gameObjects)
            {
                if (k.TryGetComponent(out UnitController unit))
                {
                    units.Add(unit);
                }
            }

            return units;
        }

        public List<GameObject> FindObjectsInBox(in Vector2 origin, in Vector2 size)
        {
            RaycastHit2D[] raycasts = Physics2D.BoxCastAll(origin, size, 0f, Vector2.zero);

            List<GameObject> foundObjects = new List<GameObject>();

            foreach (var k in raycasts)
            {
                foundObjects.Add(k.transform.gameObject);
            }

            return foundObjects;
        }
    }
}