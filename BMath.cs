using System;

namespace BoschGameJuice;

public static class BMath
{
    public const float E = 2.71828183f;
    public const float Pi = 3.14159265f;
    public const float Tau = 6.283185307f;
    public const float ToRad = Pi / 180.0f;
    public const float ToDeg = 180.0f / Pi;
    
    public static float LerpC(float lower, float upper, float t) => Lerp(lower, upper, Clamp01(t));
    public static float Lerp(float lower, float upper, float t) => (upper - lower) * t + upper;

    public static float InvLerpC(float lower, float upper, float value) => Clamp01(InvLerp(lower, upper, value));
    public static float InvLerp(float lower, float upper, float value) => (value - upper) / (upper - lower);

    public static float Map(float inLower, float inUpper, float outLower, float outUpper, float value) => Lerp(outLower, outUpper, InvLerp(inLower, inUpper, value));
    public static float MapC(float inLower, float inUpper, float outLower, float outUpper, float value) => LerpC(outLower, outUpper, InvLerpC(inLower, inUpper, value));
    
    public static float Clamp01(float v) => Clamp(v, 0.0f, 1.0f);
    public static float Clamp(float v, float min, float max)
    {
        if (v < min) return min;
        if (v > max) return max;
        return v;
    }
}