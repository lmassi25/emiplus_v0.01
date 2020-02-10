using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using System.Diagnostics;
using System.IO;
using Emiplus.Data.Helpers;
using FirebirdSql.Data.Services;
using Emiplus.Data.Database;
using RestSharp;

namespace Emiplus.Data.Core
{
    class BackupAutomatico
    {
        private const string _user = "sysdba";
        private const string _pass = "masterkey";
        private const string _db = "sysdba";
        private const string _host = "localhost";

        private string Path = IniFile.Read("Path", "LOCAL");
        private string PathDB = IniFile.Read("PathDatabase", "LOCAL");
        
        public BackupAutomatico StartBackupCupom()
        {
            string filePath = $"{Path}\\CFe\\Autorizadas\\bkp";

            if (Directory.Exists(filePath))
            {
                System.IO.DirectoryInfo di = new DirectoryInfo(filePath);
                foreach (FileInfo file in di.GetFiles("*.xml"))
                {
                    object obj = new
                    {
                        token = Program.TOKEN,
                        id_empresa = IniFile.Read("idEmpresa", "APP"),
                        id_backup = "N9TT-9G0A-B7FQ-RANC"
                    };
                    
                    var jo = new RequestApi().URL(Program.URL_BASE + "/backup/cupom").Content(obj, Method.POST).AddFile("arquivo", file.FullName).Response();
                    
                    if (jo["error"].ToString() == "False")
                    {
                        new Log().Add("BACKUPS", "[CUPOM] Backup realizado com sucesso", Log.LogType.info);
                        file.Delete();
                    }
                    else
                        new Log().Add("BACKUPS", "[CUPOM] Falha no backup", Log.LogType.info);

                }
            }

            return this;
        }

        public BackupAutomatico StartBackup()
        {
            string dateNow = DateTime.Now.ToString("dd-MM-yyyy");
            string filePath = $"{PathDB}\\{dateNow}.fbk";

            if (Validation.ConvertToInt32(PathDB.Substring(0, 1)) > 0)
                return this;

            if (!File.Exists(filePath)) {
                FbBackup backupSvc = new FbBackup();
                backupSvc.ConnectionString = $"character set=NONE;initial catalog={new Connect().GetDatabase()};user id={_user};data source={_host};user id={_db};Password={_pass};Pooling=true;Dialect=3";

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
            
            var jo = new RequestApi().URL(Program.URL_BASE + "/api/backup").Content(obj, Method.POST).AddFile("arquivo", filePath).Response();
            
            if (jo["error"].ToString() == "False")
            {
                new Log().Add("BACKUPS", "[DB] Backup realizado com sucesso", Log.LogType.info);
                CleanBackups(PathDB);
            }
            else
                new Log().Add("BACKUPS", "[DB] Falha no backup", Log.LogType.info);

            return this;
        }

        private void CleanBackups(string path, int days = 1)
        {
            DateTime date = DateTime.Now.AddDays(-days);
            
            System.IO.DirectoryInfo di = new DirectoryInfo(path);
            foreach (FileInfo file in di.GetFiles("*.fbk"))
            {
                if (date >= file.CreationTime)
                    file.Delete();
            }
        }

        public void BackupLocalDocuments()
        {
            string pathDocuments = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            if (!Directory.Exists(pathDocuments + "\\Emiplus"))
                Directory.CreateDirectory(pathDocuments + "\\Emiplus");

            if (Directory.Exists(pathDocuments + "\\Emiplus"))
            {
                string dateNow = DateTime.Now.ToString("dd-MM-yyyy");

                if (!File.Exists(pathDocuments + $"\\Emiplus\\EMIPLUS-{dateNow}.fbk"))
                {
                    FbBackup backupSvc = new FbBackup();
                    backupSvc.ConnectionString = $"character set=NONE;initial catalog={new Connect().GetDatabase()};user id={_user};data source={_host};user id={_db};Password={_pass};Pooling=true;Dialect=3";

                    backupSvc.BackupFiles.Add(new FbBackupFile(pathDocuments + $"\\Emiplus\\EMIPLUS-{dateNow}.fbk", 8192));
                    backupSvc.Verbose = true;
                    backupSvc.Options = FbBackupFlags.IgnoreLimbo;
                    backupSvc.Execute();
                }

                CleanBackups(pathDocuments + $"\\Emiplus", 5);
            }
        }
    }
}
