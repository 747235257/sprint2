using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

public class MovingNonAnimatedSprite : ISprite
{
    Texture2D texture;
    Vector2 pos;


    public MovingNonAnimatedSprite(Texture2D texture, Vector2 location)
    {
        this.texture = texture;
        this.pos = location;
    }

    public void Update()
    {
        //this.pos.X += 3;
        //if (this.pos.X >= _graphics.PreferredBackBufferWidth)
        //    this.pos.X = 1;


    }

    public void Draw(SpriteBatch spriteBatch, Vector2 loc)
    {



        spriteBatch.Draw(texture, pos, Color.White);

    }
}
