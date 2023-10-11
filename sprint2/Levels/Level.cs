using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class Level
{
    public string Name { get; set; }
    public List<Obstacle> Obstacles { get; set; }
    public List<WallHitbox> WallHitboxs { get; set; }
    // Add other properties as needed

    public Level()
    {
        Obstacles = new List<Obstacle>();
        WallHitboxs = new List<WallHitbox>();
    }
}

public class Obstacle
{
    public string Type { get; set; }
    //public Vector2 Position { get; set; }
    public float X { get; set; }
    public float Y { get; set; }
    // Add other properties as needed
}

public class WallHitbox
{
    public float X { get; set; }
    public float Y { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
}

