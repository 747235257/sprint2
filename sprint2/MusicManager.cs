﻿using Microsoft.Xna.Framework;
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
        Game1 game;
        const float on = 1f;
        const float off = 0f;

        public MusicManager(Game1 game1)
        {
            level = game1.curLevel;
            this.game = game1;
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
            if (level.Name == "Level9") MediaPlayer.Play(music);
        }

        public void MuteMusic()
        {
            if(MediaPlayer.Volume == off)
            {
                MediaPlayer.Volume = on;
            }
            else {
                MediaPlayer.Volume = off;
            }
        }
            public void InitializeMusic(Game1 game)
        {
            Song music = game.Content.Load<Song>("Audio/8-bit");
            MediaPlayer.Play(music);
        }

    }
}
