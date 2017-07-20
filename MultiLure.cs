using System;
using Microsoft.Xna.Framework.Input;
using Terraria;
using Terraria.ModLoader;

namespace MultiLure {
    public class MultiLure : Mod {
        internal const string PERMISSION_NAME = "ModifyLureCount";
        internal const string PERMISSION_DISPLAY_NAME = "Modify Lure Count";
        public const int MAX_LURES = 100;

        private ModHotKey addLureKey;
        private ModHotKey removeLureKey;

        public override void Load() {
            Properties = new ModProperties() {
                Autoload = true
            };

            AddGlobalItem("MultiLureFishingPole", new GlobalFishingPoleItem());

            addLureKey = RegisterHotKey("Add Lure", Keys.OemOpenBrackets.ToString());
            removeLureKey = RegisterHotKey("Remove Lure", Keys.OemCloseBrackets.ToString());
        }

        public override void HotKeyPressed(string name) {
            if(addLureKey.JustPressed) {
                AddLure(true);
            }
            else if(removeLureKey.JustPressed) {
                RemoveLure(true);
            }
        }

        public override void PostSetupContent() {
            Mod cheatSheet = ModLoader.GetMod("CheatSheet");
            Mod herosMod = ModLoader.GetMod("HEROsMod");

            if(cheatSheet != null && !Main.dedServ) {
                SetupCheatSheetIntegration(cheatSheet);
            }

            if(herosMod != null) {
                SetupHerosModIntegration(herosMod);
            }
        }

        private void SetupCheatSheetIntegration(Mod cheatSheet) {
            cheatSheet.Call("AddButton_Test",
                    this.GetTexture("Textures/AddLure"),
                    (Action)delegate { AddLure(); },
                    (Func<string>)AddLureTooltip);

            cheatSheet.Call("AddButton_Test",
                this.GetTexture("Textures/RemoveLure"),
                (Action)delegate { RemoveLure(); },
                (Func<string>)RemoveLureTooltip);
        }

        private void SetupHerosModIntegration(Mod herosMod) {
            herosMod.Call(
                "AddPermission",
                PERMISSION_NAME,
                PERMISSION_DISPLAY_NAME);

            if(!Main.dedServ) {
                herosMod.Call(
                    "AddSimpleButton",
                    PERMISSION_NAME,
                    GetTexture("Textures/AddLure"),
                    (Action)delegate { AddLure(); },
                    (Action<bool>)PermissionsChanged,
                    (Func<string>)AddLureTooltip);

                herosMod.Call(
                    "AddSimpleButton",
                    PERMISSION_NAME,
                    GetTexture("Textures/RemoveLure"),
                    (Action)delegate { RemoveLure(); },
                    (Action<bool>)PermissionsChanged,
                    (Func<string>)RemoveLureTooltip);
            }
        }

        public void PermissionsChanged(bool hasPermission) {
            if(!hasPermission) {
                GetModPlayer().LureCount = 1;
            }
        }

        public void AddLure(bool showMessages = false) {
            MultiLurePlayer player = GetModPlayer();
            bool success = true;

            if(Main.keyState.PressingShift()) {
                if((player.LureCount + 10) <= MAX_LURES) {
                    player.LureCount += 10;
                }
                else if(player.LureCount < MAX_LURES) {
                    player.LureCount = MAX_LURES;
                }
                else {
                    success = false;
                }
            }
            else if(player.LureCount < MAX_LURES) {
                player.LureCount++;
            }
            else {
                success = false;
            }

            if(showMessages) {
                if(success)
                    Main.NewText("Lures increased to " + player.LureCount + ".");
                else
                    Main.NewText("You already have the maximum number of lures (" + MAX_LURES + ").");
            }
        }
        
        private string AddLureTooltip() {
            return "Add Lure (Current: " + GetModPlayer().LureCount + ")";
        }

        public void RemoveLure(bool showMessages = false) {
            MultiLurePlayer player = GetModPlayer();
            bool success = true;

            if(Main.keyState.PressingShift()) {
                if((player.LureCount - 10) >= 1) {
                    player.LureCount -= 10;
                }
                else if(player.LureCount > 1) {
                    player.LureCount = 1;
                }
                else {
                    success = false;
                }
            }
            else if(player.LureCount > 1) {
                player.LureCount--;
            }
            else {
                success = false;
            }

            if(showMessages) {
                if(success)
                    Main.NewText("Lures decreased to " + player.LureCount + ".");
                else
                    Main.NewText("You already have the minimum number of lures (1).");
            }
        }

        private string RemoveLureTooltip() {
            return "Remove Lure (Current: " + GetModPlayer().LureCount.ToString() + ")";
        }

        private MultiLurePlayer GetModPlayer() {
            return Main.LocalPlayer.GetModPlayer<MultiLurePlayer>(this);
        }
    }
}
