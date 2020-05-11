using System;
using System.Runtime.InteropServices;
using System.Text;
using static System.Runtime.InteropServices.CallingConvention;

namespace Emiplus.Controller
{
    public class Sat
    {
        public const string Dll = @"C:\Emiplus\SAT.dll";

        [DllImport(Dll, CallingConvention = StdCall, CharSet = CharSet.Unicode)]
        public static extern IntPtr ConsultarStatusOperacional(int sessao, string cod);

        [DllImport(Dll, CallingConvention = StdCall, CharSet = CharSet.Unicode)]
        public static extern IntPtr EnviarDadosVenda(int sessao, string cod, string dados);

        [DllImport(Dll, CallingConvention = StdCall, CharSet = CharSet.Unicode)]
        public static extern IntPtr CancelarUltimaVenda(int sessao, string cod, string chave, string dadoscancel);

        [DllImport(Dll, CallingConvention = StdCall, CharSet = CharSet.Unicode)]
        public static extern IntPtr TesteFimAFim(int sessao, string cod, string dados);

        [DllImport(Dll, CallingConvention = StdCall)]
        public static extern IntPtr ConsultarSAT(int sessao);

        [DllImport(Dll, CallingConvention = StdCall, CharSet = CharSet.Unicode)]
        public static extern IntPtr ConsultarNumeroSessao(int sessao, string cod, int sessao_a_ser_consultada);

        [DllImport(Dll, CallingConvention = StdCall, CharSet = CharSet.Unicode)]
        public static extern IntPtr AtivarSAT(int sessao, int tipoCert, string cod_Ativacao, string cnpj, int uf);

        [DllImport(Dll, CallingConvention = StdCall, CharSet = CharSet.Unicode)]
        public static extern IntPtr ComunicarCertificadoICPBRASIL(int sessao, string cod, string csr);

        [DllImport(Dll, CallingConvention = StdCall, CharSet = CharSet.Unicode)]
        public static extern IntPtr ConfigurarInterfaceDeRede(int sessao, string cod, string xmlConfig);

        [DllImport(Dll, CallingConvention = StdCall, CharSet = CharSet.Unicode)]
        public static extern IntPtr AssociarAssinatura(int sessao, string cod, string cnpj, string sign_cnpj);

        [DllImport(Dll, CallingConvention = StdCall, CharSet = CharSet.Unicode)]
        public static extern IntPtr DesbloquearSAT(int sessao, string cod_ativacao);

        [DllImport(Dll, CallingConvention = StdCall, CharSet = CharSet.Unicode)]
        public static extern IntPtr BloquearSAT(int sessao, string cod_ativacao);

        [DllImport(Dll, CallingConvention = StdCall, CharSet = CharSet.Unicode)]
        public static extern IntPtr TrocarCodigoDeAtivacao(int sessao, string cod_ativacao, int opcao, string nova_senha, string conf_senha);

        [DllImport(Dll, CallingConvention = StdCall, CharSet = CharSet.Unicode)]
        public static extern IntPtr ExtrairLogs(int sessao, string cod_ativacao);

        [DllImport(Dll, CallingConvention = StdCall, CharSet = CharSet.Unicode)]
        public static extern IntPtr AtualizarSoftwareSAT(int sessao, string cod_ativacao);

        [DllImport(Dll, CallingConvention = StdCall, CharSet = CharSet.Unicode)]
        public static extern string Base64ToAscii(string dados);

        public static string StringFromNativeUtf8(IntPtr nativeUtf8)
        {
            try
            {
                var len = 0;
                while (Marshal.ReadByte(nativeUtf8, len) != 0) ++len;
                var buffer = new byte[len];
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