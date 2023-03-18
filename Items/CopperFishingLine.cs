using Terraria;
using Terraria.ID;

namespace MultiLure.Items {
    public class CopperFishingLine : DeprecatedItem {
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
