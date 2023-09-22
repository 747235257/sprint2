using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using sprint2;

public interface IController
{

    public void Handle(GraphicsDeviceManager _graphics, IPlayer player);
    public IBlock blockHandle(GraphicsDeviceManager _graphics, Texture2D block, int spriteRow, int spriteCol, Vector2 initPosition, IBlock blocky);

    public Vector2 HandleAttack(GraphicsDeviceManager _graphics, IPlayer player);
    public void HandleDamaged(GraphicsDeviceManager _graphics, IPlayer player);
    public IProjectile HandlePlayerItem(GraphicsDeviceManager _graphics, IPlayer player);
    public void HandleMovement(GraphicsDeviceManager _graphics, IPlayer player);


    public bool HandleSwitchEnemy(int currentNPC);

    public void HandleItem(GraphicsDeviceManager _graphics, IItem item);


}
