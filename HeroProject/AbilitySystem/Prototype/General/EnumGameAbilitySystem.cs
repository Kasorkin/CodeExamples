namespace GameAbilitySystem
{
    public enum FractionInfluence
    {
        Self,
        Ally,
        Enemy
    };

    public enum UnitTypeInfluence
    {
        Self,
        Unit,
        Building,
        Hero,
        Enemy
    }

    public enum DurationType
    {
        Instant = 0,
        Durational = 1
    };

    public enum CooldownType
    {
        NoCooldown = 0,
        Cooldown = 1,
        Charges = 2
    };

    public enum CostType
    {
        NoCost = 0,
        Mana = 1,
        PercentMana = 2,
        Health = 3,
        PercentHealth = 4,
        Unique = 5,
        PercentUnique = 6
    };
}