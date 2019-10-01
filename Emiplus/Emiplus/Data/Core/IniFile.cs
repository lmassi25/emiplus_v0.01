using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace Emiplus.Data.Core
{
    class IniFile
    {
        //private static string Path;
        private static string EXE = Assembly.GetExecutingAssembly().GetName().Name;

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        static extern long WritePrivateProfileString(string Section, string Key, string Value, string FilePath);

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        static extern int GetPrivateProfileString(string Section, string Key, string Default, StringBuilder RetVal, int Size, string FilePath);

        private static string GetPath()
        {
            if (File.Exists("C:\\emiplus_v0.01\\Emiplus\\Emiplus\\Data\\Core\\Config.ini"))
                return "C:\\emiplus_v0.01\\Emiplus\\Emiplus\\Data\\Core\\Config.ini";

            if (File.Exists(Directory.GetCurrentDirectory() + "\\Config.ini"))
                return Directory.GetCurrentDirectory() + "\\Config.ini";

            return Directory.GetCurrentDirectory() + "\\Config.ini";
        }

        /// <summary>
        /// Leitura do arquivo config.ini
        /// </summary>
        /// <param name="Key">Key para recuperar o valor</param>
        /// <param name="Section">Seção da Key</param>
        public static string Read(string Key, string Section = null)
        {
            var RetVal = new StringBuilder(255);
            GetPrivateProfileString(Section ?? EXE, Key, "", RetVal, 255, GetPath());
            return RetVal.ToString();
        }

        /// <summary>
        /// Escreve no arquivo config.ini
        /// </summary>
        /// <param name="Key">Key de Config</param>
        /// <param name="Value">Valor da Key</param>
        /// <param name="Section">Seção para adicionar a Key</param>
        public static void Write(string Key, string Value, string Section = null)
        {
            WritePrivateProfileString(Section ?? EXE, Key, Value, GetPath());
        }

        /// <summary>
        /// Deletar Key
        /// </summary>
        /// <param name="Key">Key para deletar</param>
        /// <param name="Section">Seção da Key</param>
        public static void DeleteKey(string Key, string Section = null)
        {
            Write(Key, null, Section ?? EXE);
        }

        /// <summary>
        /// Deletar uma [seção] e todas keys filhas
        /// </summary>
        public static void DeleteSection(string Section = null)
        {
            Write(null, null, Section ?? EXE);
        }

        /// <summary>
        /// Validar se a KEY existe no Config.ini
        /// </summary>
        public static bool KeyExists(string Key, string Section = null)
        {
            return Read(Key, Section).Length > 0;
        }
    }
}