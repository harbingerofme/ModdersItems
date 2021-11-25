using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moonstorm;
using RoR2;

namespace ModdersItems.Buffs
{
    public class BuffValeSprint : BuffBase
    {
        public override BuffDef BuffDef { get; set; } = Modules.Assets.mainAssetBundle.LoadAsset<BuffDef>("BuffValeSprint");
        public static BuffDef buff;

        public override void Initialize()
        {
            buff = BuffDef;
        }

        public override void AddBehavior(ref CharacterBody body, int stack)
        {
            body.AddItemBehavior<Behaviour>(stack);
        }

        public class Behaviour : CharacterBody.ItemBehavior, IStatItemBehavior
        {
            public void RecalculateStatsEnd()
            {
                body.moveSpeed *= 1 + (Items.ValeController.speedMulti + (Items.ValeController.speedStackMulti * stack));
            }

            public void RecalculateStatsStart()
            {
            }
        }
    }
}
