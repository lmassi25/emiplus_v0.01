using Emiplus.Data.Helpers;
using SqlKata.Execution;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Emiplus.Controller
{
    class Imposto
    {
        private Model.PedidoItem _modelpedidoItem = new Model.PedidoItem();
        private Model.Item _modelItem = new Model.Item();
        private Model.Imposto _modelImposto = new Model.Imposto();

        public void SetImposto(int idPedidoItem, int idImposto = 0, string tipo = "")
        {
            _modelpedidoItem = _modelpedidoItem.FindById(idPedidoItem).First<Model.PedidoItem>();
            _modelItem = _modelItem.FindById(_modelpedidoItem.Item).First<Model.Item>();

            #region IMPOSTO 

            if (idImposto == 0)
            {                
                if (_modelItem.Count() != 0)
                {
                    switch (tipo)
                    {
                        case "CFe":

                            if (_modelItem.Impostoidcfe == 0)
                                break;

                            _modelImposto = _modelImposto.FindById(_modelItem.Impostoidcfe).First<Model.Imposto>();
                            break;
                        default:
                            if (_modelItem.Impostoid == 0)
                                break;

                            _modelImposto = _modelImposto.FindById(_modelItem.Impostoid).First<Model.Imposto>();
                            break;
                    }                    
                }
            }
            else
            {
                _modelImposto = _modelImposto.FindById(idImposto).First<Model.Imposto>();
            }

            #endregion

            #region NCM | CEST | ORIGEM 

            _modelpedidoItem.Ncm = _modelItem.Ncm;
            _modelpedidoItem.Cest = _modelItem.Cest;
            _modelpedidoItem.Origem = _modelItem.Origem;

            #endregion

            #region CFOP

            _modelpedidoItem.Cfop = _modelImposto.Cfop;

            #endregion

            #region ICMS

            _modelpedidoItem.Icms = _modelImposto.Icms;

            switch (_modelImposto.Icms)
            {
                #region REGIME NORMAL

                //00 = Tributada integralmente.
                //10 = Tributada e com cobrança do ICMS por substituição tributária
                //20 = Com redução de base de cálculo
                //30 = Isenta ou não tributada e com cobrança do ICMS por substituição tributária
                //40 = Isenta
                //41 = Não tributada
                //50 = Suspensão
                //51 = Diferimento
                //60 = ICMS cobrado anteriormente por substituição tributária
                //70 = Com redução de base de cálculo e cobrança do ICMS por substituição tributária
                //90 = Outros

                case "00":
                case "90":
                    _modelpedidoItem.IcmsBase = _modelpedidoItem.Total;
                    _modelpedidoItem.IcmsAliq = Validation.RoundAliquotas(_modelImposto.IcmsAliq / 100);
                    _modelpedidoItem.IcmsVlr = Validation.Round(_modelpedidoItem.IcmsBase * _modelpedidoItem.IcmsAliq);
                    break;

                #endregion

                #region SIMPLES NACIONAL

                //101 = Tributada pelo Simples Nacional com permissão de crédito                   
                //102 = Tributada pelo Simples Nacional sem permissão de crédito                    
                //103 = Isenção do ICMS no Simples Nacional para faixa de receita bruta
                //201 = Tributada pelo Simples Nacional com permissão de crédito e com cobrança do ICMS por Substituição Tributária202 = Tributada pelo Simples Nacional sem permissão de crédito e com cobrança do ICMS por Substituição Tributária                    
                //202 
                //203 = Isenção do ICMS nos Simples Nacional para faixa de receita bruta e com cobrança do ICMS por Substituição Tributária
                //300 = Imune                    
                //400 = Não tributada pelo Simples Nacional
                //500 = ICMS cobrado anteriormente por substituição tributária (substituído) ou por antecipação                    
                //900 = Outros

                case "101":
                    _modelpedidoItem.Icms101Aliq = Validation.RoundAliquotas(_modelImposto.IcmsAliq / 100);
                    _modelpedidoItem.Icms101Vlr = Validation.Round(_modelpedidoItem.Total * _modelpedidoItem.Icms101Aliq);
                    break;
                case "201":
                case "202":
                    //---------------ICMS
                    _modelpedidoItem.IcmsBase = _modelpedidoItem.Total;
                    _modelpedidoItem.IcmsAliq = Validation.RoundAliquotas(_modelImposto.IcmsAliq / 100);
                    _modelpedidoItem.IcmsVlr = Validation.Round(_modelpedidoItem.IcmsBase * _modelpedidoItem.IcmsAliq);
                    //---------------ICMS ST
                    _modelpedidoItem.IcmsStBase = Validation.Round(_modelpedidoItem.IcmsBase + (_modelpedidoItem.IcmsBase * (_modelImposto.IcmsStIva / 100)));
                    _modelpedidoItem.IcmsStAliq = Validation.RoundAliquotas(_modelImposto.IcmsStAliq / 100);
                    if(_modelImposto.IcmsStReducaoAliq > 0)
                    {
                        _modelpedidoItem.IcmsStBase = Validation.Round(_modelpedidoItem.IcmsStBase - (_modelpedidoItem.IcmsStBase * (_modelImposto.IcmsStReducaoAliq / 100)));
                    }
                    _modelpedidoItem.Icmsstvlr = Validation.Round(_modelpedidoItem.IcmsStBase * _modelpedidoItem.IcmsStAliq);

                    //---------------ICMS ST - ICMS 
                    _modelpedidoItem.Icmsstvlr = Validation.Round(_modelpedidoItem.Icmsstvlr - _modelpedidoItem.IcmsVlr);

                    _modelpedidoItem.IcmsBase = 0;
                    _modelpedidoItem.IcmsAliq = 0;
                    _modelpedidoItem.IcmsVlr = 0;
                    break;
                case "900":
                    break;

                #endregion

                default:

                    _modelpedidoItem.IcmsBase = 0;
                    _modelpedidoItem.IcmsAliq = 0;
                    _modelpedidoItem.IcmsVlr = 0;

                    _modelpedidoItem.IcmsStAliq = 0;
                    _modelpedidoItem.IcmsStBase = 0;
                    _modelpedidoItem.IcmsStBaseComReducao = 0;
                    _modelpedidoItem.IcmsStReducaoAliq = 0;                   

                    break;
            }

            if (_modelpedidoItem.IcmsAliq > 0)
            {
                _modelpedidoItem.IcmsAliq = (_modelpedidoItem.IcmsAliq * 100);
            }

            if (_modelpedidoItem.IcmsStAliq > 0)
            {
                _modelpedidoItem.IcmsStAliq = (_modelpedidoItem.IcmsStAliq * 100);
            }

            #endregion

            #region IPI
            
            _modelpedidoItem.Ipi = _modelImposto.Ipi;

            switch (_modelImposto.Ipi)
            {
                case "50":
                case "99":
                    if (_modelImposto.IpiAliq > 0)
                    {
                        _modelpedidoItem.IpiAliq = Validation.RoundAliquotas(_modelImposto.IpiAliq / 100);
                        _modelpedidoItem.IpiVlr = Validation.Round(_modelpedidoItem.Total * _modelpedidoItem.IpiAliq);
                    }
                    break;
                default:
                    _modelpedidoItem.Ipi = "0";
                    _modelpedidoItem.IpiAliq = 0;
                    _modelpedidoItem.IpiVlr = 0;
                    break;
            }

            #endregion

            #region PIS
            
            _modelpedidoItem.Pis = _modelImposto.Pis;

            switch (_modelImposto.Pis)
            {
                case "01":
                case "99":
                    if (_modelImposto.PisAliq > 0)
                    {
                        _modelpedidoItem.PisAliq = Validation.RoundAliquotas(_modelImposto.PisAliq / 100);
                        _modelpedidoItem.PisVlr = Validation.Round(_modelpedidoItem.Total * _modelpedidoItem.PisAliq);
                    }
                    break;
                default:
                    _modelpedidoItem.Pis = "0";
                    _modelpedidoItem.PisAliq = 0;
                    _modelpedidoItem.PisVlr = 0;
                    break;
            }

            if (_modelpedidoItem.PisAliq > 0)
            {
                _modelpedidoItem.PisAliq = (_modelpedidoItem.PisAliq * 100);
            }

            #endregion

            #region COFINS

            _modelpedidoItem.Cofins = _modelImposto.Cofins;

            switch (_modelImposto.Cofins)
            {
                case "01":
                case "99":
                    if (_modelImposto.CofinsAliq > 0)
                    {
                        _modelpedidoItem.CofinsAliq = Validation.RoundAliquotas(_modelImposto.CofinsAliq / 100);
                        _modelpedidoItem.CofinsVlr = Validation.Round(_modelpedidoItem.Total * _modelpedidoItem.CofinsAliq);
                    }
                    break;
                default:
                    _modelpedidoItem.Cofins = "0";
                    _modelpedidoItem.CofinsAliq = 0;
                    _modelpedidoItem.CofinsVlr = 0;
                    break;
            }

            if (_modelpedidoItem.CofinsAliq > 0)
            {
                _modelpedidoItem.CofinsAliq = (_modelpedidoItem.CofinsAliq * 100);
            }

            #endregion

            #region vTotTrib

            _modelpedidoItem.Federal = Validation.Round(_modelpedidoItem.Total * Validation.Round(_modelItem.AliqFederal / 100));
            _modelpedidoItem.Estadual = Validation.Round(_modelpedidoItem.Total * Validation.Round(_modelItem.AliqEstadual / 100));
            _modelpedidoItem.Municipal = Validation.Round(_modelpedidoItem.Total * Validation.Round(_modelItem.AliqMunicipal / 100));

            #endregion

            _modelpedidoItem.Save(_modelpedidoItem);
        }

        public Task<IEnumerable<dynamic>> GetDataTable(string SearchText = null)
        {
            var search = "%" + SearchText + "%";

            return new Model.Imposto().Query()
                .Where("EXCLUIR", 0)
                .Where
                (
                    q => q.WhereLike("nome", search, false)
                )
                .OrderByDesc("criado")
                .GetAsync<dynamic>();
        }

        public async Task SetTable(DataGridView Table, IEnumerable<dynamic> Data = null, string SearchText = "")
        {
            Table.ColumnCount = 2;

            typeof(DataGridView).InvokeMember("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, Table, new object[] { true });
            Table.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

            Table.RowHeadersVisible = false;

            Table.Columns[0].Name = "ID";
            Table.Columns[0].Visible = false;

            Table.Columns[1].Name = "Nome";

            Table.Rows.Clear();

            if (Data == null)
            {
                IEnumerable<dynamic> dados = await GetDataTable(SearchText);
                Data = dados;
            }

            foreach (var item in Data)
            {
                Table.Rows.Add(
                    item.ID,
                    item.NOME
                );
            }

            Table.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }
    }
}