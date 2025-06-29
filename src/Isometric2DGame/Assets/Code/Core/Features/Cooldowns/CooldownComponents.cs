using Entitas;

namespace Code.Core.Features.Cooldowns
{
  [Game] public class Cooldown : IComponent { public float Value; }
  [Game] public class CooldownLeft : IComponent { public float Value; }
  [Game] public class CooldownUp : IComponent { }
  [Game] public class DestructOnCooldownUp : IComponent { }
}