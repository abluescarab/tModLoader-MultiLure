using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using ModConfiguration;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace MultiLure {
    public class MultiLure : Mod {
        internal const string PermissionName = "ModifyLureCount";
        internal const string PermissionDisplayName = "Modify Lure Count";
        internal const string EnableHotKeys = "enableHotKeys";
        internal const string AddToCheatSheet = "addToCheatSheet";
        internal const string AddToHerosMod = "addToHEROsMod";

        internal static readonly ModConfig Config = new ModConfig("MultiLure");

        private RepeatHotKey addLureKey;
        private RepeatHotKey removeLureKey;

        public override void Load() {
            Properties = new ModProperties() {
                Autoload = true
            };

            AddGlobalItem("MultiLureFishingPole", new GlobalFishingPoleItem());
            AddGlobalItem("MultiLureQuestItem", new GlobalQuestItem());

            Config.Add(EnableHotKeys, true);
            Config.Add(AddToCheatSheet, true);
            Config.Add(AddToHerosMod, true);
            Config.Load();

            if(Config.Get<bool>(EnableHotKeys)) {
                addLureKey = new RepeatHotKey(this, "Add Lure", Keys.OemCloseBrackets.ToString());
                removeLureKey = new RepeatHotKey(this, "Remove Lure", Keys.OemOpenBrackets.ToString());
            }
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

        public void PermissionsChanged(bool hasPermission) {
            if(!hasPermission) {
                GetModPlayer().LureCount = 1;
            }
        }

        public void ChangeLures(bool increase) {
            MultiLurePlayer player = GetModPlayer();
            int maxLures = player.LureMaximum;

            bool success = true;
            int count = (increase ? 1 : -1);

            if(Main.keyState.PressingShift()) {
                count = (increase ? 10 : -10);
            }

            if((!increase && player.LureCount == 1) ||
               (increase && player.LureCount == maxLures)) {
                success = false;
            }
            else {
                int newCount = player.LureCount + count;

                if(newCount >= 1 && newCount <= maxLures) {
                    player.LureCount += count;
                }
                else if(newCount < 1) {
                    player.LureCount = 1;
                }
                else if(newCount > maxLures) {
                    player.LureCount = maxLures;
                }
                else {
                    success = false;
                }
            }

            if(success) {
                Main.NewText("Lures " + (increase ? "increased" : "decreased") + " to " + player.LureCount + ".");
            }
            else {
                bool min = player.LureCount == 1;
                Main.NewText($"You already have the {(min ? "minimum" : "maximum")} numbers of lures " +
                             $"({(min ? 1 : maxLures)}).");

                if(min) removeLureKey.Stop();
                else addLureKey.Stop();
            }
        }

        private string AddLureTooltip() {
            return "Add Lure (Current: " + GetModPlayer().LureCount + ")";
        }

        private string RemoveLureTooltip() {
            return "Remove Lure (Current: " + GetModPlayer().LureCount.ToString() + ")";
        }

        private static MultiLurePlayer GetModPlayer() {
            return Main.LocalPlayer.GetModPlayer<MultiLurePlayer>();
        }
    }
}
