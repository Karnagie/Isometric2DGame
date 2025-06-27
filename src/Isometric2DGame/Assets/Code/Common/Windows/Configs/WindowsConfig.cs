using System;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Common.Windows.Configs
{
  [CreateAssetMenu(fileName = "windowsConfig", menuName = "Configs/Window Config")]
  public class WindowsConfig : ScriptableObject
  {
    public List<WindowConfig> WindowConfigs;
  }
}