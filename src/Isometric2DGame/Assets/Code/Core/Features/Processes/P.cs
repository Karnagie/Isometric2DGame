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
    }
}