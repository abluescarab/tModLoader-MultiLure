using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.UI;

namespace MultiLure.Items {
    public abstract class DeprecatedItem : FishingLineBase {
        protected struct ReplacementItem {
            public readonly int ID;
            public readonly int Stack;
            public readonly int Prefix;

            public ReplacementItem(int id, int stack = 1, int prefix = 0) {
                ID = id;
                Stack = stack;
                Prefix = prefix;
            }

            public string GetDisplayName() {
                return Lang.GetItemNameValue(ID);
            }
        }

        protected static Dictionary<Type, bool> DeprecatedMessageShown = new();
        protected abstract ReplacementItem[] Replacements { get; }

        public sealed override void Load() {
            DeprecatedMessageShown.Add(GetType(), false);
        }

        public sealed override bool CanRightClick() {
            return true;
        }

        public sealed override bool ConsumeItem(Player player) {
            return true;
        }

        public sealed override void OnConsumeItem(Player player) {
            Replace(player);
        }

        public sealed override void UpdateInventory(Player player) {
            Type thisType = GetType();

            if(DeprecatedMessageShown.ContainsKey(thisType)
                && !DeprecatedMessageShown[thisType]) {
                string[] replacements = new string[Replacements.Length];

                for(int i = 0; i < replacements.Length; i++) {
                    replacements[i] = Replacements[i].GetDisplayName();
                }

                string name = Lang.GetItemNameValue(Type);

                Main.NewText(
                    Language.GetTextValue(
                        "Mods.MultiLure.DeprecatedMessage",
                        name,
                        string.Join(", ", replacements)),
                    Color.Red);

                DeprecatedMessageShown[thisType] = true;
            }
        }

        public sealed override void UpdateArmorSet(Player player) { }

        public sealed override void UpdateEquip(Player player) { }

        public sealed override void UpdateAccessory(Player player, bool hideVisual) { }

        public sealed override void AddRecipes() { }

        public sealed override bool IsArmorSet(Item head, Item body, Item legs) {
            return false;
        }

        protected void Replace(Player player) {
            if(Replacements == null || Replacements.Length == 0) {
                Item.maxStack = 0;
                Item.TurnToAir();
                return;
            }

            for(int i = 0; i < Replacements.Length; i++) {
                int index = -1;

                if(i == 0) {
                    index = Array.IndexOf(player.inventory, Item);
                }
                else {
                    index = Array.FindIndex(player.inventory, i => i.stack == 0);
                }

                Item item = new(Replacements[i].ID, Replacements[i].Stack, Replacements[i].Prefix);

                // copied from game source to highlight as new item
                if(ItemSlot.Options.HighlightNewItems && item.type >= ItemID.None && !ItemID.Sets.NeverAppearsAsNewInInventory[item.type]) {
                    item.newAndShiny = true;
                }

                if(index >= 0 && index < 50) {
                    player.inventory[index] = item;
                }
                else {
                    player.QuickSpawnItem(player.GetSource_Misc("PlayerDropItemCheck"), item);
                }
            }
        }
    }
}
