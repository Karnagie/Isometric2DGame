using Code.Core.Levels;
using UnityEngine;
using Zenject;

namespace Code.Core.Features.Routing.Behaviours
{
    public class RoutePointRegistrator : MonoBehaviour
    {
        private ILevelDataProvider _levelDataProvider;

        [Inject]
        private void Construct(ILevelDataProvider levelDataProvider)
        {
            _levelDataProvider = levelDataProvider;
        }

        private void Start()
        {
            _levelDataProvider.AddRoutePoint(transform.position);
        }
    }
}