using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Terraria;
using Terraria.ModLoader;

namespace MultiLure {
    public class MultiLure : Mod {
        internal const string PermissionName = "ModifyLureCount";
        internal const string PermissionDisplayName = "Modify Lure Count";

        private RepeatHotKey addLureKey;
        private RepeatHotKey removeLureKey;

        public override void Load() {
            Properties = new ModProperties() {
                Autoload = true
            };

            AddGlobalItem("MultiLureFishingPole", new GlobalFishingPoleItem());
            AddGlobalItem("MultiLureQuestItem", new GlobalQuestItem());

            addLureKey = new RepeatHotKey(this, "Add Lure", Keys.OemCloseBrackets.ToString());
            removeLureKey = new RepeatHotKey(this, "Remove Lure", Keys.OemOpenBrackets.ToString());
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
                    (Action)delegate { ChangeLures(true); },
                    (Func<string>)AddLureTooltip);

            cheatSheet.Call("AddButton_Test",
                this.GetTexture("Textures/RemoveLure"),
                (Action)delegate { ChangeLures(false); },
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
                    (Action)delegate { ChangeLures(true); },
                    (Action<bool>)PermissionsChanged,
                    (Func<string>)AddLureTooltip);

                herosMod.Call(
                    "AddSimpleButton",
                    PERMISSION_NAME,
                    GetTexture("Textures/RemoveLure"),
                    (Action)delegate { ChangeLures(false); },
                    (Action<bool>)PermissionsChanged,
                    (Func<string>)RemoveLureTooltip);
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

        private MultiLurePlayer GetModPlayer() {
            return Main.LocalPlayer.GetModPlayer<MultiLurePlayer>(this);
        }
    }
}
