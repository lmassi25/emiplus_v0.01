using System;
using System.Runtime.InteropServices;

namespace Emiplus.Controller
{
    public class Bematech
    {
        public const string DLL = @"C:\Emiplus\BematechPrinters.dll";
        //public const string DLL = @"MP2032.dll";

        //MP100s TH
        //Configura o modelo da impressora
        [DllImport(DLL)]
        public static extern int ConfiguraModeloImpressora(int modelo);
        //Inicia Porta
        [DllImport(DLL, CallingConvention = CallingConvention.StdCall)]
        public static extern int IniciaPorta(String porta);
        //Enviar texto formatado
        [DllImport(DLL, CallingConvention = CallingConvention.StdCall)]
        public static extern int FormataTX(String texto, int TipoLetra, int italico, int sublinhado, int expandido, int enfatizado);
        //Enviar comandos para a impressora
        [DllImport(DLL, CallingConvention = CallingConvention.StdCall)]
        public static extern int ComandoTX(String comando, int tComando);
        //Fecha a porta
        [DllImport(DLL, CallingConvention = CallingConvention.StdCall)]
        public static extern int FechaPorta();
        //Aciona a Guilhotina
        [DllImport(DLL, CallingConvention = CallingConvention.StdCall)]
        public static extern int AcionaGuilhotina(int tComando);
        //QRCODE
        [DllImport(DLL, CallingConvention = CallingConvention.StdCall)]
        public static extern int ImprimeCodigoQRCODE(int errorCorrectionLevel, int moduleSize, int codeType, int QRCodeVersion, int encodingModes, String codeQr);
        //ConfiguraCodigoBarras()
        [DllImport(DLL, CallingConvention = CallingConvention.StdCall)]
        public static extern int ConfiguraCodigoBarras(int altura, int largura, int posicao, int fonte, int margem);
        //ImprimeCodigoBarrasCODE128
        [DllImport(DLL, CallingConvention = CallingConvention.StdCall)]
        public static extern int ImprimeCodigoBarrasCODE128(String comando);
    }
}
