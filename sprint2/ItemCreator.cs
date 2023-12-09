using Microsoft.Xna.Framework;

using sprint2;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;

public class ItemCreator
{
    Game1 game1;
	public ItemCreator(Game1    game)
	{
        game1 = game;
	}

    public  bool  isItem(string name)
    {
        bool    isItemBool = false;

        if(name =="wep1"|| name == "wep2" || name == "wep3" || name == "key" || name == "triforce" || name == "mapItem" || name == "healthItem")
        {
            isItemBool = true;
        }

        return isItemBool;
    }
	public	IItem	produceItem(string name,    Vector2	pos)
	{
		IItem item;

        switch (name) {
        case "wep1":
            item=new wep1(pos, game1, game1._spriteBatch);
            break;
        case "wep2":
            item=new wep2(pos, game1, game1._spriteBatch);
            break;
        case "wep3":
            item=new wep3(pos, game1, game1._spriteBatch);
            break;
        case "key":
            item=new key(pos, game1, game1._spriteBatch);
            break;
        case "triforce":
            item=new triforce(pos, game1, game1._spriteBatch);
            break;
        case "mapItem":
            item=new mapItem(pos, game1, game1._spriteBatch);
            break;
        case "healthItem":
            item=new healthItem(pos, game1, game1._spriteBatch);
            break;
        default: 
            item = null;
            break;
        }

        return item;
    }
    
}
