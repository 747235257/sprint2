using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using sprint2;

using System.Collections;
using System.Collections.Generic;
public interface IController
{

    //public IBlock blockHandle(GraphicsDeviceManager _graphics, Texture2D block, int spriteRow, int spriteCol, Vector2 initPosition, IBlock blocky);
    //public void HandleCommand();
    public void RegisterCommand(IPlayer player);
    public void HandleAttack(IPlayer player);
    public List<IProjectile> HandlePlayerItem(IPlayer player);
    public void HandleMovement(IPlayer player);
    public bool HandleSwitchEnemy(int currentNPC);

    public void handleLevelSwitch(Game1 game);
    public void HandleSwitchInventory(Inventory inventory);

    public void HandlePause(Game1 game);

}
