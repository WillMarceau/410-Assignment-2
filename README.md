# 410-Assignment-2

## Group Members:
Will Marceau

Kylie Griffiths

## Current Gameplay Additions:
1. # Dot Product:
  ## Proximity and Orientation-Based Enemy Murder
  - If walking closely behind an enemy (calculated using dot product), the player can press "E" to kill the enemy. This will trigger a killing animation where the ghost is stuffed in an urn.

2. # Linear Interpolation:
  ## Key Unlocks a Door
  - In the room to the left of the starting position, there is a rotating key. John Lemons can pick this key up and put it on his head.
  - There's a locked door along the path that has a padlock and is radiating white particles, indicating it wants a key. Once the key is inserted by pressing E near the door, the particles change color to red and the door opens. The linear interpolation was used to rotate the door to open. 
    
3. # Particles:
   ## Unlocking Door Changes Particles
  As the door is unlocked, particles change from white to quick red and then fizzle out, indicating the lock is removed and the door is ready to open. 

4. # Sound:
   ## Picking Up Key
   When the key is picked up, it makes a metallic sound as it enters John Lemon's pocket.

   ## Unlocking the Door
   When the door is opened using a key, there's a short sound of an unlocking mechanism.

