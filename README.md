# EndlessRun - Game Test Submission

**Candidate Name:** Đoàn Duy Anh
**Date:** 24/7/2025

## How to Run the Project

1.  Unzip the project folder.
2.  Open the project using Unity Hub with Unity version `2022.3.62f1`.
3.  Open the `Assets/Scenes/MainScene.unity` file.
4.  Press the "Play" button in the editor.

## Features Implemented

- [x] Automatic player movement
- [x] Side-to-side controls
- [x] Fixed side-scrolling camera
- [x] Random obstacle spawning
- [x] Player health and damage system
- [x] Shooting and destroying obstacles
- [x] Gun with ammo and reload mechanic
- [x] Ragdoll on player death
- [x] Increasing difficulty over time
- [x] UI for Health, Score, and Ammo

## Architectural Decisions

### Singleton Pattern
GameManager use this to handle PlayerPrefs such as highscore and music config, i use Singleton for GameManager because it manage global value of user - dont need to be reinstantiate
InputManager use this to handle PlayerInput from InputAction, i use Singleton for InputManager for easy access player input and ensure there's only one input system at time


### Observer Pattern
I use Observer pattern to notify the difficuty up value change to enemy spawner to make it spawn more enemy, and Player Controller - to make player run faster, spawn enemy faster.
subject: GameplayController - observers: EnemySpawner, PlayerController


## External Assets Used

- https://assetstore.unity.com/packages/p/3d-sprite-sci-fi-props-246349
- https://assetstore.unity.com/packages/p/flying-carnivorous-beetle-95575
- https://assetstore.unity.com/packages/p/ten-power-ups-217666
- Animations and player model from https://www.mixamo.com/
- BGM and SFX from https://freesound.org/


