using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using ModConfiguration;
using MultiLure.Items;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace MultiLure {
    public class MultiLure : Mod {
        internal const string WhiteStringGroup = "MultiLure:WhiteString";
        internal const string CopperBarGroup = "MultiLure:CopperBar";
        internal const string IronBarGroup = "MultiLure:IronBar";
        internal const string SilverBarGroup = "MultiLure:SilverBar";
        internal const string GoldBarGroup = "MultiLure:GoldBar";
        internal const string CobaltBarGroup = "MultiLure:CobaltBar";
        internal const string MythrilBarGroup = "MultiLure:MythrilBar";
        internal const string AdamantiteBarGroup = "MultiLure:AdamantiteBar";
        internal const string PermissionName = "ModifyLureCount";
        internal const string PermissionDisplayName = "Modify Lure Count";
        internal const string EnableHotKeys = "enableHotKeys";
        internal const string EnableFishingLines = "enableFishingLines";
        internal const string AddToCheatSheet = "addToCheatSheet";
        internal const string AddToHerosMod = "addToHEROsMod";

        internal static ModConfig Config;
        internal readonly Dictionary<string, int> FishingLineItems = new Dictionary<string, int>();

        private RepeatHotKey addLureKey;
        private RepeatHotKey removeLureKey;

        public override void Load() {
            Properties = new ModProperties {
                Autoload = true
            };

            Config = new ModConfig("MultiLure");

            Config.Add(EnableHotKeys, true);
            Config.Add(EnableFishingLines, false);
            Config.Add(AddToCheatSheet, true);
            Config.Add(AddToHerosMod, true);
            Config.Load();

            if(Config.Get<bool>(EnableFishingLines)) {
                AddFishingLines(typeof(CopperFishingLine),
                                typeof(IronFishingLine),
                                typeof(SilverFishingLine),
                                typeof(GoldFishingLine),
                                typeof(CobaltFishingLine),
                                typeof(MythrilFishingLine),
                                typeof(AdamantiteFishingLine));
            }

            if(Config.Get<bool>(EnableHotKeys)) {
                addLureKey = new RepeatHotKey(this, "Add Lure", Keys.OemCloseBrackets.ToString());
                removeLureKey = new RepeatHotKey(this, "Remove Lure", Keys.OemOpenBrackets.ToString());
            }
        }

        public override void Unload() {
            Config = null;
        }

        public override void HotKeyPressed(string name) {
            if(addLureKey.Key.JustPressed) {
                ChangeLures(true);
                addLureKey.Start(Main._drawInterfaceGameTime);
            }
            else if(removeLureKey.Key.JustPressed) {
                ChangeLures(false);
                removeLureKey.Start(Main._drawInterfaceGameTime);
            }
        }

        public override void PostUpdateInput() {
            if(addLureKey == null || removeLureKey == null)
                return;

            GameTime time = Main._drawInterfaceGameTime;

            if(addLureKey.Update(time)) {
                ChangeLures(true);
            }
            else if(removeLureKey.Update(time)) {
                ChangeLures(false);
            }
        }

        public override void PostSetupContent() {
            Func<string> addTooltip = () => $"Add Lure (Current: {GetModPlayer().LureCount})";
            Func<string> removeTooltip = () => $"Remove Lure (Current: {GetModPlayer().LureCount})";

            if(Config.Get<bool>(AddToCheatSheet)) {
                Mod cheatSheet = ModLoader.GetMod("CheatSheet");

                if(cheatSheet != null && !Main.dedServ) {
                    cheatSheet.Call("AddButton_Test",
                                    GetTexture("Textures/AddLure"),
                                    (Action)delegate { ChangeLures(true); },
                                    addTooltip);

                    cheatSheet.Call("AddButton_Test",
                                    GetTexture("Textures/RemoveLure"),
                                    (Action)delegate { ChangeLures(false); },
                                    removeTooltip);
                }
            }

            if(Config.Get<bool>(AddToHerosMod)) {
                Mod herosMod = ModLoader.GetMod("HEROsMod");

                if(herosMod != null) {
                    herosMod.Call("AddPermission", PermissionName, PermissionDisplayName);

                    if(!Main.dedServ) {
                        herosMod.Call("AddSimpleButton", PermissionName, GetTexture("Textures/AddLure"),
                                      (Action)delegate { ChangeLures(true); },
                                      (Action<bool>)PermissionsChanged,
                                      addTooltip);

                        herosMod.Call("AddSimpleButton", PermissionName, GetTexture("Textures/RemoveLure"),
                                      (Action)delegate { ChangeLures(false); },
                                      (Action<bool>)PermissionsChanged,
                                      removeTooltip);
                    }
                }
            }
        }

        public override void AddRecipeGroups() {
            if(!Config.Get<bool>(EnableFishingLines)) return;

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

        public void PermissionsChanged(bool hasPermission) {
            if(!hasPermission) {
                MultiLurePlayer player = GetModPlayer();
                player.LureMinimum = 1;
                player.LureMaximum = 1;
            }
        }

        public void ChangeLures(bool increase) {
            MultiLurePlayer player = GetModPlayer();

            bool success = true;
            int count = (increase ? 1 : -1);

            if(Main.keyState.PressingShift()) {
                count = (increase ? 10 : -10);
            }

            int minimum = 1;

            player.AnyLineEquipped(out minimum);

            if((!increase && player.LureMinimum == minimum) || (increase && player.LureMinimum == player.LureMaximum)) {
                success = false;
            }
            else {
                int newCount = player.LureMinimum + count;

                if(newCount >= 1 && newCount <= player.LureMaximum) {
                    player.LureMinimum += count;
                }
                else if(newCount < minimum) {
                    player.LureMinimum = minimum;
                }
                else if(newCount > player.LureMaximum) {
                    player.LureMinimum = player.LureMaximum;
                }
                else {
                    success = false;
                }
            }

            if(success) {
                Main.NewText("Lures " + (increase ? "increased" : "decreased") + " to " + player.LureMinimum + ".");
            }
            else {
                Main.NewText($"You already have the {(increase ? "maximum" : "minimum")} numbers of lures " +
                             $"({(increase ? player.LureMaximum : minimum)}).");

                if(increase) addLureKey.Stop();
                else removeLureKey.Stop();
            }
        }

        public void AddFishingLines(params Type[] types) {
            foreach(var type in types) {
                var inst1 = (FishingLineBase)Activator.CreateInstance(type);
                var inst2 = (FishingLineBase)Activator.CreateInstance(type);

                AddItem(inst1.OriginalName, inst1);
                AddItem(inst1.AlternativeName, inst2);

                FishingLineItems.Add(inst1.OriginalName, ItemType(inst1.OriginalName));
                FishingLineItems.Add(inst1.AlternativeName, ItemType(inst1.AlternativeName));
            }
        }

        private static MultiLurePlayer GetModPlayer() {
            return Main.LocalPlayer.GetModPlayer<MultiLurePlayer>();
        }
    }
}
