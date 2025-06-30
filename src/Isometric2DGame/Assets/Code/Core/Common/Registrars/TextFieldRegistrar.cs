using Code.Infrastructure.View.Registrars;
using TMPro;

namespace Code.Core.Common.Registrars
{
    public class TextFieldRegistrar : EntityComponentRegistrar
    {
        public TMP_Text _field;
    
        public override void RegisterComponents()
        {
            Entity.AddTextField(_field);
        }

        public override void UnregisterComponents()
        {
            if (Entity.hasTextField)
                Entity.RemoveTextField();
        }
    }
}