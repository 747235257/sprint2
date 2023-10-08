using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
public interface ISprite
{
    void Update(); //updates sprite based on location and animation etc.
    void Draw(SpriteBatch spriteBatch, Vector2 loc); //draws the sprite

    void DrawHitbox(SpriteBatch spriteBatch, Vector2 loc, Rectangle hitbox);

}
