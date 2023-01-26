namespace YukiChan.Server.Utils;

public static class YukiServerDir
{
    // Global
    public static readonly string Root = AppDomain.CurrentDomain.BaseDirectory;
    public static readonly string Assets = Path.Combine(Root, "assets");
    public static readonly string Cache = Path.Combine(Root, "cache");
    public static readonly string Configs = Path.Combine(Root, "configs");
    public static readonly string Logs = Path.Combine(Root, "logs");
    public static readonly string Data = Path.Combine(Root, "data");
    public static readonly string Databases = Path.Combine(Root, "databases");

    public static readonly string ArcaeaAssets = Path.Combine(Assets, "arcaea");
    public static readonly string ArcaeaCache = Path.Combine(Cache, "arcaea");
    public static readonly string ArcaeaData = Path.Combine(Data, "arcaea");

    public static void CreateIfNotExists(string path)
    {
        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);
    }

    static YukiServerDir()
    {
        var dirs = new[]
        {
            Assets, Cache, Configs, Logs, Data, Databases,

            // Arcaea
            $"{ArcaeaAssets}/images", $"{ArcaeaData}/best30",
            $"{ArcaeaCache}/song", $"{ArcaeaCache}/char", $"{ArcaeaCache}/preview"
        };

        foreach (var dir in dirs)
            CreateIfNotExists(dir);
    }
}