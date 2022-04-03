namespace Hero
{
    public static class HeroConsts
    {
        public const float MainAttributeDamageFactor = 1.5f;
        
        public const float AgilityAttackSpeedValue = 0.01f;
        public const float AgilityArmorValue = 0.25f;

        public const float StrengthRegenerationValue = 0.1f;
        public const float StrengthMaxHealthValue = 17f;

        public const float IntelligenceManaRegenerationValue = 0.03f;
        public const float IntelligenceMaxManaValue = 15f;

        public const float BaseTimeToRespawn = 10f;
        public const float GrowthTimeToRespawnPerLevel = 2f;

        public const int ExperienceForLevelTwo = 200;
        public const int ExperienceGrowthFactor = 100;
        public const int MaxLevel = 100;
    }

    public enum MainAttribute
    {
        Strength,
        Agility,
        Intelligence
    };
}