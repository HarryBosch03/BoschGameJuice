using System.ComponentModel;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace BoschChallengeMode;

public class ServerConfig : ModConfig
{
    public float recoilSpeed { get; set; } = 0;
    public float recoilAmplitude { get; set; } = 0;
    
    public override ConfigScope Mode => ConfigScope.ServerSide;
    public static ServerConfig instance => ModContent.GetInstance<ServerConfig>();
}