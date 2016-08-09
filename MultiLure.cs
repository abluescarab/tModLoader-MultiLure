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
                    (Action)this.AddLure,
                    (Func<string>)this.AddLureTooltip);

            cheatSheet.Call("AddButton_Test",
                this.GetTexture("Textures/RemoveLure"),
                (Action)this.RemoveLure,
                (Func<string>)this.RemoveLureTooltip);
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
                    (Action)AddLure,
                    (Action<bool>)PermissionsChanged,
                    (Func<string>)AddLureTooltip);

                herosMod.Call(
                    "AddSimpleButton",
                    PERMISSION_NAME,
                    GetTexture("Textures/RemoveLure"),
                    (Action)RemoveLure,
                    (Action<bool>)PermissionsChanged,
                    (Func<string>)RemoveLureTooltip);
            }
        }

        public void PermissionsChanged(bool hasPermission) {
            if(!hasPermission) {
                Main.player[Main.myPlayer].GetModPlayer<MPlayer>(this).LureCount = 1;
            }
        }

        public string AddLureTooltip() {
            return "Add Lure (Current: " + Main.player[Main.myPlayer].GetModPlayer<MPlayer>(this).LureCount.ToString() + ")";
        }

        public void AddLure() {
            Main.player[Main.myPlayer].GetModPlayer<MPlayer>(this).LureCount++;
        }

        public string RemoveLureTooltip() {
            return "Remove Lure (Current: " + Main.player[Main.myPlayer].GetModPlayer<MPlayer>(this).LureCount.ToString() + ")";
        }

        public void RemoveLure() {
            Main.player[Main.myPlayer].GetModPlayer<MPlayer>(this).LureCount--;
        }
    }
}
