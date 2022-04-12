namespace GameAbilitySystem
{
    public interface IPassiveAbility : IAbility
    {
        public void OnEnable();

        public void OnDisable();
    }
}