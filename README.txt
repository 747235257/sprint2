README FOR SPRINT 3 (DZNT) - Oct 11 to Oct 20

Authors: Haikal Bin Rozaidi, Akmal Hasrudin, Beal Norazilan, Kyrie Iswandy, Dengchen Mei, Nathan Sprott.

Program: 2D top down zelda-like game.

Description for sprint3: Now, the program supports collision handling, level transitions through doors and level changing through JSON files.

Code Metric Analysis: Data can be found attached in the excel file.

During Sprint 3,

In General: only # of lines of source code and exec code were changed heavily. Due to added features. Complexities remained the same.

CollisionHandler: Coupling increased, so did complexity. Mainly due to added Collision handling methods with Wall hitboxes and door hitboxes - also having to handle level changes.

Level: Barely any changes to complexity and lines of code. Changes are mainly due to newly added door hitboxes.

LevelManager: The same

Controls: 
(WASD/ARROWS): Move
(1), (2), (3): Shoot items
(mouse1/mouse2): change level

To fix/bugs/smells (sprint 2): 
1) Walk animation doesn't reset in PLAYER 
2) Make multiple different projectile classes, not just one PROJECTILE (DONE)
3) Remove magic numbers in ALL CLASSES (DONE)
4) Comments (INSYAALLAH)
5) Switching for BLOCKS and ITEMS is too fast. (DONE)

