README FOR SPRINT 4 (DZNT) - Oct 11 to Oct 20

Authors: Haikal Bin Rozaidi, Akmal Hasrudin, Beal Norazilan, Kyrie Iswandy, Dengchen Mei, Nathan Sprott.

Program: 2D top down zelda-like game. Now supports additional features - including: Random Level Contents, Scoreboards, Enemy tracking for Bat and Skeleton enemies, Parry projectiles, Randomized chests, Final Boss. Also added minor
features like mute music, debug room, and level transitions with mouse.

Description for sprint5: Now supports additional features - including: Random Level Contents, Scoreboards, Enemy tracking for Bat and Skeleton enemies, Parry projectiles, Randomized chests, Final Boss. Also added minor
features like mute music, debug room, and level transitions with mouse.

Code Metric Analysis: Data can be found attached in the excel file.

During Sprint 5,

In General: Compleity, coupling and lines increased in sprint 5, naturally. Seeing as more functionality was added. Added classes include RandomLevelHandler, Boss1, and the RandomChest.

RandomLevelHandler: Saw a decrease in compleity, coupling and higher maintainability, seeing as items and enemies are now produced through a factory - reducing the coupling in any class that uses them.

ObstacleHandler: An old class that saw a decrease in compleity, coupling and higher maintainability - mainly because of the new use of item and enemy factories.

Boss1: Barely any changes in data, seeing as functionality was barely changed from original implementation.

RandomChest: Same comments as Boss1.

Bat and Skeleton: These two types of enemy classes saw an increase in compleity and coupling, because they know employ the use of newly implemented enemy tracking. This is natural seeing as more functionality is added.

OVERALL: The team plans to address increased compleity in enemy classes by somehow factoring out enemy behaviour into another class - so that enemy classes don't take the hit in terms of compleity and coupling.


Controls: 
(WASD/ARROWS): Move
(1), (2), (3): Shoot items in slots [1], [2], [3]
(z): Attack
(mouse clicks): Change Levels
(B): Debug Room
(r): reset (after death /victory)
(TAB): Pause/Inventory Screen
(I):Mute/Unmute Music
(1, 2,...9): In inventory Screen, corresponds to which item in inventory you want to switch items slots to. [First press will correspond to first item slot, Second press to 2nd item slot and so forth]


To fix/bugs/smells (sprint 2): 
1) Walk animation doesn't reset in PLAYER 
2) Make multiple different projectile classes, not just one PROJECTILE (DONE)
3) Remove magic numbers in ALL CLASSES (DONE)
4) Comments (INSYAALLAH)
5) Switching for BLOCKS and ITEMS is too fast. (DONE)
6) Mute Music/Sound Button (DONE)
7) Music change in Victory/Level changes (DONE)
8) 

