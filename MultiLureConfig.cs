using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace MultiLure {
    public class MultiLureConfig : ModConfig {
        public override ConfigScope Mode => ConfigScope.ClientSide;

        [DefaultValue(true)]
        [ReloadRequired]
        public bool EnableHotkeys;

        [DefaultValue(true)]
        [ReloadRequired]
        public bool EnableCheatSheetIntegration;

        [DefaultValue(true)]
        [ReloadRequired]
        public bool EnableHerosModIntegration;
    }
}
