using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ModLoader;

namespace MultiLure {
    public class MultiLure : Mod {
        private const string AddLureTexture = "MultiLure/Textures/AddLure";
        private const string RemoveLureTexture = "MultiLure/Textures/RemoveLure";

        internal const string PermissionName = "ModifyLureCount";
        internal const string PermissionDisplayName = "Modify Lure Count";

        public MultiLure() {
            ContentAutoloadingEnabled = true;
        }

        public override void PostSetupContent() {
            Func<string> addTooltip = () => {
                MultiLurePlayer player = Main.CurrentPlayer.GetModPlayer<MultiLurePlayer>();
                return $"Add Lure (Current: {player.LureCount})";
            };

            Func<string> removeTooltip = () => {
                MultiLurePlayer player = Main.CurrentPlayer.GetModPlayer<MultiLurePlayer>();
                return $"Remove Lure (Current: {player.LureCount})";
            };

            MultiLureConfig config = ModContent.GetInstance<MultiLureConfig>();

            if(config.EnableCheatSheetIntegration) {
                if(ModLoader.TryGetMod("CheatSheet", out Mod cheatSheet) && !Main.dedServ) {
                    cheatSheet.Call("AddButton_Test",
                                    ModContent.Request<Texture2D>(AddLureTexture),
                                    (Action)delegate { MultiLureSystem.ChangeLures(true); },
                                    addTooltip);

                    cheatSheet.Call("AddButton_Test",
                                    ModContent.Request<Texture2D>(RemoveLureTexture),
                                    (Action)delegate { MultiLureSystem.ChangeLures(false); },
                                    removeTooltip);
                }
            }

            if(config.EnableHerosModIntegration) {
                if(ModLoader.TryGetMod("HEROsMod", out Mod herosMod)) {
                    herosMod.Call("AddPermission", PermissionName, PermissionDisplayName);

                    if(!Main.dedServ) {
                        herosMod.Call("AddSimpleButton", PermissionName, ModContent.Request<Texture2D>(AddLureTexture),
                                      (Action)delegate { MultiLureSystem.ChangeLures(true); },
                                      (Action<bool>)PermissionsChanged,
                                      addTooltip);

                        herosMod.Call("AddSimpleButton", PermissionName, ModContent.Request<Texture2D>(RemoveLureTexture),
                                      (Action)delegate { MultiLureSystem.ChangeLures(false); },
                                      (Action<bool>)PermissionsChanged,
                                      removeTooltip);
                    }
                }
            }
        }

        public void PermissionsChanged(bool hasPermission) {
            if(!hasPermission) {
                MultiLurePlayer player 
                    = Main.CurrentPlayer.GetModPlayer<MultiLurePlayer>();
                player.LureMinimum = 1;
                player.LureMaximum = 1;
            }
        }
    }
}
