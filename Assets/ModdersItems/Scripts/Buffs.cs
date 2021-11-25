using Moonstorm;
using RoR2.ContentManagement;
using System.Collections.Generic;
using System.Linq;

namespace ModdersItems.Modules
{
    public class Buffs : BuffModuleBase
    {
        public static Buffs Instance { get; set; }
        public override SerializableContentPack ContentPack { get; set; } = Assets.serialContentPack;

        public override void Init()
        {
            Instance = this;
            base.Init();
            InitializeBuffs();
        }
        public override IEnumerable<BuffBase> InitializeBuffs()
        {
            base.InitializeBuffs()
                .ToList()
                .ForEach(buff => AddBuff(buff, ContentPack));
            return null;
        }
    }
}
