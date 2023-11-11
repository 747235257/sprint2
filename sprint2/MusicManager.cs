using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace sprint2
{
    public class MusicManager
    {
        readonly Dictionary<string, Song> album = new Dictionary<string, Song>();
        Level level;
        Game1 game1;

        public MusicManager(Game1 game1)
        {
            level = game1.curLevel;
        }
        public void MusicLoader(Game1 game, Level level)
        {
            Song music;

            switch(level.Name)
            {
                case "Level9":
                    music = game.Content.Load<Song>("Audio/enemyBGM");
                    break;
                default:
                    music = game.Content.Load<Song>("Audio/8-bit");
                    break;
            }
            MediaPlayer.Play(music);
        }

        public void InitializeMusic(Game1 game)
        {
            Song music = game.Content.Load<Song>("Audio/8-bit");
            MediaPlayer.Play(music);
        }

    }
}
