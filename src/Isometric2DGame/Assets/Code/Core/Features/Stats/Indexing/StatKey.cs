namespace Code.Core.Features.Stats.Indexing
{
  public struct StatKey
  {
    public readonly int TargetId;
    public readonly StatId StatId;

    public StatKey(int targetId, StatId statId)
    {
      TargetId = targetId;
      StatId = statId;
    }
  }
}