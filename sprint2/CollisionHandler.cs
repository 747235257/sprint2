using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections;

using System.Collections.Generic;
using System.ComponentModel;
using sprint2;
using Microsoft.Xna.Framework.Audio;
using System.Security.Cryptography.X509Certificates;

namespace sprint2;
public class CollisionHandler
{
    public Game1 _game;
    public CollisionHandler(Game1 game)
    {
        _game = game;
    }

    public void HandlePlayerParryProjectile(IPlayer player, List<IProjectile> projectiles, Game1 game)
    {
        if (player.InAttack())
        {
            Rectangle attackRect = player.getAttackHitbox();
            for (int i = 0; i < projectiles.Count; i++)
            {
                {
                    if (projectiles[i] != null)
                    {
                        Rectangle projRect = projectiles[i].getHitbox();

                        if (projRect.Intersects(attackRect))
                        {
                            projectiles[i].parryProjectile();
                            game.playerProjectiles.Add(projectiles[i]);
                            projectiles[i] = null;
                        }
                    }
                }
            }
        }
    }
    public void HandlePlayerAttackCollision(IPlayer player, List<INPC> NPCList)
    {
        if (player.InAttack())
        {
            Rectangle attackRect = player.getAttackHitbox();
            foreach (INPC npc in NPCList)
            {
                if(npc != null)
                {
                    Rectangle npcRect = npc.getHitbox();

                    if(npcRect.Intersects(attackRect))
                    {
                        npc.giveDamage();
                    }
                }
            }
        }
    }
    public void HandlePlayerItemCollision(List<IItem> items, IPlayer player)
	{
		foreach(IItem item in items)
		{
			Rectangle iHitbox = item.getHitbox();
			Rectangle pHitbox = player.getHitbox();

			if(iHitbox.Intersects(pHitbox))
			{
				if (item.isAlive())
				{
                    SoundEffectInstance pickupSound = SoundManager.Instance.CreateSound("pickupitem");
                    pickupSound.Play();
                    player.pickUpItem(item.getItemName());
                    _game.pickupCount++;
                    
                }
				item.setInactive();
				
			}
		}
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
                        _game.killCount ++;
						projectile.setToInactive();
						npc.giveDamage();
                        SoundEffectInstance damageSound = SoundManager.Instance.CreateSound("enemykill");
                        damageSound.Play();
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
    //Player vs Chest
    public void HandlePlayerChestCollision(IPlayer player, List<IChest> chests, List<IItem> items, SpriteBatch spriteBatch)
    {
        Rectangle playerHitbox = player.getHitbox();

        foreach (IChest chest in chests)
        {
            if (chest != null)
            {
                Rectangle chestHitbox = chest.getHitbox();
                if (chestHitbox.Intersects(playerHitbox))
                {
                    chest.openChest(chest, spriteBatch);
                    player.setLastPos();
                }
            }
        }
    }
    //Enemy vs Chest
    public void HandleEnemyChestCollision(List<INPC> enemies, List<IChest> chests)
    {
        foreach (INPC enemy in enemies)
        {
            foreach (IChest chest in chests)
            {
                if (enemy != null && chest != null)
                {
                    Rectangle cHitbox = chest.getHitbox();
                    Rectangle eHitbox = enemy.getHitbox();

                    if (cHitbox.Intersects(eHitbox))
                    {
                        enemy.setLastPos();
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

    public void HandleEnemyDoorCollision(List<INPC> enemies, List<Rectangle> doors)
    {
        foreach (INPC enemy in enemies)
        {
            foreach (Rectangle door in doors)
            {
                if (enemy != null)
                {

                    Rectangle eHitbox = enemy.getHitbox();

                    if (door.Intersects(eHitbox))
                    {
                        enemy.setLastPos();
                    }
                }
            }

        }
    }

    public void HandlePlayerDoorCollision(IPlayer player, List<Rectangle> doorHitboxes, List<DoorHitbox> doors, Game1 game)
    {
        Rectangle playerHitbox = player.getHitbox();
		//MusicManager music = new MusicManager(game);

		for(int i = 0; i < doors.Count; i++)
		{
			if (doorHitboxes[i].Intersects(playerHitbox))
			{
                if (game.curLevel.getClearStatus())
                {
                    SoundEffectInstance changeRoom = SoundManager.Instance.CreateSound("nextroom");
                    changeRoom.Play();
                    game.curLevel = game.levelManager.Levels[doors[i].NextLevel - 1]; //changes current level
                    game.hud.AddToGrid(game.curLevel.Name);
                    //music.MusicLoader(game, game.curLevel);
                    game.LockDoorHandler();
                    game.obstacleHandler = new ObstacleHandler(game, game, game.Blocks, game.ranChests);
                    game.obstacleHandler.Update(); //resets lists in game with new objects

                    game.randomLevelHandler = new RandomLevelHandler(game, game.blocks);
                    game.randomLevelHandler.Update();

                    player.setLocation(new Vector2((int)doors[i].NextX, (int)doors[i].NextY)); //new player location
                    game.wallHitboxes = game.WallHitboxHandler();
                    game.doors = game.DoorHitboxHandler();//doorlists is reset
                    if (!game.curLevel.getClearStatus())
                    {
                        _game.roomCount++;
                    }
                    
                } 
                else if (!game.curLevel.getClearStatus())
                    player.setLastPos();

                }
            }
		}
    

    //PlayerProj vs Wall
    public void HandleProjectileDoorCollision(List<Rectangle> doors, List<IProjectile> enemyProj, List<IProjectile> playerProj)
    {
        foreach (Rectangle door in doors)
        {


            foreach (IProjectile proj in enemyProj) //untested - enemies don't have hitboxes
            {
                if (proj != null)
                {
                    Rectangle projHitbox = proj.getHitbox();
                    if (door.Intersects(projHitbox)) proj.setToInactive();
                }
            }

            foreach (IProjectile proj in playerProj)
            {
                if (proj != null)
                {
                    Rectangle projHitbox = proj.getHitbox();
                    if (door.Intersects(projHitbox)) proj.setToInactive();
                }
            }
        }


    }

    public void HandleEnemyLockDoorCollision(List<INPC> enemies, List<LockDoorInstance> lockDoors)
    {
        foreach (INPC enemy in enemies)
        {
            foreach (LockDoorInstance lockDoor in lockDoors)
            {
                if (enemy != null)
                {

                    Rectangle eHitbox = enemy.getHitbox();

                    if (lockDoor.position.Intersects(eHitbox))
                    {
                        enemy.setLastPos();
                    }
                }
            }

        }

    }

    public void HandlePlayerLockDoorCollision(IPlayer player, List<LockDoorInstance> lockDoorInstances, Game1 game)
    {
        Rectangle playerHitbox = player.getHitbox();
        //MusicManager music = new MusicManager(game);

        for (int i = 0; i < lockDoorInstances.Count; i++)
        {
            if (lockDoorInstances[i].position.Intersects(playerHitbox))
            {
                if (lockDoorInstances[i].state == 0)
                {
                    if (player.getKeyCount() > 0)
                    {
                        lockDoorInstances[i].state = 1;
                        player.decrementKeyCount();
                        //may add some notifications that the door is open now
                    }
                    else
                    {
                        player.setLastPos();
                    }
                }
                else
                {
                    game.curLevel = game.levelManager.Levels[lockDoorInstances[i].NextLevel - 1]; //changes current level
                    game.hud.AddToGrid(game.curLevel.Name);
                    //music.MusicLoader(game, game.curLevel);
                    game.LockDoorHandler();
                    game.obstacleHandler = new ObstacleHandler(game, game, game.Blocks, game.ranChests);
                    game.obstacleHandler.Update(); //resets lists in game with new objects
                    player.setLocation(lockDoorInstances[i].playerPos); //new player location
                    game.wallHitboxes = game.WallHitboxHandler();
                    game.doors = game.DoorHitboxHandler();//doorlists is reset
                }
            }
            
            
        }
    }
    public void HandleProjectileLockDoorCollision(List<LockDoorInstance> lockDoors, List<IProjectile> enemyProj, List<IProjectile> playerProj)
    {
        foreach (LockDoorInstance lockDoor in lockDoors)
        {


            foreach (IProjectile proj in enemyProj) //untested - enemies don't have hitboxes
            {
                if (proj != null)
                {
                    Rectangle projHitbox = proj.getHitbox();
                    if (lockDoor.position.Intersects(projHitbox)) proj.setToInactive();
                }
            }

            foreach (IProjectile proj in playerProj)
            {
                if (proj != null)
                {
                    Rectangle projHitbox = proj.getHitbox();
                    if (lockDoor.position.Intersects(projHitbox)) proj.setToInactive();
                }
            }
        }


    }
}

