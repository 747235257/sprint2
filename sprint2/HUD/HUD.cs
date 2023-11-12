using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.ComponentModel;
using System.Collections.Generic;
using System;

namespace sprint2
{
    public class HUD
    {
        private ISprite HUDbg;
        private ISprite hpSprite;
        private ISprite mapSprite;
        private ISprite playerIconSprite;
        private Dictionary<string, ISprite> itemSprites;
        private SpriteBatch spriteBatch;
        private Game1 game;
        private Vector2 HUDpos;
        private List<ISprite> gridSprites;
        private Dictionary<int, Vector2> gridDictionary = new Dictionary<int, Vector2>();

        private enum posAdj
        {
            HP_X = 265, HP_Y = 75,
            ITEM1_X = 0, ITEM1_Y = 60,
            ITEM2_X = 80, ITEM2_Y = 60,
            ITEM3_X = 160, ITEM3_Y = 60,
            PLAYER_X = 7, PLAYER_Y = 4,
            MAP_X = 550, MAP_Y = 20,
            HUD_WIDTH = 30 , HUD_HEIGHT =21,
            KEY_X = 10, KEY_Y = 10, KEY_ADJUST_X = 60, KEY_ADJUST_Y = 0
        }

        public HUD(Vector2 HUDpos, Game1 game, SpriteBatch spriteBatch) 
        {
            HUDbg = new NonMoveAnimatedSprite(game.Content.Load<Texture2D>("hudbg1"), 1, 1, HUDpos);
            hpSprite = new NonMoveAnimatedSprite(game.Content.Load<Texture2D>("hpSprite"), 1, 1, HUDpos);
            initializeGridSprites();
            //define the grid with the positions
            positionGrid(gridDictionary);

            //mapSprite = new NonMovingNonAnimatedSprite(game.Content.Load<Texture2D>("levels/level1hud"), HUDpos);
            //playerLocSprite
            playerIconSprite = new NonMovingNonAnimatedSprite(game.Content.Load<Texture2D>("playerLoc"), HUDpos);
            //gridSprites = 
            this.spriteBatch = spriteBatch;
            this.game = game;
            this.HUDpos = HUDpos;

            initItemSprites(); //init sprite dictionary for items
        }

        public void initializeGridSprites()
        {
            gridSprites = new List<ISprite>();
            for(int i = 0; i<20;i++ )
            {
                gridSprites.Add(null);
            }
        }

        private void initItemSprites()
        {
            itemSprites = new Dictionary<string, ISprite>();
            itemSprites.Add("Nunchucks", new NonMoveAnimatedSprite(game.Content.Load<Texture2D>("wep1hud"), 1, 1, HUDpos));
            itemSprites.Add("Dragon", new NonMoveAnimatedSprite(game.Content.Load<Texture2D>("wep2hud"), 1, 1, HUDpos));
            itemSprites.Add("Goriya", new NonMoveAnimatedSprite(game.Content.Load<Texture2D>("wep3hud"), 1, 1, HUDpos));
            itemSprites.Add("key", new NonMoveAnimatedSprite(game.Content.Load<Texture2D>("key"), 1, 1, HUDpos));


        }
        public void Draw()
        {
            HUDbg.Draw(spriteBatch, HUDpos);
            DrawHP();
            DrawItems();
            //Draw Map
            DrawMap();
            //Draw Player Legend
            DrawPlayerIcon();
            DrawKeyCount();
        }

        public void DrawKeyCount()
        {
            int keyCount = game.player.getKeyCount();
            Vector2 keyPos = new Vector2((int)posAdj.KEY_X, (int)posAdj.KEY_Y);

            //updates key position with each key
            for (int i = 0; i < keyCount; i++)
            {
                itemSprites["key"].Draw(spriteBatch, keyPos);
                keyPos.X += (int)posAdj.KEY_ADJUST_X;
                keyPos.Y += (int)posAdj.KEY_ADJUST_Y;
            }

        }

        public void DrawItems()
        {
            List<string> items = game.player.getItemSlot();
            //only draws the items in player's inventory
            if (items.Count > 0)
            {
                itemSprites[items[0]].Draw(spriteBatch, new Vector2((int)posAdj.ITEM1_X + HUDpos.X, (int)posAdj.ITEM1_Y + HUDpos.Y));
            }

            if (items.Count > 1)
            {
                itemSprites[items[1]].Draw(spriteBatch, new Vector2((int)posAdj.ITEM2_X + HUDpos.X, (int)posAdj.ITEM2_Y + HUDpos.Y));
            }

            if (items.Count > 2)
            {
                itemSprites[items[2]].Draw(spriteBatch, new Vector2((int)posAdj.ITEM3_X + HUDpos.X, (int)posAdj.ITEM3_Y + HUDpos.Y));
            }

        }

        
        public void positionGrid(Dictionary<int, Vector2> gridDictionary)
        {
            gridDictionary.Add(0, new Vector2(580 + (int)posAdj.HUD_WIDTH * 0, 20 + (int)posAdj.HUD_HEIGHT * 0)); //0
            gridDictionary.Add(1, new Vector2(580 + (int)posAdj.HUD_WIDTH * 1,20 + (int)posAdj.HUD_HEIGHT * 0)); //1
            gridDictionary.Add(2, new Vector2(580 + (int)posAdj.HUD_WIDTH * 2, 20 + (int)posAdj.HUD_HEIGHT * 0)); //2
            gridDictionary.Add(3, new Vector2(580 + (int)posAdj.HUD_WIDTH * 3, 20 + (int)posAdj.HUD_HEIGHT * 0)); //3
            gridDictionary.Add(4, new Vector2(580 + (int)posAdj.HUD_WIDTH * 0, 20 + (int)posAdj.HUD_HEIGHT * 1)); //4
            gridDictionary.Add(5, new Vector2(580 + (int)posAdj.HUD_WIDTH * 1, 20 + (int)posAdj.HUD_HEIGHT * 1)); //5
            gridDictionary.Add(6, new Vector2(580 + (int)posAdj.HUD_WIDTH * 2, 20 + (int)posAdj.HUD_HEIGHT * 1)); //6
            gridDictionary.Add(7, new Vector2(580 + (int)posAdj.HUD_WIDTH * 3, 20 + (int)posAdj.HUD_HEIGHT * 1)); //7
            gridDictionary.Add(8, new Vector2(580 + (int)posAdj.HUD_WIDTH * 0, 20 + (int)posAdj.HUD_HEIGHT * 2)); //8
            gridDictionary.Add(9, new Vector2(580 + (int)posAdj.HUD_WIDTH * 1, 20 + (int)posAdj.HUD_HEIGHT * 2)); //9
            gridDictionary.Add(10, new Vector2(580 + (int)posAdj.HUD_WIDTH * 2, 20 + (int)posAdj.HUD_HEIGHT * 2)); //10
            gridDictionary.Add(11, new Vector2(580 + (int)posAdj.HUD_WIDTH * 3, 20 + (int)posAdj.HUD_HEIGHT * 2)); //11
            gridDictionary.Add(12, new Vector2(580 + (int)posAdj.HUD_WIDTH * 0, 20 + (int)posAdj.HUD_HEIGHT * 3)); //12
            gridDictionary.Add(13, new Vector2(580 + (int)posAdj.HUD_WIDTH * 1, 20 + (int)posAdj.HUD_HEIGHT * 3)); //13
            gridDictionary.Add(14, new Vector2(580 + (int)posAdj.HUD_WIDTH * 2, 20 + (int)posAdj.HUD_HEIGHT * 3)); //14
            gridDictionary.Add(15, new Vector2(580 + (int)posAdj.HUD_WIDTH * 3, 20 + (int)posAdj.HUD_HEIGHT * 3)); //15
            gridDictionary.Add(16, new Vector2(580 + (int)posAdj.HUD_WIDTH * 0, 20 + (int)posAdj.HUD_HEIGHT * 4)); //16
            gridDictionary.Add(17, new Vector2(580 + (int)posAdj.HUD_WIDTH * 1, 20 + (int)posAdj.HUD_HEIGHT * 4)); //17
            gridDictionary.Add(18, new Vector2(580 + (int)posAdj.HUD_WIDTH * 2, 20 + (int)posAdj.HUD_HEIGHT * 4)); //18
            gridDictionary.Add(19, new Vector2(580 + (int)posAdj.HUD_WIDTH * 3, 20 + (int)posAdj.HUD_HEIGHT * 4)); //19

        }

        public void AddToGrid(string levelName)
        {
            //add the sprites to the grid
            if (gridSprites[game.curLevel.GridLocation.Grid] == null)
            {
                gridSprites[game.curLevel.GridLocation.Grid] = new NonMovingNonAnimatedSprite(game.Content.Load<Texture2D>("levels/" + levelName + "hud"), HUDpos);
            }
        }
        
        public void DrawHP()
        {
            int hp = game.player.getHealth();
            int gapX = 48;

            if(hp > 0)
            {
                for(int  i = 0; i < hp; i++)
                {
                    hpSprite.Draw(spriteBatch, new Vector2((int)posAdj.HP_X + (int)HUDpos.X + i * gapX, (int)posAdj.HP_Y + (int)HUDpos.Y));
                }
            }
        }

        //draw map
        public void DrawMap()
        {
           
            //mapSprite.Draw(spriteBatch, new Vector2(gridDictionary[0].X + (int)HUDpos.X, gridDictionary[0].Y +(int)HUDpos.Y));
            for(int i=0; i < 20; i++)
            {
                if (gridSprites[i] != null)
                {
                   gridSprites[i].Draw(spriteBatch, new Vector2(gridDictionary[i].X + (int)HUDpos.X, gridDictionary[i].Y + (int)HUDpos.Y));
                }
                
            }

            ////gridSprites[2000] = null;
        }

        //draw player in map
        public void DrawPlayerIcon() 
        {
            int currentGrid = game.curLevel.GridLocation.Grid;
            Vector2 gridPos = gridDictionary[currentGrid];
            playerIconSprite.Draw(spriteBatch, new Vector2((int)posAdj.PLAYER_X + gridPos.X + (int)HUDpos.X, (int)posAdj.PLAYER_Y + gridPos.Y + (int)HUDpos.Y));        }

    }
}
