using CombatFantasyGame.Models;

namespace CombatFantasyGame.Services;

public static class LocalizationResources
{
    public static void Seed(LocalizationService localization)
    {
        RegisterCommon(localization);
        RegisterMain(localization);
        RegisterPlayerSetup(localization);
        RegisterBattle(localization);
        RegisterSummary(localization);
        RegisterStats(localization);
        RegisterCredits(localization);
        RegisterDocs(localization);
        RegisterLoadGame(localization);
    }

    private static void RegisterCommon(LocalizationService localization)
    {
        Add(localization, "Common_Accept",
            "Aceptar",
            "OK",
            "OK",
            "OK");

        Add(localization, "Common_Cancel",
            "Cancelar",
            "Cancel",
            "Cancelar",
            "キャンセル");

        Add(localization, "Common_NoBattleTitle",
            "Sin partida",
            "No match",
            "Sem partida",
            "対戦なし");

        Add(localization, "Common_NoBattleMessage",
            "Primero debes configurar una partida.",
            "Set up a match first.",
            "Configure uma partida primeiro.",
            "先に対戦を設定してください。");

        Add(localization, "Common_MissingDataTitle",
            "Datos incompletos",
            "Incomplete data",
            "Dados incompletos",
            "未入力のデータ");

        Add(localization, "Common_NoSession",
            "No hay sesión activa.",
            "No active session.",
            "Nenhuma sessão ativa.",
            "進行中のセッションがありません。");

        Add(localization, "Common_GenericError",
            "Ocurrió un error inesperado. Consulta la sección de documentación para obtener la ruta del registro.",
            "Something went wrong. Check the Documentation section for the log file location.",
            "Algo deu errado. Consulte a seção de documentação para encontrar o arquivo de log.",
            "予期しないエラーが発生しました。ドキュメント内の診断ログの場所を確認してください。");

        Add(localization, "Shell_Home",
            "Inicio",
            "Home",
            "Início",
            "ホーム");
    }

    private static void RegisterMain(LocalizationService localization)
    {
        Add(localization, "App_Title",
            "Middlearth Wars",
            "Middlearth Wars",
            "Middlearth Wars",
            "Middlearth Wars");

        Add(localization, "Main_Title",
            "Middlearth Wars: Nardnia Edition",
            "Middlearth Wars: Nardnia Edition",
            "Middlearth Wars: Nardnia Edition",
            "Middlearth Wars: Nardnia Edition");

        Add(localization, "Main_Subtitle",
            "Batalla por turnos",
            "Turn-based battle",
            "Batalha em turnos",
            "ターン制バトル");

        Add(localization, "Splash_Loading",
            "Preparando el campo de batalla...",
            "Preparing the battlefield...",
            "Preparando o campo de batalha...",
            "戦場を準備中...");

        Add(localization, "Main_LanguagePrompt",
            "Idioma / Language",
            "Language",
            "Idioma",
            "言語");

        Add(localization, "Main_LanguagePickerTitle",
            "Idioma",
            "Language",
            "Idioma",
            "言語");

        Add(localization, "Main_ThemeToggle",
            "Modo nocturno",
            "Dark mode",
            "Modo noturno",
            "ダークモード");

        Add(localization, "Main_NewGame",
            "Nueva partida",
            "New match",
            "Nova partida",
            "新しい対戦");

        Add(localization, "Main_Continue",
            "Continuar partida guardada",
            "Continue saved battle",
            "Continuar partida salva",
            "保存した対戦を再開");

        Add(localization, "Main_ContinueError",
            "No se pudo cargar la partida guardada.",
            "Unable to load the saved battle.",
            "Não foi possível carregar a partida salva.",
            "保存された対戦を読み込めませんでした。");

        Add(localization, "Main_Stats",
            "Estadísticas",
            "Stats",
            "Estatísticas",
            "統計");

        Add(localization, "Main_Credits",
            "Créditos",
            "Credits",
            "Créditos",
            "クレジット");

        Add(localization, "Main_Docs",
            "Reglas y documentación",
            "Rules & docs",
            "Regras e documentação",
            "ルールとドキュメント");
    }

    private static void RegisterPlayerSetup(LocalizationService localization)
    {
        Add(localization, "Setup_Title",
            "Configuración de jugadores",
            "Player setup",
            "Configuração de jogadores",
            "プレイヤー設定");

        Add(localization, "Setup_Header",
            "Configurar partida",
            "Set up match",
            "Configurar partida",
            "対戦設定");

        Add(localization, "Setup_Player1Title",
            "Jugador 1",
            "Player 1",
            "Jogador 1",
            "プレイヤー1");

        Add(localization, "Setup_Player2Title",
            "Jugador 2",
            "Player 2",
            "Jogador 2",
            "プレイヤー2");

        Add(localization, "Setup_Player1Placeholder",
            "Nombre del jugador 1",
            "Player 1 name",
            "Nome do jogador 1",
            "プレイヤー1の名前");

        Add(localization, "Setup_Player2Placeholder",
            "Nombre del jugador 2",
            "Player 2 name",
            "Nome do jogador 2",
            "プレイヤー2の名前");

        Add(localization, "Setup_RacePickerTitle",
            "Raza",
            "Race",
            "Raça",
            "種族");

        Add(localization, "Setup_ElementPickerTitle",
            "Elemento (solo elfos)",
            "Element (elves only)",
            "Elemento (apenas elfos)",
            "属性（エルフ専用）");

        Add(localization, "Setup_WeaponPickerTitle",
            "Arma",
            "Weapon",
            "Arma",
            "武器");

        Add(localization, "Setup_StartButton",
            "Iniciar combate",
            "Start battle",
            "Iniciar combate",
            "バトル開始");

        Add(localization, "Setup_LoadButton",
            "Cargar partida",
            "Load saved battle",
            "Carregar partida",
            "セーブデータを読み込む");

        Add(localization, "Setup_IncompleteMessage",
            "Debes ingresar nombre, raza y arma/elemento para ambos jugadores.",
            "Enter name, race and weapon/element for both players.",
            "Informe nome, raça e arma/elemento para ambos os jogadores.",
            "両プレイヤーの名前・種族・武器/属性を入力してください。");

        Add(localization, "Race_Human",
            "Humano",
            "Human",
            "Humano",
            "人間");

        Add(localization, "Race_Elf",
            "Elfo",
            "Elf",
            "Elfo",
            "エルフ");

        Add(localization, "Race_Orc",
            "Orco",
            "Orc",
            "Orc",
            "オーク");

        Add(localization, "Race_Beast",
            "Bestia",
            "Beast",
            "Fera",
            "ビースト");

        Add(localization, "Element_Fire",
            "Fuego",
            "Fire",
            "Fogo",
            "火");

        Add(localization, "Element_Earth",
            "Tierra",
            "Earth",
            "Terra",
            "土");

        Add(localization, "Element_Air",
            "Aire",
            "Air",
            "Ar",
            "風");

        Add(localization, "Element_Water",
            "Agua",
            "Water",
            "Água",
            "水");

        Add(localization, "Weapon_Shotgun",
            "Escopeta",
            "Shotgun",
            "Espingarda",
            "ショットガン");

        Add(localization, "Weapon_SniperRifle",
            "Rifle francotirador",
            "Sniper rifle",
            "Rifle de precisão",
            "スナイパーライフル");

        Add(localization, "Weapon_Axe",
            "Hacha",
            "Axe",
            "Machado",
            "斧");

        Add(localization, "Weapon_Hammer",
            "Martillo",
            "Hammer",
            "Martelo",
            "ハンマー");

        Add(localization, "Weapon_Fists",
            "Puños",
            "Fists",
            "Punhos",
            "拳");

        Add(localization, "Weapon_Sword",
            "Espada",
            "Sword",
            "Espada",
            "剣");
    }

    private static void RegisterBattle(LocalizationService localization)
    {
        Add(localization, "Battle_Title",
            "Combate",
            "Battle",
            "Combate",
            "バトル");

        Add(localization, "Battle_Attack",
            "Atacar",
            "Attack",
            "Atacar",
            "攻撃");

        Add(localization, "Battle_Heal",
            "Sanar",
            "Heal",
            "Curar",
            "回復");

        Add(localization, "Battle_MoveForward",
            "Avanzar",
            "Advance",
            "Avançar",
            "前進");

        Add(localization, "Battle_MoveBack",
            "Retroceder",
            "Retreat",
            "Recuar",
            "後退");

        Add(localization, "Battle_SaveButton",
            "Guardar partida",
            "Save battle",
            "Salvar partida",
            "対戦を保存");

        Add(localization, "Battle_SaveSuccess",
            "Partida guardada correctamente.",
            "Battle saved successfully.",
            "Partida salva com sucesso.",
            "対戦を保存しました。");

        Add(localization, "Battle_SaveError",
            "No se pudo guardar la partida.",
            "Unable to save the battle.",
            "Não foi possível salvar a partida.",
            "対戦を保存できませんでした。");

        Add(localization, "Battle_SavePromptTitle",
            "Guardar partida",
            "Save battle",
            "Salvar partida",
            "セーブ");

        Add(localization, "Battle_SavePromptMessage",
            "Ingresa un nombre para esta partida guardada.",
            "Enter a name for this saved battle.",
            "Informe um nome para este jogo salvo.",
            "セーブデータの名前を入力してください。");

        Add(localization, "Battle_SavePromptAccept",
            "Guardar",
            "Save",
            "Salvar",
            "保存");

        Add(localization, "Battle_SavePromptCancel",
            "Cancelar",
            "Cancel",
            "Cancelar",
            "キャンセル");

        Add(localization, "Battle_SaveDefaultName",
            "{0} vs {1} - Turno {2}",
            "{0} vs {1} - Turn {2}",
            "{0} vs {1} - Turno {2}",
            "{0} vs {1} - ターン{2}");

        Add(localization, "Battle_NoSession",
            "No hay partida en curso.",
            "No battle in progress.",
            "Nenhuma batalha em andamento.",
            "対戦は進行中ではありません。");

        Add(localization, "Battle_TurnFormat",
            "Turno {0} - Juega {1}",
            "Turn {0} - {1}'s move",
            "Turno {0} - vez de {1}",
            "ターン{0} - {1}の手番");

        Add(localization, "Battle_DistanceFormat",
            "Distancia: {0} (0 = cuerpo a cuerpo)",
            "Distance: {0} (0 = melee)",
            "Distância: {0} (0 = corpo a corpo)",
            "距離: {0}（0 = 近接）");

        Add(localization, "Battle_HealthFormat",
            "{0}: {1}/{2} HP",
            "{0}: {1}/{2} HP",
            "{0}: {1}/{2} HP",
            "{0}: {1}/{2} HP");

        Add(localization, "Battle_CurrentPlayerInfoFormat",
            "{0} - {1}",
            "{0} - {1}",
            "{0} - {1}",
            "{0} - {1}");

        Add(localization, "Battle_AttackOutOfRange",
            "Están demasiado lejos, solo francotirador o magia de aire pueden atacar.",
            "Too far away; only sniper shots or air magic can attack.",
            "Vocês estão muito distantes; só rifle de precisão ou magia de ar podem atacar.",
            "距離が離れ過ぎています。スナイパーライフルか風魔法のみ攻撃可能です。");

        Add(localization, "Battle_AttackMissed",
            "{0} falló el ataque.",
            "{0} missed the attack.",
            "{0} errou o ataque.",
            "{0} は攻撃を外しました。");

        Add(localization, "Battle_AttackDamage",
            "{0} causó {1} de daño.",
            "{0} dealt {1} damage.",
            "{0} causou {1} de dano.",
            "{0} は {1} ダメージを与えた。");

        Add(localization, "Battle_BleedApplied",
            "El objetivo está sangrando por 2 turnos (-3 HP por turno).",
            "The target bleeds for 2 turns (-3 HP each turn).",
            "O alvo sangra por 2 turnos (-3 HP por turno).",
            "対象は2ターン出血し（各ターン -3 HP）、ダメージを受けます。");

        Add(localization, "Battle_BeastSelfDamage",
            "{0} recibe 10 de daño por el ataque brutal.",
            "{0} takes 10 self-damage from the brutal strike.",
            "{0} recebe 10 de dano próprio pelo ataque brutal.",
            "{0} は猛攻の反動で 10 ダメージを受ける。");

        Add(localization, "Battle_AlreadyFullHealth",
            "{0} ya tiene la vida completa.",
            "{0} is already at full health.",
            "{0} já está com a vida cheia.",
            "{0} はすでに全快です。");

        Add(localization, "Battle_HumanHeal",
            "{0} come y recupera {1} puntos de vida.",
            "{0} eats and recovers {1} HP.",
            "{0} come e recupera {1} de HP.",
            "{0} は食事をして {1} HP 回復した。");

        Add(localization, "Battle_ElfHeal",
            "{0} lanza un hechizo de sanación y recupera {1} puntos de vida.",
            "{0} casts a healing spell and recovers {1} HP.",
            "{0} lança uma magia de cura e recupera {1} de HP.",
            "{0} は回復魔法を唱え {1} HP を回復した。");

        Add(localization, "Battle_OrcHeal",
            "{0} bebe una poción y recupera {1} puntos de vida ahora, y se curará más en el próximo turno.",
            "{0} drinks a potion, recovers {1} HP now, and will heal more next turn.",
            "{0} bebe uma poção, recupera {1} de HP agora e curará mais no próximo turno.",
            "{0} はポーションを飲み {1} HP を回復。次のターンにも追加回復。");

        Add(localization, "Battle_BeastHeal",
            "{0} duerme y recupera {1} puntos de vida.",
            "{0} sleeps and recovers {1} HP.",
            "{0} dorme e recupera {1} de HP.",
            "{0} は眠って {1} HP を回復した。");

        Add(localization, "Battle_CannotHeal",
            "{0} no puede sanar.",
            "{0} cannot heal.",
            "{0} não pode curar.",
            "{0} は回復できない。");

        Add(localization, "Battle_AlreadyMelee",
            "Ya estás en distancia de cuerpo a cuerpo.",
            "You are already at melee distance.",
            "Você já está em alcance corpo a corpo.",
            "すでに近接距離です。");

        Add(localization, "Battle_MoveForwardResult",
            "{0} avanza. Distancia actual: {1}.",
            "{0} advances. Current distance: {1}.",
            "{0} avança. Distância atual: {1}.",
            "{0} が前進。現在の距離: {1}");

        Add(localization, "Battle_AlreadyMaxDistance",
            "Ya estás lo más lejos posible.",
            "You are as far away as possible.",
            "Você já está o mais distante possível.",
            "これ以上離れられません。");

        Add(localization, "Battle_MoveBackResult",
            "{0} retrocede. Distancia actual: {1}.",
            "{0} retreats. Current distance: {1}.",
            "{0} recua. Distância atual: {1}.",
            "{0} が後退。現在の距離: {1}");
    }

    private static void RegisterSummary(LocalizationService localization)
    {
        Add(localization, "Summary_Title",
            "Resumen de partida",
            "Match summary",
            "Resumo da partida",
            "対戦サマリー");

        Add(localization, "Summary_BackButton",
            "Volver al menú principal",
            "Back to main menu",
            "Voltar ao menu principal",
            "メインメニューへ戻る");

        Add(localization, "Summary_ViewStatsButton",
            "Ver estadísticas de jugadores",
            "View player stats",
            "Ver estatísticas dos jogadores",
            "プレイヤー統計を見る");

        Add(localization, "Summary_NoData",
            "No hay datos de la partida.",
            "No battle data available.",
            "Não há dados da partida.",
            "対戦データがありません。");

        Add(localization, "Summary_WinnerFormat",
            "Ganador: {0}",
            "Winner: {0}",
            "Vencedor: {0}",
            "勝者: {0}");

        Add(localization, "Summary_DrawTitle",
            "La partida terminó en empate.",
            "The battle ended in a draw.",
            "A partida terminou empatada.",
            "対戦は引き分けでした。");

        Add(localization, "Summary_DefaultTitle",
            "Resumen de partida",
            "Match summary",
            "Resumo da partida",
            "対戦サマリー");

        Add(localization, "Summary_PlayerLineFormat",
            "{0} - {1} - Vida restante: {2}/{3}",
            "{0} - {1} - Remaining HP: {2}/{3}",
            "{0} - {1} - Vida restante: {2}/{3}",
            "{0} - {1} - 残りHP: {2}/{3}");

        Add(localization, "Summary_DrawLabel",
            "Empate",
            "Draw",
            "Empate",
            "引き分け");
    }

    private static void RegisterStats(LocalizationService localization)
    {
        Add(localization, "Stats_Title",
            "Estadísticas",
            "Statistics",
            "Estatísticas",
            "統計");

        Add(localization, "Stats_Header",
            "Estadísticas de jugadores",
            "Player statistics",
            "Estatísticas dos jogadores",
            "プレイヤー統計");

        Add(localization, "Stats_HistoryHeader",
            "Historial de partidas",
            "Match history",
            "Histórico de partidas",
            "対戦履歴");

        Add(localization, "Stats_RaceFormat",
            "Última raza: {0}",
            "Last race: {0}",
            "Última raça: {0}",
            "最後の種族: {0}");

        Add(localization, "Stats_WeaponFormat",
            "Último arma/elemento: {0}",
            "Last weapon/element: {0}",
            "Última arma/elemento: {0}",
            "最後の武器/属性: {0}");

        Add(localization, "Stats_WinsFormat",
            "Ganadas: {0}",
            "Wins: {0}",
            "Vitórias: {0}",
            "勝利: {0}");

        Add(localization, "Stats_LossesFormat",
            "Perdidas: {0}",
            "Losses: {0}",
            "Derrotas: {0}",
            "敗北: {0}");

        Add(localization, "Stats_DrawsFormat",
            "Empates: {0}",
            "Draws: {0}",
            "Empates: {0}",
            "引き分け: {0}");

        Add(localization, "Stats_DateFormat",
            "Fecha: {0}",
            "Date: {0}",
            "Data: {0}",
            "日時: {0}");

        Add(localization, "Stats_Player1Label",
            "Jugador 1",
            "Player 1",
            "Jogador 1",
            "プレイヤー1");

        Add(localization, "Stats_Player2Label",
            "Jugador 2",
            "Player 2",
            "Jogador 2",
            "プレイヤー2");

        Add(localization, "Stats_PlayerSummaryFormat",
            "{0} - {1}",
            "{0} - {1}",
            "{0} - {1}",
            "{0} - {1}");

        Add(localization, "Stats_PlayerEntryFormat",
            "{0}: {1}",
            "{0}: {1}",
            "{0}: {1}",
            "{0}: {1}");

        Add(localization, "Stats_WinnerFormat",
            "Ganador: {0}",
            "Winner: {0}",
            "Vencedor: {0}",
            "勝者: {0}");

        Add(localization, "Stats_UnknownValue",
            "N/D",
            "N/A",
            "N/D",
            "不明");
    }

    private static void RegisterCredits(LocalizationService localization)
    {
        Add(localization, "Credits_Title",
            "Créditos",
            "Credits",
            "Créditos",
            "クレジット");

        Add(localization, "Credits_Header",
            "Créditos",
            "Credits",
            "Créditos",
            "クレジット");

        Add(localization, "Credits_TeamHeader",
            "Integrantes del proyecto:",
            "Project team:",
            "Integrantes do projeto:",
            "プロジェクトメンバー:");

        Add(localization, "Credits_Member1",
            "• High GPT-5.1-Codex Plus de AndrewJey – Programación y lógica del juego",
            "• High GPT-5.1-Codex Plus by AndrewJey – Programming & game logic",
            "• High GPT-5.1-Codex Plus de AndrewJey – Programação e lógica do jogo",
            "• High GPT-5.1-Codex Plus（AndrewJey）– プログラミング／ゲームロジック");

        Add(localization, "Credits_Member2",
            "• Chat GTP-5.1 Extended Thinking Plus con Sora – Diseño de UI / Imágenes",
            "• Chat GTP-5.1 Extended Thinking Plus with Sora – UI / Art design",
            "• Chat GTP-5.1 Extended Thinking Plus com Sora – Design de UI / Imagens",
            "• Chat GTP-5.1 Extended Thinking Plus（Sora）– UI／画像デザイン");

        Add(localization, "Credits_Member3",
            "• Andrés J. Jiménez Leandro, DBA – Bases de datos / Persistencia",
            "• Andrés J. Jiménez Leandro, DBA – Databases / Persistence",
            "• Andrés J. Jiménez Leandro, DBA – Banco de dados / Persistência",
            "• Andrés J. Jiménez Leandro, DBA – データベース／永続化");

        Add(localization, "Credits_Member4",
            "• BMAD Claude Agent – Pruebas / Documentación",
            "• BMAD Claude Agent – Testing / Documentation",
            "• BMAD Claude Agent – Testes / Documentação",
            "• BMAD Claude Agent – テスト／ドキュメント");

        Add(localization, "Credits_TechHeader",
            "Tecnologías utilizadas:",
            "Technologies:",
            "Tecnologias:",
            "使用技術:");

        Add(localization, "Credits_Tech1",
            ".NET MAUI (C#) para Android",
            ".NET MAUI (C#) for Android",
            ".NET MAUI (C#) para Android",
            ".NET MAUI（C#／Android）");

        Add(localization, "Credits_Tech2",
            "SQLite (sqlite-net-pcl) para persistencia local",
            "SQLite (sqlite-net-pcl) for local storage",
            "SQLite (sqlite-net-pcl) para persistência local",
            "SQLite（sqlite-net-pcl）によるローカル保存");

        Add(localization, "Credits_Tech3",
            "Visual Studio Code como IDE principal",
            "Visual Studio Code as primary IDE",
            "Visual Studio Code como IDE principal",
            "Visual Studio Code（主要IDE）");

        Add(localization, "Credits_Tech4",
            "Visual Studio 2022 Community como IDE secundario y entorno de pruebas",
            "Visual Studio 2022 Community as secondary/testing IDE",
            "Visual Studio 2022 Community como IDE secundário e ambiente de testes",
            "Visual Studio 2022 Community（サブ／テストIDE）");

        Add(localization, "Credits_ImagesHeader",
            "Imágenes:",
            "Images:",
            "Imagens:",
            "画像:");

        Add(localization, "Credits_ImagesNote",
            "Las imágenes de personajes han sido generadas por IA, a solicitud del encargado del proyecto, AndrewJey.",
            "Character images were generated by AI at the request of project lead AndrewJey.",
            "As imagens dos personagens foram geradas por IA a pedido do responsável pelo projeto, AndrewJey.",
            "キャラクター画像は、プロジェクト担当AndrewJeyの依頼でAI生成されています。");
    }

    private static void RegisterDocs(LocalizationService localization)
    {
        Add(localization, "Docs_Title",
            "Documentación técnica del juego",
            "Game technical documentation",
            "Documentação técnica do jogo",
            "ゲーム技術ドキュメント");

        Add(localization, "Docs_Intro",
            "• Dos jugadores locales (pass-and-play) comparten el dispositivo.\n• Cada jugador elige nombre, raza y arma/elemento antes de iniciar la partida.\n• Todos comienzan con 100 HP, excepto elfos de agua con 115 HP.",
            "• Two local players (pass-and-play) share the device.\n• Each player selects a name, race, and weapon/element before starting.\n• Everyone starts with 100 HP, except water elves with 115 HP.",
            "• Dois jogadores locais (pass-and-play) compartilham o dispositivo.\n• Cada jogador escolhe nome, raça e arma/elemento antes da partida.\n• Todos começam com 100 HP, exceto elfos da água com 115 HP.",
            "• ローカルの2人対戦（パス＆プレイ）。\n• プレイヤーは名前／種族／武器・属性を選択して開始。\n• 基本HPは100、ただし水のエルフは115。");

        Add(localization, "Docs_RacesHeader",
            "Razas y armas",
            "Races & weapons",
            "Raças e armas",
            "種族と武器");

        Add(localization, "Docs_Humans",
            "Humanos:\n- Escopeta: daño base 1–5 con +20% adicional.\n- Rifle francotirador: 1–5 en cuerpo a cuerpo, 10–20 a distancia.\n- Sanación: acción \"comer\" que recupera 42%–48% de la vida perdida.",
            "Humans:\n- Shotgun: base damage 1–5 with +20% bonus.\n- Sniper rifle: 1–5 in melee, 10–20 at range.\n- Healing: \"eat\" action restoring 42%–48% of lost HP.",
            "Humanos:\n- Espingarda: dano 1–5 com bônus de 20%.\n- Rifle de precisão: 1–5 no corpo a corpo, 10–20 à distância.\n- Cura: ação \"comer\" recupera 42%–48% da vida perdida.",
            "人間:\n- ショットガン: 基本ダメージ1〜5に20%追加。\n- スナイパーライフル: 近接1〜5、遠距離10〜20。\n- 回復: 「食事」で失ったHPの42〜48%回復。");

        Add(localization, "Docs_Elves",
            "Elfos:\n- Fuego: daño base con +40%.\n- Tierra: +1 de daño y mayor precisión.\n- Aire: mejora daño a distancia y evasión como defensor.\n- Agua: 115 HP iniciales y mejor curación.",
            "Elves:\n- Fire: +40% damage.\n- Earth: +1 damage and higher accuracy.\n- Air: boosts ranged damage and evasion when defending.\n- Water: starts at 115 HP and heals more.",
            "Elfos:\n- Fogo: +40% de dano.\n- Terra: +1 de dano e mais precisão.\n- Ar: melhora dano à distância e evasão.\n- Água: 115 HP iniciais e cura superior.",
            "エルフ:\n- 火: ダメージ+40%。\n- 土: ダメージ+1と命中率向上。\n- 風: 遠距離火力と回避力アップ。\n- 水: 初期HP115で回復量増。");

        Add(localization, "Docs_Orcs",
            "Orcos:\n- Hacha: 1–5 de daño y sangrado (-3 HP por 2 turnos).\n- Martillo: 2–7 de daño estable.\n- Sanación: poción que cura 25%–45% ahora y 5%–25% extra en el próximo turno.",
            "Orcs:\n- Axe: 1–5 damage plus bleeding (-3 HP for 2 turns).\n- Hammer: steady 2–7 damage.\n- Healing: potion heals 25%–45% now and 5%–25% next turn.",
            "Orcs:\n- Machado: 1–5 de dano e sangramento (-3 HP por 2 turnos).\n- Martelo: dano constante de 2–7.\n- Cura: poção cura 25%–45% agora e mais 5%–25% no próximo turno.",
            "オーク:\n- 斧: ダメージ1〜5に加え2ターン出血（-3 HP）。\n- ハンマー: 安定した2〜7ダメージ。\n- 回復: ポーションで25〜45%を即回復、次ターンに追加5〜25%。");

        Add(localization, "Docs_Beasts",
            "Bestias:\n- Puños: hacen 20–30 de daño pero pierden 10 HP.\n- Espada: daño 1–10.\n- Sanación: dormir recupera 50% de la vida perdida.",
            "Beasts:\n- Fists: deal 20–30 damage but lose 10 HP.\n- Sword: deals 1–10 damage.\n- Healing: sleeping restores 50% of lost HP.",
            "Bestas:\n- Punhos: causam 20–30 de dano mas perdem 10 HP.\n- Espada: dano 1–10.\n- Cura: dormir recupera 50% da vida perdida.",
            "ビースト:\n- 拳: 20〜30ダメージを与えるが自分に10ダメージ。\n- 剣: ダメージ1〜10。\n- 回復: 眠ると失ったHPの50%回復。");

        Add(localization, "Docs_DistanceHeader",
            "Distancia y turnos",
            "Distance & turns",
            "Distância e turnos",
            "距離とターン");

        Add(localization, "Docs_Distance",
            "• La distancia inicia en 2 (lejos) y 0 significa combate cuerpo a cuerpo.\n• Si la distancia es mayor que 0, solo rifle francotirador o magia de aire pueden atacar.\n• Cada turno puedes atacar, sanar, avanzar o retroceder.",
            "• Distance starts at 2 (far) and 0 means melee.\n• If distance > 0, only sniper rifles or air magic can attack.\n• Each turn you may attack, heal, advance, or retreat.",
            "• A distância começa em 2 (longe) e 0 é corpo a corpo.\n• Se a distância > 0, apenas rifle de precisão ou magia de ar atacam.\n• Em cada turno é possível atacar, curar, avançar ou recuar.",
            "• 距離は2から開始し、0で近接状態。\n• 距離>0のとき攻撃できるのはスナイパー／風魔法のみ。\n• 各ターンで攻撃／回復／前進／後退を選択。");

        Add(localization, "Docs_TurnLogicHeader",
            "Lógica de turnos implementada",
            "Turn logic implementation",
            "Lógica de turnos implementada",
            "ターンロジックの実装");

        Add(localization, "Docs_TurnLogic",
            "• GameService alterna el jugador activo cada turno y evalúa prioridad según la distancia actual.\n• Cada acción valida recursos y aplica multiplicadores de raza/arma antes de resolver el daño o la curación.\n• Efectos residuales (sangrado, curación diferida) se aplican al finalizar el turno y se revisan las condiciones de victoria.",
            "• GameService swaps the active player each turn and evaluates priority based on the current distance.\n• Every action validates resources and applies race/weapon modifiers before resolving damage or healing.\n• Residual effects (bleeding, delayed healing) run at the end of the turn and victory conditions are checked.",
            "• O GameService alterna o jogador ativo a cada turno e calcula a prioridade conforme a distância atual.\n• Toda ação valida recursos e aplica modificadores de raça/arma antes de resolver o dano ou a cura.\n• Efeitos residuais (sangramento, cura adiada) são aplicados ao fim do turno e as condições de vitória são verificadas.",
            "• GameService はターンごとにアクティブプレイヤーを切り替え、現在の距離で優先度を判定します。\n• 各アクションはリソースを検証し、種族／武器の補正を適用してからダメージや回復を解決します。\n• 出血や遅延回復などの継続効果はターン終了時に処理され、勝利条件を確認します。");

        Add(localization, "Docs_DiagramHeader",
            "Diagrama explicativo simple",
            "Simple explanatory diagram",
            "Diagrama explicativo simples",
            "シンプルな解説図");

        Add(localization, "Docs_DiagramDescription",
            "El flujo muestra cómo los jugadores envían sus acciones al motor de turnos y cómo los resultados terminan en la persistencia SQLite.",
            "The flow shows how players send their actions to the turn engine and how the results end up in the SQLite persistence layer.",
            "O fluxo mostra como os jogadores enviam ações para o motor de turnos e como os resultados chegam à persistência em SQLite.",
            "このフローはプレイヤーがターンエンジンへ行動を送り、結果が SQLite の永続層に保存される流れを示しています。");

        Add(localization, "Docs_DiagramPlayerOne",
            "Jugador 1",
            "Player 1",
            "Jogador 1",
            "プレイヤー1");

        Add(localization, "Docs_DiagramPlayerTwo",
            "Jugador 2",
            "Player 2",
            "Jogador 2",
            "プレイヤー2");

        Add(localization, "Docs_DiagramTurnEngine",
            "Motor de turnos (GameService)",
            "Turn engine (GameService)",
            "Motor de turnos (GameService)",
            "ターンエンジン（GameService）");

        Add(localization, "Docs_DiagramDataStore",
            "SQLite / estadísticas persistidas",
            "SQLite / persisted stats",
            "SQLite / estatísticas persistidas",
            "SQLite／統計データ");

        Add(localization, "Docs_VictoryHeader",
            "Condición de victoria",
            "Victory condition",
            "Condição de vitória",
            "勝利条件");

        Add(localization, "Docs_Victory",
            "• Si un jugador llega a 0 HP o menos, el otro gana.\n• Si ambos llegan a 0 HP en el mismo turno, es empate.",
            "• If a player drops to 0 HP or less, the other one wins.\n• If both reach 0 HP on the same turn, it's a draw.",
            "• Se um jogador chega a 0 HP ou menos, o outro vence.\n• Se ambos chegam a 0 HP no mesmo turno, é empate.",
            "• どちらかがHP0以下になれば相手の勝ち。\n• 同一ターンで両者0HPなら引き分け。");

        Add(localization, "Docs_PersistenceHeader",
            "Persistencia de datos",
            "Data persistence",
            "Persistência de dados",
            "データ保存");

        Add(localization, "Docs_Persistence",
            "• Tabla PlayerStats (SQLite) almacena: Id, Wins, Losses, Draws, LastRace, LastWeapon.\n• Tabla MatchResult guarda: Id, CreatedAt, Player1Race, Player2Race, Player1Weapon, Player2Weapon, Winner.\n• Se actualiza automáticamente al finalizar cada combate.",
            "• PlayerStats table (SQLite) stores: Id, Wins, Losses, Draws, LastRace, LastWeapon.\n• MatchResult table keeps: Id, CreatedAt, Player1Race, Player2Race, Player1Weapon, Player2Weapon, Winner.\n• Everything refreshes automatically when a battle concludes.",
            "• A tabela PlayerStats (SQLite) armazena: Id, Wins, Losses, Draws, LastRace, LastWeapon.\n• A tabela MatchResult guarda: Id, CreatedAt, Player1Race, Player2Race, Player1Weapon, Player2Weapon, Winner.\n• Tudo é atualizado automaticamente ao fim de cada combate.",
            "• PlayerStats テーブル（SQLite）には Id, Wins, Losses, Draws, LastRace, LastWeapon を保存。\n• MatchResult テーブルには Id, CreatedAt, Player1Race, Player2Race, Player1Weapon, Player2Weapon, Winner を保存。\n• 対戦終了ごとに自動更新されます。");

        Add(localization, "Docs_ArchitectureHeader",
            "Arquitectura básica",
            "Basic architecture",
            "Arquitetura básica",
            "基本アーキテクチャ");

        Add(localization, "Docs_Architecture",
            "• Models: Race, PlayerCharacter, GameSession, PlayerStats, MatchResult.\n• Services: GameService (lógica de combate) y GameDatabase (SQLite).\n• Pages: menú principal, configuración, combate, resumen, estadísticas, créditos y reglas.",
            "• Models: Race, PlayerCharacter, GameSession, PlayerStats, MatchResult.\n• Services: GameService (battle logic) and GameDatabase (SQLite).\n• Pages: main menu, setup, battle, summary, stats, credits, docs.",
            "• Models: Race, PlayerCharacter, GameSession, PlayerStats, MatchResult.\n• Services: GameService (lógica de combate) e GameDatabase (SQLite).\n• Pages: menu principal, configuração, combate, resumo, estatísticas, créditos e regras.",
            "• モデル: Race, PlayerCharacter, GameSession, PlayerStats, MatchResult。\n• サービス: GameService（戦闘ロジック）、GameDatabase（SQLite）。\n• ページ: メインメニュー、設定、バトル、サマリー、統計、クレジット、ルール。");

        Add(localization, "Docs_DiagnosticsHeader",
            "Diagnóstico y registro de errores",
            "Diagnostics & error log",
            "Diagnóstico e log de erros",
            "診断とエラーログ");

        Add(localization, "Docs_DiagnosticsDescription",
            "La aplicación genera automáticamente un archivo de registro diario dentro del almacenamiento local. Copia la ruta siguiente y envíala cuando necesites reportar un fallo.",
            "The app creates a daily diagnostic log inside the local storage. Copy the path below and share it when reporting an issue.",
            "O aplicativo cria um log de diagnóstico diário no armazenamento local. Copie o caminho abaixo e compartilhe-o ao relatar um problema.",
            "アプリはローカルストレージに日次の診断ログを保存します。問題を報告する際は、以下のパスをコピーして共有してください。");

        Add(localization, "Docs_LogPathHeader",
            "Ruta del archivo actual",
            "Current log file path",
            "Caminho do arquivo atual",
            "現在のログファイルの場所");

        Add(localization, "Docs_CopyLogButton",
            "Copiar ruta del log",
            "Copy log path",
            "Copiar caminho do log",
            "ログのパスをコピー");

        Add(localization, "Docs_HostLogPathHeader",
            "Ruta del log en la PC (depuración)",
            "Host-side log path (debugging)",
            "Caminho do log no PC (depuração)",
            "PC側のログファイル（デバッグ用）");

        Add(localization, "Docs_CopyHostLogButton",
            "Copiar ruta en PC",
            "Copy host log path",
            "Copiar caminho no PC",
            "PC側のパスをコピー");

        Add(localization, "Docs_ShareLogButton",
            "Compartir archivo de log",
            "Share log file",
            "Compartilhar arquivo de log",
            "ログファイルを共有");

        Add(localization, "Docs_ShareLogTitle",
            "Logs de Middlearth Wars",
            "Middlearth Wars logs",
            "Logs do Middlearth Wars",
            "Middlearth Wars ログ");

        Add(localization, "Docs_CopyLogSuccess",
            "Ruta copiada al portapapeles.",
            "Path copied to the clipboard.",
            "Caminho copiado para a área de transferência.",
            "パスをクリップボードにコピーしました。");

        Add(localization, "Docs_CopyLogError",
            "No se pudo copiar la ruta. Consulta el archivo manualmente.",
            "Unable to copy the path. Please check the file manually.",
            "Não foi possível copiar o caminho. Verifique o arquivo manualmente.",
            "パスをコピーできませんでした。手動でファイルを確認してください。");
    }

    private static void RegisterLoadGame(LocalizationService localization)
    {
        Add(localization, "LoadGame_Title",
            "Cargar partida",
            "Load battle",
            "Carregar partida",
            "セーブを読み込む");

        Add(localization, "LoadGame_Header",
            "Selecciona una partida guardada",
            "Select a saved battle",
            "Selecione uma partida salva",
            "セーブデータを選択");

        Add(localization, "LoadGame_Empty",
            "Aún no hay partidas guardadas.",
            "No saved battles yet.",
            "Ainda não há partidas salvas.",
            "セーブデータがまだありません。");

        Add(localization, "LoadGame_Close",
            "Volver",
            "Back",
            "Voltar",
            "戻る");

        Add(localization, "LoadGame_PlayersFormat",
            "{0} vs {1}",
            "{0} vs {1}",
            "{0} vs {1}",
            "{0} vs {1}");

        Add(localization, "LoadGame_TurnFormat",
            "Turno {0}",
            "Turn {0}",
            "Turno {0}",
            "ターン{0}");

        Add(localization, "LoadGame_CreatedAtFormat",
            "Guardado: {0}",
            "Saved: {0}",
            "Salvo: {0}",
            "保存日時: {0}");

        Add(localization, "LoadGame_Confirm",
            "¿Deseas cargar \"{0}\"?",
            "Load \"{0}\"?",
            "Deseja carregar \"{0}\"?",
            "「{0}」を読み込みますか？");

        Add(localization, "LoadGame_Cancel",
            "Volver",
            "Back",
            "Voltar",
            "戻る");

        Add(localization, "LoadGame_Error",
            "No se pudo cargar la partida seleccionada.",
            "The selected battle could not be loaded.",
            "Não foi possível carregar a partida selecionada.",
            "選択したセーブデータを読み込めませんでした。");
    }

    private static void Add(
        LocalizationService localization,
        string key,
        string spanish,
        string english,
        string portuguese,
        string japanese)
    {
        localization.RegisterString(GameLanguage.Spanish, key, spanish);
        localization.RegisterString(GameLanguage.English, key, english);
        localization.RegisterString(GameLanguage.Portuguese, key, portuguese);
        localization.RegisterString(GameLanguage.Japanese, key, japanese);
    }
}
