using System.Collections.Generic;

namespace GameAbilitySystem
{
    public static class InfluenceEnumExtensions
    {
        public static List<int> ReturnSelectedElements(FractionInfluence fractionInfluence)
        {
            List<int> selectedElements = new List<int>();
            for (int i = 0; i < System.Enum.GetValues(typeof(FractionInfluence)).Length; i++)
            {
                int layer = 1 << i;
                if (((int)fractionInfluence & layer) != 0)
                {
                    selectedElements.Add(i);
                }
            }
            return selectedElements;
        }

        public static List<int> ReturnSelectedElements(UnitTypeInfluence unitTypeInfluence)
        {
            List<int> selectedElements = new List<int>();
            for (int i = 0; i < System.Enum.GetValues(typeof(UnitTypeInfluence)).Length; i++)
            {
                int layer = 1 << i;
                if (((int)unitTypeInfluence & layer) != 0)
                {
                    selectedElements.Add(i);
                }
            }
            return selectedElements;
        }
    }
}