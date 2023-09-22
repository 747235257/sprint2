using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using sprint2;
using System;

public class KeyboardCont : IController
{
    public KeyboardCont()
    {
    }

    public Vector2 HandleAttack(GraphicsDeviceManager _graphics, IPlayer player)
    {
        var kstate = Keyboard.GetState();
        Vector2 range = new Vector2(0, 0);
        if (kstate.IsKeyDown(Keys.N) || kstate.IsKeyDown(Keys.Z))
        {
            range = player.attack();
        }
        HandleNoPlayerInput(kstate, player);
        return range;
    }

    public void HandleDamaged(GraphicsDeviceManager _graphics, IPlayer player)
    {
        var kstate = Keyboard.GetState();

        if (kstate.IsKeyDown(Keys.E))
        {
            player.setDamaged();
        }
        HandleNoPlayerInput(kstate, player);
    }
    //return projectile class
    public int HandlePlayerItem(GraphicsDeviceManager _graphics, IPlayer player)
    {
        var kstate = Keyboard.GetState();

        if (kstate.IsKeyDown(Keys.D1) || kstate.IsKeyDown(Keys.D2) || kstate.IsKeyDown(Keys.D3))
        {
            player.useItem();
        }

        HandleNoPlayerInput(kstate, player);
        return 0;
    }

    public void HandleMovement(GraphicsDeviceManager _graphics, IPlayer player)
    {
        KeyboardState kstate = Keyboard.GetState();
        if (kstate.IsKeyDown(Keys.W) || kstate.IsKeyDown(Keys.Up))
        {
            player.moveUp();
        }
        else if (kstate.IsKeyDown(Keys.A) || kstate.IsKeyDown(Keys.Left))
        {
            player.moveLeft();
        }
        else if (kstate.IsKeyDown(Keys.S) || kstate.IsKeyDown(Keys.Down))
        {
            player.moveDown();
        }
        else if (kstate.IsKeyDown(Keys.D) || kstate.IsKeyDown(Keys.Right))
        {
            player.moveRight();
        }

        HandleNoPlayerInput(kstate, player);

    }

    public void HandleNoPlayerInput(KeyboardState kstate, IPlayer player)
    {
        if (kstate.IsKeyUp(Keys.W) && kstate.IsKeyUp(Keys.Up) && kstate.IsKeyUp(Keys.A) && kstate.IsKeyUp(Keys.Left) && kstate.IsKeyUp(Keys.S)
            && kstate.IsKeyUp(Keys.Down) && kstate.IsKeyUp(Keys.D) && kstate.IsKeyUp(Keys.Right) && kstate.IsKeyUp(Keys.N) && kstate.IsKeyUp(Keys.Z)
            && kstate.IsKeyUp(Keys.E) && kstate.IsKeyUp(Keys.D1) && kstate.IsKeyUp(Keys.D2) && kstate.IsKeyUp(Keys.D3))
        {
            player.setIdle();
        }




    }
}
