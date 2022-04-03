namespace BaseGameLogic
{
    public enum DamageType
    {
        Physical = 0,
        Magical = 1,
        Pure = 2
    };

    public enum ArmorType
    {
        Physical = 0,
        Magical = 1
    };

    public enum Attribute
    {
        Health = 0,
        Regeneration = 1,
        Damage = 2,
        AttackSpeed = 3,
        AttackRange = 4,
        PhysicArmor = 5,
        MagicArmor = 6,
        MoveSpeed = 7,
        Mana = 8,
        ManaRegeneration = 9,
        Strength = 10,
        Agility = 11,
        Intelligence = 12
    };

    //TODO : Перенести
    public enum Fraction
    {
        Light,
        Dark,
        Neutral
    };

    public enum UnitType
    {
        Unit,
        Boss
    };

    public enum UnitAgressionType
    {
        Agressive,
        Passive
    };
}