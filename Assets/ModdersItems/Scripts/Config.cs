using BepInEx.Configuration;

namespace ModdersItems.Modules
{
    public static class Config
    {
        public static ConfigEntry<bool> valeEnabled;

        public static void Init()
        {
            //valeEnabled = ModdersItemsPlugin.instance.Config.Bind<bool>(new ConfigDefinition("Items", "{Item Name}"), true, new ConfigDescription("{Item Enabled Toggle Description}"));
        }
    }
}
