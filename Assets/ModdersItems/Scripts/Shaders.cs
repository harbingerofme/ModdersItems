using System.Collections.Generic;
using UnityEngine;
using StubbedConverter;

namespace ModdersItems.Modules
{
    public static class Shaders
    {
        public static List<Material> materialStorage = new List<Material>();

        public static void Init()
        {
            ConvertBundleMaterials(Assets.mainAssetBundle);
            CreateMaterialStorage(Assets.mainAssetBundle);
        }

        // Uses StubbedShaderConverter to convert all materials within the Asset Bundle on Awake().
        // This now sorts out CloudRemap materials as well! Woo!
        private static void ConvertBundleMaterials(AssetBundle inAssetBundle)
        {
            ShaderConverter.ConvertStubbedShaders(inAssetBundle);
        }

        private static void CreateMaterialStorage(AssetBundle inAssetBundle)
        {
            Material[] tempArray = inAssetBundle.LoadAllAssets<Material>();

            materialStorage.AddRange(tempArray);
        }

        public static Material GetMaterialFromStorage(string matName)
        {
            return materialStorage.Find(x => x.name == matName);
        }
    }
}
