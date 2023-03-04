using Terraria;
using Terraria.ID;

namespace MultiLure.Items {
    public class CopperFishingLine : FishingLineBase {
        public override string OriginalName => "CopperFishingLine";
        public override string AlternativeName => "TinFishingLine";
        public override string OriginalDisplayName => "Copper Fishing Line";
        public override string AlternativeDisplayName => "Tin Fishing Line";
        public override int Lures => 2;

        public override void SetStaticDefaults() {
            base.SetStaticDefaults();
            Tooltip.SetDefault("Adds an extra fishing lure");
        }

        public override void SetDefaults() {
            base.SetDefaults();
            Item.value = Item.sellPrice(0, 0, 16, 25);
            Item.rare = ItemRarityID.Blue;
        }

        public override void AddRecipes() {
            AddRecipes(MultiLureSystem.CopperBarGroup);
        }
    }
}
