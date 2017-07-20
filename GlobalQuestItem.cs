using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace StackingQuestFish {
    class GlobalSQFItem : GlobalItem {
        public override bool Autoload(ref string name) {
            return true;
        }

        public override void SetDefaults(Item item) {
            if(item.questItem) {
                item.maxStack = 99;
                item.uniqueStack = false;
            }
        }
    }
}
