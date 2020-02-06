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

namespace Emiplus.Data.Core
{
    class BackupAutomatico
    {
        private const string _user = "sysdba";
        private const string _pass = "masterkey";
        private const string _db = "sysdba";
        private const string _host = "localhost";

        private string ServiceFireBird = "FirebirdServerDefaultInstance";
        private string PathFirebird { get; set; }
        private string PathDB = IniFile.Read("PathDatabase", "LOCAL");

        public BackupAutomatico()
        {
            using (ManagementObject wmiService = new ManagementObject("Win32_Service.Name='" + ServiceFireBird + "'"))
            {
                wmiService.Get();
                string currentserviceExePath = wmiService["PathName"].ToString();

                int pFrom = currentserviceExePath.IndexOf("\"") + "\"".Length;
                int pTo = currentserviceExePath.LastIndexOf("\"");

                PathFirebird = currentserviceExePath.Substring(pFrom, pTo - pFrom).Replace("firebird.exe", "");
            }
        }
        
        public BackupAutomatico StartBackup()
        {
            string dateNow = DateTime.Now.ToString("dd-MM-yyyy");
            if (!File.Exists($"{PathDB}\\BACKUP-{dateNow}.fbk")) {
                FbBackup backupSvc = new FbBackup();
                backupSvc.ConnectionString = $"character set=NONE;initial catalog={new Connect().GetDatabase()};user id={_user};data source={_host};user id={_db};Password={_pass};Pooling=true;Dialect=3";

                string filePath = $"{PathDB}\\BACKUP-{dateNow}.fbk";
                backupSvc.BackupFiles.Add(new FbBackupFile(filePath, 8192));
                backupSvc.Verbose = true;
                backupSvc.Options = FbBackupFlags.IgnoreLimbo;
                backupSvc.Execute();
            }

            return this;
        }

        public void BackupLocalDocuments()
        {
            string pathDocuments = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            if (!Directory.Exists(pathDocuments + "\\Emiplus"))
                Directory.CreateDirectory(pathDocuments + "\\Emiplus");

            if (Directory.Exists(pathDocuments + "\\Emiplus"))
            {
                string dateNow = DateTime.Now.ToString("dd-MM-yyyy");
                if (File.Exists(pathDocuments + $"\\Emiplus\\EMIPLUS-{dateNow}.FDB"))
                    return;
                else
                {
                    string getDB = IniFile.Read("PathDatabase", "LOCAL");
                    if (File.Exists(getDB + $"\\EMIPLUS.FDB"))
                    {
                        if (File.Exists(pathDocuments + $"\\Emiplus\\EMIPLUS-{dateNow}.FDB"))
                            return;
                        else
                        {
                            File.Copy(getDB + "\\EMIPLUS.FDB", pathDocuments + $"\\Emiplus\\EMIPLUS-{dateNow}.FDB");
                        }
                    }
                }

                foreach (var file in Directory.GetFiles(pathDocuments + $"\\Emiplus"))
                {
                    string data = file.Replace(pathDocuments + "\\Emiplus\\EMIPLUS-", "").Replace(".FDB", "");
                    string dateOld = DateTime.Now.AddDays(-7).ToString("dd-MM-yyyy");

                    if (dateOld == data)
                        File.Delete(file);
                }

            }
        }
    }
}
