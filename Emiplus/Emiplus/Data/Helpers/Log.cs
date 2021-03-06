﻿using Serilog;

namespace Emiplus.Data.Helpers
{
    public class Log
    {
        public enum LogType
        {
            info,
            warning,
            error,
            fatal
        }

        public void Add(string classe, string mensagem, LogType type)
        {
            Serilog.Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.File(
                    "Logs/" + classe + ".txt",
                    outputTemplate: "[{Timestamp:dd/MM/yyyy HH:mm:ss} {Level}] {Message:lj}{NewLine}{Exception}"
                )
                .CreateLogger();

            switch (type)
            {
                case LogType.info:
                    Serilog.Log.Information(mensagem);
                    break;

                case LogType.warning:
                    Serilog.Log.Warning(mensagem);
                    break;

                case LogType.error:
                    Serilog.Log.Error(mensagem);
                    break;

                case LogType.fatal:
                    Serilog.Log.Fatal(mensagem);
                    break;
            }

            Serilog.Log.CloseAndFlush();
        }
    }
}