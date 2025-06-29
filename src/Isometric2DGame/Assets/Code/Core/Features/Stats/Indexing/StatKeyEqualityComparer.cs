using System;
using System.Collections.Generic;

namespace Code.Core.Features.Stats.Indexing
{
  public class StatKeyEqualityComparer : IEqualityComparer<StatKey>
  {
    public bool Equals(StatKey x, StatKey y)
    {
      return x.TargetId == y.TargetId && x.StatId == y.StatId;
    }

    public int GetHashCode(StatKey obj)
    {
      return HashCode.Combine(obj.TargetId, (int) obj.StatId);
    }
  }
}