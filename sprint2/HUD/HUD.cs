using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.ComponentModel;
using System.Collections.Generic;

namespace sprint2
{
    public class HUD
    {
        private ISprite HUDbg;
        private ISprite hpSprite;
        private ISprite mapSprite;
        private ISprite playerIconSprite;
        private ISprite item1;
        private ISprite item2;
        private ISprite item3;
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
            PLAYER_X = 480, PLAYER_Y = 48,
            MAP_X = 550, MAP_Y = 20,
            HUD_WIDTH = 30 , HUD_HEIGHT =21
        }

        public HUD(Vector2 HUDpos, Game1 game, SpriteBatch spriteBatch) 
        {
            HUDbg = new NonMoveAnimatedSprite(game.Content.Load<Texture2D>("hudbg1"), 1, 1, HUDpos);
            hpSprite = new NonMoveAnimatedSprite(game.Content.Load<Texture2D>("hpSprite"), 1, 1, HUDpos);
            item1 = new NonMoveAnimatedSprite(game.Content.Load<Texture2D>("wep1hud"), 1, 1, HUDpos);
            item2 = new NonMoveAnimatedSprite(game.Content.Load<Texture2D>("wep2hud"), 1, 1, HUDpos);
            item3 = new NonMoveAnimatedSprite(game.Content.Load<Texture2D>("wep3hud"), 1, 1, HUDpos);
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
        }

        public void initializeGridSprites()
        {
            for(int i = 0; i<20;i++ )
            {
                gridSprites.Add(0);
            }
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
        }

        public void DrawItems()
        {
            List<string> items = game.player.getInventory();

            //only draws the items in player's inventory
            if (items.Contains("Nunchucks"))
            {
                item1.Draw(spriteBatch, new Vector2((int)posAdj.ITEM1_X + HUDpos.X, (int)posAdj.ITEM1_Y + HUDpos.Y));
            }

            if (items.Contains("Dragon"))
            {
                item2.Draw(spriteBatch, new Vector2((int)posAdj.ITEM2_X + HUDpos.X, (int)posAdj.ITEM2_Y + HUDpos.Y));
            }

            if (items.Contains("Goriya"))
            {
                item3.Draw(spriteBatch, new Vector2((int)posAdj.ITEM3_X + HUDpos.X, (int)posAdj.ITEM3_Y + HUDpos.Y));
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
            if (gridSprites[game.curLevel.GridLocation.Grid] != null)
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

        }

        //draw player in map
        public void DrawPlayerIcon() 
        {

            //playerIconSprite.Draw(spriteBatch, new Vector2((int)posAdj.PLAYER_X + (int)game.curLevel.HUDLocation.X + (int)HUDpos.X, (int)posAdj.PLAYER_Y + (int)game.curLevel.HUDLocation.Y + (int)HUDpos.Y));
            playerIconSprite.Draw(spriteBatch, new Vector2(587 + (int)HUDpos.X, 22 + (int)HUDpos.Y));
        }

    }
}
