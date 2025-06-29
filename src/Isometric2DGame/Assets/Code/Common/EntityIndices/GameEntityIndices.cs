using System.Collections.Generic;
using Code.Core.Features.Movement;
using Code.Core.Features.Stats;
using Code.Core.Features.Stats.Indexing;
using Entitas;
using Zenject;

namespace Code.Common.EntityIndices
{
  public class GameEntityIndices : IInitializable
  {
    private readonly GameContext _game;
    
    public const string StatChanges = "StatChanges";

    public GameEntityIndices(GameContext game)
    {
      _game = game;
    }

    public void Initialize()
    {
      _game.AddEntityIndex(new EntityIndex<GameEntity, StatKey>(
        name: StatChanges,
        _game.GetGroup(GameMatcher.AllOf(
          GameMatcher.StatChange,
          GameMatcher.Value,
          GameMatcher.TargetId
          )),
        getKey: GetTargetStatsKey,
        new StatKeyEqualityComparer()
        ));
    }

    private StatKey GetTargetStatsKey(GameEntity entity, IComponent component)
    {
      return new StatKey(
        (component as TargetId)?.Value ?? entity.TargetId,
        (component as StatChange)?.Value ?? entity.StatChange
      );
    }
  }

  public static class ContextIndicesExtensions
  {
    public static HashSet<GameEntity> TargetStatChanges(this GameContext context, StatId statId, int targetId)
    {
      return ((EntityIndex<GameEntity, StatKey>) context.GetEntityIndex(GameEntityIndices.StatChanges))
        .GetEntities(new StatKey(targetId, statId));
    }
  }
}