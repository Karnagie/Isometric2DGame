namespace Code.Core.Features.Processes
{
    public static class P
    {
        public static ProcessSetup Damage(float value)
        {
            return new ProcessSetup()
            {
                Process = "damage",
                Value = value,
            };
        }

        public static ProcessSetup EnemySpeedUp(float value, float duration)
        {
            return new ProcessSetup()
            {
                Process = "speed-up",
                Value = value,
                Duration = duration
            };
        }
    }
}