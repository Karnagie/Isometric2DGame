using Code.Common.Windows;
using Code.Infrastructure.Ui;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Installers
{
  public class UIInitializer : MonoBehaviour, IInitializable
  {
    private IWindowFactory _windowFactory;
    
    public RectTransform UIRoot;
    public CanvasSortOrderSetter CanvasSortOrderSetter;

    [Inject]
    private void Construct(IWindowFactory windowFactory) => 
      _windowFactory = windowFactory;
    
    public void Initialize()
    {
      _windowFactory.SetUIRoot(UIRoot);
      CanvasSortOrderSetter.Init();
    }
  }
}