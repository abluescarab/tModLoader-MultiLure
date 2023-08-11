using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace MultiLure.Items {
    public class GoldFishingLine : FishingLineBase {
        public override string OriginalName => "GoldFishingLine";
        public override string AlternativeName => "PlatinumFishingLine";
        public override string OriginalType => Language.GetTextValue("MapObject.Gold");
        public override string AlternativeType => Language.GetTextValue("MapObject.Platinum");
        public override int Lures => ModContent.GetInstance<MultiLureConfig>().GoldFishingLineLures;

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
