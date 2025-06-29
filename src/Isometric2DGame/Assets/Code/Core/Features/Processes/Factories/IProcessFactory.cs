namespace Code.Core.Features.Processes.Factories
{
    public interface IProcessFactory
    {
        GameEntity Damage(int targetId, ProcessSetup setup);
        GameEntity[] Process(string process, int targetId, ProcessSetup setup);
    }
}