using MultiLure.Items;
using System;
using System.Linq;
using Terraria;
using Terraria.GameInput;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace MultiLure {
    public class MultiLurePlayer : ModPlayer {
        private const string LureCountTag = "lurecount";

        public const int LURE_MIN = 1;

        public int LureMaximum { get; set; } = 100;
        public int LureMinimum { get; set; } = LURE_MIN;
        public int CheatLureMinimum { get; set; } = LURE_MIN;
        public int LureCount { get; set; } = LURE_MIN;

        public override void ResetEffects() {
            if(CheatLureMinimum < LureMinimum) {
                CheatLureMinimum = LureMinimum;
            }

            LureCount = CheatLureMinimum > LureMinimum 
                ? CheatLureMinimum 
                : LureMinimum;
            LureMinimum = LURE_MIN;
        }

        public override void SaveData(TagCompound tag) {
            tag.Add(LureCountTag, (byte)CheatLureMinimum);
        }

        public override void LoadData(TagCompound tag) {
            MultiLureConfig config = ModContent.GetInstance<MultiLureConfig>();

            if(config.EnableCheatSheetIntegration
                || config.EnableHerosModIntegration
                || config.EnableHotkeys) {
                CheatLureMinimum = tag.GetByte(LureCountTag);
            }
        }

        public override void ProcessTriggers(TriggersSet triggersSet) {
            if(MultiLureSystem.AddLureKey != null && MultiLureSystem.AddLureKey.Key.JustPressed) {
                MultiLureSystem.ChangeLures(true);
                MultiLureSystem.AddLureKey.Start(Main._drawInterfaceGameTime);
            }
            else if(MultiLureSystem.RemoveLureKey != null && MultiLureSystem.RemoveLureKey.Key.JustPressed) {
                MultiLureSystem.ChangeLures(false);
                MultiLureSystem.RemoveLureKey.Start(Main._drawInterfaceGameTime);
            }
        }

        internal bool HasLine() {
            return HasLine(out int _, out int _);
        }

        internal bool HasLine(out int minimumLures, out int accessorySlot) {
            var items = Mod.GetContent<ModItem>().Select(i => i.Type);
            int index = Array.FindIndex(
                Player.armor,
                a => items.Contains(a.type));
            FishingLineBase lineItem;

            // check armor/accessories
            if(index == -1) {
                accessorySlot = -1;

                // check inventory
                index = Array.FindIndex(
                    Player.inventory,
                    i => items.Contains(i.type));

                if(index == -1) {
                    minimumLures = LURE_MIN;
                    return false;
                }

                lineItem =
                    ItemLoader.GetItem(Player.inventory[index].type)
                    as FishingLineBase;
            }
            else {
                accessorySlot = index;

                lineItem =
                    ItemLoader.GetItem(Player.armor[index].type)
                    as FishingLineBase;
            }

            minimumLures = lineItem.Lures;
            return true;
        }
    }
}
