using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace BoschGameJuice;

[Autoload(Side = ModSide.Client)]
public class GunDrawLayer : ModSystem
{
    public override void Load() { On_PlayerDrawLayers.DrawPlayer_27_HeldItem += OnDrawHeldItem; }

    private void OnDrawHeldItem(On_PlayerDrawLayers.orig_DrawPlayer_27_HeldItem og, ref PlayerDrawSet drawinfo)
    {
        var player = drawinfo.drawPlayer;
        var item = drawinfo.heldItem;

        if (item != null && item.DamageType == DamageClass.Ranged)
        {
            Main.instance.LoadItem(item.type);
            var tex = TextureAssets.Item[item.type].Value;
            var rect = player.GetItemDrawFrame(item.type);
            var pivot = rect.Size() * BetterGuns.GetPivotNormalized(item);

            if (player.direction == -1)
            {
                pivot.X = rect.Width - pivot.X;
            }

            var drawData = new DrawData
            (
                tex,
                drawinfo.ItemLocation - Main.screenPosition,
                rect,
                Color.White,
                player.itemRotation + (player.direction == 1 ? 0f : MathHelper.ToRadians(180f)),
                pivot,
                Vector2.One,
                drawinfo.itemEffect
            );
            drawinfo.DrawDataCache.Add(drawData);
            return;
        }

        og(ref drawinfo);
    }
}