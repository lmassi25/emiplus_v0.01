using System;
using System.IO;
using Emiplus.Data.Database;
using Emiplus.Data.Helpers;
using FirebirdSql.Data.Services;
using RestSharp;

namespace Emiplus.Data.Core
{
    internal class BackupAutomatico
    {
        private const string _user = "sysdba";
        private const string _pass = "masterkey";
        private const string _db = "sysdba";
        private const string _host = "localhost";

        private readonly string Path = IniFile.Read("Path", "LOCAL");
        private readonly string PathDB = IniFile.Read("PathDatabase", "LOCAL");

        public BackupAutomatico StartBackupCupom()
        {
            var filePath = $"{Path}\\CFe\\Autorizadas\\bkp";

            if (Directory.Exists(filePath))
            {
                var di = new DirectoryInfo(filePath);
                foreach (var file in di.GetFiles("*.xml"))
                {
                    object obj = new
                    {
                        token = Program.TOKEN,
                        id_empresa = IniFile.Read("idEmpresa", "APP"),
                        id_backup = "N9TT-9G0A-B7FQ-RANC"
                    };

                    var jo = new RequestApi().URL(Program.URL_BASE + "/api/backup/cupom").Content(obj, Method.POST)
                        .AddFile("arquivo", file.FullName).Response();

                    if (jo["error"]?.ToString() == "False")
                    {
                        new Log().Add("BACKUPS", "[CUPOM] Backup realizado com sucesso", Log.LogType.info);
                        file.Delete();
                    }
                    else
                    {
                        new Log().Add("BACKUPS", "[CUPOM] Falha no backup", Log.LogType.info);
                    }
                }
            }

            return this;
        }

        public BackupAutomatico StartBackup()
        {
            var dateNow = DateTime.Now.ToString("dd-MM-yyyy");
            var filePath = $"{PathDB}\\{dateNow}.fbk";

            var isNumeric = int.TryParse(PathDB.Substring(0, 1), out var n);
            if (isNumeric)
                return this;

            if (!File.Exists(filePath))
            {
                var backupSvc = new FbBackup
                {
                    ConnectionString = $"character set=NONE;initial catalog={new Connect().GetDatabase()};user id={_user};data source={_host};user id={_db};Password={_pass};Pooling=true;Dialect=3"
                };

                backupSvc.BackupFiles.Add(new FbBackupFile(filePath, 8192));
                backupSvc.Verbose = true;
                backupSvc.Options = FbBackupFlags.IgnoreLimbo;
                backupSvc.Execute();
            }

            object obj = new
            {
                token = Program.TOKEN,
                id_empresa = IniFile.Read("idEmpresa", "APP"),
                id_backup = "N9TT-9G0A-B7FQ-RANC"
            };

            var jo = new RequestApi().URL(Program.URL_BASE + "/api/backup").Content(obj, Method.POST)
                .AddFile("arquivo", filePath).Response();

            if (jo == null)
            {
                new Log().Add("BACKUPS", "[DB] Falha no backup", Log.LogType.info);
            }
            else if (jo["error"]?.ToString() == "False")
            {
                new Log().Add("BACKUPS", "[DB] Backup realizado com sucesso", Log.LogType.info);
                CleanBackups(PathDB);
            }
            else
            {
                new Log().Add("BACKUPS", "[DB] Falha no backup", Log.LogType.info);
            }

            return this;
        }

        private void CleanBackups(string path, int days = 1)
        {
            var date = DateTime.Now.AddDays(-days);

            var di = new DirectoryInfo(path);
            foreach (var file in di.GetFiles("*.fbk"))
                if (date >= file.CreationTime)
                    file.Delete();
        }

        public void BackupLocalDocuments()
        {
            var pathDocuments = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            if (!Directory.Exists(pathDocuments + "\\Emiplus"))
                Directory.CreateDirectory(pathDocuments + "\\Emiplus");

            var isNumeric = int.TryParse(PathDB.Substring(0, 1), out var n);
            if (isNumeric)
                return;

            if (Directory.Exists(pathDocuments + "\\Emiplus"))
            {
                var dateNow = DateTime.Now.ToString("dd-MM-yyyy");

                if (!File.Exists(pathDocuments + $"\\Emiplus\\EMIPLUS-{dateNow}.fbk"))
                {
                    var backupSvc = new FbBackup
                    {
                        ConnectionString = $"character set=NONE;initial catalog={new Connect().GetDatabase()};user id={_user};data source={_host};user id={_db};Password={_pass};Pooling=true;Dialect=3"
                    };

                    backupSvc.BackupFiles.Add(new FbBackupFile(pathDocuments + $"\\Emiplus\\EMIPLUS-{dateNow}.fbk",
                        8192));
                    backupSvc.Verbose = true;
                    backupSvc.Options = FbBackupFlags.IgnoreLimbo;
                    backupSvc.Execute();
                }

                CleanBackups(pathDocuments + "\\Emiplus", 5);
            }
        }
    }
}