using BepInEx;
using Moonstorm;
using R2API.Utils;
using System.Security;
using System.Security.Permissions;
using RoR2.ContentManagement;


[module: UnverifiableCode]
[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]
namespace ModdersItems
{
    [BepInDependency("com.bepis.r2api", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("com.valex.ShaderConverter", BepInDependency.DependencyFlags.HardDependency)]
    [NetworkCompatibility(CompatibilityLevel.EveryoneMustHaveMod, VersionStrictness.EveryoneNeedSameModVersion)]
    [BepInPlugin(MODUID, MODNAME, MODVERSION)]
    [R2APISubmoduleDependency(new string[]
    {
        "LanguageAPI"
    })]
    public class ModdersItemsPlugin : BaseUnityPlugin
{
        public const string MODUID = "com.valex.ModdersItems";
        public const string MODNAME = "ModdersItems";
        public const string MODVERSION = "0.1.0";

        public const string developerPrefix = "MITEMS";
        public const bool DEBUG = true;

        public static ModdersItemsPlugin instance;
        public static PluginInfo pluginInfo;

        internal static bool cancel;

        private void Awake()
        {
            ConfigurableFieldManager.AddMod(Config);
            //TokenModifierManager.AddMod();

            instance = this;
            pluginInfo = Info;

            Modules.Config.Init();
            Modules.Assets.Init();
            if (cancel) return;
            Modules.Shaders.Init();

            Modules.ContentPackProvider.Init();
        }
    }
}
