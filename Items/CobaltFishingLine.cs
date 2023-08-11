using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace MultiLure.Items {
    public class CobaltFishingLine : FishingLineBase {
        public override string OriginalName => "CobaltFishingLine";
        public override string AlternativeName => "PalladiumFishingLine";
        public override string OriginalType => Language.GetTextValue("MapObject.Cobalt");
        public override string AlternativeType => Language.GetTextValue("MapObject.Palladium");
        public override int Lures => ModContent.GetInstance<MultiLureConfig>().CobaltFishingLineLures;

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
