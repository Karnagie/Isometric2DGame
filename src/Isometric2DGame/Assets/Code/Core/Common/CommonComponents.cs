using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace Code.Core.Common
{
  [Game, Meta] public class Id : IComponent { [PrimaryEntityIndex] public int Value; }
  [Game] public class EntityLink : IComponent { [EntityIndex] public int Value; }
  
  [Game] public class Active : IComponent { }
}