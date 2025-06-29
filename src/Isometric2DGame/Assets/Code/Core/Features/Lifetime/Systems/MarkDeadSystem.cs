using System.Collections.Generic;
using Code.Core.Features.Cooldowns;
using DG.Tweening;
using Entitas;
using UnityEngine;

namespace Code.Core.Features.Lifetime.Systems
{
  public class MarkDeadSystem : IExecuteSystem
  {
    private readonly IGroup<GameEntity> _entities;
    private readonly List<GameEntity> _buffer = new(128);

    public MarkDeadSystem(GameContext game)
    {
      _entities = game.GetGroup(GameMatcher
        .AllOf(
          GameMatcher.CurrentHp,
          GameMatcher.MaxHp)
        .NoneOf(GameMatcher.Dead));
    }

    public void Execute()
    {
      foreach (GameEntity entity in _entities.GetEntities(_buffer))
      {
        if (entity.CurrentHp <= 0)
        {
          entity.isDead = true;
          entity.isProcessingDeath = true;

          entity.isMovementAvailable = false;

          if (entity.hasTransform)
          {
            entity.PutOnCooldown(1);
            entity.Transform.DOScale(Vector3.zero, 1f);
          }
          else
            entity.isDestructed = true;
        }
      }
    }
  }
}