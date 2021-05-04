Folklore of Lunk is a top down twinstick shooter type game that was inspired by the Legend of Zelda series. It emphasizes ranged combat with a variety of items to choose from in order to aid your player in combat or traversing through the level.

A lot of our code's foundation was built upon using YouTube content creator Mister Taft Creates' video series titled Make a Game Like Legend of Zelda using Unity and C#
Playlist can be found here:https://www.youtube.com/playlist?list=PL4vbr3u7UKWp0iM1WIfRjCDTI03u43Zfu

This repository mainly contains the code used for the enemy AI as it was the main role I was tasked with.


ENEMY TYPES

1. Log

The log script derives from the base enemy script that all the enemies derive from and is the foundation for the other enemy types. It has an idle, walk, attack, and stagger state that each go off when interacted by the player, the object most enemies have as their targetted position. Each enemy also has their base stats (movement speed, attack radius, chase radius) contained in a serialize field.
  a. Patrol Logs
     The patrol logs function similar to the base log enemy type, however they are given waypoints to traverse that they can leave and return towards depending on how 
     close the player is to their chase radius. 
  b. Turret Logs
     The turret logs are stationary and exist to shoot projectiles at the player using its own chase radius. 
  c. Melee Ogres
     The melee enemies derive from the log script, but uses a coroutine to attack the player for more damage. The coroutine allows for a visual indicator to tell the      
     player the enemy is about to attack.
     
2. Bosses
   Bosses derive from the base enemy script, but have specific conditions when they reach a certain threshold that changes the properties in the way they move 
   and attack.
   a. Mole Boss
      The mole boss functions like the turret log combined with the patrol log, however since it burrows underground while going through its waypoints, the player can 
      only attack it when it appears above ground to fire its projectile. Upon reaching a certain health threshold, it moves between its waypoints faster and fires 
      projectiles faster as well.
   b. Flame Lord
      The Flame Lord boss functions like a regular log, however it fires projectiles in a circular pattern. Upon reaching a certain health threshold,
      it moves faster and the amount of projectiles that makes up the circular pattern increases.
   c. Yeti
      The Yeti functions like the turret log, but shoots projectiles that freeze the player. Upon reaching a certain threshold, it moves between its way points faster
      and fires projectiles faster as well.
