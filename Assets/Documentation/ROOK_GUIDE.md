# ROOK

Version: 0.3 (Stable Core Architecture)

---

# Purpose

Rook is a Unity foundation designed for:

- Learning game development
- Rapid prototyping
- Small indie games
- Long-term maintainability

Rook prioritizes:

1. Stability over cleverness
2. Explicit flow over implicit behavior
3. Readability over abstraction
4. Verifiability over magic
5. Extensibility without fragility

A developer should be able to understand the entire core architecture in one sitting.

If a system becomes difficult to understand, it should be simplified or removed.

---

# Core Philosophy

## 1. The Engine Is Not the Game

Rook separates:

- Engine Layer (flow, state, scenes, input)
- Gameplay Layer (player, enemies, mechanics)
- Presentation Layer (UI, feedback)

The engine must remain stable regardless of game content.

---

## 2. Explicit Over Implicit

Nothing happens automatically or “by convention.”

All behavior must be traceable:

Input → GameManager → GameFlow → SceneSystem → Scene Ready → Gameplay

No hidden triggers or side effects.

---

## 3. Scene Safety Principle

Unity scenes are not trusted as initialized environments.

A scene is only valid when explicitly declared READY.

Scene Loaded ≠ Scene Ready.

---

# Core Rules

## Rule 1 — Single Responsibility
Each script has one clear responsibility.

## Rule 2 — Single Owner
Each system or state has exactly one owner.

## Rule 3 — Explicit Over Clever
Prefer readable direct calls over abstraction layers.

## Rule 4 — Beginner Test
If a beginner cannot explain a script after reading it, it is too complex.

## Rule 5 — Delete Before Abstracting
Remove complexity before adding systems.

## Rule 6 — Just-In-Time Systems
Do not build systems before they are needed.

## Rule 7 — Scene Persistence Is Explicit
Only systems marked with DontDestroyOnLoad persist.

Persistence is intentional and rare.

---

# Definition of Done

A system is complete when:

- It is understandable without external explanation
- It has a single responsibility
- It does not depend on future systems
- It can be removed without breaking unrelated code

---

# Project Structure
```text
Assets/
├── Core
├── Systems
├── Gameplay
├── UI
├── Scenes
├── Utilities
└── Documentation
```
---

# Core

Minimum runtime foundation of the engine.

Contains:
- GameManager
- Bootstrap
- GameFlow

Responsibilities:
- Manage game state authority
- Control startup sequence
- Coordinate transitions

Does NOT:
- Handle input
- Load scenes directly
- Contain gameplay logic

---

# Systems

Reusable runtime services.

Contains:
- InputSystem
- SceneSystem
- UISystem

Responsibilities:
- Provide engine services
- React to GameState
- Operate independently of gameplay

Rules:
- Single responsibility
- No ownership of game state
- Explicit communication only

---

# Gameplay

Game-specific mechanics and logic.

Contains:
- PlayerController
- EnemyAI
- TestActor
- Weapons
- Game interactions

Responsibilities:
- Define game behavior
- React to engine state
- Execute gameplay rules

Does NOT:
- Control scene loading
- Control game state
- Manage input systems

---

# UI

Presentation layer only.

Responsibilities:
- Display game state
- Reflect GameManager state

Does NOT:
- Control gameplay
- Decide state changes
- Trigger scene transitions

---

# Scenes

Unity scene assets only.

- BootstrapScene
- MenuScene
- GameScene

Scenes are execution contexts, not logic containers.

---

# Startup Flow
```text
BootstrapScene
↓
Bootstrap
↓
GameFlow → EnterMenu
↓
SceneSystem → Load MenuScene
↓
Scene Ready
↓
GameManager = Menu
↓
InputSystem active
```
---

# Runtime Responsibilities

## Bootstrap

Entry point of the application.

- Starts initial flow
- Hands control to GameFlow
- Must remain minimal

---

## GameManager

Single source of truth for game state.

Owns:
- GameState
- CurrentScene
- Transition lock state

Does NOT:
- Load scenes
- Handle input
- Run gameplay logic

---

## Game States

- Boot
- Menu
- Playing
- Paused

---

## Game Loop Contract

Boot:
- Initialization state
- Immediately transitions to Menu

Menu:
- No gameplay simulation
- Player can start game

Playing:
- Gameplay active
- Escape → Pause

Paused:
- Gameplay frozen
- Escape → Resume

---

## InputSystem

Converts player input into intent.

Flow:
Input → GameFlow / GameManager

Responsibilities:
- Read input
- Trigger transitions

Does NOT:
- Load scenes directly
- Control gameplay logic

Persistence:
Must survive scene transitions (DontDestroyOnLoad)

---

## UISystem

Displays game state.

Responsibilities:
- Observe GameManager
- Reflect state only

Does NOT:
- Control gameplay
- Make decisions

---

## SceneSystem

Handles Unity scene loading.

Responsibilities:
- Load scenes
- Report scene loaded event

Does NOT:
- Decide game state
- Control gameplay logic

Scenes:
- MenuScene → UI context
- GameScene → gameplay context

---

## GameFlow

Transition coordinator for the engine.

Responsibilities:
- Orchestrate scene transitions
- Receive Scene Ready events
- Unlock gameplay timing

Does NOT:
- Replace GameManager
- Replace SceneSystem
- Implement gameplay logic

---

# Scene Lifecycle

A Unity scene is NOT usable when loaded.
```text
Scene Load Requested
↓
Unity Loads Scene
↓
Scene Loaded Event
↓
SceneSystem reports Scene Loaded
↓
GameFlow receives Scene Ready
↓
GameManager updates state
↓
Gameplay Allowed
```
---

## Definitions

Scene Loaded:
Unity has finished instantiating scene objects.

NOT guaranteed:
- system initialization complete
- references bound
- gameplay safe

---

Scene Ready:
Scene initialization handshake complete.

At this point:
- systems may communicate
- input may be processed
- gameplay may begin

---

Gameplay Allowed:
GameState == Playing AND Scene Ready

---

# Persistent Systems Pattern

Persistent systems:
- GameManager
- SceneSystem
- GameFlow
- InputSystem

Rules:
- Must use DontDestroyOnLoad
- Must survive scene transitions
- Must initialize safely

---

# Patterns (v0.3)

## State-Driven Flow Pattern

Game flow is controlled exclusively by GameState.

Input → GameManager → GameFlow → SceneSystem → Scene Ready

---

## Scene as Context Pattern

Scenes are execution contexts:
- MenuScene = navigation
- GameScene = gameplay

Scenes are consequences of state, not drivers.

---

## Transition Coordination Pattern

GameFlow ensures:
- Safe transitions
- Correct sequencing
- No premature gameplay activation

---

# Known Working Loop
```text
BootstrapScene
↓
Bootstrap
↓
GameFlow → Menu
↓
SceneSystem → MenuScene
↓
GameManager = Menu
↓
InputSystem waits
↓
Space pressed
↓
GameFlow → Game
↓
SceneSystem → GameScene
↓
GameManager = Playing
↓
Gameplay runs
```
---

# Version Snapshot — v0.3 Stable Core

- Scene transition pipeline stabilized
- GameFlow introduced as transition coordinator
- GameManager simplified to state authority
- InputSystem made persistent
- Scene Ready lifecycle formalized
- Gameplay gating enforced via state + readiness
- Clear separation of engine vs gameplay established

---

# Development Principle

Documentation comes before implementation.

If a system cannot be explained clearly, it should not exist yet.