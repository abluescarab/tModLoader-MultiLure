﻿using Terraria;
using Terraria.ModLoader;

namespace MultiLure {
    public class GlobalQuestItem : GlobalItem {
        public override void SetDefaults(Item item) {
            if(!item.questItem) return;

            item.maxStack = 99;
            item.uniqueStack = false;
        }
    }
}
