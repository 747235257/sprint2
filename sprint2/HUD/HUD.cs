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

        private enum posAdj
        {
            HP_X = 265, HP_Y = 75,
            ITEM1_X = 0, ITEM1_Y = 60,
            ITEM2_X = 80, ITEM2_Y = 60,
            ITEM3_X = 160, ITEM3_Y = 60,
            PLAYER_X = 480, PLAYER_Y = 48,
            MAP_X = 550, MAP_Y = 20
        }

        public HUD(Vector2 HUDpos, Game1 game, SpriteBatch spriteBatch) 
        {
            HUDbg = new NonMoveAnimatedSprite(game.Content.Load<Texture2D>("hudbg1"), 1, 1, HUDpos);
            hpSprite = new NonMoveAnimatedSprite(game.Content.Load<Texture2D>("hpSprite"), 1, 1, HUDpos);
            item1 = new NonMoveAnimatedSprite(game.Content.Load<Texture2D>("wep1hud"), 1, 1, HUDpos);
            item2 = new NonMoveAnimatedSprite(game.Content.Load<Texture2D>("wep2hud"), 1, 1, HUDpos);
            item3 = new NonMoveAnimatedSprite(game.Content.Load<Texture2D>("wep3hud"), 1, 1, HUDpos);
            //mapSprite
            mapSprite = new NonMovingNonAnimatedSprite(game.Content.Load<Texture2D>("mapTexture"), HUDpos);
           //playerLocSprite
            playerIconSprite = new NonMovingNonAnimatedSprite(game.Content.Load<Texture2D>("playerLoc"), HUDpos);
            this.spriteBatch = spriteBatch;
            this.game = game;
            this.HUDpos = HUDpos;
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

            mapSprite.Draw(spriteBatch, new Vector2((int)HUDpos.X + (int)posAdj.MAP_X, (int)HUDpos.Y + (int)posAdj.MAP_Y));
        }

        //draw player in map
        public void DrawPlayerIcon() 
        {
            
            playerIconSprite.Draw(spriteBatch, new Vector2((int)posAdj.PLAYER_X + (int)game.curLevel.HUDLocation.X + (int)HUDpos.X, (int)posAdj.PLAYER_Y + (int)game.curLevel.HUDLocation.Y + (int)HUDpos.Y));
        }
    }
}
