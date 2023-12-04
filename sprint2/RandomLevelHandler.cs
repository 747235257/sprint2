using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections;
using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Mime;
using static System.Reflection.Metadata.BlobBuilder;
using System.Reflection.Emit;
using System.IO;

namespace sprint2;
public class RandomLevelHandler
{
	Game1 game;

    private List<INPC>enemyList = new List<INPC>();
    private List<IItem> itemList = new List<IItem>();
	private Dictionary<int, List<String>> EnemiesAllowed = new Dictionary<int, List<String>>();
	private Dictionary<int, List<String>> ItemsAllowed = new Dictionary<int, List<String>>();

    private Dictionary<int, int> numEnemies = new Dictionary<int, int>();
    private Dictionary<int, int> numItems = new Dictionary<int, int>();

    private List<IBlock> blocks = new List<IBlock>();

    public enum position
    {
        minX = 100, maxX = 600,
        minY = 100, maxY = 400,

    }
    public RandomLevelHandler(Game1 game, List<IBlock> blocks)
	{
		this.game = game;
        this.blocks = blocks;
		initDict();
	}

	private void initDict()
	{
		//handles diff 1
		List<String> enem1 = new List<String>();
		enem1.Add("Skull");
        enem1.Add("Bat");

		List<String> item1 = new List<String>();
		item1.Add("key");

        numEnemies.Add(1, 4);
        numItems.Add(1, 1);

        EnemiesAllowed.Add(1, enem1 );
		ItemsAllowed.Add(1, item1 );
        //handles diff 2
        List<String> enem2 = new List<String>();
        enem2.Add("Skull");
        enem2.Add("Bat");
        enem2.Add("Goriya");

        List<String> item2 = new List<String>();
        item2.Add("key");
        item2.Add("healthItem");

        numEnemies.Add(2, 4);
        numItems.Add(2, 1);

        EnemiesAllowed.Add(2, enem2);
        ItemsAllowed.Add(2, item2);
        //handles diff 3
        List<String> enem3 = new List<String>();
        enem3.Add("Dragon");
        enem3.Add("Goriya");
        enem3.Add("Bat");

        List<String> item3 = new List<String>();
        item3.Add("key");
        item3.Add("healthItem");

        EnemiesAllowed.Add(3, enem3);
        ItemsAllowed.Add(3, item3);

        numEnemies.Add(3, 5);
        numItems.Add(3, 2);

        //handles diff 4
        List<String> enem4 = new List<String>();
        enem4.Add("Boss");

        List<String> item4 = new List<String>();
        item4.Add("healthItem");

        EnemiesAllowed.Add(4, enem4);
        ItemsAllowed.Add(4, item4);

        numEnemies.Add(4, 1);
        numItems.Add(4, 3);
    }

    private bool hasConflictListItem(IItem item, List<IItem> items)
    {
        Rectangle hb = item.getHitbox();
        bool hasConflict = false;
        int i = 0;
        while (!hasConflict && (i < items.Count))
        {
            Rectangle currHB = items[i].getHitbox();

            if (currHB.Intersects(hb))
            {
                hasConflict = true;
            }

            i++;
            

        }

        return hasConflict;
    }

    private bool hasConflictItemBlocks(IItem item, List<IBlock> blocks)
    {
        Rectangle hb = item.getHitbox();
        bool hasConflict = false;
        int i = 0;
        while (!hasConflict && (i <blocks.Count))
        {
            Rectangle currHB = blocks[i].getHitbox();

            if (currHB.Intersects(hb))
            {
                hasConflict = true;
            }

            i++;


        }

        return hasConflict;
    }

    private bool hasConflictEnemBlocks(INPC enem, List<IBlock> blocks)
    {
        Rectangle hb = enem.getHitbox();
        bool hasConflict = false;
        int i = 0;
        while (!hasConflict && (i < blocks.Count))
        {
            Rectangle currHB = blocks[i].getHitbox();

            if (currHB.Intersects(hb))
            {
                hasConflict = true;
            }

            i++;


        }

        return hasConflict;
    }

    private bool hasConflictListEnem(INPC enem, List<INPC> enems)
    {
        Rectangle hb = enem.getHitbox();
        bool hasConflict = false;
        int i = 0;
        while (!hasConflict && (i < enems.Count))
        {
            Rectangle currHB = enems[i].getHitbox();

            if (currHB.Intersects(hb))
            {
                hasConflict = true;
            }

            i++;


        }

        return hasConflict;
    }
    public void Update()
	{
        Random rnd = new Random();
		int diff = game.curLevel.Diff;
        Level level = game.curLevel;

        int mItems = numItems[diff];
        int mEnemies = numEnemies[diff];
        enemyList = game.NPCList;
        itemList = game.items;

        //populates items
        for(int i = 0; i < mItems; i++)
        {
            int x = rnd.Next((int)position.minX, (int)position.maxX);
            int y = rnd.Next((int)position.minY, (int)position.maxY);
            IItem itemToAdd = null;
            Vector2 pos = new Vector2(x, y);

            List<string> currList = ItemsAllowed[diff];
            string itemName = currList[rnd.Next(0, currList.Count)];

            if (itemName.Equals("key"))
            {
                itemToAdd = new key(new Vector2(x, y), game, game._spriteBatch);
            } else if (itemName.Equals("healthItem"))
            {
                itemToAdd = new healthItem(new Vector2(x, y), game, game._spriteBatch);

            }

            if(!hasConflictListItem(itemToAdd, itemList)    &&  !hasConflictItemBlocks(itemToAdd,blocks))
            {
                itemList.Add(itemToAdd);
            } else
            {
                i--;
            }

        }

        //populates enemies
        for (int j = 0; j < mEnemies; j++)
        {
            int x = rnd.Next((int)position.minX, (int)position.maxX);
            int y = rnd.Next((int)position.minY, (int)position.maxY);
            INPC enemToAdd = null;
            Vector2 pos = new Vector2(x, y);

            List<string> currList = EnemiesAllowed[diff];
            string enemName = currList[rnd.Next(0, currList.Count)];

            if(enemName.Equals("Dragon")) 
            { 
                enemToAdd = new Dragon(game.Bosses, game._spriteBatch, game, pos);
            }
            else if (enemName.Equals("Skull"))
            {
                enemToAdd = new Skull(game.Enemies, game._spriteBatch, game, pos);

            }
            else if (enemName.Equals("Goriya"))
            {
                enemToAdd = new Goriya(game.Enemies, game._spriteBatch, game, pos);

            }
            else if (enemName.Equals("Bat"))
            {
                enemToAdd = new Bat(game.Enemies, game._spriteBatch, game, pos);

            }
            else if (enemName.Equals("Boss"))
            {
                enemToAdd = new Boss1(game.Boss1, game._spriteBatch, game, new Vector2(236, 96));
            }


                if (!hasConflictListEnem(enemToAdd, enemyList) && !hasConflictEnemBlocks(enemToAdd, blocks))
            {
                enemyList.Add(enemToAdd);
            }
            else
            {
                j--;
            }

        }

        //game now has updated lists
        if (!level.getClearStatus())
        {
            game.NPCList = enemyList;
        } else
        {
            game.NPCList = new List<INPC>();
        }
        game.blocks = blocks;
        if (!level.getClearStatus())
        { 
            game.items = itemList;
        } else
        {
            game.items = new List<IItem>();
        }

//clears projectiles in the game
game.playerProjectiles.Clear();
        game.enemyProjectiles.Clear();
        LoadBack(level.Name);
    }

    //loads level background
    public void LoadBack(string levelName)
    {
        game.LevelBack = game.Content.Load<Texture2D>(Path.Combine("levels", levelName));
    }
}

