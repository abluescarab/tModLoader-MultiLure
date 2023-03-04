using Microsoft.Xna.Framework.Graphics;
using MultiLure.Items;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace MultiLure {
    public class MultiLure : Mod {
        private const string AddLureTexture = "MultiLure/Textures/AddLure";
        private const string RemoveLureTexture = "MultiLure/Textures/RemoveLure";

        internal const string PermissionName = "ModifyLureCount";
        internal const string PermissionDisplayName = "Modify Lure Count";

        internal readonly Dictionary<string, int> FishingLineItems = new Dictionary<string, int>();

        public MultiLure() {
            ContentAutoloadingEnabled = true;
        }

        public override void Load() {
            if(MultiLureConfig.Instance.EnableFishingLines) {
                AddFishingLines(typeof(CopperFishingLine),
                                typeof(IronFishingLine),
                                typeof(SilverFishingLine),
                                typeof(GoldFishingLine),
                                typeof(CobaltFishingLine),
                                typeof(MythrilFishingLine),
                                typeof(AdamantiteFishingLine));
            }
        }

        public override void PostSetupContent() {
            Func<string> addTooltip = () => $"Add Lure (Current: {GetModPlayer().LureCount})";
            Func<string> removeTooltip = () => $"Remove Lure (Current: {GetModPlayer().LureCount})";

            if(MultiLureConfig.Instance.EnableCheatSheetIntegration) {
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

            if(MultiLureConfig.Instance.EnableHerosModIntegration) {
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
                MultiLurePlayer player = GetModPlayer();
                player.LureMinimum = 1;
                player.LureMaximum = 1;
            }
        }

        public void AddFishingLines(params Type[] types) {
            foreach(var type in types) {
                var inst = (FishingLineBase)Activator.CreateInstance(type);

                AddContent(inst);

                FishingLineItems.Add(inst.OriginalName, Find<ModItem>(inst.OriginalName).Type);
            }
        }

        private static MultiLurePlayer GetModPlayer() {
            return Main.LocalPlayer.GetModPlayer<MultiLurePlayer>();
        }
    }
}
