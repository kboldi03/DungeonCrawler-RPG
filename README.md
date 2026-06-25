## 🎮 Gameplay Demo

Recommended watch: 1-minute full gameplay loop (town → recruitment → combat)

📽️ https://youtu.be/8anRH5H328M

# DungeonCrawler-RPG

A solo-developed turn-based dungeon crawler RPG built in Unity and C#. The project focuses on scalable, data-driven gameplay systems and modular RPG architecture.

---

## 🎮 Overview

A solo-developed turn-based RPG focusing on system-driven gameplay rather than content-heavy design.

Players manage a party of procedurally generated adventurers, equip them through shops, and progress through increasingly dangerous dungeon encounters with permanent death mechanics.

The project emphasizes modular architecture, data-driven design, and scalable gameplay systems built in Unity and C#.

---

## 🧩 Core Systems

### Combat System

* Speed-based turn order (initiative resolved at battle start)
* Physical and magical damage pipelines with separate mitigation (armor / resistance)
* Flat armor and magic penetration mechanics
* Critical hit system (scaled multiplier)
* Single-target and AoE skill support
* Permanent death for defeated party members

---

### Skill System

* ScriptableObject-based skill definitions
* Configurable damage type, targeting rules, and scaling
* Class-based skill progression (tiered unlock system)
* Fully data-driven and extensible design

---

### Character & Recruitment System

* Procedural adventurer generation in tavern
* Class-based stat ranges and randomization
* Persistent recruited character data
* Permadeath-driven roster management

---

### Item & Equipment System

- Procedural item generation (rarity/stat variation)
- Equipment slots system (in progress)
- Blacksmith shop for item purchasing (implemented)
- Inventory management (in progress)

---

### Town Management System

* Tavern (recruitment system)
* Blacksmith (item shop / gear progression)
* Barracks (party and equipment management)
* Central navigation hub for game flow

---

## 🧠 Technical Highlights

### Data-Driven Architecture

All game content (classes, enemies, skills, items) is defined using ScriptableObjects. This allows balancing and content expansion without modifying gameplay code.

### Runtime Entity System

Characters and enemies are generated at runtime using class-based templates with randomized stat distribution. Enemy scaling adapts based on encounter context.

### Modular Combat Architecture

Combat is fully system-driven and decoupled from presentation, enabling easy extension for new mechanics such as status effects, abilities, or AI behaviors.

---

## 👥 Current Classes

* Warrior
* Archer
* Mage

---

## 🚧 Planned Features

* Class evolution trees with branching paths
* Race system with passive traits
* Multi-floor dungeon progression system
* Boss encounters with unique mechanics
* Town building / upgrade systems
* Extended save/load system improvements

---

## 🛠️ Built With

* Unity (2D)
* C#
* ScriptableObjects
* Object-Oriented Design
* Data-Driven Architecture

---

## 📸 Screenshots
The following screenshots show the main gameplay loop and implemented systems in action.

### Combat encounter (turn order + skills)
<img width="860" height="480" alt="Combat" src="https://github.com/user-attachments/assets/45954325-0ec7-4e80-9ede-3c7b804eb012" />

### Tavern recruitment (procedural units)
<img width="857" height="481" alt="Tavern" src="https://github.com/user-attachments/assets/28e37de8-cdc4-4152-bb2d-f3710bd06b1a" />

### Barracks (party management & equipment)
<img width="857" height="477" alt="Barracks" src="https://github.com/user-attachments/assets/05434be0-67e3-4395-99b5-d75fb5662a2e" />

### Blacksmith (item shop system)
<img width="857" height="480" alt="Blacksmith" src="https://github.com/user-attachments/assets/03ee2b2e-3ebe-4aa2-a3a9-cd202d5167b0" />

---

## 🎯 Status

This project is in active development. Core gameplay systems are implemented and being expanded with progression, class evolution, and additional dungeon content.
