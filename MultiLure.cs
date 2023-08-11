using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ModLoader;

// TODO: reset lures if hotkeys/integrations off and item deleted

namespace MultiLure {
    public class MultiLure : Mod {
        private const string AddLureTexture = "MultiLure/Textures/AddLure";
        private const string RemoveLureTexture = "MultiLure/Textures/RemoveLure";

        internal const string PermissionName = "ModifyLureCount";

        public MultiLure() {
            ContentAutoloadingEnabled = true;
        }

        public override void PostSetupContent() {
            Func<string> addTooltip = () 
                => $"Add Lure (Current: " +
                   $"{Main.CurrentPlayer.GetModPlayer<MultiLurePlayer>().LureCount})";

            Func<string> removeTooltip = ()
                => $"Remove Lure (Current: " +
                   $"{Main.CurrentPlayer.GetModPlayer<MultiLurePlayer>().LureCount})";

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
                    herosMod.Call("AddPermission", PermissionName, "Modify Lure Count");

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

        public static void PermissionsChanged(bool hasPermission) {
            if(!hasPermission) {
                MultiLurePlayer player 
                    = Main.CurrentPlayer.GetModPlayer<MultiLurePlayer>();
                player.LureMinimum = 1;
                player.LureMaximum = 1;
            }
        }
    }
}
