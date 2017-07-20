using System;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.IO;
using Terraria.ModLoader.IO;

namespace MultiLure {
    public class MultiLurePlayer : ModPlayer {
        private const string LURE_COUNT = "lurecount";
        private byte lureCount = 1;

        public byte LureCount {
            get { return lureCount; }
            set {
                if(value < 1) {
                    lureCount = 1;
                }
                else if(value > MultiLure.MAX_LURES) {
                    lureCount = MultiLure.MAX_LURES;
                }
                else {
                    lureCount = value;
                }
            }
        }

        public override bool Autoload(ref string name) {
            return true; 
        }

        public override TagCompound Save() {
            return new TagCompound {
                { LURE_COUNT, lureCount }
            };
        }

        public override void Load(TagCompound tag) {
            lureCount = tag.GetByte(LURE_COUNT);
        }

        public override void LoadLegacy(BinaryReader reader) {
            try {
                lureCount = reader.ReadByte();
            }
            catch(EndOfStreamException) {
                lureCount = 1;
            }
        }
    }
}
