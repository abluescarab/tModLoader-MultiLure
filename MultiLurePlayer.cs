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
            if(AnyCheatsEnabled()) {
                if(CheatLureMinimum < LureMinimum) {
                    CheatLureMinimum = LureMinimum;
                }

                LureCount = CheatLureMinimum > LureMinimum
                    ? CheatLureMinimum
                    : LureMinimum;
            }
            else {
                LureCount = LureMinimum;
            }
            
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

        private static bool AnyCheatsEnabled() {
            MultiLureConfig config = ModContent.GetInstance<MultiLureConfig>();

            return config.EnableCheatSheetIntegration 
                || config.EnableHerosModIntegration 
                || config.EnableHotkeys;
        }
    }
}
