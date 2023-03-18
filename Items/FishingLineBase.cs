using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace MultiLure.Items {
    public abstract class FishingLineBase : ModItem {
        public abstract string OriginalName { get; }
        public abstract string AlternativeName { get; }
        public abstract string OriginalDisplayName { get; }
        public abstract string AlternativeDisplayName { get; }
        public abstract int Lures { get; }

        public override void SetStaticDefaults() {
            if(Name == OriginalName)
                DisplayName.SetDefault(OriginalDisplayName);
            else if(Name == AlternativeName)
                DisplayName.SetDefault(AlternativeDisplayName);

            Tooltip.SetDefault(
                Language.GetTextValue(
                    "Mods.MultiLure.FishingLine_Tooltip", 
                    Lures - 1));
        }

        public override void SetDefaults() {
            Item.accessory = true;
        }

        public override bool CanEquipAccessory(Player player, int slot, bool modded) {
            MultiLurePlayer mp = player.GetModPlayer<MultiLurePlayer>();
            mp.HasLine(out int minimumLures, out int equippedSlot);

            return slot == equippedSlot || minimumLures == MultiLurePlayer.LURE_MIN;
        }

        public override void UpdateAccessory(Player player, bool hideVisual) {
            MultiLurePlayer mp = player.GetModPlayer<MultiLurePlayer>();

            if(mp.LureMinimum < Lures) {
                mp.LureMinimum = Lures;
            }
        }

        public override void UpdateInventory(Player player) {
            MultiLurePlayer mp = player.GetModPlayer<MultiLurePlayer>();

            if(mp.LureMinimum < Lures) {
                mp.LureMinimum = Lures;
            }
        }

        public void AddRecipes(string barGroup) {
            Recipe rcp = CreateRecipe();
            rcp.AddIngredient(ItemID.Hook);
            rcp.AddRecipeGroup(MultiLureSystem.WhiteStringGroup);
            rcp.AddRecipeGroup(barGroup, 5);
            rcp.AddTile(TileID.Anvils);
            rcp.Register();
        }
    }
}
