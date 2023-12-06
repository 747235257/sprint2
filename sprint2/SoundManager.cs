using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;


namespace sprint2
{
    public class SoundManager
    {
        private static SoundManager instance = new SoundManager();
        private Dictionary<string, SoundEffect> soundBank = new Dictionary<string, SoundEffect>();
        private SoundEffectInstance soundInstance;

        public static SoundManager Instance
        {
            get
            {
                return instance;
            }
        }

        public void InitializeSound(Game1 game)
        {

            soundBank["Nunchucks"] = game.Content.Load<SoundEffect>("audio/nunchuckstrike");
            soundBank["damaged"] = game.Content.Load<SoundEffect>("audio/takedamage");
            soundBank["pickupitem"] = game.Content.Load<SoundEffect>("audio/itempick");
            soundBank["attack"] = game.Content.Load<SoundEffect>("audio/meleestrike");
            soundBank["Dragon"] = game.Content.Load<SoundEffect>("audio/projectile2");
            soundBank["Goriya"] = game.Content.Load<SoundEffect>("audio/projectile3");
            soundBank["pause"] = game.Content.Load<SoundEffect>("audio/pause");
            soundBank["unlock"] = game.Content.Load<SoundEffect>("audio/unlockdoor");
            soundBank["switchitem"] = game.Content.Load<SoundEffect>("audio/switchitem");
            soundBank["enemykill"] = game.Content.Load<SoundEffect>("audio/enemykill");
            soundBank["beam"] = game.Content.Load<SoundEffect>("audio/beam");
            soundBank["nextroom"] = game.Content.Load<SoundEffect>("audio/nextroom");
            soundBank["health"] = game.Content.Load<SoundEffect>("audio/health");
            soundBank["chest"] = game.Content.Load<SoundEffect>("audio/chest");
        }
        public SoundEffectInstance CreateSound(string soundType)
        {
            soundInstance = soundBank[soundType].CreateInstance();
            return soundInstance;
        }
    }
}