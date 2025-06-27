using System;
using System.Linq;
using System.Text;
using Code.Common.Entity.ToStrings;
using Code.Common.Extensions;
using Code.Infrastructure.Loggers;
using Code.Infrastructure.Loggers.Unity;
using Code.Meta.Features.Storage;
using Entitas;
using UnityEngine;
using LogType = Code.Infrastructure.Loggers.LogType;

// ReSharper disable once CheckNamespace
public sealed partial class MetaEntity : INamedEntity
{
  private EntityPrinter _printer;

  public void RenameDirty()
  {
    _printer.ClearCache();
  }

  public override string ToString()
  {
    if (_printer == null)
      _printer = new EntityPrinter(this);

    _printer.InvalidateCache();

    return _printer.BuildToString();
  }

  public string EntityName(IComponent[] components)
  {
    try
    {
      if (components.Length == 1)
        return components[0].GetType().Name;

      foreach (IComponent component in components)
      {
        switch (component.GetType().Name)
        {
          case nameof(Storage):
            return PrintStorage();
        }
      }
    }
    catch (Exception exception)
    {
      exception.Message
        .Setup()
        .AddFeatureType(FeatureType.Infrastructure)
        .AddLogType(LogType.Exception)
        .Log();
    }

    return components.First().GetType().Name;
  }

  private string PrintStorage()
  {
    return new StringBuilder($"Storage ")
      .ToString();
  }

  public string BaseToString() => base.ToString();
}