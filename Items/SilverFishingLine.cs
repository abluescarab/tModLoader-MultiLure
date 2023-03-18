using Terraria;
using Terraria.ID;

namespace MultiLure.Items {
    public class SilverFishingLine : FishingLineBase {
        public override string OriginalName => "SilverFishingLine";
        public override string AlternativeName => "TungstenFishingLine";
        public override string OriginalDisplayName => "{$Mods.MultiLure.Items.SilverFishingLine}";
        public override string AlternativeDisplayName => "{$Mods.MultiLure.Items.TungstenFishingLine}";
        public override int Lures => 10;

        public override void SetDefaults() {
            base.SetDefaults();
            Item.value = Item.sellPrice(0, 0, 50, 0);
            Item.rare = ItemRarityID.Blue;
        }

        public override void AddRecipes() {
            AddRecipes(MultiLureSystem.SilverBarGroup);
        }
    }
}
