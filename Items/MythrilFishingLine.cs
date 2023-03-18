using Terraria;
using Terraria.ID;

namespace MultiLure.Items {
    public class MythrilFishingLine : FishingLineBase {
        public override string OriginalName => "MythrilFishingLine";
        public override string AlternativeName => "OrichalcumFishingLine";
        public override string OriginalDisplayName => "{$Mods.MultiLure.Items.MythrilFishingLine}";
        public override string AlternativeDisplayName => "{$Mods.MultiLure.Items.OrichalcumFishingLine}";
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
