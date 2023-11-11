using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace sprint2
{
    public class LockDoorInstance
    {
        enum State
        {
            Lock = 0,
            Unlock = 1
        }

        public int state;
        public Rectangle position;
        public int NextLevel;
        public Vector2 playerPos;
        public LockDoorInstance(Game1 game, LockDoor lockDoor) {
            state = lockDoor.state;
            position = new Rectangle((int)lockDoor.X, (int)lockDoor.Y, lockDoor.Width, lockDoor.Height);
            NextLevel = lockDoor.NextLevel;
            playerPos = new Vector2(lockDoor.NextX, lockDoor.NextY);
        }

        

    }
}
