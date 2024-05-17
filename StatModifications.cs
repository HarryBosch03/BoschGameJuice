using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BoschGameJuice;

public class StatModifications : GlobalItem
{
    public override void SetDefaults(Item item)
    {
        switch (item.type)
        {
            case ItemID.Minishark:
            case ItemID.Megashark:
                item.useTime /= 2;
                item.damage = item.damage * 2 / 3;
                break;
        }

        if (IsRangedWeapon(item)) 
        { 
            item.holdStyle = 1;
        }

        if (item.useAmmo == AmmoID.Bullet) item.shootSpeed *= 2f;
        if (item.useAmmo == AmmoID.Arrow) item.shootSpeed *= IsCrossbow(item) ? 5f : 3f;
        if (item.useAmmo == AmmoID.Rocket) item.shootSpeed *= 4f;
        if (item.useAmmo == AmmoID.FallenStar) item.shootSpeed *= 2f;
    }

    private static bool IsCrossbow(Item item) => item.type switch
    {
        ItemID.AdamantiteRepeater => true,
        ItemID.ChlorophyteShotbow => true,
        ItemID.CobaltRepeater => true,
        ItemID.HallowedRepeater => true,
        ItemID.MythrilRepeater => true,
        ItemID.OrichalcumRepeater => true,
        ItemID.PalladiumRepeater => true,
        ItemID.StakeLauncher => true,
        ItemID.TitaniumRepeater => true,
        _ => false,
    };
    
    private static bool IsRangedWeapon(Item item) => item.DamageType == DamageClass.Ranged;
}