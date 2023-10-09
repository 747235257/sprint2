using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections;

using System.Collections.Generic;
using System.ComponentModel;
using sprint2;

public class CollisionHandler
{
	public CollisionHandler()
	{

	}

		//PlayerProj vs Enemy
	public void HandleEnemyProjectileCollision(List<INPC> enemies, List<IProjectile> playerProj)
	{
		foreach (INPC npc in enemies)
		{
			foreach(IProjectile projectile in playerProj)
			{
				if(projectile != null && npc != null)
				{
					Rectangle eHitbox = npc.getHitbox();
					Rectangle pHitbox = projectile.getHitbox();

					if(eHitbox.Intersects(pHitbox))
					{
						projectile.setToInactive();
						npc.giveDamage();
                    }
				}
			}
		}
	}



    //EnemyProj vs Player
    public void HandlePlayerProjectileCollision(IPlayer player, List<IProjectile> enemyProj)
    {
		foreach(IProjectile proj in enemyProj)
		{
			if(proj != null)
			{
				Rectangle playerHitBox = player.getHitbox();
				Rectangle projHitBox = proj.getHitbox();

				if (playerHitBox.Intersects(projHitBox))
				{
					player.setDamaged();
					proj.setToInactive();
				}
			}
		}
        
    }
    //Player vs Enemy

    //Enemy vs Enemy

    //Player vs Block
	public void HandlePlayerBlockCollision(IPlayer player, List<IBlock> blocks)
	{
		Rectangle playerHitbox = player.getHitbox();

		foreach (IBlock block in blocks)
		{
			if(block != null)
			{
				Rectangle blockHitbox = block.getHitbox();
				if(blockHitbox.Intersects(playerHitbox))
				{
					player.setLastPos();
				}
			}
		}
	}

    //Enemy vs Block
	public void HandleEnemyBlockCollision(List<INPC> enemies, List<IBlock> blocks)
	{
		foreach(INPC enemy in enemies)
		{
			foreach(IBlock block in blocks)
			{
				if(enemy != null && block != null) 
				{
					Rectangle bHitbox = block.getHitbox();
					Rectangle eHitbox = enemy.getHitbox();

					if (bHitbox.Intersects(eHitbox))
					{
						enemy.setLastPos();
					}
				}
			}

		}
	}

    //PlayerProj vs Block
    public void HandleProjectileBlockCollision(List<IBlock> blocks, List<IProjectile> enemyProj, List<IProjectile> playerProj)
    {
        foreach (IBlock block in blocks)
        {
			if (block != null)
			{
				Rectangle blockHitbox = block.getHitbox();
				foreach (IProjectile proj in enemyProj) //untested - enemies don't have hitboxes
				{
					if (proj != null)
					{
						Rectangle projHitbox = proj.getHitbox();
						if(blockHitbox.Intersects(projHitbox))proj.setToInactive();
					}
				}

				foreach (IProjectile proj in playerProj)
				{
					if (proj != null)
					{
                        Rectangle projHitbox = proj.getHitbox();
                        if (blockHitbox.Intersects(projHitbox)) proj.setToInactive();
                    }
				}
			}
        }

    }
}

