using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace MultiLure {
    public class MultiLureConfig : ModConfig {
        public override ConfigScope Mode => ConfigScope.ClientSide;

        [DefaultValue(true)]
        [Label("Enable hotkeys")]
        [ReloadRequired]
        public bool EnableHotkeys;

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
