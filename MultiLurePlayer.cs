using System;
using System.Linq;
using MultiLure.Items;
using Terraria;
using Terraria.GameInput;
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

        public override void SaveData(TagCompound tag) {
            tag.Add(LureCountTag, (byte)LureMinimum);
        }

        public override void LoadData(TagCompound tag) {
            LureMinimum = tag.GetByte(LureCountTag);
        }

        public override void ProcessTriggers(TriggersSet triggersSet) {
            if(MultiLureSystem.AddLureKey.Key.JustPressed) {
                MultiLureSystem.ChangeLures(true);
                MultiLureSystem.AddLureKey.Start(Main._drawInterfaceGameTime);
            }
            else if(MultiLureSystem.RemoveLureKey.Key.JustPressed) {
                MultiLureSystem.ChangeLures(false);
                MultiLureSystem.RemoveLureKey.Start(Main._drawInterfaceGameTime);
            }
        }

        internal bool AnyLineEquipped(int slot = -1) {
            int ignore = -1;
            return AnyLineEquipped(out ignore, slot);
        }

        internal bool AnyLineEquipped(out int minimumLures, int slot = -1) {
            var items = ((MultiLure)Mod).FishingLineItems;
            var equipped = Array.FindIndex(Player.armor.Select(a => a.type).ToArray(), t => items.ContainsValue(t));

            if(equipped == -1) {
                minimumLures = 1;
                return true;
            }

            int item = items.FirstOrDefault(i => i.Value == Player.armor[equipped].type).Value;

            minimumLures = ((FishingLineBase)ItemLoader.GetItem(item)).Lures;

            return equipped == slot;
        }
    }
}
