﻿using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;
using System.IO;

namespace sprint2
{


    public class ObstacleHandler
    {
        IPlayer player;
        List<IBlock> blocks;
        Level level;
        Game1 game1;
        Game game;
        
        Texture2D Blocks;
        public ObstacleHandler(Game1 game1, Game game, Texture2D Blocks)
        {
            this.Blocks = Blocks;
            level = game1.curLevel;
            blocks = new List<IBlock>();
            this.game1 = game1;
            this.game = game;
            
             
        }

        public void Update()
        {
            for (int i = 0; i < level.Obstacles.Count; i++)
            {
                switch (level.Obstacles[i].Type)
                {
                    case "Player":
                        game1.player.setLocation(new Vector2(level.Obstacles[i].X, level.Obstacles[i].Y));
                        break;
                    case "Block":
                        blocks.Add(new Block(Blocks, game1.blockRow, game1.blockCol, new Vector2(level.Obstacles[i].X, level.Obstacles[i].Y), game1._spriteBatch, game));
                        break;
                    default:
                        break;


                }
            }
            
            game1.blocks = blocks;
            LoadBack(level.Name);
        }

        public void LoadBack(string levelName)
        {
             game1.LevelBack = game1.Content.Load<Texture2D>(Path.Combine("levels", levelName));
        }
    }
}