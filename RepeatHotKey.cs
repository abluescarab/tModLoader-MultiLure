using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace MultiLure {
    public class RepeatHotKey {
        private readonly int maxMultiplier;
        private readonly int speedDelta;
        private bool pressed;
        private double pressTime;
        private int speedMultiplier = 1;
        private double increaseTime;

        public ModKeybind Key { get; }

        public RepeatHotKey(Mod mod, string name, string key, int speedDelta = 2, int maxMultiplier = 32) {
            Key = KeybindLoader.RegisterKeybind(mod, name, key);
            this.speedDelta = speedDelta;
            this.maxMultiplier = maxMultiplier;
        }

        public void Start(GameTime time) {
            pressed = true;
            pressTime = time.TotalGameTime.TotalMilliseconds;
        }

        public void Stop() {
            pressed = false;
            speedMultiplier = 1;
        }

        public bool Update(GameTime time) {
            if(!pressed) return false;

            if(!Key.Current) {
                pressed = false;
                speedMultiplier = 1;
                return false;
            }

            double ms = time.TotalGameTime.TotalMilliseconds;

            if((ms - increaseTime >= 1000.0) & (speedMultiplier < maxMultiplier)) {
                speedMultiplier *= speedDelta;
                increaseTime = ms;
            }

            if((ms - pressTime) < (1000.0 / speedMultiplier)) return false;

            pressTime = ms;
            return true;
        }
    }
}
