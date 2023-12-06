README FOR SPRINT 4 (DZNT) - Oct 11 to Oct 20

Authors: Haikal Bin Rozaidi, Akmal Hasrudin, Beal Norazilan, Kyrie Iswandy, Dengchen Mei, Nathan Sprott.

Program: 2D top down zelda-like game.

Description for sprint3: Now, The program has a HUD, Inventory/Pause Screen, Victory and Death screens. Additionally, Sound and Music is now present in the game.
Also, the game supports item switching in the inventory screen. Levels need to be cleared (both items and enemies) before allowing the player to move to other rooms.
Keys are in the game world to unlock secret doors. Triforce implemented to win the game.

Code Metric Analysis: Data can be found attached in the excel file.

During Sprint 4,

In General: Naturally, # of lines of code increased. Compleity and coupling also increased with newly added features.

HUD: A big spike in complexity and # of lines because the dynamic grid map system was added.

SoundManager: Barely any changes to complexity and lines of code.

MusicManager: See above.
The music is made by Haowen Deng who is in NYU.
Inventory: Limited Data, seeing as it was implemented late in development.

Controls: 
(WASD/ARROWS): Move
(1), (2), (3): Shoot items in slots [1], [2], [3]
(z): Attack
(mouse clicks): Change Levels
(B): Debug Room
(r): reset (after death /victory)
(TAB): Pause/Inventory Screen
(1, 2,...9): In inventory Screen, corresponds to which item in inventory you want to switch items slots to. [First press will correspond to first item slot, Second press to 2nd item slot and so forth]


To fix/bugs/smells (sprint 2): 
1) Walk animation doesn't reset in PLAYER 
2) Make multiple different projectile classes, not just one PROJECTILE (DONE)
3) Remove magic numbers in ALL CLASSES (DONE)
4) Comments (INSYAALLAH)
5) Switching for BLOCKS and ITEMS is too fast. (DONE)
6) Mute Music/Sound Button (In-Progress)
7) Music change in Victory/Level changes (In-Progress)
8) 

