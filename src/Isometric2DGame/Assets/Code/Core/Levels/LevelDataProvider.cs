﻿using System.Collections.Generic;
using UnityEngine;

namespace Code.Core.Levels
{
  public class LevelDataProvider : ILevelDataProvider
  {
    public Vector3 StartPoint { get; private set; }

    public void SetStartPoint(Vector3 startPoint)
    {
      StartPoint = startPoint;
    }
  }
}