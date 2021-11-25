using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Moonstorm;
using RoR2;
using RoR2.Skills;

namespace ModdersItems.Items
{
    public class DifficultyForCredit : ItemBase
    {
        public override ItemDef ItemDef { get; set; } = Modules.Assets.mainAssetBundle.LoadAsset<ItemDef>("HarbMark");

        [ConfigurableField(ConfigName = "Difficulty increase per stack", ConfigDesc = "How much should the difficulty increase per stack in percent?", ConfigSection = "HarbMark")]
        public static float DifficultyIncreasePercent = 10f;
        [ConfigurableField(ConfigName = "director interactible credit increase per stack", ConfigDesc = "How much more income should the interactible director get per stack in percent?", ConfigSection = "HarbMark")]
        public static float CreditIncreasePercent = 5f;

		public void Initialize()
        {
            IL.RoR2.Run.RecalculateDifficultyCoefficentInternal += Run_RecalculateDifficultyCoefficentInternal;
            SceneDirector.onPrePopulateSceneServer += SceneDirector_onPrePopulateSceneServer;
        }

        private void SceneDirector_onPrePopulateSceneServer(SceneDirector director)
        {
            director.interactableCredit = (int)(director.interactableCredit * (1 + (CreditIncreasePercent * 0.01f * getTeamItemCountHarb())));
        }

        private void Run_RecalculateDifficultyCoefficentInternal(MonoMod.Cil.ILContext il)
        {
            var cursor = new ILCursor(il);
            
            while(cursor.TryGotoNext(
                x => x.MatchLdfld<DifficultyDef>("scalingValue")
                )) {
                cursor.EmitDelegate<Func<float>>(getTeamItemCountHarb);
                cursor.Emit(OpCodes.Ldc_R4, DifficultyIncreasePercent);
                cursor.Emit(OpCodes.Ldc_R4, 0.01);
                cursor.Emit(OpCodes.Mul);
                cursor.Emit(OpCodes.Add);
            }
        }

        public static float getTeamItemCountHarb()
        {
            return getTeamItemCount(def);
        }

        //Move this to some util class
        public static float getTeamItemCount(ItemDef itemDef)
        {
            ReadOnlyCollection<TeamComponent> teamMembers = TeamComponent.GetTeamMembers(TeamIndex.Player);
            int itemCount = 0;
            for (int j = 0; j < teamMembers.Count; j++)
            {
                TeamComponent teamComponent = teamMembers[j];
                CharacterBody body = teamComponent.body;
                if (body)
                {
                    CharacterMaster master = teamComponent.body.master;
                    if (master)
                    {
                        itemCount += master.inventory.GetItemCount(itemDef);
                    }
                }
            }
            return itemCount;
        }
    }
    }
}