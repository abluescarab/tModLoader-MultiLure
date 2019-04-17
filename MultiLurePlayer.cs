using System.Collections.Generic;
using System.Linq;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace MultiLure {
    public class MultiLurePlayer : ModPlayer {
        private const string LURE_COUNT = "lurecount";

        public int LureMaximum { get; set; } = 100;
        public int LureMinimum { get; set; } = 1;
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
