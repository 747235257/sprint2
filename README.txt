README FOR SPRINT 2 (DZNT) - Sept 22 to Sept 29

Authors: Haikal Bin Rozaidi, Akmal Hasrudin, Beal Norazilan, Kyrie Iswandy, Dengchen Mei, Nathan Sprott.

Program: 2D top down zelda-like game.

Description: Currently, the program supports the player class, the enemy class, projectiles, blocks and items. The player is controlled through keyboard input and switching enemies/blocks/items are also handled through the keyboard

Code Metric Analysis: The maintainability of the code remains the same. The complexity also remained the same. So was Inheritance. Coupling between classes saw a slight increase, maybe due to adding the factory class to make new projectiles in the PlayerState. The lines of source code increased. We expected this because we added more features/fixes in classes such as the Projectile class, the factory class and the player class. The lines of executable code also increased.

To fix/bugs/smells: 
1) Walk animation doesn't reset in PLAYER 
2) Make multiple different projectile classes, not just one PROJECTILE (DONE)
3) Remove magic numbers in ALL CLASSES (DONE)
4) Comments (INSYAALLAH)
5) Switching for BLOCKS and ITEMS is too fast. (DONE)

