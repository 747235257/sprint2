﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

public class NonMovingNonAnimatedSprite : ISprite
{

    Texture2D texture;
    Vector2 pos;


    public NonMovingNonAnimatedSprite(Texture2D texture, Vector2 location)
    {
        this.texture = texture;
        this.pos = location;
    }

    public void Update()
    {
        //to satisfy interface
    }

    public void Draw(SpriteBatch spriteBatch, Vector2 loc)
    {



        spriteBatch.Draw(texture, loc, Color.White);

    }

    public void DrawHitbox(SpriteBatch spriteBatch, Vector2 loc, Rectangle hitbox)
    {
        spriteBatch.Draw(texture, hitbox, Color.AliceBlue);
    }
}
