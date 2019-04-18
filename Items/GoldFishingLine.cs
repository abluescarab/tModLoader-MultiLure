using Terraria;
using Terraria.ID;

namespace MultiLure.Items {
    public class GoldFishingLine : FishingLineBase {
        public override string OriginalName => "GoldFishingLine";
        public override string AlternativeName => "PlatinumFishingLine";
        public override string OriginalDisplayName => "Gold Fishing Line";
        public override string AlternativeDisplayName => "Platinum Fishing Line";
        public override int Lures => 25;

        public override void SetDefaults() {
            base.SetDefaults();
            item.value = Item.sellPrice(0, 0, 95, 0);
            item.rare = ItemRarityID.Blue;
        }

        public override void AddRecipes() {
            AddRecipes(MultiLure.SilverBarGroup);
        }
    }
}
