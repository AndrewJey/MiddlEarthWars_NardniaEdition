# Middearth Wars – Videojuego de combate por turnos (Android)

## Descripción general
Middearth Wars es un videojuego de combate de fantasía por turnos para **Android**, en modo local “pass-and-play” (2 jugadores en el mismo dispositivo). Cada jugador elige nombre, raza y arma/elemento; luego se enfrentan alternando turnos hasta que uno quede con 0 puntos de vida o menos.

El proyecto está desarrollado en **C# con .NET MAUI** y usa **SQLite** para persistir estadísticas de jugadores e historial de partidas.

## Integrantes

- High GPT-5.1-Codex Plus de AndrewJey – Programación y lógica del juego  
- Chat GTP-5.1 Extended Thinking Plus con Sora – Diseño de UI / Imágenes  
- Andrés J. Jiménez Leandro, DBA – Bases de datos / Persistencia  
- BMAD Claude Agent – Pruebas / Documentación  

## Requisitos y cómo generar el APK

- IDE recomendado: Visual Studio Code (principal) y Visual Studio 2022 Community (secundario y para pruebas).  
- SDK: .NET 9 SDK con workloads de .NET MAUI para Android.  
- Versión mínima de Android: API 21 (Android 5.0), configurado en `CombatFantasyGame.csproj`.

Pasos generales (línea de comandos):

1. Restaurar paquetes y workloads (si es necesario):
   - `dotnet workload restore`
   - `dotnet restore`
2. Publicar APK de Release para Android:
   - `dotnet publish -f net9.0-android -c Release`
3. El APK quedará en una carpeta similar a:
   - `bin/Release/net9.0-android/*-Signed.apk`
4. Copiar el APK al dispositivo Android (cable USB, correo, nube, etc.) e instalarlo habilitando orígenes desconocidos si es necesario.

## Cómo ejecutar el juego

1. Instalar el APK en un teléfono Android real o en un emulador.  
2. Abrir la app `Middearth Wars`.  
3. Desde el menú principal:
   - Elegir `Nueva partida` para comenzar un combate.
   - Ver `Estadísticas` para revisar jugadores e historial.
   - Ver `Créditos` y `Reglas y documentación` para detalles técnicos.

## Instrucciones de juego

1. Selección de jugadores (`PlayerSetupPage`):
   - Cada jugador ingresa su nombre.
   - Cada jugador elige su raza: Humano, Elfo, Orco o Bestia.
   - Humano / Orco / Bestia eligen su arma.
   - Elfo elige su elemento (Fuego, Tierra, Aire, Agua).
2. Inicio del combate (`BattlePage`):
   - Ambos jugadores comienzan con 100 HP, excepto elfos de agua que empiezan con 115 HP.
   - La distancia inicial es 2 (están lejos). Distancia 0 significa combate cuerpo a cuerpo.
3. Turnos:
   - El combate es por turnos alternos: Jugador 1 → Jugador 2 → …
   - En su turno, cada jugador puede:
     - Atacar
     - Sanar
     - Avanzar (acercarse)
     - Retroceder (alejarse)
4. Distancia y ataques:
   - Si la distancia es mayor que 0, solo pueden atacar:
     - Humanos con rifle francotirador.
     - Elfos con magia de aire.
   - Para ataques cuerpo a cuerpo (escopeta, hacha, martillo, puños, espada, otros hechizos), primero se debe avanzar hasta que la distancia sea 0.
5. Condición de victoria:
   - Gana el jugador que reduzca la vida del oponente a 0 HP o menos.
   - Si ambos llegan a 0 HP en el mismo turno, se considera empate.
   - Al terminar, se muestra un resumen de la partida y se actualizan las estadísticas en SQLite.

## Reglas detalladas implementadas

### Vida inicial
- Todos los personajes empiezan con 100 HP, excepto:
  - Elfo de agua: 115 HP de vida inicial.

### Razas, armas y sanación

Humanos
- Armas disponibles:
  - Escopeta: daño base aleatorio 1–5 con un 20% extra aprox.  
  - Rifle francotirador:  
    - En cuerpo a cuerpo: daño base 1–5.  
    - A distancia (distancia > 0): daño elevado 10–20.
- Sanación (acción "comer"):  
  - Recupera entre 42% y 48% de la vida perdida.

Elfos (arma: báculo mágico)
- Elementos:
  - Fuego: daño base 1–5 con 40% de bonificación.  
  - Tierra: daño base 1–5 con +1 de bono y mayor precisión.  
  - Aire: daño base 1–5; si están a distancia, daño aumentado ~15%. Como defensor, tiene más probabilidad de evasión (reduce la precisión del atacante).  
  - Agua: vida inicial 115 HP y mejor sanación.
- Sanación:
  - Elfos normales: curan alrededor de 65% de la vida perdida.
  - Elfos de agua: curan entre 75% y 90% de la vida perdida.

Orcos
- Armas:
  - Hacha: daño base 1–5 e inflige sangrado durante 2 turnos (−3 HP por turno adicional).  
  - Martillo: daño base 2–7, sin efectos extra.
- Sanación (poción):
  - Cura inmediatamente entre 25% y 45% de la vida perdida.
  - Deja un efecto de curación adicional para el siguiente turno entre 5% y 25% de la vida perdida.

Bestias
- Ataques:
  - Puños: daño 20–30 al oponente, pero la bestia pierde 10 HP al atacar.  
  - Espada: daño aleatorio 1–10.
- Sanación (dormir):
  - Recupera exactamente el 50% de la vida perdida.

### Mecánica de distancia
- `GameSession.Distance` controla la distancia entre los personajes:
  - 0 → cuerpo a cuerpo (todos los ataques físicos y mágicos estándar pueden usarse).  
  - 1–3 → a distancia (solo rifle francotirador y magia de aire pueden atacar).
- Acciones de movimiento:
  - Avanzar: disminuye la distancia hasta un mínimo de 0.  
  - Retroceder: aumenta la distancia hasta un máximo simbólico de 3.

### Persistencia de datos (SQLite)

Se usa `sqlite-net-pcl` (`GameDatabase`) con dos tablas principales:

- PlayerStats
  - Name (nombre único del jugador).  
  - LastRace (última raza usada).  
  - LastWeaponOrElement (última arma o elemento usado).  
  - Wins (partidas ganadas).  
  - Losses (partidas perdidas).  
  - Draws (empates).

- MatchResult
  - PlayedAt (fecha y hora de la partida).  
  - Player1Name, Player2Name.  
  - Player1Race, Player2Race.  
  - Player1Weapon, Player2Weapon.  
  - WinnerName y bandera IsDraw.  
  - Player1RemainingHealth, Player2RemainingHealth.

Al terminar cada partida, `GameService` llama a `GameDatabase.UpdateStatsForMatchAsync`, actualizando estadísticas de jugadores e insertando una fila en `MatchResult`. La pantalla Estadísticas (`StatsPage`) lee ambas tablas y muestra ranking básico e historial.

## Arquitectura del proyecto

- Models:
  - Race, ElfElement, HumanWeapon, OrcWeapon, BeastWeapon.  
  - PlayerCharacter: estado en memoria de cada personaje (HP, raza, arma/elemento y efectos de sangrado/curación).  
  - GameSession: controla jugadores, turno actual, número de turno y distancia.  
  - PlayerStats, MatchResult: entidades persistentes en SQLite.
- Services:
  - GameService: lógica de combate, cálculo de daño, sanación, avance/retroceso y cambio de turno.  
  - GameDatabase: inicializa SQLite y expone métodos para leer/escribir estadísticas e historial.
- Vistas / pantallas (páginas MAUI):
  - MainPage: menú principal (Nueva partida, Estadísticas, Créditos, Reglas).  
  - PlayerSetupPage: selección de nombres, razas y armas/elementos.  
  - BattlePage: pantalla de combate, muestra vida de ambos jugadores, distancia e imagen del personaje activo en cada turno.  
  - SummaryPage: resumen de la partida (ganador, razas, armas, vida restante).  
  - StatsPage: estadísticas de jugadores e historial de partidas almacenados en SQLite.  
  - CreditsPage: créditos e información técnica básica.  
  - DocsPage: explicación de reglas, lógica de turnos y persistencia.

La navegación se implementa con Shell (`AppShell`), registrando rutas para cada página y usando `Shell.Current.GoToAsync(...)` para navegar entre ellas.

## Diagnóstico y registro de errores

- Todas las excepciones no controladas y los errores críticos se capturan mediante `Microsoft.Extensions.Logging` y se escriben en un archivo de texto diario dentro de `FileSystem.AppDataDirectory/logs`, por ejemplo `.../logs/combatfantasy-20251118.log`.
- Desde la aplicación puedes abrir **Reglas y documentación → Diagnóstico y registro de errores** para ver la ruta exacta del archivo y copiarla al portapapeles.
- En un dispositivo/emulador Android la ruta corresponde normalmente a `/data/data/com.andrewjey.middearthwars/files/logs/...`. Para extraerlo puedes usar `adb shell` / `adb pull` o el Device File Explorer de Visual Studio.
- La misma pantalla incluye un botón “Compartir archivo de log” que utiliza el share sheet de Android para enviarte el archivo por correo, chat, etc. Úsalo cuando no puedas acceder al `data/` del dispositivo.
- Si la app muestra el mensaje “Ocurrió un error inesperado…”, vuelve al menú principal, abre la sección de documentación y comparte el log resultante al reportar el problema.

## Imágenes y requisito visual por turno

- Las imágenes de personajes se cargan desde `Resources/Images`. Las imágenes de personajes han sido generadas por IA, a solicitud del encargado del proyecto, AndrewJey.
- `BattlePage` muestra una imagen que cambia según el personaje activo y su combinación raza/arma/elemento:
  - `human_shotgun.png`, `human_sniper.png`  
  - `elf_fire.png`, `elf_earth.png`, `elf_air.png`, `elf_water.png`  
  - `orc_axe.png`, `orc_hammer.png`  
  - `beast_fists.png`, `beast_sword.png`
- En este proyecto se crearon estos archivos inicialmente como copias de `dotnet_bot.png` para que la app funcione; se recomienda reemplazarlos por imágenes generadas por IA o tomadas de internet y documentar su origen.

## Capturas de pantalla (placeholders)

Añadir las capturas reales a una carpeta `docs/` o similar y actualizar las rutas según corresponda en este README:

- Menú principal: imagen de la pantalla inicial con botones de navegación.  
- Selección de personajes: pantalla con nombres, razas y armas/elementos.  
- Combate: pantalla de batalla con barras de vida, distancia e imagen por turno.  
- Resumen de partida: pantalla de resultado con ganador y estadísticas.
