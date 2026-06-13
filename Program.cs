using Raylib_cs;
using System.Numerics;

internal static class Program {

    private const int windowWidth = 800;
    private const int windowHeight = 480;

    public static void Main() {
        Raylib.InitWindow(windowWidth, windowHeight, "Test Program");
        Raylib.InitAudioDevice();

        Raylib.SetTargetFPS(120);

        Camera3D camera;
        camera.Position = new Vector3(10, 10, 10);
        camera.Target = new Vector3(0, 0, 0);
        camera.Up = new Vector3(0, 1, 0);
        camera.FovY = 60;
        camera.Projection = CameraProjection.Perspective;

        Vector3 cubePosition = new(0, 0, 0);

        Model model = Raylib.LoadModel("resources\\models\\scp173.glb");
        Texture2D texture = Raylib.LoadTexture("resources\\textures\\scp173.png");

        Raylib.SetMaterialTexture(ref model, 0, MaterialMapIndex.Diffuse, ref texture);

        // Main loop
        while (!Raylib.WindowShouldClose()) {
            Raylib.UpdateCamera(ref camera, CameraMode.Free);

            if (Raylib.IsKeyPressed(KeyboardKey.Z)) camera.Target = new Vector3(0, 0, 0);
            if (Raylib.IsKeyPressed(KeyboardKey.F11)) Raylib.ToggleFullscreen();

            Raylib.DisableCursor();

            // Draw
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.Black);

            Raylib.BeginMode3D(camera);

            Raylib.DrawGrid(20, 0.5f);

            Raylib.DrawLine3D(cubePosition, new Vector3(cubePosition.X + 2, cubePosition.Y, cubePosition.Z), Color.Red);
            Raylib.DrawLine3D(cubePosition, new Vector3(cubePosition.X, cubePosition.Y + 2, cubePosition.Z), Color.Green);
            Raylib.DrawLine3D(cubePosition, new Vector3(cubePosition.X, cubePosition.Y, cubePosition.Z + 2), Color.Blue);

            Raylib.DrawModel(model, cubePosition, 1, Color.White);

            Raylib.EndMode3D();

            Raylib.DrawText("Running the super cool rendering engine by ThatJamGuy", 10, 40, 15, Color.White);
            Raylib.DrawFPS(10, 10);

            Raylib.EndDrawing();
        }

        // De-initialize all the things
        Raylib.UnloadModel(model);
        Raylib.UnloadTexture(texture);
        Raylib.CloseAudioDevice();
        Raylib.CloseWindow();
    }
}