using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MultiLure.Items {
    public abstract class FishingLineBase : ModItem {
        public abstract string OriginalName { get; }
        public abstract string AlternativeName { get; }
        public abstract string OriginalDisplayName { get; }
        public abstract string AlternativeDisplayName { get; }
        public abstract int Lures { get; }

        public override bool Autoload(ref string name) {
            return false;
        }

        public override void SetStaticDefaults() {
            if(Name == OriginalName)
                DisplayName.SetDefault(OriginalDisplayName);
            else if(Name == AlternativeName)
                DisplayName.SetDefault(AlternativeDisplayName);

            Tooltip.SetDefault($"Adds an extra {Lures - 1} fishing lures");
        }

        public override void SetDefaults() {
            item.accessory = true;
        }

        public override bool CanEquipAccessory(Player player, int slot) {
            MultiLurePlayer mp = player.GetModPlayer<MultiLurePlayer>();
            return mp.AnyLineEquipped(slot);
        }

        public override void UpdateAccessory(Player player, bool hideVisual) {
            MultiLurePlayer mp = player.GetModPlayer<MultiLurePlayer>();

            if(mp.LureMinimum < Lures) {
                mp.LureMinimum = Lures;
            }
            else {
                mp.LureCount = mp.LureMinimum + Lures;
            }
        }

        public void AddRecipes(string barGroup) {
            ModRecipe rcp = new ModRecipe(mod);
            rcp.AddIngredient(ItemID.Hook);
            rcp.AddRecipeGroup(MultiLure.WhiteStringGroup);
            rcp.AddRecipeGroup(barGroup, 5);
            rcp.AddTile(TileID.Anvils);
            rcp.SetResult(this);
            rcp.AddRecipe();
        }
    }
}
