using Entitas;

namespace Code.Core.Features.Enemies
{
    [Game] public class Enemy : IComponent { }
    [Game] public class AttackRadius : IComponent { public float Value; }
}