using Moonstorm;
using RoR2.ContentManagement;
using System.Collections.Generic;
using System.Linq;

namespace ModdersItems.Modules
{
    public class Pickups : PickupModuleBase
    {
        public static Pickups Instance { get; set; }
        public override SerializableContentPack ContentPack { get; set; } = Assets.serialContentPack;

        public override void Init()
        {
            Instance = this;
            base.Init();

            InitializeEquipments();
            InitializeEliteEquipments();
            InitializeItems();
        }
        public override IEnumerable<EquipmentBase> InitializeEquipments()
        {
            base.InitializeEquipments()
                .Where(eqp => ModdersItemsPlugin.instance.Config.Bind($"{eqp.EquipmentDef.name}", $"Enable {eqp.EquipmentDef.name}", true, "Enable/disable this Equipment.").Value)
                .ToList()
                .ForEach(eqp => AddEquipment(eqp, ContentPack));
            return null;
        }

        public override IEnumerable<EliteEquipmentBase> InitializeEliteEquipments()
        {
            base.InitializeEliteEquipments()
                .ToList()
                .ForEach(eqp => AddEliteEquipment(eqp));
            return null;
        }

        public override IEnumerable<ItemBase> InitializeItems()
        {
            base.InitializeItems()
                .Where(item => item.ItemDef.hidden || ModdersItemsPlugin.instance.Config.Bind($"{item.ItemDef.name}", $"Enable {item.ItemDef.name}", true, "Enable/disable this Item.").Value)
                .ToList()
                .ForEach(item => AddItem(item, ContentPack));
            return null;
        }
    }
}
