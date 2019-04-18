using Terraria;
using Terraria.ID;

namespace MultiLure.Items {
    public class MythrilFishingLine : FishingLineBase {
        public override string OriginalName => "MythrilFishingLine";
        public override string AlternativeName => "OrichalcumFishingLine";
        public override string OriginalDisplayName => "Mythril Fishing Line";
        public override string AlternativeDisplayName => "Orichalcum Fishing Line";
        public override int Lures => 75;

        public override void SetDefaults() {
            base.SetDefaults();
            item.value = Item.sellPrice(0, 2, 25, 0);
            item.rare = ItemRarityID.LightRed;
        }

        public override void AddRecipes() {
            AddRecipes(MultiLure.MythrilBarGroup);
        }
    }
}
