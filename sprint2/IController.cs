﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using sprint2;

public interface IController
{
    public Vector2 HandleAttack(GraphicsDeviceManager _graphics, IPlayer player);
    public void HandleDamaged(GraphicsDeviceManager _graphics, IPlayer player);
    public int HandlePlayerItem(GraphicsDeviceManager _graphics, IPlayer player);
    public void HandleMovement(GraphicsDeviceManager _graphics, IPlayer player);
    public void HandleItem(GraphicsDeviceManager _graphics, IItem item);
}