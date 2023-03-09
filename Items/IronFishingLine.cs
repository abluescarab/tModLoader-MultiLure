﻿using Terraria;
using Terraria.ID;

namespace MultiLure.Items {
    public class IronFishingLine : FishingLineBase {
        public override string OriginalName => "IronFishingLine";
        public override string AlternativeName => "LeadFishingLine";
        public override string OriginalDisplayName => "Iron Fishing Line";
        public override string AlternativeDisplayName => "Lead Fishing Line";
        public override int Lures => 10;

        public override void SetDefaults() {
            base.SetDefaults();
            Item.value = Item.sellPrice(0, 0, 27, 50);
            Item.rare = ItemRarityID.Blue;
        }

        public override void AddRecipes() {
            AddRecipes(MultiLureSystem.IronBarGroup);
        }
    }
}
