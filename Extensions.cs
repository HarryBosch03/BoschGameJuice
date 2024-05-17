using System;
using Microsoft.Xna.Framework;

namespace BoschGameJuice;

public static class Extensions
{
    public static float NextFloat(this Random rng) => rng.NextFloat(0.0f, 1.0f);

    public static float NextFloat(this Random rng, float max) => rng.NextFloat(0.0f, max);
    public static float NextFloat(this Random rng, float min, float max)
    {
        return (float)rng.NextDouble() * (max - min) + min;
    }

    public static float ToAngle(this Vector2 v) => MathF.Atan2(v.Y, v.X);
    public static Vector2 ToVector(this float angleRad, float magnitude = 1.0f) => new Vector2(MathF.Cos(angleRad), MathF.Sin(angleRad)) * magnitude;
}