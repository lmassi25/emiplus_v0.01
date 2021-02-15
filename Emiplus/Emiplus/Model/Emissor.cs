using System;
using Emiplus.Data.Helpers;
using SqlKata;

namespace Emiplus.Model
{
    internal class Emissor
    {
        public Emissor() 
        { 
        
        }
        public string emissao { get; set; }
        public string aaaamm { get; set; }
        public string chavedeacesso { get; set; }
        public string numero { get; set; }
        public string serie { get; set; }
        public string emitente { get; set; }
        public string destinatario { get; set; }
        public string total { get; set; }
        public string xml { get; set; }
    }
}