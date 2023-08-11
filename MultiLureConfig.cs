using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace MultiLure {
    public class MultiLureConfig : ModConfig {
        public override ConfigScope Mode => ConfigScope.ClientSide;

        [Header("FishingLureOptions")]
        [DefaultValue(5)]
        [Range(0, 99999)]
        public int IronFishingLineLures;

        [DefaultValue(10)]
        [Range(0, 99999)]
        public int SilverFishingLineLures;
        
        [DefaultValue(25)]
        [Range(0, 99999)]
        public int GoldFishingLineLures;

        [DefaultValue(50)]
        [Range(0, 99999)]
        public int CobaltFishingLineLures;

        [DefaultValue(75)]
        [Range(0, 99999)]
        public int MythrilFishingLineLures;

        [DefaultValue(100)]
        [Range(0, 99999)]
        public int AdamantiteFishingLineLures;

        [Header("CheatOptions")]
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
