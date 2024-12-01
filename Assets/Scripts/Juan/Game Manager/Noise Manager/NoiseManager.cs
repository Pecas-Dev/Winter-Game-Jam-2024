using System;
using UnityEngine;

public static class NoiseManager
{
    public static event Action<Vector2> OnNoiseMade;

    public static void MakeNoise(Vector2 position)
    {
        OnNoiseMade?.Invoke(position);
    }
}
