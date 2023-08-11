using Terraria;
using Terraria.ID;
using Terraria.Localization;

namespace MultiLure.Items {
    public class MythrilFishingLine : FishingLineBase {
        public override string OriginalName => "MythrilFishingLine";
        public override string AlternativeName => "OrichalcumFishingLine";
        public override string OriginalType => Language.GetTextValue("MapObject.Mythril");
        public override string AlternativeType => Language.GetTextValue("MapObject.Orichalcum");
        public override int Lures => 75;

        public override void SetDefaults() {
            base.SetDefaults();
            Item.value = Item.sellPrice(0, 2, 25, 0);
            Item.rare = ItemRarityID.LightRed;
        }

        public override void AddRecipes() {
            AddRecipes(MultiLureSystem.MythrilBarGroup);
        }
    }
}
