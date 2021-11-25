using R2API;
using ModdersItems;

namespace ModdersItems.Tokens
{
    public class ValeTokens : TokenBase
    {
        public override string name { get; set; } = "MI_HARB";
        public override void AddTokens()
        {
            string lore = string.join("\n",
                "<color=#690420>The [Redacted] [Redacted] [Redacted].</color>",
                "You consider yourself the best. You are the cream of the crop. You have escaped the planet's pull. You have conquered the prisoner of their own brother. The Apex.",
                "<color=#690420>The [Redacted] is [Redacted].</color>",
                "But then she beckoned, and you obeyed. You offered to <style=cDeath>her</style> to no end, and it was not enough. It left you wanting. You set <style=cDeath>her</style> free, escaped from our prison's grasp. And still, you came back.",
                "<color=#690420>The [Redacted] are watching.</color>",
                "<color=#224470>With an audience of <color=white>stars</color>, give it your all.</style>",
            )

            LanguageAPI.Add(prefix + "ITEM_" + name + "_NAME", $"Harb's Bracelet");
            LanguageAPI.Add(prefix + "ITEM_" + name + "_PICKUP", $"As difficulty mounts, so too will rewards.");
            LanguageAPI.Add(prefix + "ITEM_" + name + "_DESC", $"Difficulty becomes <color=#FF7F7F>{Items.DifficultyForCredit.DifficultyIncreasePercent}%</color> harder for each stack. Stages generate <style=cIsUtility>{Items.DifficultyForCredit}%</style> more worth of interactibles for each stack.");
            LanguageAPI.Add(prefix + "ITEM_" + name + "_LORE", lore);
        }
    }
}
