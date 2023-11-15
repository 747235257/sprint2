using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Audio;

namespace sprint2
{

    public class Inventory
    {
        Game1 game;
        SpriteBatch spriteBatch;
        List<String> inventoryGrid;
        ISprite inventoryScreenSprite;
        Vector2 screenPos;
        Dictionary<String, ISprite> itemSprites;
        int lastItemIndex;
        int updateCounter;
        private const int UPDATE_MAX = 10;

        public enum ScreenPos
        {
            POS_X = 0, POS_Y = 0,
            ADJ_X = 70, ADJ_Y = 0,
            ITEM_X = 22, ITEM_Y = 390
        }

        public Inventory(Game1 game, SpriteBatch spriteBatch) 
        { 
            this.game = game;
            this.spriteBatch = spriteBatch;
            screenPos = new Vector2((int)ScreenPos.POS_X, (int)ScreenPos.POS_Y);
            inventoryScreenSprite = new NonMoveAnimatedSprite(game.Content.Load<Texture2D>("InventoryScreen"), 1, 1, screenPos);
            initInventoryGrid();
            initItemSprites();
            lastItemIndex = 0;
            updateCounter = 0;
        }

        public void updateInventory()
        {
            initInventoryGrid();
        }
        public void handleSwitchInventory(int code)
        {
            if (updateCounter >= UPDATE_MAX)
            {
                if (code - 1 < inventoryGrid.Count)
                {
                    game.player.setItems(lastItemIndex, inventoryGrid[code - 1]);
                    SoundEffectInstance switchItem = SoundManager.Instance.CreateSound("switchitem");
                    switchItem.Play();
                }

                //inc/resets the index for next change
                updateLastItemIndex();
                updateCounterInventory();
                updateCounter = 0;
            }
        }

        public void updateCounterInventory()
        {
            updateCounter++;
        }
        public void resetItemIndex()
        {
            lastItemIndex = 0;
        }
        private void updateLastItemIndex()
        {
            lastItemIndex++;
            if(lastItemIndex >= game.player.getItemSlot().Count)
            {
                resetItemIndex();
            }
        }

        //adds all inventory items to the list
        public void initInventoryGrid()
        {
            inventoryGrid = new List<String>();
            if (game.player.getInventory() != null)
            {
                HashSet<String> inventory = game.player.getInventory();

                foreach (String item in inventory)
                {
                    inventoryGrid.Add(item);
                }
            }
        }

        private void initItemSprites()
        {
            itemSprites = new Dictionary<string, ISprite>();
            itemSprites.Add("Nunchucks", new NonMoveAnimatedSprite(game.Content.Load<Texture2D>("wep1hud"), 1, 1, screenPos));
            itemSprites.Add("Dragon", new NonMoveAnimatedSprite(game.Content.Load<Texture2D>("wep2hud"), 1, 1, screenPos));
            itemSprites.Add("Goriya", new NonMoveAnimatedSprite(game.Content.Load<Texture2D>("wep3hud"), 1, 1, screenPos));
        }

        public void Draw()
        {
            inventoryScreenSprite.Draw(spriteBatch, screenPos);
            DrawInventory();
        }

        private void DrawInventory()
        {
            Vector2 itemPos = new Vector2((int)ScreenPos.ITEM_X, (int)ScreenPos.ITEM_Y);
            for(int i = 0; i < inventoryGrid.Count; i++)
            {
                itemSprites[inventoryGrid[i]].Draw(spriteBatch, itemPos);
                itemPos.X += (int)ScreenPos.ADJ_X;
                itemPos.Y += (int)ScreenPos.ADJ_Y;
            }
        }
    }
}
