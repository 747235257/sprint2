﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using sprint2;

public interface IController
{
    public void Handle(GraphicsDeviceManager _graphics, IPlayer player);
}
