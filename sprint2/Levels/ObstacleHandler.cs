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
        Level level;
        Game1 game1;
        Game game;
        
        Texture2D Blocks;
        public ObstacleHandler(Game1 game1, Game game, Texture2D Blocks)
        {
            this.Blocks = Blocks;
            level = game1.curLevel; //gets current level in game
            blocks = new List<IBlock>();
            enemies= new List<INPC>();
            items = new List<IItem>();
            this.game1 = game1; //TO CHECK
            this.game = game;
            
             
        }

        public void Update()
        {
            //places/modifies game elements based on Level Contents
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
                    case "Dragon":
                        enemies.Add(new Dragon(game1.Bosses, game1._spriteBatch, game1, new Vector2(level.Obstacles[i].X, level.Obstacles[i].Y)));
                        break;
                    case "Skull":
                        enemies.Add(new Skull(game1.Enemies, game1._spriteBatch, game1, new Vector2(level.Obstacles[i].X, level.Obstacles[i].Y)));
                        break;
                    case "Goriya":
                        enemies.Add(new Goriya(game1.Enemies, game1._spriteBatch, game1, new Vector2(level.Obstacles[i].X, level.Obstacles[i].Y)));
                        break;
                    case "Bat":
                        enemies.Add(new Bat(game1.Enemies, game1._spriteBatch, game1, new Vector2(level.Obstacles[i].X, level.Obstacles[i].Y)));
                        break;
                    case "wep1":
                        items.Add(new wep1(new Vector2(level.Obstacles[i].X, level.Obstacles[i].Y), game1, game1._spriteBatch));
                        break;
                    case "wep2":
                        items.Add(new wep2(new Vector2(level.Obstacles[i].X, level.Obstacles[i].Y), game1, game1._spriteBatch));
                        break;
                    case "wep3":
                        items.Add(new wep3(new Vector2(level.Obstacles[i].X, level.Obstacles[i].Y), game1, game1._spriteBatch));
                        break;
                    case "key":
                        items.Add(new key(new Vector2(level.Obstacles[i].X, level.Obstacles[i].Y), game1, game1._spriteBatch));
                        break;
                    case "triforce":
                        items.Add(new triforce(new Vector2(level.Obstacles[i].X, level.Obstacles[i].Y), game1, game1._spriteBatch));
                        break;
                    case "mapItem":
                        items.Add(new mapItem(new Vector2(level.Obstacles[i].X, level.Obstacles[i].Y), game1, game1._spriteBatch));
                        break;
                    case "healthItem":
                        items.Add(new healthItem(new Vector2(level.Obstacles[i].X, level.Obstacles[i].Y), game1, game1._spriteBatch));
                        break;
                    default:
                        break;
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
