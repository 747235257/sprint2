using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;






public class Level1: ILevel
{
    

    public class GameObjectData
    {
        public Vector2 Position { get; set; }
        public string Name { get; set; }
    }

    public class LevelData
    {
        public string Name { get; set; }
        public GameObjectData[] Objects { get; set; }
        public Vector2 WorldPosition { get; set; }
        public string Background { get; set; }
    }

    public Level1()
    {
        FileStream jsonStream = new FileStream(Path.Combine("level1.json"), FileMode.Open);
        JsonSerializerOptions options = new JsonSerializerOptions
        {
            IncludeFields = true,
        };
        LevelData data = JsonSerializer.Deserialize<LevelData>(jsonStream, options);
        

    }

}

