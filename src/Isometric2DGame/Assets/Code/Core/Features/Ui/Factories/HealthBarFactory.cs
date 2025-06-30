using Code.Common.Entity;
using Code.Common.Extensions;
using Code.Infrastructure.Identifiers;
using UnityEngine;

namespace Code.Core.Features.Ui.Factories
{
    public class HealthBarFactory : IHealthBarFactory
    {
        private readonly IIdentifierService _identifiers;

        public HealthBarFactory(IIdentifierService identifiers)
        {
            _identifiers = identifiers;
        }

        public GameEntity Create(Vector3 at, int targetId)
        {
            return CreateEntity.Empty()
                    .AddId(_identifiers.Next())
                    .With(x => x.isHealth = true)
                    
                    .AddWorldPosition(at)
                    .AddViewPath("UI/healthBar")
                    .AddEntityLink(targetId)
                
                    .AddOffset(new Vector3(0, 1.0f, 0))
                    .AddTargetId(targetId)
                ;
        }
    }
}