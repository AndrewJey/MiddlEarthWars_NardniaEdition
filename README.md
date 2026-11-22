# Middearth Wars (Android con .NET MAUI)

Este repositorio contiene un único proyecto **.NET MAUI** pensado exclusivamente para Android. El proyecto real vive en `CombatFantasyGame/CombatFantasyGame.csproj` y ya incluye todo el XAML, imágenes y código necesarios para ejecutar el videojuego de combate por turnos Middearth Wars en un teléfono o emulador Android.

## Requisitos previos

- **.NET 9 SDK** (o la versión indicada en `global.json` si se añade en el futuro).
- Workload de **.NET MAUI para Android** (`dotnet workload install maui`) y los complementos de Visual Studio 2022 17.10+ si prefieres usar IDE.
- Android SDK + emulador o un dispositivo físico con modo desarrollador y depuración USB habilitada.
- (Opcional) `adb` del Android Platform Tools para instalar el APK desde la terminal.

## Flujo de compilación y publicación

```bash
# Restaurar workloads y paquetes una sola vez
dotnet workload restore
dotnet restore CombatFantasyGame/CombatFantasyGame.csproj

# Compilación Debug para pruebas rápidas
dotnet build CombatFantasyGame/CombatFantasyGame.csproj -f net9.0-android

# Publicación Release y generación del APK firmado con la clave de desarrollo
dotnet publish CombatFantasyGame/CombatFantasyGame.csproj -f net9.0-android -c Release
```

El APK final queda en `CombatFantasyGame/bin/Release/net9.0-android/publish/*-Signed.apk`. Copia ese archivo a tu dispositivo y ejecútalo, o bien instala con `adb install <apk>` si está conectado por USB.

## Ejecución en un emulador o dispositivo

1. Abre un emulador Android desde Android Studio o Visual Studio, o bien conecta tu teléfono y acepta la depuración USB.
2. Ejecuta `dotnet build` o `dotnet publish` con la opción `-t:Run` para lanzar la app directamente, por ejemplo:<br>`dotnet build CombatFantasyGame/CombatFantasyGame.csproj -f net9.0-android -t:Run`
3. Si prefieres el APK, una vez generado cópialo y ábrelo en el dispositivo; acepta “instalar desde orígenes desconocidos” si se solicita.

## Logo de la app

El ícono mostrado en el lanzador de Android y en la pantalla de bienvenida se genera a partir de los archivos dentro de `CombatFantasyGame/Resources/AppIcon/`. Sustituye `appicon.svg` y `appiconfg.svg` por tu propio logo (mismas rutas y nombres) o actualiza el `<MauiIcon>` del `.csproj` para apuntar a nuevos recursos. El mismo logo se utilizará automáticamente para el icono instalado y demás tamaños generados por MAUI.

## Archivos importantes

- `FantasyCombat.sln`: solución que ya apunta al proyecto MAUI real.
- `CombatFantasyGame/CombatFantasyGame.csproj`: configuración Android y dependencias (`sqlite-net-pcl`, `Microsoft.Maui.Controls`).
- `CombatFantasyGame/Platforms/Android/`: `AndroidManifest.xml`, `MainActivity` y recursos específicos de Android.
- `CombatFantasyGame/README.md`: documentación de reglas de juego, pantallas y detalles funcionales.

Con esta estructura, basta abrir la solución en Visual Studio o usar `dotnet build`/`dotnet publish` para obtener un binario Android listo para instalar.
