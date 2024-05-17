using Terraria.ModLoader;

namespace BoschGameJuice;

public class BetterPlayer : ModPlayer
{
    public override void PreUpdate()
    {
        //var vec = Main.MouseWorld - Player.Center;
        //Player.direction = vec.X > 0.0f ? 1 : -1;
        
        Informator.Set("Item Location", Player.itemLocation);
        Informator.Set("Item Location Diff", Player.itemLocation - Player.Center);
        Informator.Set("Item Rotation", Player.itemRotation);
        Informator.Set("Item Width", Player.itemWidth);
        Informator.Set("Item Height", Player.itemHeight);
        Informator.Set("Item Animation", Player.itemAnimation);
        Informator.Set("Item Center", Player.HeldItem?.position.ToString() ?? "</>");
        Informator.Set("Body Frame", Player.bodyFrame);
    }    
}