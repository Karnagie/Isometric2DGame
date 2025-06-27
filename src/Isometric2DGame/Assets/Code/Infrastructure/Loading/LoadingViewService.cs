using Code.Common.StaticData;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Loading
{
    public class LoadingViewService : ILoadingViewService
    {
        private readonly IStaticDataService _staticDataService;
        private readonly IInstantiator _instantiator;

        private LoadingScreen _screen;

        public LoadingViewService(IStaticDataService staticDataService, IInstantiator instantiator)
        {
            _instantiator = instantiator;
            _staticDataService = staticDataService;
        }

        public void Init()
        {
            PrepareLoadingScreen();
        }

        public void ShowScreen()
        {
            _screen.On();
        }

        public void HideScreen()
        {
            _screen.Off();
        }

        private void PrepareLoadingScreen()
        {
            var prefab = _staticDataService.GetLoadingScreenPrefab();

            _screen = _instantiator.InstantiatePrefabForComponent<LoadingScreen>(prefab);
            _screen.Init();
        }
    }
}