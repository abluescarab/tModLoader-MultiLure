using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace MultiLure {
    public class MultiLurePlayer : ModPlayer {
        private const string LURE_COUNT = "lurecount";

        public int LureCount { get; set; } = 1;

        public override TagCompound Save() {
            return new TagCompound {
                { LURE_COUNT, (byte)LureCount }
            };
        }

        public override void Load(TagCompound tag) {
            LureCount = tag.GetByte(LURE_COUNT);
        }
    }
}
