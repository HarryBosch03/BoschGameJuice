using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace BoschGameJuice;

public class Informator : ModSystem
{
    private static bool enabled = true;
    private static Dictionary<string, object> data = new();

    public static void Set(string key, object value)
    {
        if (data.ContainsKey(key)) data[key] = value;
        else data.Add(key, value);
    }

    public static void Remove(string key) => data.Remove(key);

    public override void Load()
    {
        On_Main.Draw += OnDraw;
    }

    private void OnDraw(On_Main.orig_Draw orig, Main self, GameTime gametime)
    {
        orig(self, gametime);

        var sb = new StringBuilder($"--- {GetType().Name} ---\n");
        var sorted = new List<KeyValuePair<string, object>>();
        foreach (var pair in data) sorted.Add(pair);
        sorted.Sort((a, b) => string.Compare(a.Key, b.Key));
        
        foreach (var e in sorted) sb.AppendLine($"- {e.Key}: {e.Value.ToString()}");
        
        var d = Main.spriteBatch;
        d.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullNone);
        d.DrawString(FontAssets.MouseText.Value, sb, new Vector2(10.0f, 300.0f), Color.White);
        d.End();
    }
}