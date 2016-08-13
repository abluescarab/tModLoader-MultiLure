using System;
using Microsoft.Xna.Framework.Input;
using Terraria;
using Terraria.ModLoader;
using Terraria.GameInput;

namespace MultiLure {
    public class MultiLure : Mod {
        internal const string PERMISSION_NAME = "ModifyLureCount";
        internal const string PERMISSION_DISPLAY_NAME = "Modify Lure Count";
        public const int MAX_LURES = 20;
        
        private HotKey addLureKey = new HotKey("Add Lure", Keys.OemOpenBrackets);
        private HotKey removeLureKey = new HotKey("Remove Lure", Keys.OemCloseBrackets);

        public override void Load() {
            Properties = new ModProperties() {
                Autoload = true
            };

            AddGlobalItem("MultiLureFishingPole", new GlobalFishingPoleItem());

            RegisterHotKey(addLureKey.Name, addLureKey.DefaultKey.ToString());
            RegisterHotKey(removeLureKey.Name, removeLureKey.DefaultKey.ToString());
        }

        public override void HotKeyPressed(string name) {
            if(HotKey.JustPressed(this, name)) {
                if(name.Equals(addLureKey.Name)) {
                    AddLure();
                }
                else if(name.Equals(removeLureKey.Name)) {
                    RemoveLure();
                }
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
                    (Action)AddLureAction,
                    (Func<string>)AddLureTooltip);

            cheatSheet.Call("AddButton_Test",
                this.GetTexture("Textures/RemoveLure"),
                (Action)RemoveLureAction,
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
                    (Action)AddLureAction,
                    (Action<bool>)PermissionsChanged,
                    (Func<string>)AddLureTooltip);

                herosMod.Call(
                    "AddSimpleButton",
                    PERMISSION_NAME,
                    GetTexture("Textures/RemoveLure"),
                    (Action)RemoveLureAction,
                    (Action<bool>)PermissionsChanged,
                    (Func<string>)RemoveLureTooltip);
            }
        }

        public void PermissionsChanged(bool hasPermission) {
            if(!hasPermission) {
                GetModPlayer().LureCount = 1;
            }
        }

        public void AddLure() {
            MPlayer player = GetModPlayer();

            if(player.LureCount < MAX_LURES) {
                AddLureAction();
                Main.NewText("Lures increased to " + player.LureCount.ToString() + ".");
            }
            else {
                Main.NewText("You already have the maximum number of lures (" + MAX_LURES + ").");
            }
        }

        private void AddLureAction() {
            GetModPlayer().LureCount++;
        }

        private string AddLureTooltip() {
            return "Add Lure (Current: " + GetModPlayer().LureCount.ToString() + ")";
        }

        public void RemoveLure() {
            MPlayer player = GetModPlayer();

            if(player.LureCount > 1) {
                RemoveLureAction();
                Main.NewText("Lures decreased to " + player.LureCount.ToString() + ".");
            }
            else {
                Main.NewText("You already have the minimum number of lures (1).");
            }
        }

        private void RemoveLureAction() {
            GetModPlayer().LureCount--;
        }

        private string RemoveLureTooltip() {
            return "Remove Lure (Current: " + GetModPlayer().LureCount.ToString() + ")";
        }

        private MPlayer GetModPlayer() {
            return Main.player[Main.myPlayer].GetModPlayer<MPlayer>(this);
        }
    }
}
