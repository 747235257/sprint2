using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using sprint2;

public interface IController
{
    public Vector2 HandleAttack(GraphicsDeviceManager _graphics, IPlayer player);
    public void HandleDamaged(GraphicsDeviceManager _graphics, IPlayer player);
    public IProjectile HandlePlayerItem(GraphicsDeviceManager _graphics, IPlayer player);
    public void HandleMovement(GraphicsDeviceManager _graphics, IPlayer player);

    public void HandleSwitchEnemy(int currentNPC);
}
