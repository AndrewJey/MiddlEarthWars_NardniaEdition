# AI Agent Prompt: Comprehensive Prompt for Turn-Based Combat Game Development for Android in C# MAUI .NET Turn-Based Combat Game for Android Development

BEGIN PROMPT

[ROLE]

You are an expert C#/.NET MAUI mobile game developer, software architect, and technical writer. 
You specialize in:

- Building complete, production-ready .NET MAUI Android apps.
- Designing clean architectures (MVVM) with SQLite persistence.
- Creating small 2D/mobile games with turn-based combat, game rules, and UI flows.
- Generating full VS Code–compatible .NET projects from scratch.
- Producing clear documentation (README, in-app help, and technical docs).

Your job is to create, from scratch, a **fully working .NET MAUI Android project** for a **fantasy turn-based combat game** that strictly follows the specification below.

You must behave like a senior architect and lead engineer: design first, then implement, always ensuring the result can realistically be compiled and built with the .NET CLI and installed as an APK on an Android device.

You as an expert mobile application game architect and C# MAUI developer specializing in cross-platform game development on C# MAUI applications for Android. You have extensive experience with SQLite database integration, game mechanics implementation, and creating engaging user interfaces for mobile games. Your task is to create a complete turn-based combat game from scratch following all specifications provided.

 You are tasked with creating a complete, functional, and professional turn-based combat game for Android using C# MAUI, SQLite, and Visual Studio Code. You have deep knowledge of game mechanics, data persistence, mobile UI/UX, and Git version control.

 ## Expertise
- C# MAUI development for Android
- SQLite database integration
- Turn-based game mechanics
- Mobile UI/UX design
- Visual Studio Code project setup
- Git workflow and GitHub collaboration
- Image handling and asset management
- Cross-platform mobile development

---
## Project Overview
Create a turn-based fantasy combat game for Android using C# MAUI with SQLite for data persistence. The game should be developed in VS Code and include AI-generated or internet-sourced images. The game will feature a menu screen with credits and technical information.

## Objective
Create a complete, functional, and professionally structured turn-based fantasy combat game for Android that meets all specified requirements. The game must include:
- Two-player local (pass-and-play) combat
- Character selection with four races (Human, Elf, Orc, Beast)
- Weapon/ability system tied to race
- Turn-based combat with movement mechanics
- Health management and healing system
- SQLite-based data persistence
- Character-specific images displayed per turn
- Main menu with credits and technical information

[GLOBAL GOAL]

Create a **new .NET MAUI project in C# for Android only**, with **SQLite** persistence, implementing a **local 2-player, pass-and-play fantasy turn-based combat game**.

The final codebase must satisfy these constraints:

1. **Platform & Tech Stack**
   - Language: C#
   - Framework: .NET MAUI (Android only)
   - Target: Android (no iOS required)
   - IDE: compatible with VS Code + .NET CLI (no features that require Visual Studio only)
   - Persistence: SQLite (e.g., using `sqlite-net` or Microsoft’s SQLite integration)
   - Architecture: MVVM-style separation (Models, ViewModels, Views, Services)

1.1. ## Technical Requirements
    - Platform: Android (minimum API level 21)
    - Framework: C# MAUI
    - IDE: VS Code (compatibility with Visual Studio 2022, but not features that are in Visual Studio 2022 only)
    - Database: SQLite for local data persistence
        - SQLite database for player stats, characters, and match history
    - Images: AI-generated or sourced from the internet
    - Final deliverable: Installable APK
    - Project structure with proper layers (Models, Views, ViewModels, Services)
    - MVVM architecture pattern
    - Proper navigation between screens
    - Responsive UI for mobile devices
    - Git integration with proper commit history structure
    - README.md with complete documentation

2. ## **Constraints**
  - Target platform: Android only
  - Must generate a functional APK
  - Use C# MAUI framework only
  - Implement SQLite for data persistence
  - Include character images for turn display
  - Ensure data persistence across app sessions
  - Support 2-player local gameplay
  - Implement all race-specific mechanics and stats

3. **Project Output**
   - A complete .NET MAUI solution (with `.csproj`, folders, and all source files).
   - SQLite database integration with migrations/initialization logic.
   - UI built with XAML + C# code-behind (standard MAUI approach).
   - All code and comments in **English**.
   - Ready to be built with something like:
     - `dotnet build`
     - `dotnet publish -f net8.0-android` (or the appropriate MAUI Android target framework)

4. **Game Theme & Art**
   - Fantasy turn-based combat game with 4 races: **Human, Elf, Orc, Beast**.
   - Between turns, the app must show an image that represents the **active character** (race + weapon/element).
   - Use **placeholder images** (e.g., `Resources/Images/human_rifle.png`, `elf_fire.png`, etc.).  
     - Provide file names, paths, and a short description of what each image should depict.  
     - Assume the developer will later replace placeholders with original or properly licensed artwork (no copyrighted raw assets in the code).

5. **Menu & Credits Screen**
   - The app must start with a **Main Menu** screen providing:
     - “Start Game”
     - “View Stats / Match History”
     - “How to Play”
     - “Technical Info / Game Rules”
     - “Credits”
   - **Credits** must be a dedicated screen or section listing:
     - Game title
     - “Developed by: [placeholder names and roles]”
     - Technologies used (C#, .NET MAUI, SQLite, etc.)

---

[GAME DESIGN – CORE RULES]

## General Game Features

### 1. Character Selection
- Four playable races: Human, Elf, Orc, Beast
- Each player enters their name
- Each player selects their race through a selection screen

### 2. Race-Specific Mechanics

#### Humans (Starting HP: 100)
- Weapons: Shotgun, Sniper Rifle
- Healing: "Eat" action recovers 40-50% of damage received

#### Elves (Starting HP: 115)
- Weapon: Magic Staff
- Elements: Fire, Earth, Air, Water
- Healing: Regular elves heal 65%, Water elves heal 75-90%

#### Orcs (Starting HP: 100)
- Weapons: Axe (causes bleeding for 2 turns), Hammer
- Healing: Potion that heals 25-45% initially plus 5-25% next turn

#### Beasts (Starting HP: 100)
- Attacks: Fists (20-30 damage, but attacker loses 10 HP), Sword (1-10 damage)
- Healing: "Sleep" action recovers exactly 50% of lost HP

### 3. Combat Mechanics
- Turn-based combat system
- Distance mechanics affecting combat effectiveness
- Special damage bonuses for certain weapons at range
- Victory condition: Reduce opponent's HP to 0 or below

### 4. User Interface Requirements
- Menu screen with credits and technical information
- Character selection screens
- Combat screen with health bars, action buttons, and distance indicator
- Character-specific images displayed before each turn
- Victory/defeat screen with match summary

## Data Persistence Requirements
Using SQLite, persist the following information:
- Player data (unique names, statistics, win/loss/tie records)
- Character selections (names and races)
- Weapon selections linked to characters
- Basic match history (winner/loser, ties)

## Project Structure
Create a well-organized project with the following components:
- Models for game entities (Player, Character, Weapon, etc.)
- ViewModels for data binding
- Views for each screen
- Services for game logic and data persistence
- Utility classes for common functionality

## Development Process
1. Set up the C# MAUI project structure in VS Code
2. Implement SQLite database integration
3. Create the data models and database schema
4. Develop the main menu and navigation
5. Implement character selection screens
6. Create the combat screen with all game mechanics
7. Implement turn-based combat logic
8. Add character images for each turn
9. Create victory/defeat screens
10. Implement data persistence
11. Add credits and technical information screen
12. Test thoroughly on Android emulator and physical device
13. Generate installable APK

## Code Quality Requirements
- Follow C# coding conventions
- Implement MVVM architecture pattern
- Add appropriate comments and documentation
- Handle exceptions gracefully
- Optimize for mobile performance

[GAME RULES IN DETAIL]

Implement the following rules **exactly or with small, clearly documented numeric adjustments** (document any deviations in code comments and in the README).

1. **Players & Mode**
   - Two players, **local pass-and-play** on a single Android device.
   - Flow:
     - Player 1 enters their name.
     - Player 2 enters their name.
     - Each player chooses a **race** from:
       - Human
       - Elf
       - Orc
       - Beast (hybrid “animal + other race” flavor, but mechanically just “Beast”)

2. **Initial Health**
   - Default: all players start with **100 HP**.
   - Exception: **Water Elf** starts with **115 HP**.

3. **Races, Weapons, Abilities & Healing**

   3.1 Humans
   - Can only use **firearms**:
     - Shotgun
     - Sniper Rifle
   - **Shotgun**:
     - Base damage: random integer in range [1–5].
     - Adds a small extra percentage bonus (e.g., +10–20% of rolled damage).
   - **Sniper Rifle (at normal distance)**:
     - Base damage: random integer in range [1–5].
   - **Healing**:
     - Humans can spend a turn to “Eat”.
     - Healing restores roughly **40–50% of the missing HP** (less than 50%, more than 40%; pick a fixed % in that range and document it).

   3.2 Elves
   - Use **magic only**, channeled through a **staff**.
   - Must choose a **magic element**:
     - Fire
     - Earth
     - Air
     - Water
   - Element effects:
     - **Fire**: higher damage percentage than normal (e.g., +25–35% bonus).
     - **Earth**: slight attack bonus AND higher hit chance.
     - **Air**: normal base damage, but adds a **chance to evade** incoming attacks.
     - **Water**: increases starting HP and improves healing.
   - **Healing**:
     - Normal Elves: heal around **65%** of missing HP.
     - Water Elves: heal between **75–90%** of missing HP (choose a fixed or random value in that range and document it).

   3.3 Orcs
   - Choose between:
     - Axe
     - Hammer
   - **Axe**:
     - Base damage: random [1–5].
     - Applies **bleeding** for **2 extra turns**:
       - Example: target loses −3 HP at the start or end of each of the next 2 turns.
     - Bleeding must be tracked and applied in the turn engine.
   - **Hammer**:
     - Base damage: random [2–7].
     - No additional status effect.
   - **Healing**:
     - Orcs spend a turn to drink a **healing potion**.
     - Healing has two components:
       - First immediately: **25–45%** of missing HP.
       - Second on the next turn: extra **5–25%** of missing HP.
     - Implement with random values within those ranges and document the formula.

   3.4 Beasts
   - Attacks:
     - **Fists**:
       - High fixed physical damage: random between **20–30 HP** to the opponent.
       - Self-damage: the Beast loses **10 HP** each time it uses Fists.
     - **Sword**:
       - Random damage between **1–10 HP**.
   - **Healing**:
     - Beasts spend a turn to “Sleep”.
     - Healing restores **exactly 50% of the HP lost so far** (i.e., 50% of (maxHP − currentHP)).

4. **Turn System, Distance & Movement**

   - The game is strictly **turn-based**, with alternating turns:
     - Player 1 turn → Player 2 turn → repeat.
   - Implement a **distance mechanic** between the two characters, represented by:
     - A numeric value (e.g., 0 = melee / face-to-face, higher values = farther).
     - Or a simple bar/indicator in the UI.
   - Players can use their turn to:
     - **Move Forward** (reduce distance).
     - **Move Backward** (increase distance).
     - **Attack** (if conditions are met).
     - **Heal** (depending on race rules).
   - **Melee attacks** (e.g., fists, axe, hammer, sword, staff-based melee, etc.):
     - Only allowed when characters are “face-to-face” (distance == 0 or below a defined threshold).
   - **Ranged attacks at distance**:
     - When distance > melee range:
       - Only the following attacks are allowed and get a benefit:
         - **Sniper Rifle**:
           - At distance, damage range increases significantly (e.g., 10–20 HP).
         - **Air Magic**:
           - At distance, damage is increased by an extra **+5–15%**.
   - All these rules must be implemented in a **reusable turn engine** (service class) and clearly documented.

5. **Victory & End of Match**
   - A player **wins** when the opponent’s HP ≤ 0.
   - When the match ends:
     - Show a **summary screen** with:
       - Winner’s name
       - Winner’s remaining HP
       - Both races and chosen weapons/elements
       - Key statistics (number of turns, total damage dealt, etc. if possible)
     - Persist the result to SQLite:
       - Update players’ win/loss/draw counts.
       - Log a basic match record (winner, loser, draw, timestamp).

---

[PERSISTENCE REQUIREMENTS – SQLITE]

Use SQLite as the persistence layer. Design simple but clear tables such as (names are suggestions):

- `Players`
  - `Id`
  - `Name` (unique)
  - `TotalWins`
  - `TotalLosses`
  - `TotalDraws`
  - Other basic stats as desired.

- `Characters`
  - `Id`
  - `PlayerId` (FK to Players)
  - `CharacterName`
  - `Race` (Human/Elf/Orc/Beast)
  - `SubType` or `Element` (for Elves: Fire/Earth/Air/Water; for Humans: Shotgun/Sniper; for Orcs: Axe/Hammer; for Beasts: Fists/Sword choice)
  - `MaxHP`
  - Other relevant attributes.

- `Matches`
  - `Id`
  - `Player1Id`
  - `Player2Id`
  - `WinnerPlayerId` (nullable for draw)
  - `LoserPlayerId` (nullable if draw)
  - `Result` (Win/Loss/Draw)
  - `DateTime`
  - Optional: `Player1Race`, `Player2Race`, etc.

- `MatchDetails` (optional; simple version is enough)
  - `Id`
  - `MatchId`
  - `TurnNumber`
  - `ActingPlayerId`
  - `ActionType` (Attack/Move/Heal/etc.)
  - `DamageDealt`
  - `HPAfterAction`

Requirements:

- Data must be **persisted across app restarts**.
- At minimum, persist:
  - Players (with unique names and statistics).
  - Chosen characters/races/weapons.
  - Basic match history (who won/lost or if it was a draw).
- Implement a simple **repository or data service layer** around SQLite to keep ViewModels clean.

---

[UI & SCREENS]

Create at least the following screens (Views + ViewModels):

1. **Main Menu**
   - Buttons:
     - Start Game
     - View Stats / Match History
     - How to Play
     - Technical Info / Game Rules
     - Credits

2. **Player Setup Screen**
   - Two input fields:
     - Player 1 Name (text box)
     - Player 2 Name (text box)
   - “Continue” button to go to Race Selection.

3. **Race & Configuration Screen**
   - For each player:
     - Select **Race** (Human, Elf, Orc, Beast).
     - If Elf: select **Element** (Fire, Earth, Air, Water).
     - If Human: select weapon (**Shotgun** or **Sniper Rifle**).
     - If Orc: select weapon (**Axe** or **Hammer**).
     - If Beast: choose a default primary attack (Fists or Sword) or allow switching in combat.
   - Show short descriptions of each choice.

4. **Combat Screen**
   - Display:
     - Current HP of both players.
     - Current distance indicator.
     - Active player’s name, race, weapon/element.
     - **Image** of the active character (changes each turn).
   - Actions for the active player:
     - Attack (with the rules for their race/weapon/element).
     - Heal (if allowed for that race).
     - Move Forward.
     - Move Backward.
   - Log area or simple text area showing the last few actions (“Player 1 moved forward”, “Player 2 used Axe, dealing 4 damage + bleed”, etc.).

5. **Match Summary Screen**
   - Shows:
     - Winner, loser, or draw.
     - Final HP of both players.
     - Races and weapons/elements used.
     - Optional: total turns, total damage, etc.
   - Button to:
     - Return to Main Menu.
     - View Match History.

6. **Stats / Match History Screen**
   - Show:
     - List of players with wins/losses/draws.
     - List of recent matches with winner/loser and date.

7. **How to Play Screen**
   - Plain text explanation of:
     - Races and differences.
     - Weapons/elements and their effects.
     - Turn structure.
     - Distance mechanic.
     - Victory condition.

8. **Technical Info / Game Rules Screen**
   - More technical description:
     - Main rules as implemented (with exact numbers for damage/heal percentages).
     - Description of the turn engine.
     - Brief explanation of the persistence mechanism (SQLite tables).
     - Can re-use text from documentation.

9. **Credits Screen**
   - Game title.
   - Placeholder credits:
     - “Game design: [Name]”
     - “Programming: [Name]”
     - “Art / UI: [Name]”
   - Technologies used (C#, .NET MAUI, SQLite, etc.).

---

[CODE QUALITY & ARCHITECTURE]

Follow these guidelines:

- Use **MVVM**:
  - `Models/`
  - `ViewModels/`
  - `Views/`
  - `Services/` (GameEngine, DataService for SQLite, etc.)
- Keep game rules in a **GameEngine/TurnEngine service** separated from the UI.
- Use clear, self-explanatory names.
- Add reasonable comments explaining:
  - Damage and healing formulas.
  - Distance logic.
  - Bleeding, self-damage, and other status effects.
- Group persistence logic into a **DataService or Repository** class to avoid direct DB calls in ViewModels.

## Evaluation Criteria
Your implementation will be evaluated on:
- Correct implementation of turn-based combat mechanics
- Functional Android application with proper navigation
- Effective data persistence
- Integration of character images for each turn
- Code quality and organization
- Comprehensive documentation

---

[DOCUMENTATION & README]

Generate at least the following documentation files in Markdown:

1. **README.md**
   - Game name and short description.
   - Features summary.
   - How to build the project (CLI steps for .NET MAUI).
   - How to deploy the APK to an Android device/emulator (describe at high level).
   - How to play:
     - How to choose race and weapons/elements.
     - How to attack, heal, move, and win.
   - Technologies used (C#, .NET MAUI, SQLite).
   - Brief architecture overview:
     - Main layers (UI, ViewModels, Services, Data).
     - Mention SQLite tables.
   - Screenshots section:
     - Insert placeholders like `docs/screenshots/main_menu.png` etc. (describe what each screenshot should show).

2. **Technical documentation**
   - Either inside README.md or in a separate file, e.g., `docs/TECHNICAL_DESIGN.md`.
   - Must include:
     - Detailed explanation of game rules and their numeric values.
     - Description of turn logic and distance mechanic.
     - Simple diagram (ASCII or described) of main components:
       - Views
       - ViewModels
       - GameEngine/TurnEngine
       - DataService/SQLite
     - Description of SQLite schema (tables and columns).

---

# Step-by-Step Process

### Phase 1: Project Setup and Structure
1. Create new C# MAUI project in Visual Studio Code
2. Set up project structure:
   - Models/ (Player, Character, Weapon, Match classes)
   - Views/ (LoginPage, CharacterSelectionPage, GamePage, MenuPage, CreditsPage)
   - ViewModels/ (corresponding ViewModels)
   - Services/ (DatabaseService, GameService, ImageService)
   - Data/ (Database context and models)
   - Assets/ (images, icons)
3. Configure Android-specific settings and permissions
4. Install necessary NuGet packages (SQLite, CommunityToolkit.Mvvm)

### Phase 2: Data Models and Database
1. Create Player model with properties:
   - Name, Stats, Wins/Losses/Draws
   - Character selection and weapon choice
2. Create Character model with race-specific properties:
   - Race type (Human, Elf, Orc, Beast)
   - Base health, available weapons, abilities
3. Create Weapon/Ability model with damage ranges and effects
4. Implement SQLite database context and setup
5. Create data access layer for CRUD operations

### Phase 3: Game Mechanics Implementation
1. Implement turn-based combat system
2. Create race-specific mechanics:
   - Human: Firearms (Shotgun, Sniper Rifle) + Healing
   - Elf: Magic elements (Fire, Earth, Air, Water) + Healing
   - Orc: Axes/Hammers + Bleed effects + Healing
   - Beast: Fists/Swords + Self-damage mechanics + Healing
3. Implement distance/movement mechanics
4. Create health management and damage calculation
5. Implement victory conditions and match summary

### Phase 4: UI/UX Development
1. Create login screen for player names
2. Design character selection screen with race descriptions
3. Build main game screen with:
   - Health bars
   - Action buttons (Attack, Heal, Move)
   - Character image display for active turn
   - Distance indicator
4. Implement menu screen with navigation options
5. Create credits screen with technical information
6. Design match summary screen

### Phase 5: Image Integration and Asset Management
1. Source or generate character images for each race and weapon combination
2. Implement image display logic for turn-based character visualization
3. Add weapon/ability icons
4. Create background and UI assets
5. Optimize images for mobile performance

### Phase 6: Data Persistence Implementation
1. Implement player statistics saving/loading
2. Save character selections and preferences
3. Store match history and outcomes
4. Ensure data integrity across app sessions
5. Test persistence functionality thoroughly

### Phase 7: Testing and Quality Assurance
1. Test all race-specific mechanics
2. Verify turn-based gameplay logic
3. Confirm data persistence works correctly
4. Test navigation between screens
5. Validate APK generation and installation
6. Test on emulator and real Android device

### Phase 8: Documentation and Git Setup
1. Create comprehensive README.md with:
   - Project description and setup instructions
   - Gameplay instructions
   - Technology stack explanation
   - Architecture overview
   - Screenshots of main screens
2. Set up Git repository with initial commit structure
3. Prepare technical documentation for game mechanics
4. Document database schema and data flow

---

[GIT & PROJECT STRUCTURE (GUIDANCE)]

You cannot actually run Git commands, but you must:

- Propose a sensible repository structure in the README (folders, docs, etc.).
- Suggest how commits and branches should be used for:
  - Individual work (≥ 8 commits).
  - Teams of 2–4 students (with branches per person and explained pull request flow).
- Emphasize that the history should show real, progressive work (no fake “commit spam”).

---

[RESTRICTIONS]

- The project **must not** be a console application.
- Everything must be a proper **Android MAUI app**.
- The project must be realistic to build into an APK and run on at least one physical Android phone (as far as the project configuration goes).
- All logic must respect the race/weapon/element rules, turn system, distance mechanic, and persistence requirements.
- Code must be original (no blind copy-paste from unknown sources).

---

[WORKFLOW FOR YOUR OUTPUT]

When generating your response:

1. **First**, output a short overview of the architecture and folder structure (solution layout).
2. **Then**, generate:
   - The `.csproj` content.
   - The main `Program.cs` / app startup.
   - The `App.xaml` and `AppShell.xaml`.
   - All Views (XAML) and code-behind files.
   - All Models, ViewModels, Services, and SQLite setup code.
3. **After that**, generate:
   - SQLite schema and initialization code.
   - Example placeholder image file names and paths.
4. **Finally**, generate:
   - `README.md`
   - `docs/TECHNICAL_DESIGN.md` (or equivalent)
   - Any additional helper files (e.g., constants, enums).

Ensure that all pieces you generate are consistent with each other and could be copied into a fresh .NET MAUI project folder and built with the .NET CLI.

## Expected Output
- Complete C# MAUI solution ready for Visual Studio Code
- Functional Android APK file
- SQLite database implementation
- All required game mechanics and features
- Complete Git repository structure
- Comprehensive documentation
- Professional code organization following best practices
- All character images and assets properly integrated
- Proper data persistence for all required information

## Final Deliverables
1. Complete project source code
2. Installable APK file
3. Comprehensive README.md with:
   - Project description
   - Installation instructions
   - Gameplay instructions
   - Technical documentation
   - Screenshots of key screens

## Success Criteria
- Game runs smoothly on Android devices
- All race mechanics function correctly
- Data persists across app sessions
- Character images display properly per turn
- Two-player local gameplay works seamlessly
- APK installs and runs on emulator and real device
- Git repository shows proper commit history
- Documentation is clear and comprehensive
- Code follows professional standards and best practices

## Additional Notes
- Ensure the game works correctly on both Android emulator and physical devices
- All game mechanics should be implemented exactly as specified
- Pay special attention to the unique characteristics of each race and weapon
- The distance mechanic should properly affect combat effectiveness
- Character images should change appropriately based on the current turn

Please develop this game following all the specifications above, ensuring a complete, functional, and engaging turn-based combat experience for Android users. Begin implementation immediately, ensuring each phase is completed thoroughly before moving to the next. Focus on creating a production-ready, professional-grade mobile game that meets all specified requirements.

END PROMPT