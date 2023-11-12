using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using sprint2;

public class Level
{
    public string Name { get; set; }
    public GridPosition GridLocation{ get; set; }

    public List<Obstacle> Obstacles { get; set; }
    public List<WallHitbox> WallHitboxs { get; set; }

    public List<DoorHitbox> DoorHitboxs { get; set; }
    // Add other properties as needed
    public List<LockDoor> LockDoors { get; set; }

    public bool isCleared;
    public Level()
    {
        Obstacles = new List<Obstacle>(); //enemies, player, blocks
        WallHitboxs = new List<WallHitbox>(); //walls
        DoorHitboxs = new List<DoorHitbox>(); //Doors
        GridLocation = new GridPosition();
        LockDoors = new List<LockDoor>();
        isCleared = false;
    }

    public bool getClearStatus()
    {
        return isCleared;
    }

    //if the level doesn't have any enemies, level is clear
    public void checkLevelClear(Game1 game)
    {
        isCleared = true;
        if (!isEnemyEmpty(game)) isCleared = false;
        if(!isItemEmpty(game)) isCleared = false;
    }

    private bool isEnemyEmpty(Game1 game)
    {
        bool isEmpty = true;
        foreach(INPC enemy in game.NPCList)
        {
            if(enemy != null)
            {
                isEmpty = false;
                break;
            }
        }

        return isEmpty;
    }

    private bool isItemEmpty(Game1 game)
    {
        bool isEmpty = true;
        foreach(IItem item in game.items)
        {
            if(item.isAlive())
            {
                isEmpty = false;
                break;
            }
        }
        return isEmpty;
    }
}

//class for gridPosition
public class GridPosition
{
    public int Grid { get; set; }

}

public class Obstacle
{
    public string Type { get; set; }
    //public Vector2 Position { get; set; }
    public float X { get; set; }
    public float Y { get; set; }
    // Add other properties as needed
}

//dimensions and position of walls around room
public class WallHitbox
{
    public float X { get; set; }
    public float Y { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
}

public class DoorHitbox
{
    public float X { get; set; }
    public float Y { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }

    public float NextX {  get; set; }

    public float NextY { get; set; }

    public int NextLevel { get; set; }
}

enum State
{
    Lock = 0,
    Unlock = 1
}
public class LockDoor
{
    public int state;
    public float X { get; set; }
    public float Y { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public float NextX { get; set; }

    public float NextY { get; set; }

    public int NextLevel { get; set; }
}

