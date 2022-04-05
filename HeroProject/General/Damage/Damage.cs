namespace BaseGameLogic.DamageSystem
{
    //TODO : —юда нужно еще добавить эффекты
    public class Damage
    {
        public readonly float BasePhysicalDamage;
        public readonly float BaseMagicalDamage;
        public readonly float BasePureDamage;

        public float PhysicalDamage;
        public float MagicalDamage;
        public float PureDamage;

        public Damage(UnitData ownerOfDamage, float physicalDamage, float magicalDamage = 0, float pureDamage = 0)
        {
            OwnerOfDamage = ownerOfDamage;

            BasePhysicalDamage = physicalDamage;
            BaseMagicalDamage = magicalDamage;
            BasePureDamage = pureDamage;

            PhysicalDamage = physicalDamage;
            MagicalDamage = magicalDamage;
            PureDamage = pureDamage;
        }

        public Damage(UnitData ownerOfDamage, DamageData damageData)
        {
            OwnerOfDamage = ownerOfDamage;

            BasePhysicalDamage = damageData.PhysicalDamage;
            BaseMagicalDamage = damageData.MagicalDamage;
            BasePureDamage = damageData.PureDamage;

            PhysicalDamage = damageData.PhysicalDamage;
            MagicalDamage = damageData.MagicalDamage;
            PureDamage = damageData.PureDamage;
        }

        public UnitData OwnerOfDamage { get; private set; }
    }
}