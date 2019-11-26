using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Emiplus.Controller
{
    public class Sat
    {
        public const string DLL = "C:\\Emiplus\\SAT.dll";

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr ConsultarStatusOperacional(int sessao, string cod);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr EnviarDadosVenda(int sessao, string cod, string dados);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr CancelarUltimaVenda(int sessao, string cod, string chave, string dadoscancel);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr TesteFimAFim(int sessao, string cod, string dados);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr ConsultarSAT(int sessao);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr ConsultarNumeroSessao(int sessao, string cod, int sessao_a_ser_consultada);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr AtivarSAT(int sessao, int tipoCert, string cod_Ativacao, string cnpj, int uf);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr ComunicarCertificadoICPBRASIL(int sessao, string cod, string csr);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr ConfigurarInterfaceDeRede(int sessao, string cod, string xmlConfig);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr AssociarAssinatura(int sessao, string cod, string cnpj, string sign_cnpj);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr DesbloquearSAT(int sessao, string cod_ativacao);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr BloquearSAT(int sessao, string cod_ativacao);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr TrocarCodigoDeAtivacao(int sessao, string cod_ativacao, int opcao, string nova_senha, string conf_senha);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr ExtrairLogs(int sessao, string cod_ativacao);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr AtualizarSoftwareSAT(int sessao, string cod_ativacao);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall)]
        
        public static extern String Base64ToAscii(string dados);

        public static string StringFromNativeUtf8(IntPtr nativeUtf8)
        {
            try
            {
                int len = 0;
                while (Marshal.ReadByte(nativeUtf8, len) != 0) ++len;
                byte[] buffer = new byte[len];
                Marshal.Copy(nativeUtf8, buffer, 0, buffer.Length);

                return Encoding.UTF8.GetString(buffer);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
