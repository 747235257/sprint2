using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sprint2
{

    public class Inventory
    {
        Game1 game;
        SpriteBatch spriteBatch;
        List<String> inventoryGrid;

        public Inventory(Game1 game, SpriteBatch spriteBatch) 
        { 
            this.game = game;
            this.spriteBatch = spriteBatch;
            initInventoryGrid();
        }

        public void initInventoryGrid()
        {
            
        }
    }
}
