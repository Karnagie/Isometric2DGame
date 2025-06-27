using Code.Core.Common.Layers;
using Code.Infrastructure.View.Registrars;
using UnityEngine;

namespace Code.Core.Common.Registrars
{
    public class LayerRegistrar : EntityComponentRegistrar
    {
        public Layer Layer;

        public override void RegisterComponents()
        {
            Entity.AddLayer(Layer);
        }

        public override void UnregisterComponents()
        {
            if (Entity.hasLayer)
                Entity.RemoveLayer();
        }
    }
}