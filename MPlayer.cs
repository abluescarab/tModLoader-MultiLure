using System;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.IO;

namespace MultiLure {
    public class MPlayer : ModPlayer {
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

        public override void SaveCustomData(BinaryWriter writer) {
            writer.Write(lureCount);
        }

        public override void LoadCustomData(BinaryReader reader) {
            try {
                lureCount = reader.ReadByte();
            }
            catch(EndOfStreamException) {
                lureCount = 1;
            }
        }
    }
}
