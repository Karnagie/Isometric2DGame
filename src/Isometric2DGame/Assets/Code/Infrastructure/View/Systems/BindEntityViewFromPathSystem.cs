using System.Collections.Generic;
using Code.Infrastructure.View.Factory;
using Entitas;
using UnityEngine;

namespace Code.Infrastructure.View.Systems
{
  public class BindEntityViewFromPathSystem : IExecuteSystem
  {
    private readonly IEntityViewFactory _entityViewFactory;
    private readonly IGroup<GameEntity> _entities;
    private readonly List<GameEntity> _buffer = new(32);

    public BindEntityViewFromPathSystem(GameContext game, IEntityViewFactory entityViewFactory)
    {
      _entityViewFactory = entityViewFactory;
      _entities = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.ViewPath)
        .NoneOf(GameMatcher.View));
    }

    public void Execute()
    {
      foreach (GameEntity entity in _entities.GetEntities(_buffer))
      {
        var parent = entity.hasViewParent ? entity.ViewParent : null;
        var usePrefabLocalPosition = entity.isUsePrefabLocalPosition ? true : false;
        _entityViewFactory.CreateViewForEntity(entity, parent, usePrefabLocalPosition);
      }
    }
  }
}