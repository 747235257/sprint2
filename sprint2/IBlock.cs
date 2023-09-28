using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Block;

public interface IBlock
{
    void switchBlock(GraphicsDeviceManager _graphics, FrameDirection direction);

    void blockPosition(Vector2 position);
    public void drawBlock(SpriteBatch spriteBatch);
}
