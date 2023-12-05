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
    public void RegisterCommand();
    public void HandleAttack();
    public List<IProjectile> HandlePlayerItem();
    public void HandleMovement();
    public void handleLevelSwitch();
    public void HandleSwitchInventory();
    public void HandlePause();

}
