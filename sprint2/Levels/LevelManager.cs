using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


public class LevelManager
{
    public List<Level> Levels { get; private set; }

    public LevelManager()
    {
        Levels = new List<Level>();
    }

    public void LoadLevels(string filePath)
    {
        
        string json = File.ReadAllText(filePath);
        Levels = JsonConvert.DeserializeObject<List<Level>>(json);
    }
}

