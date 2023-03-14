using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace MultiLure {
    internal class MultiLureSystem : ModSystem {
        internal const string WhiteStringGroup = "MultiLure:WhiteString";
        internal const string CopperBarGroup = "MultiLure:CopperBar";
        internal const string IronBarGroup = "MultiLure:IronBar";
        internal const string SilverBarGroup = "MultiLure:SilverBar";
        internal const string GoldBarGroup = "MultiLure:GoldBar";
        internal const string CobaltBarGroup = "MultiLure:CobaltBar";
        internal const string MythrilBarGroup = "MultiLure:MythrilBar";
        internal const string AdamantiteBarGroup = "MultiLure:AdamantiteBar";

        public static RepeatHotKey AddLureKey;
        public static RepeatHotKey RemoveLureKey;

        public override void Load() {
            if(ModContent.GetInstance<MultiLureConfig>().EnableHotkeys) {
                AddLureKey = new RepeatHotKey(Mod, "Add Lure", Keys.OemCloseBrackets.ToString());
                RemoveLureKey = new RepeatHotKey(Mod, "Remove Lure", Keys.OemOpenBrackets.ToString());
            }
        }

        public override void AddRecipeGroups() {
            var any = Language.GetText("LegacyMisc.37");

            RecipeGroup whiteString = new RecipeGroup(
                () => $"{any} {Language.GetTextValue("ItemName.WhiteString")}",
                ItemID.WhiteString, ItemID.BlackString, ItemID.BlueString,
                ItemID.BrownString, ItemID.CyanString, ItemID.GreenString,
                ItemID.LimeString, ItemID.OrangeString, ItemID.PinkString,
                ItemID.PurpleString, ItemID.RainbowString, ItemID.RedString,
                ItemID.SkyBlueString, ItemID.TealString, ItemID.VioletString,
                ItemID.YellowString);
            RecipeGroup copperBar = new RecipeGroup(
                () => $"{any} {Language.GetText("ItemName.CopperBar")}",
                ItemID.CopperBar, ItemID.TinBar);
            RecipeGroup ironBar = new RecipeGroup(
                () => $"{any} {Language.GetText("ItemName.IronBar")}",
                ItemID.IronBar, ItemID.LeadBar);
            RecipeGroup silverBar = new RecipeGroup(
                () => $"{any} {Language.GetText("ItemName.SilverBar")}",
                ItemID.SilverBar, ItemID.TungstenBar);
            RecipeGroup goldBar = new RecipeGroup(
                () => $"{any} {Language.GetText("ItemName.GoldBar")}",
                ItemID.GoldBar, ItemID.PlatinumBar);
            RecipeGroup cobaltBar = new RecipeGroup(
                () => $"{any} {Language.GetText("ItemName.CobaltBar")}",
                ItemID.CobaltBar, ItemID.PalladiumBar);
            RecipeGroup mythrilBar = new RecipeGroup(
                () => $"{any} {Language.GetText("ItemName.MythrilBar")}",
                ItemID.MythrilBar, ItemID.OrichalcumBar);
            RecipeGroup adamantiteBar = new RecipeGroup(
                () => $"{any} {Language.GetText("ItemName.AdamantiteBar")}",
                ItemID.AdamantiteBar, ItemID.TitaniumBar);

            RecipeGroup.RegisterGroup(WhiteStringGroup, whiteString);
            RecipeGroup.RegisterGroup(CopperBarGroup, copperBar);
            RecipeGroup.RegisterGroup(IronBarGroup, ironBar);
            RecipeGroup.RegisterGroup(SilverBarGroup, silverBar);
            RecipeGroup.RegisterGroup(GoldBarGroup, goldBar);
            RecipeGroup.RegisterGroup(CobaltBarGroup, cobaltBar);
            RecipeGroup.RegisterGroup(MythrilBarGroup, mythrilBar);
            RecipeGroup.RegisterGroup(AdamantiteBarGroup, adamantiteBar);
        }

        public override void PostUpdateInput() {
            if(AddLureKey == null || RemoveLureKey == null)
                return;

            GameTime time = Main._drawInterfaceGameTime;

            if(AddLureKey.Update(time)) {
                ChangeLures(true);
            }
            else if(RemoveLureKey.Update(time)) {
                ChangeLures(false);
            }
        }

        public static void ChangeLures(bool increase) {
            MultiLurePlayer player = Main.CurrentPlayer.GetModPlayer<MultiLurePlayer>();

            if(player.CheatLureMinimum < player.LureMinimum) {
                player.CheatLureMinimum = player.LureMinimum;
            }

            bool success = true;
            int count = (increase ? 1 : -1);

            if(Main.keyState.PressingShift()) {
                count = (increase ? 10 : -10);
            }

            int cheatMinimum = player.CheatLureMinimum + count;

            if(increase && cheatMinimum > player.LureMaximum ||
                !increase && cheatMinimum < player.LureMinimum) {
                success = false;
            }
            else {
                player.CheatLureMinimum = 
                    Math.Clamp(cheatMinimum, player.LureMinimum, player.LureMaximum);
            }

            if(success) {
                Main.NewText("Lures " + (increase ? "increased" : "decreased") + " to " + player.CheatLureMinimum + ".");
            }
            else {
                Main.NewText($"You already have the {(increase ? "maximum" : "minimum")} numbers of lures " +
                             $"({(increase ? player.LureMaximum : player.LureMinimum)}).");

                if(increase) AddLureKey.Stop();
                else RemoveLureKey.Stop();
            }
        }
    }
}
