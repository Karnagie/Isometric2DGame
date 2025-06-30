using Code.Core.Features.Ui.Systems;
using Code.Infrastructure.Systems;

namespace Code.Core.Features.Ui
{
    public sealed class UiFeature : Feature
    {
        public UiFeature(ISystemFactory systems)
        {
            Add(systems.Create<UpdateNameBarSystem>());
        }
    }
}