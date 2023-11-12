using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using sprint2;

using System.Collections;
using System.Collections.Generic;
public interface IController
{

    public IBlock blockHandle(GraphicsDeviceManager _graphics, Texture2D block, int spriteRow, int spriteCol, Vector2 initPosition, IBlock blocky);

    public Vector2 HandleAttack(GraphicsDeviceManager _graphics, IPlayer player);
    public void HandleDamaged(GraphicsDeviceManager _graphics, IPlayer player);
    public List<IProjectile> HandlePlayerItem(GraphicsDeviceManager _graphics, IPlayer player);
    public void HandleMovement(GraphicsDeviceManager _graphics, IPlayer player);


    public bool HandleSwitchEnemy(int currentNPC);

    public void handleLevelSwitch(Game1 game);
    public void HandleSwitchInventory(IPlayer player, Inventory inventory);

    public void HandlePause(Game1 game);

}
