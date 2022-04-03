namespace BaseGameLogic
{
    //TODO : —юда нужно еще добавить эффекты
    public class Damage
    {
        public readonly float BasePhysicalDamage;
        public readonly float BaseMagicalDamage;

        public float PhysicalDamage;
        public float MagicalDamage;

        public Damage(UnitData ownerOfDamage, float physicalDamage, float magicalDamage = 0)
        {
            OwnerOfDamage = ownerOfDamage;

            BasePhysicalDamage = physicalDamage;
            BaseMagicalDamage = magicalDamage;

            PhysicalDamage = physicalDamage;
            MagicalDamage = magicalDamage;
        }

        public UnitData OwnerOfDamage { get; private set; }
    }
}