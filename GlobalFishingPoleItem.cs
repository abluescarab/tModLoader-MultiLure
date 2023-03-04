using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace MultiLure {
    public class GlobalFishingPoleItem : GlobalItem {
        public override bool Shoot(Item item, Player player,
            EntitySource_ItemUse_WithAmmo source, Vector2 position,
            Vector2 velocity, int type, int damage, float knockback) {
            if(item.fishingPole > 0) {
                int lures = player.GetModPlayer<MultiLurePlayer>().LureCount;

                for(int i = 0; i < lures; i++) {
                    Projectile.NewProjectile(source,
                                             position.X + Main.rand.Next(5),
                                             position.Y + Main.rand.Next(5),
                                             velocity.X + Main.rand.Next(5),
                                             velocity.Y + Main.rand.Next(5),
                                             type,
                                             damage,
                                             knockback,
                                             player.whoAmI);
                }

                return false;
            }

            return base.Shoot(item, player, source, position, velocity, type, damage, knockback);
        }
    }
}
