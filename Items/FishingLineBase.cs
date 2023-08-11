using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace MultiLure.Items {
    public abstract class FishingLineBase : ModItem {
        public abstract string OriginalName { get; }
        public abstract string AlternativeName { get; }
        public abstract string OriginalType { get; }
        public abstract string AlternativeType { get; }
        public abstract int Lures { get; }

        public override LocalizedText DisplayName 
            => Name == OriginalName 
                ? Language.GetOrRegister(Mod.GetLocalizationKey("Items.FishingLine.DisplayName"))
                            .WithFormatArgs(OriginalType) 
                : Language.GetOrRegister(Mod.GetLocalizationKey("Items.FishingLine.DisplayName"))
                            .WithFormatArgs(AlternativeType);

        public override LocalizedText Tooltip 
            => Language.GetOrRegister(Mod.GetLocalizationKey("Items.FishingLine.Tooltip"))
                            .WithFormatArgs(Lures - 1);

        public override void SetDefaults() {
            Item.accessory = true;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips) {
            base.ModifyTooltips(tooltips);
            var tooltip = tooltips.Find(t => t.Name == "Tooltip0");
            tooltip.Text = Tooltip.Value;
        }

        public override void UpdateAccessory(Player player, bool hideVisual) {
            UpdateItem(player);
        }

        public override void UpdateInventory(Player player) {
            UpdateItem(player);
        }

        public void AddRecipes(string barGroup) {
            Recipe rcp = CreateRecipe();
            rcp.AddIngredient(ItemID.Hook);
            rcp.AddRecipeGroup(MultiLureSystem.WhiteStringGroup);
            rcp.AddRecipeGroup(barGroup, 5);
            rcp.AddTile(TileID.Anvils);
            rcp.Register();
        }

        protected void UpdateItem(Player player) {
            MultiLurePlayer mp = player.GetModPlayer<MultiLurePlayer>();

            if(mp.LureMinimum < Lures) {
                mp.LureMinimum = Lures;
            }
        }
    }
}
