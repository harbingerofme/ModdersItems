using R2API;
using ModdersItems;

namespace ModdersItems.Tokens
{
    public class ValeTokens : TokenBase
    {
        public override string name { get; set; } = "VALECONTROLLER";
        public override void AddTokens()
        {
            string lore = "";

            LanguageAPI.Add(prefix + "ITEM_" + name + "_NAME",      $"Vale's Controller");
            LanguageAPI.Add(prefix + "ITEM_" + name + "_PICKUP",    $"Can sprint in any direction. Gain periodic bursts of speed while sprinting.");
            LanguageAPI.Add(prefix + "ITEM_" + name + "_DESC",      $"Can sprint in any direction. Gain <style=cIsUtility>{Items.ValeController.speedMulti * 100f}%</style> <style=cStack>(+{Items.ValeController.speedStackMulti * 100f}% per stack)</style> movement speed for {Items.ValeController.buffDuration} seconds when sprinting. {Items.ValeController.buffCooldown} second cooldown.");
            LanguageAPI.Add(prefix + "ITEM_" + name + "_LORE",      lore);
        }
    }
}
