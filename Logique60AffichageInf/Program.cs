using Raylib_cs;

namespace Logique60AffichageInf;

class Program
{
    private const float PAS_LOGIQUE = 1.0f / 60.0f; // 60Hz
    private static int counter = 0; // Compteur de clics TEMP
    private static bool clickDetected = false; // Variable pour savoir si le clic a été détecté

    static void Main(string[] args)
    {
        Raylib.InitWindow(800, 600, "Logique 60Hz / Affichage inf");
        Raylib.SetConfigFlags(ConfigFlags.BorderlessWindowMode);
        Raylib.SetConfigFlags(ConfigFlags.Msaa4xHint);
        Raylib.SetConfigFlags(ConfigFlags.VSyncHint);
        Raylib.SetTargetFPS(1000); // FPS peut être illimité mais faud limiter pour éviter de faire trop de calculs inutiles et après kaboom

        float timerLogique = 0.0f;

        while (!Raylib.WindowShouldClose())
        {
            timerLogique += Raylib.GetFrameTime();

            // Vérifier si le clic gauche de la souris est détecté parce que si je le fais dans update, il est détecté une fois sur beaucoup,
            if (Raylib.IsMouseButtonPressed(MouseButton.Left))
            {
                clickDetected = true;
            }
            
            // Mettre à jour la logique à 60Hz
            while (timerLogique >= PAS_LOGIQUE)
            {
                UpdateLogic();
                timerLogique -= PAS_LOGIQUE;
            }

            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.DarkGray);
            Draw();
            Raylib.EndDrawing();
        }

        
        Raylib.CloseWindow();
    }

    static void UpdateLogic()
    {
        if (clickDetected)
        {
            counter++;
            clickDetected = false; // Consommer le clic
        }
    }

    static void Draw()
    {
        Raylib.DrawFPS(300,10);
        Raylib.DrawText($"Compteur clics : {counter}", 10, 10, 20, Color.Red);
    }
}