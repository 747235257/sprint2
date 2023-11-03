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
                //if (instance == null)
                //{
                //    instance = new SoundManager();
                //}
                return instance;
            }
        }

        public void InitializeSound(Game1 game)
        {

            soundBank["Nunchucks"] = game.Content.Load<SoundEffect>("audio/nunchuckstrike");
            soundBank["damaged"] = game.Content.Load<SoundEffect>("audio/takedamage");
            soundBank["walking"] = game.Content.Load<SoundEffect>("audio/playerwalk");
            soundBank["pickupitem"] = game.Content.Load<SoundEffect>("audio/itempick");
            soundBank["attack"] = game.Content.Load<SoundEffect>("audio/meleestrike");
            //soundBank["attack"] = game.Content.Load<SoundEffect>("audio/nunchuckstrike");

        }
        public void PlaySound(string soundType)
        {
            soundInstance = soundBank[soundType].CreateInstance();
            soundInstance.Volume = 1.0f;
            soundInstance.Play();
        }

        public void StopSound( ) { 
            soundInstance.Stop();
        }
    }
}