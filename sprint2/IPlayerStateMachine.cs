using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

public interface IPlayerStateMachine
{

    public void drawCurrentSprite();
    public void setIdle();
    public void moveLeft();


    public void moveRight();


    public void moveDown();


    public void moveUp();
    public void updateAttack();
    public void updateItem();

    public bool InAttack();
    public bool InItem();

    public Vector2 attack();

    public int useItem();
    public void setDamaged();

}
