using Terraria;
using Terraria.ID;

namespace MultiLure.Items {
    public class CobaltFishingLine : FishingLineBase {
        public override string OriginalName => "CobaltFishingLine";
        public override string AlternativeName => "PalladiumFishingLine";
        public override string OriginalDisplayName => "Cobalt Fishing Line";
        public override string AlternativeDisplayName => "Palladium Fishing Line";
        public override int Lures => 50;

        public override void SetDefaults() {
            base.SetDefaults();
            Item.value = Item.sellPrice(0, 1, 10, 0);
            Item.rare = ItemRarityID.LightRed;
        }

        public override void AddRecipes() {
            AddRecipes(MultiLureSystem.CobaltBarGroup);
        }
    }
}
