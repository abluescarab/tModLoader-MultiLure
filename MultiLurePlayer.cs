using System;
using System.Linq;
using MultiLure.Items;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace MultiLure {
    public class MultiLurePlayer : ModPlayer {
        private const string LureCountTag = "lurecount";

        public int LureMaximum { get; set; } = 100;
        public int LureMinimum { get; set; } = 1;
        public int LureCount { get; set; } = 1;

        public override void ResetEffects() {
            LureCount = LureMinimum;
        }

        public override TagCompound Save() {
            return new TagCompound {
                { LureCountTag, (byte)LureMinimum }
            };
        }

        public override void Load(TagCompound tag) {
            LureMinimum = tag.GetByte(LureCountTag);
        }

        internal bool AnyLineEquipped(int slot = -1) {
            int ignore = -1;
            return AnyLineEquipped(out ignore, slot);
        }

        internal bool AnyLineEquipped(out int minimumLures, int slot = -1) {
            var items = ((MultiLure)mod).FishingLineItems;
            var equipped = Array.FindIndex(player.armor.Select(a => a.type).ToArray(), t => items.ContainsValue(t));

            if(equipped == -1) {
                minimumLures = 1;
                return true;
            }

            var item = items.FirstOrDefault(i => i.Value == player.armor[equipped].type).Key;

            minimumLures = ((FishingLineBase)mod.GetItem(item)).Lures;

            return equipped == slot;
        }
    }
}
