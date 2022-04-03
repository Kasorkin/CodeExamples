using System;

namespace Hero
{
    //TODO : Может покрасивше сделаешь?
    public sealed class HeroLevel
    {
        public event Action LevelChanged;
        public event Action<HeroLevel> ExperienceChanged;

        public int ExperienceForNextLevel { get; private set; } = HeroConsts.ExperienceForLevelTwo;
        public int CurrentExperience { get; private set; } = 0;
        public int CurrentLevel { get; private set; } = 1;

        public void AddExperience(int value)
        {
             if (CurrentLevel == HeroConsts.MaxLevel)
                return;

            int restOfExperience = value;  
            do
            {
                int restForNextLevel = ExperienceForNextLevel - CurrentExperience;
                restOfExperience -= restForNextLevel;

                if (restOfExperience < 0)
                {
                    ChangeExperience(value);
                    break;
                }
                else
                {
                    LevelUp();
                    value = restOfExperience;

                    if (CurrentLevel == HeroConsts.MaxLevel)
                        break;
                }
            } while (true);
        }

        private void ChangeExperience(in int value)
        {
            CurrentExperience += value;
            //Debug.Log("Текущее значение опыта = " + _currentExperience);
            ExperienceChanged?.Invoke(this);
        }

        private void LevelUp()
        {
            CurrentLevel += 1;
            CurrentExperience = ExperienceForNextLevel;

            //Debug.Log("Уровень увеличен и сейчас = " + _currentLevel);
            if (CurrentLevel != HeroConsts.MaxLevel)
                ChangeExperienceForNextLevel();

            ExperienceChanged?.Invoke(this);
            LevelChanged?.Invoke();
        }

        private void ChangeExperienceForNextLevel()
        {
            ExperienceForNextLevel += (CurrentLevel + 1) * HeroConsts.ExperienceGrowthFactor;
            //Debug.Log("Количество опыта до следующего уровня = " + _experienceForNextLevel);
            //Debug.Log("Количество опыта сейчас = " + _currentExperience);
        }
    }
}