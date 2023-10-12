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

	//PlayerProj vs Enemy

	//(IN PROGRESS)
	//public void HandleEnemyEnemyProjectileCollision(List<INPC> enemies, List<IProjectile> enemyProj)
	//{
		//foreach (INPC npc in enemies)
		//{
		//	foreach (IProjectile projectile in enemyProj)
		//	{
		//		if (projectile != null && npc != null)
		//		{
		//			Rectangle eHitbox = npc.getHitbox();
		//			Rectangle pHitbox = projectile.getHitbox();

		//			if (eHitbox.Intersects(pHitbox))
		//			{
		//				projectile.setToInactive();
		//			}
		//		}
		//	}
		//}
	//}



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
	public void HandlePlayerEnemyCollision(IPlayer player, List<INPC> enemies)
	{
		foreach(INPC enemy in enemies)
		{
			if (enemy != null)
			{
				Rectangle playerHitbox = player.getHitbox();
				Rectangle enemyHitbox = enemy.getHitbox();

				if (playerHitbox.Intersects(enemyHitbox))
				{
					player.setDamaged();
				}
			}
		}
	}

    //Enemy vs Enemy
	public void HandleEnemyEnemyCollision(List<INPC> enemies)
	{
		foreach(INPC enemy1 in enemies)
		{
			foreach(INPC enemy2 in enemies)
			{
				
				if(enemy1 != null && enemy2 != null && enemy1 != enemy2)
				{
                    Rectangle enemy1Hitbox = enemy1.getHitbox();
                    Rectangle enemy2Hitbox = enemy2.getHitbox();
					if (enemy1Hitbox.Intersects(enemy2Hitbox))
					{
						enemy1.setLastPos();
						enemy2.setLastPos();
					}

                }
			}
		}
	}

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
	//Enemy vs Wall
    public void HandleEnemyWallCollision(List<INPC> enemies, List<Rectangle> walls)
    {
        foreach (INPC enemy in enemies)
        {
            foreach (Rectangle wall in walls)
            {
                if (enemy != null && walls != null)
                {
                    
                    Rectangle eHitbox = enemy.getHitbox();

                    if (wall.Intersects(eHitbox))
                    {
                        enemy.setLastPos();
                    }
                }
            }

        }
    }

    //PlayerProj vs Wall
    public void HandleProjectileWallCollision(List<Rectangle> walls, List<IProjectile> enemyProj, List<IProjectile> playerProj)
    {
        foreach (Rectangle wall in walls)
        {
            
                
                foreach (IProjectile proj in enemyProj) //untested - enemies don't have hitboxes
                {
                    if (proj != null)
                    {
                        Rectangle projHitbox = proj.getHitbox();
                        if (wall.Intersects(projHitbox)) proj.setToInactive();
                    }
                }

                foreach (IProjectile proj in playerProj)
                {
                    if (proj != null)
                    {
                        Rectangle projHitbox = proj.getHitbox();
                        if (wall.Intersects(projHitbox)) proj.setToInactive();
                    }
                }
            }
        

    }
    //Player vs Block
    public void HandlePlayerWallCollision(IPlayer player, List<Rectangle> walls)
    {
        Rectangle playerHitbox = player.getHitbox();

        foreach (Rectangle wall in walls)
        {
            
                
                if (wall.Intersects(playerHitbox))
                {
                    player.setLastPos();
                }
            
        }
    }


}

