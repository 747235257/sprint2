using Microsoft.Xna.Framework.Graphics;
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
        
        List<IBlock> blocks;
        List<INPC> enemies;
        List<IItem> items;
        List<IChest> chests;
        Level level;
        Game1 game1;
        Game game;
        
        Texture2D Blocks;
        Texture2D Chests;
        public ObstacleHandler(Game1 game1, Game game, Texture2D Blocks, Texture2D Chests)
        {
            this.Blocks = Blocks;
            this.Chests = Chests;
            level = game1.curLevel; //gets current level in game
            blocks = new List<IBlock>();
            enemies= new List<INPC>();
            items = new List<IItem>();
            chests = new List<IChest>();
            this.game1 = game1; //TO CHECK
            this.game = game;
            
             
        }

        public void Update()
        {   
            //places/modifies game elements based on Level Contents
            for (int i = 0; i < level.Obstacles.Count; i++)
            {
                string type = level.Obstacles[i].Type;
                switch (type)
                {
                    case "Player":
                        game1.player.setLocation(new Vector2(level.Obstacles[i].X, level.Obstacles[i].Y));
                        break;
                    case "Block":
                        blocks.Add(new Block(Blocks, game1.blockRow, game1.blockCol, new Vector2(level.Obstacles[i].X, level.Obstacles[i].Y), game1._spriteBatch, game));
                        break;
                    case "Chest":
                        chests.Add(new RandomChest(Chests, game1.chestRow, game1.chestCol, new Vector2(level.Obstacles[i].X, level.Obstacles[i].Y), game1._spriteBatch, game1, game));
                        break;
                }


                //adds  enems
                if(game1.enemyCreator.isNormalEnem(type))
                {
                    enemies.Add(game1.enemyCreator.produceEnemy(type, new Vector2(level.Obstacles[i].X, level.Obstacles[i].Y)));
                }

                if(game1.enemyCreator.isBossEnem(type))
                {
                    enemies.Add(game1.enemyCreator.produceEnemy(type, new Vector2(236,96)));

                }
                //adds  items
                if (game1.itemCreator.isItem(type))
                {
                    items.Add(game1.itemCreator.produceItem(type, new Vector2(level.Obstacles[i].X, level.Obstacles[i].Y)));
                }
            }
            //game now has updated lists
            if (!level.getClearStatus())
            {
                game1.NPCList = enemies;
            }
            else
            {
                game1.NPCList = new List<INPC>();
            }
            game1.blocks = blocks;

            if (!level.getClearStatus()) game1.chests = chests;
            if (!level.getClearStatus())
            {
                game1.items = items;
            } else
            {
                game1.items = new List<IItem>();
            }

            //clears projectiles in the game
            game1.playerProjectiles.Clear();
            game1.enemyProjectiles.Clear();
            LoadBack(level.Name);
        }

        //loads level background
        public void LoadBack(string levelName)
        {
             game1.LevelBack = game1.Content.Load<Texture2D>(Path.Combine("levels", levelName));
        }
    }
}
