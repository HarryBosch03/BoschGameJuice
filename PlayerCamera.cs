using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.CameraModifiers;
using Terraria.ModLoader;

namespace BoschGameJuice;

public class PlayerCamera : ModSystem, ICameraModifier
{
    public string UniqueIdentity => GetType().FullName;
    public bool Finished { get; }

    private float shakeIntensity = 200.0f;
    private Random rng = new Random();

    private Vector2 position;
    private Vector2 velocity;
    
    private float spring = 200.0f;
    private float damper = 30.0f;

    private const float dt = 1.0f / 60.0f;
    public static PlayerCamera Instance { get; private set; }
    
    public override void Load()
    {
        Instance = this;
        Main.instance.CameraModifiers.Add(this);
        On_Player.Update += OnPlayerUpdate;
    }

    private void OnPlayerUpdate(On_Player.orig_Update orig, Player self, int i)
    {
        orig(self, i);
        if (Main.LocalPlayer != self) return;

        var tPos = self.Center;

        if ((tPos - position).Length() > 5000.0f)
        {
            position = tPos;
            velocity = Vector2.Zero;
        }
        
        var force = (tPos - position) * spring - velocity * damper;

        position += velocity * dt;
        velocity += force * dt;
    }

    public void Shake(float v)
    {
        var shakeAngle = rng.NextFloat(0.0f, MathF.Tau);
        velocity += new Vector2(MathF.Sin(shakeAngle), MathF.Cos(shakeAngle)) * v * shakeIntensity;
    }

    public void Update(ref CameraInfo cameraPosition)
    {
        cameraPosition.CameraPosition = position - Main.Camera.UnscaledSize * 0.5f;
    }

    private static bool InCameraBounds(Vector2 position)
    {
        var c = Main.Camera;
        
        if (position.X < c.UnscaledPosition.X) return false;
        if (position.X > c.UnscaledPosition.X + c.UnscaledSize.X) return false;
        
        if (position.Y < c.UnscaledPosition.Y) return false;
        if (position.Y > c.UnscaledPosition.Y + c.UnscaledSize.Y) return false;

        return true;
    }
}