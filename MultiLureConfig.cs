using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace MultiLure {
    public class MultiLureConfig : ModConfig {
        public override ConfigScope Mode => ConfigScope.ClientSide;

        public static MultiLureConfig Instance;

        [DefaultValue(true)]
        [Label("Enable hotkeys")]
        [ReloadRequired]
        public bool EnableHotkeys;

        [DefaultValue(false)]
        [Label("Enable fishing lines")]
        [ReloadRequired]
        public bool EnableFishingLines;

        [DefaultValue(true)]
        [Label("Enable Cheat Sheet integration")]
        [ReloadRequired]
        public bool EnableCheatSheetIntegration;

        [DefaultValue(true)]
        [Label("Enable HERO's Mod integration")]
        [ReloadRequired]
        public bool EnableHerosModIntegration;
    }
}
