using Terraria;
using Terraria.ID;
using Terraria.Localization;

namespace MultiLure.Items {
    public class SilverFishingLine : FishingLineBase {
        public override string OriginalName => "SilverFishingLine";
        public override string AlternativeName => "TungstenFishingLine";
        public override string OriginalType => Language.GetTextValue("MapObject.Silver");
        public override string AlternativeType => Language.GetTextValue("MapObject.Tungsten");
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
