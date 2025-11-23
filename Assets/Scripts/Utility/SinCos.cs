using System.Collections.Generic;
using UnityEngine;
using MapCfg;

public static class SinCos
{
    private static readonly int _anglesQuantity = MapConfig.CircleStructurePoint;
    private static IReadOnlyList<float[]> _sinCos;

    public static IReadOnlyList<float[]> GetSinCos()
    {
        if (_sinCos == null)
            FullSinCosDictionary();

        return _sinCos;
    }

    private static void FullSinCosDictionary()
    {
        List<float[]> sinCos = new List<float[]>();

        for (int i = 0; i < _anglesQuantity; i++)
        {
            sinCos.Add(new float[] {Mathf.Sin((360 / _anglesQuantity) * i * Mathf.Deg2Rad), Mathf.Cos((360 / _anglesQuantity) * i * Mathf.Deg2Rad)});
        }

        _sinCos = sinCos;
    }
}
