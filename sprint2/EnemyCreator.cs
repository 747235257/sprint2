using Microsoft.Xna.Framework;

using sprint2;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using static System.Reflection.Metadata.BlobBuilder;

public class EnemyCreator
{
	private Game1 game1;
	public EnemyCreator(Game1	game)
	{
		game1 = game;
	}

    public bool isNormalEnem(string name)
    {
        bool isEnemBool = false;

        if (name == "Dragon" || name == "Skull" || name == "Bat" || name == "Goriya")
        {
            isEnemBool = true;
        }

        return isEnemBool;
    }

    public bool isBossEnem(string name)
    {
        bool isEnemBool = false;

        if (name    ==  "Boss1")
        {
            isEnemBool = true;
        }

        return isEnemBool;
    }

    public INPC produceEnemy(string name, Vector2 pos)
    {
        INPC enem = null;
        switch (name)
        {
            case "Boss1":
                enem=new Boss1(game1.Boss1, game1._spriteBatch, game1, pos);
                break;
            case "Dragon":
                enem=new Dragon(game1.Bosses, game1._spriteBatch, game1, pos);
                break;
            case "Skull":
                enem=new Skull(game1.Enemies, game1._spriteBatch, game1, pos);
                break;
            case "Goriya":
                enem=new Goriya(game1.Enemies, game1._spriteBatch, game1, pos);
                break;
            case "Bat":
                enem=new Bat(game1.Enemies, game1._spriteBatch, game1, pos);
                break;
            default: break;


        }

        return enem;
    }
}
