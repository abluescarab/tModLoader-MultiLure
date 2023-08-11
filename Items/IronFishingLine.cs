using Terraria;
using Terraria.ID;
using Terraria.Localization;

namespace MultiLure.Items {
    public class IronFishingLine : FishingLineBase {
        public override string OriginalName => "IronFishingLine";
        public override string AlternativeName => "LeadFishingLine";
        public override string OriginalType => Language.GetTextValue("MapObject.Iron");
        public override string AlternativeType => Language.GetTextValue("MapObject.Lead");
        public override int Lures => 5;

        public override void SetDefaults() {
            base.SetDefaults();
            Item.value = Item.sellPrice(0, 0, 27, 50);
            Item.rare = ItemRarityID.Blue;
        }

        public override void AddRecipes() {
            AddRecipes(MultiLureSystem.IronBarGroup);
        }
    }
}
