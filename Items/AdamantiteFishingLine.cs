using Terraria;
using Terraria.ID;
using Terraria.Localization;

namespace MultiLure.Items {
    public class AdamantiteFishingLine : FishingLineBase {
        public override string OriginalName => "AdamantiteFishingLine";
        public override string AlternativeName => "TitaniumFishingLine";
        public override string OriginalType => Language.GetTextValue("MapObject.Adamantite");
        public override string AlternativeType => Language.GetTextValue("MapObject.Titanium");
        public override int Lures => 100;

        public override void SetDefaults() {
            base.SetDefaults();
            Item.value = Item.sellPrice(0, 3, 80, 0);
            Item.rare = ItemRarityID.LightRed;
        }

        public override void AddRecipes() {
            AddRecipes(MultiLureSystem.AdamantiteBarGroup);
        }
    }
}
