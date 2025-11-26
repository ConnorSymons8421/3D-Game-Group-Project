# Title: Conquer or Crashout

A colorful, playful 3D obstacle-course challenge game inspired by **Wipeout** and **American Ninja Warrior**.  
The goal: The player will try to conquer the course by **reaching the end without falling or "crashing out"**—and will attempt to achieve their best time!

---

## Game Overview

In **Conquer or Crashout**, players race through a chaotic obstacle course filled with unstable pads, spinning traps, shifting platforms, and timing-based challenges.  
If the player falls, they *crash out*. If they make it to the end and hit the completion button, they *conquer* the course!  
A timer tracks the player’s completion time, which will encourage replayability of the game.

---

## Features

**Player**
- Simple 3D character  
- Rigidbody + Capsule Collider  
- WASD movement  
- Jump  
- Fall detection  
- Animations: Idle, Run/Walk, Jump  

**Camera**
- 3rd-person follow camera  
- Smooth rotation + tracking  

**Lighting**
- Directional Light (sunlight for the outdoor course)  
- Point/Spot Lights (highlight key parts of the obstacles)

**Obstacles**
- 5 zones, each with 1–3 obstacles  
- Physics-based hazards and moving obstacles

---

## Obstacle Course Layout

**Zone 1**  
- Bounce Balls: 5 spheres that launch the player slightly upward as they try to bounce across  
- Balance Beam: Tilts left/right for balancing as they player aims to not fall off

**Zone 2**  
- Push Wall: Thin walkway beside a wall with pushers that jut out at random timings
- Sliding Platforms: 5 horizontally-moving platforms, which required timed jumps to be made

**Zone 3**  
- Rotating Sweeper Arms: Circular platform with rotating bars
- Rotating gear-like obstacle (x3): Players aim to time their jumps between each of the gear-like rotators

**Zone 4**  
- Slip & Slide + Rope Swing: Slide down the water slide, jump to grab the rope, use momentum to swing to platform  
- Spinning Columns: Four columns spinning in alternating directions, requires jump timing
- Pendulum: Swinging ball between four platforms, try to avoid getting knocked off 

**Zone 5**  
- Rotating Maze: Navigate rotating maze to climb out and reach the final platform  
- Final Red Button: Press the red buzzer to complete the course  

---

## Menus

**Main Menu**
- Background image of the obstacle course  
- Play  
- Quit  

**Pause Menu**
- Freezes gameplay and timer  
- Resume game
- Quit

**Retry Menu**
- Freezes gameplay and timer  
- Player can choose to retry last obstacle
- Or go to fail menu

**Game Over Screen**
- Displayed after player falls/fails the course  
- Play Again  
- Quit  

**Victory Screen**
- Displayed upon completion  of the course
- Play Again  
- Quit  

---

## Tech Used
- Unity 3D  
- Animator / Animation System  
- C# Scripts  
- Rigidbody & Colliders  
- UI Canvas / Toolkit  

---

## How to Play Game
1. Launch the game  
2. Select **Play**  
3. Move: **WASD**, Jump: **Spacebar**  
4. Avoid getting conquered by the obstacles and reach the red button  
5. Don’t fall—or you crash out!  

---

## Credits
Created by **Angela Lee, Connor Symons, Aleksa Ocampo, Jeremiah Cho**  
- Angela: Level design (Menus, fall animation), art (Menu backgrounds)
- Connor: Level design (Obstacles)
- Aleksa: Level design/Animation (Player)
- Jeremiah: Level design (Obstacles)
- 
Inspiration: *Wipeout* & *American Ninja Warrior*

Engine: Unity 3D

