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

        private bool lureKeyPressed = false;
        private double lureKeyPressTime = 0.0;
        private int lureKeySpeedMultipler = 1;
        private double lureKeyIncreaseTime = 0.0;

        public override void Load() {
            Properties = new ModProperties() {
                Autoload = true
            };

            AddGlobalItem("MultiLureFishingPole", new GlobalFishingPoleItem());
            AddGlobalItem("MultiLureQuestItem", new GlobalQuestItem());

            addLureKey = RegisterHotKey("Add Lure", Keys.OemOpenBrackets.ToString());
            removeLureKey = RegisterHotKey("Remove Lure", Keys.OemCloseBrackets.ToString());
        }

        public override void HotKeyPressed(string name) {
            if(addLureKey.JustPressed) {
                ChangeLures(true);
                lureKeyPressed = true;
                lureKeyPressTime = Main._drawInterfaceGameTime.TotalGameTime.TotalMilliseconds;
            }
            else if(removeLureKey.JustPressed) {
                ChangeLures(false);
                lureKeyPressed = true;
                lureKeyPressTime = Main._drawInterfaceGameTime.TotalGameTime.TotalMilliseconds;
            }
        }

        public override void PostUpdateInput() {
            if(!lureKeyPressed) return;

            double time = Main._drawInterfaceGameTime.TotalGameTime.TotalMilliseconds;

            if(!addLureKey.Current && !removeLureKey.Current) {
                lureKeyPressed = false;
                lureKeySpeedMultipler = 1;
                return;
            }

            if(time - lureKeyIncreaseTime >= 1000.0 & (lureKeySpeedMultipler < 32)) {
                lureKeySpeedMultipler *= 2;
                lureKeyIncreaseTime = time;
            }

            if(time - lureKeyPressTime >= 1000.0 / lureKeySpeedMultipler) {
                if(addLureKey.Current) {
                    ChangeLures(true);
                }
                else if(removeLureKey.Current) {
                    ChangeLures(false);
                }

                lureKeyPressTime = time;
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
            bool success = true;
            int count = (increase ? 1 : -1);
            int newCount = 0;

            if(Main.keyState.PressingShift()) {
                count = (increase ? 10 : -10);
            }

            if((!increase && player.LureCount == 1) ||
               (increase && player.LureCount == MAX_LURES)) {
                success = false;
            }
            else {
                newCount = player.LureCount + count;

                if(newCount >= 1 && newCount <= MAX_LURES) {
                    player.LureCount += count;
                }
                else if(newCount < 1) {
                    player.LureCount = 1;
                }
                else if(newCount > MAX_LURES) {
                    player.LureCount = MAX_LURES;
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
                             $"({(min ? 1 : MAX_LURES)}).");
                lureKeyPressed = false;
                lureKeySpeedMultipler = 1;
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
