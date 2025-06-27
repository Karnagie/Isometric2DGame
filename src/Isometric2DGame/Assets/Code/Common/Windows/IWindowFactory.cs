using UnityEngine;

namespace Code.Common.Windows
{
  public interface IWindowFactory
  {
    Transform Root { get; }
    public void SetUIRoot(RectTransform uiRoot);
    public BaseWindow CreateWindow(WindowId windowId);
  }
}