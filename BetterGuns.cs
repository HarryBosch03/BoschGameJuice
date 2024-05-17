using System;
using System.Collections.Generic;
using BoschChallengeMode;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace BoschGameJuice;

public class BetterGuns : GlobalItem
{
    private float recoil;

    private static Random rng = new();
    private static Action drawCall;
    private static List<DustInstance> dustRegister = new();

    public override bool InstancePerEntity => true;

    public override void Load() { On_Main.Update += OnUpdate; }

    private void OnUpdate(On_Main.orig_Update orig, Main self, GameTime gametime)
    {
        orig(self, gametime);

        for (var i = 0; i < dustRegister.Count; i++)
        {
            var dust = dustRegister[i];
            dust.dust.scale = MathF.Pow(2.0f, -dust.age * 6.0f) * dust.baseScale;
            dust.age += Helper.dt;

            if (dust.age > 1.0f)
            {
                dust.dust.active = false;
                dustRegister.RemoveAt(i--);
            }
        }
    }

    public override void UseItemFrame(Item item, Player player)
    {
        if (!IsRanged(item)) return;

        player.direction = Main.MouseWorld.X - player.Center.X > 0 ? 1 : -1;

        player.itemRotation = (Main.MouseWorld - player.Center).ToAngle() - recoil * 0.08f * player.direction;
        player.itemLocation = player.Center + GetOffset(item);

        recoil *= 0.7f;
    }

    public override void HoldItemFrame(Item item, Player player) { UseItemFrame(item, player); }

    public override bool Shoot(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        if (IsGun(item))
        {
            PlayerCamera.Instance.Shake(1.0f);

            var speed = velocity.Length();
            var d0 = velocity / speed;

            for (var it = 0; it < 16; it++)
            {
                var p = rng.NextFloat(-1.0f, 1.0f);
                var a = d0.ToAngle() + p * 30.0f * MathF.PI / 180.0f;
                var d = rng.NextFloat(0.0f, BMath.Map(-1.0f, 1.0f, 60.0f, 40.0f, p));
                var offset = a.ToVector(d);
                var dust = Dust.NewDustPerfect(position + d0 * item.width + offset, DustID.Torch, player.velocity, Scale: rng.NextFloat(6.0f));
                dust.noGravity = true;
                dust.rotation = rng.NextFloat(360.0f);
                dustRegister.Add(new DustInstance(dust));
            }

            recoil += 4f;
        }

        return base.Shoot(item, player, source, position, velocity, type, damage, knockback);
    }

    public static bool IsGun(Item item) => item.useAmmo == AmmoID.Bullet;
    public static bool IsRanged(Item item) => item.DamageType == DamageClass.Ranged;

    public static Vector2 GetOffset(Item item) => item.type switch
    {
        ItemID.Minishark => new Vector2(0, 4),
        ItemID.Megashark => new Vector2(0, 4),
        _ => new Vector2(0, 0)
    };

    public static Vector2 GetPivotNormalized(Item item) => item.type switch
    {
        ItemID.Minishark => new Vector2(0.1f, 0.2f),
        ItemID.Megashark => new Vector2(0.35f, 0.2f),
        _ => new Vector2(0f, 0.5f)
    };

    private class DustInstance
    {
        public Dust dust;
        public float age;
        public float baseScale;

        public DustInstance(Dust dust)
        {
            this.dust = dust;
            baseScale = dust.scale;
        }
    }
}