using Serilog;

namespace Emiplus.Data.Helpers
{
    public static class Logs
    {
        public static void Add(string classe, string texto)
        {
            Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .WriteTo.Console()
            .WriteTo.File("Logs//" + classe + ".txt")
            .CreateLogger();

            Log.Information(texto);

            Log.CloseAndFlush();
        }
    }
}
