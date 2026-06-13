using Raylib_cs;
using System.Numerics;

internal static class Program {
    public const string versionNumber = "0.0.1";
    private const int windowWidth = 900;
    private const int windowHeight = 580;

    public static readonly Vector3 WORLD_ORIGIN = new Vector3(0, 0, 0);

    private static Camera3D camera;

    public static void Main() {
        bool showDebug3D = false;
        bool showDebug2D = false;

        // Initialize all the things
        Raylib.InitWindow(windowWidth, windowHeight, "Revival Engine v" + versionNumber);
        Raylib.InitAudioDevice();
        //Raylib.SetTargetFPS(Raylib.GetMonitorRefreshRate(Raylib.GetCurrentMonitor()));
        Raylib.DisableCursor();

        // Set up the freecamera. If I add first person support later this will need to be looked at again
        camera.Position = new Vector3(10, 10, 10);
        camera.Target = new Vector3(0, 0, 0);
        camera.Up = new Vector3(0, 1, 0);
        camera.FovY = 60;
        camera.Projection = CameraProjection.Perspective;

        // Load the test model and texture, then apply the texture to the model's material
        Model model = Raylib.LoadModel("resources\\models\\scp173.glb");
        Model room173 = Raylib.LoadModel("resources/models/room173.glb");

        Texture2D texture173 = Raylib.LoadTexture("resources\\textures\\scp173.png");
        Texture2D texture173Normal = Raylib.LoadTexture("resources\\textures\\scp173_normal.png");
        Texture2D concreteFloor = Raylib.LoadTexture("resources\\textures\\concretefloor.png");
        Texture2D dirtyMetal = Raylib.LoadTexture("resources\\textures\\dirtymetal2.png");
        //Texture2D whiteWall = Raylib.LoadTexture("resources\\textures\\whitewall.png");
        Texture2D white = Raylib.LoadTexture("resources\\textures\\white.png");
        Texture2D metal = Raylib.LoadTexture("resources\\textures\\metal.png");
        Texture2D label173 = Raylib.LoadTexture("resources\\textures\\label_173.png");
        Texture2D containmentDoors = Raylib.LoadTexture("resources\\textures\\containment_doors.png");
        Texture2D vent = Raylib.LoadTexture("resources\\textures\\vent.png");
        Texture2D misc = Raylib.LoadTexture("resources\\textures\\misc.png");
        Texture2D door01 = Raylib.LoadTexture("resources\\textures\\door01.png");
        Texture2D glass = Raylib.LoadTexture("resources\\textures\\glass.png");
            
        // Set up mip-maps and texture filtering
        Raylib.GenTextureMipmaps(ref texture173);
        Raylib.GenTextureMipmaps(ref texture173Normal);
        Raylib.GenTextureMipmaps(ref white);
        Raylib.GenTextureMipmaps(ref concreteFloor);
        Raylib.GenTextureMipmaps(ref dirtyMetal);
        Raylib.GenTextureMipmaps(ref label173);
        Raylib.GenTextureMipmaps(ref containmentDoors);
        Raylib.GenTextureMipmaps(ref vent);
        Raylib.GenTextureMipmaps(ref misc);
        Raylib.GenTextureMipmaps(ref door01);
        Raylib.GenTextureMipmaps(ref glass);

        Raylib.SetTextureFilter(texture173, TextureFilter.Bilinear);
        Raylib.SetTextureFilter(texture173Normal, TextureFilter.Bilinear);
        Raylib.SetTextureFilter(white, TextureFilter.Bilinear);
        Raylib.SetTextureFilter(concreteFloor, TextureFilter.Bilinear);
        Raylib.SetTextureFilter(dirtyMetal, TextureFilter.Bilinear);
        Raylib.SetTextureFilter(label173, TextureFilter.Bilinear);
        Raylib.SetTextureFilter(containmentDoors, TextureFilter.Bilinear);
        Raylib.SetTextureFilter(vent, TextureFilter.Bilinear);
        Raylib.SetTextureFilter(misc, TextureFilter.Bilinear);
        Raylib.SetTextureFilter(door01, TextureFilter.Bilinear);
        Raylib.SetTextureFilter(glass, TextureFilter.Bilinear);

        // SCP-173 Model
        Raylib.SetMaterialTexture(ref model, 0, MaterialMapIndex.Diffuse, ref texture173);
        Raylib.SetMaterialTexture(ref model, 0, MaterialMapIndex.Normal, ref texture173Normal);

        // Room173 Model
        Raylib.SetMaterialTexture(ref room173, 6, MaterialMapIndex.Diffuse, ref white);
        Raylib.SetMaterialTexture(ref room173, 7, MaterialMapIndex.Diffuse, ref vent);
        Raylib.SetMaterialTexture(ref room173, 3, MaterialMapIndex.Diffuse, ref concreteFloor);
        Raylib.SetMaterialTexture(ref room173, 4, MaterialMapIndex.Diffuse, ref dirtyMetal);
        Raylib.SetMaterialTexture(ref room173, 5, MaterialMapIndex.Diffuse, ref misc);
        Raylib.SetMaterialTexture(ref room173, 8, MaterialMapIndex.Diffuse, ref metal);
        Raylib.SetMaterialTexture(ref room173, 12, MaterialMapIndex.Diffuse, ref label173);
        Raylib.SetMaterialTexture(ref room173, 11, MaterialMapIndex.Diffuse, ref door01);
        Raylib.SetMaterialTexture(ref room173, 2, MaterialMapIndex.Diffuse, ref containmentDoors);
        Raylib.SetMaterialTexture(ref room173, 1, MaterialMapIndex.Diffuse, ref glass);

        // Main game loop loop
        while (!Raylib.WindowShouldClose()) {
            Raylib.UpdateCamera(ref camera, CameraMode.Free);

            // Check for some inputs
            if (Raylib.IsKeyPressed(KeyboardKey.Z)) camera.Target = WORLD_ORIGIN;
            if (Raylib.IsKeyPressed(KeyboardKey.F1)) { showDebug2D = !showDebug2D; showDebug3D= !showDebug3D; }
            if (Raylib.IsKeyPressed(KeyboardKey.F11)) Raylib.ToggleBorderlessWindowed();

            // Draw 3D and 2D things, order of operations matters
            Raylib.BeginDrawing();

            Raylib.ClearBackground(Color.Black);

            Raylib.BeginMode3D(camera);

            Raylib.DrawModel(room173, WORLD_ORIGIN, 1, Color.White);
            Raylib.DrawModel(model, WORLD_ORIGIN, 1.3f, Color.White);

            if (showDebug3D) ShowDebug3D();

            Raylib.EndMode3D();

            if (showDebug2D) ShowDebug2D();

            Raylib.DrawFPS(10, 10);

            Raylib.EndDrawing();
        }

        // Unload models
        Raylib.UnloadModel(model);
        Raylib.UnloadModel(room173);

        // Unload textures
        Raylib.UnloadTexture(texture173);
        Raylib.UnloadTexture(texture173Normal);
        Raylib.UnloadTexture(concreteFloor);
        Raylib.UnloadTexture(dirtyMetal);
        //Raylib.UnloadTexture(whiteWall);
        Raylib.UnloadTexture(white);
        Raylib.UnloadTexture(metal);
        Raylib.UnloadTexture(label173);
        Raylib.UnloadTexture(containmentDoors);
        Raylib.UnloadTexture(vent);
        Raylib.UnloadTexture(misc);
        Raylib.UnloadTexture(door01);
        Raylib.UnloadTexture(glass);

        Raylib.CloseAudioDevice();
        Raylib.CloseWindow();
    }

    private static void ShowDebug3D() {
        // Small grid
        Raylib.DrawGrid(40, 1);

        // XYZ Gizmos
        Raylib.DrawLine3D(WORLD_ORIGIN, new Vector3(WORLD_ORIGIN.X + 2, WORLD_ORIGIN.Y, WORLD_ORIGIN.Z), Color.Red);
        Raylib.DrawLine3D(WORLD_ORIGIN, new Vector3(WORLD_ORIGIN.X, WORLD_ORIGIN.Y + 2, WORLD_ORIGIN.Z), Color.Green);
        Raylib.DrawLine3D(WORLD_ORIGIN, new Vector3(WORLD_ORIGIN.X, WORLD_ORIGIN.Y, WORLD_ORIGIN.Z + 2), Color.Blue);
    }

    private static void ShowDebug2D() {
        // Realtime player position
        Raylib.DrawText("Player pos: " + MathF.Round(camera.Position.X, 2) +
            ", " + MathF.Round(camera.Position.Y, 2) +
            ", " + MathF.Round(camera.Position.Z, 2),
            10, 40, 14, Color.White);
    }
}