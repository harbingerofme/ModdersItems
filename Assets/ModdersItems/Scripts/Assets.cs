using EntityStates;
using RoR2;
using RoR2.ContentManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using Path = System.IO.Path;

namespace ModdersItems.Modules
{
    public static class Assets
    {
        public static AssetBundle mainAssetBundle = null;
        public static ContentPack mainContentPack = null;
        public static SerializableContentPack serialContentPack = null;

        public const string assetBundleName = "moddersitemsassets";
        public const string contentPackName = "MIContentPack";

        public static string assemblyDir { get { return Path.GetDirectoryName(ModdersItemsPlugin.pluginInfo.Location); } }

        internal static List<EffectDef> effectDefs = new List<EffectDef>();

        public static void Init()
        {
            LoadAssets();
            
        }

        public static void LoadAssets()
        {
            if (mainAssetBundle == null)
            {
                var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                mainAssetBundle = AssetBundle.LoadFromFile(Path.Combine(path, assetBundleName));

                if (!mainAssetBundle)
                {
                    MILog.LogError($"{ModdersItemsPlugin.MODNAME}: AssetBundle not found. File missing or assetBundleName is incorrect.");
                    ModdersItemsPlugin.cancel = true;
                    return;
                }

                serialContentPack = mainAssetBundle.LoadAsset<SerializableContentPack>(contentPackName);
                mainContentPack = serialContentPack.CreateContentPack();
                AddEffectDefs();
                AddEntityStateTypes();
                AddEntityStateConfigs();
                ContentPackProvider.contentPack = mainContentPack;  
            }
        }

        internal static void AddEffectDefs()
        {
            List<GameObject> effects = new List<GameObject>();

            GameObject[] assets = mainAssetBundle.LoadAllAssets<GameObject>();
            foreach (GameObject g in assets)
            {
                if (g.GetComponent<EffectComponent>())
                {
                    effects.Add(g);
                }
            }
            foreach (GameObject g in effects)
            {
                EffectDef def = new EffectDef();
                def.prefab = g;

                effectDefs.Add(def);
            }

            mainContentPack.effectDefs.Add(effectDefs.ToArray());
        }

        internal static void AddEntityStateTypes()
        {
            mainContentPack.entityStateTypes.Add(((IEnumerable<System.Type>)Assembly.GetExecutingAssembly().GetTypes()).Where<System.Type>
                ((Func<System.Type, bool>)(type => typeof(EntityState).IsAssignableFrom(type))).ToArray<System.Type>());

            if (ModdersItemsPlugin.DEBUG)
            {
                foreach (Type t in mainContentPack.entityStateTypes)
                {
                    MILog.LogDebug($"{ModdersItemsPlugin.MODNAME}: Added EntityStateType: " + t);
                }
            }
        }

        internal static void AddEntityStateConfigs()
        {
            mainContentPack.entityStateConfigurations.Add(mainAssetBundle.LoadAllAssets<EntityStateConfiguration>());

            if (ModdersItemsPlugin.DEBUG)
            {
                foreach (EntityStateConfiguration t in mainContentPack.entityStateConfigurations)
                {
                    MILog.LogDebug($"{ModdersItemsPlugin.MODNAME}: Added EntityStateConfiguration: " + t);
                }
            }
        }
    }

    public class ContentPackProvider : IContentPackProvider
    {
        public static ContentPack contentPack;
        public string identifier { get { return ModdersItemsPlugin.MODNAME; } }

        internal static void Init()
        {
            ContentManager.collectContentPackProviders += AddContent;
        }

        private static void AddContent(ContentManager.AddContentPackProviderDelegate addContentPackProvider)
        {
            addContentPackProvider(new ContentPackProvider());
        }

        public IEnumerator FinalizeAsync(FinalizeAsyncArgs args)
        {
            args.ReportProgress(1f);
            yield break;
        }

        public IEnumerator GenerateContentPackAsync(GetContentPackAsyncArgs args)
        {
            ContentPack.Copy(contentPack, args.output);
            args.ReportProgress(1f);
            yield break;
        }

        public IEnumerator LoadStaticContentAsync(LoadStaticContentAsyncArgs args)
        {
            new Tokens.TokensModule().Init();
            new Pickups().Init();
            new Buffs().Init();

            args.ReportProgress(1f);
            yield break;
        }
    }
}
