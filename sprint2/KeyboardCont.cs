using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using sprint2;

public class KeyboardCont : IController
{

    enum KeyboardBinds
    {
        W = 3, A = 1, S = 2, D = 0
    }
    private Game1 game;
    private int currentNPC;
    KeyboardBinds _lastBind = KeyboardBinds.W;
    public KeyboardCont(Game1 game, int currentNPC)
    {
        this.game = game;
        this.currentNPC = currentNPC;
    }

    public void Handle(GraphicsDeviceManager _graphics, IPlayer player)
    {
        var kstate = Keyboard.GetState();
        const int dist = 4;

        if (kstate.IsKeyDown(Keys.W) || kstate.IsKeyDown(Keys.Up))
        {
            if (_lastBind != KeyboardBinds.W) player.setDirection((int)KeyboardBinds.W);
            player.move(new Vector2(0, -dist), _graphics);
            _lastBind = KeyboardBinds.W;
        }
        if (kstate.IsKeyDown(Keys.A) || kstate.IsKeyDown(Keys.Left))
        {
            if (_lastBind != KeyboardBinds.A) player.setDirection((int)KeyboardBinds.A);
            player.move(new Vector2(-dist, 0), _graphics);
            _lastBind = KeyboardBinds.A;
        }
        if (kstate.IsKeyDown(Keys.S) || kstate.IsKeyDown(Keys.Down))
        {
            if (_lastBind != KeyboardBinds.S) player.setDirection((int)KeyboardBinds.S);
            player.move(new Vector2(0, dist), _graphics);
            _lastBind = KeyboardBinds.S;
        }
        if (kstate.IsKeyDown(Keys.D) || kstate.IsKeyDown(Keys.Right))
        {
            if (_lastBind != KeyboardBinds.D) player.setDirection((int)KeyboardBinds.D);
            player.move(new Vector2(dist, 0), _graphics);
            _lastBind = KeyboardBinds.D;
        }
        if(kstate.IsKeyDown(Keys.O))
        {
            currentNPC = (currentNPC + 1) % 6;
            game.currentNPC = currentNPC;
        }
        else if (kstate.IsKeyDown(Keys.P))
        {
            currentNPC = (currentNPC + 5) % 6;
            
            game.currentNPC = currentNPC;
        }

    }
}
