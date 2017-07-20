using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace MultiLure {
    public class GlobalFishingPoleItem : GlobalItem {
        public override bool Autoload(ref string name) {
            return true;
        }

        public override bool Shoot(Item item, Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack) {
            if(item.fishingPole > 0) {
                byte lures = player.GetModPlayer<MultiLurePlayer>(mod).LureCount;

                for(int i = 0; i < lures; i++) {
                    Projectile.NewProjectile(position.X + Main.rand.Next(5), position.Y + Main.rand.Next(5), speedX + Main.rand.Next(5), speedY + Main.rand.Next(5), type, damage, knockBack, player.whoAmI);
                }

                return false;
            }
            else {
                return base.Shoot(item, player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
            }
        }
    }
}
