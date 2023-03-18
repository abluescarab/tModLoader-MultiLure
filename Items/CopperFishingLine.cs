using Terraria;
using Terraria.ID;

namespace MultiLure.Items {
    public class CopperFishingLine : DeprecatedItem {
        public override string OriginalName => "CopperFishingLine";
        public override string AlternativeName => "TinFishingLine";
        public override string OriginalDisplayName => "{$Mods.MultiLure.Items.CopperFishingLine}";
        public override string AlternativeDisplayName => "{$Mods.MultiLure.Items.TinFishingLine}";
        public override int Lures => 0;

        protected override ReplacementItem[] Replacements => new ReplacementItem[] {
            new(ItemID.CopperBar, 5),
            new(ItemID.WhiteString),
            new(ItemID.Hook)
        };

        public override void SetDefaults() {
            base.SetDefaults();
            Item.value = Item.sellPrice(0, 0, 16, 25);
            Item.rare = ItemRarityID.Blue;
        }
    }
}
