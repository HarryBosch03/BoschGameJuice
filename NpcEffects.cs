using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BoschGameJuice;

public class NpcEffects : GlobalNPC
{
    private static Random rng = new();

    public override void HitEffect(NPC npc, NPC.HitInfo hit)
    {
        var a0 = rng.NextFloat(-1.0f, 1.0f) * 20.0f - 45.0f;
        
        for (var i = 0; i < 6 + hit.Damage / 4; i++)
        {
            var a1 = (a0 + rng.NextFloat(-1.0f, 1.0f) * 10.0f) * BMath.ToRad;
            var velocity = a1.ToVector() * rng.NextFloat(-1.0f, 1.0f) * (5.0f + hit.Damage * 0.6f);
            
            Dust.NewDustPerfect(npc.Center, DustID.Blood, velocity, Scale: 2.0f);
        }

        if (npc.life <= 0)
        {
            Dust.NewDust(npc.position, npc.width, npc.height, DustID.Blood, 0f, 0f ,0, default, 2.5f);
        }
    }
}