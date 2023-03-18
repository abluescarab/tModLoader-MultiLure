using Terraria;
using Terraria.ID;

namespace MultiLure.Items {
    public class GoldFishingLine : FishingLineBase {
        public override string OriginalName => "GoldFishingLine";
        public override string AlternativeName => "PlatinumFishingLine";
        public override string OriginalDisplayName => "{$Mods.MultiLure.Items.GoldFishingLine}";
        public override string AlternativeDisplayName => "{$Mods.MultiLure.Items.PlatinumFishingLine}";
        public override int Lures => 25;

        public override void SetDefaults() {
            base.SetDefaults();
            Item.value = Item.sellPrice(0, 0, 95, 0);
            Item.rare = ItemRarityID.Blue;
        }

        public override void AddRecipes() {
            AddRecipes(MultiLureSystem.GoldBarGroup);
        }
    }
}
