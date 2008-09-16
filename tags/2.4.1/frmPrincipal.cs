using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using Microsoft.Win32;
using ConsultaCotacaoClient.br.com.riscozeroprojetos.consultacotacao;

namespace ConsultaCotacaoClient
{
    public partial class frmPrincipal : Form
    {
        private CotacaoService webservice;

        /// <summary>
        /// Inicializa o formul�rio principal
        /// </summary>
        public frmPrincipal()
        {
            InitializeComponent();
            webservice = new CotacaoService();
            try
            {
                webservice.UseDefaultCredentials = true;
                webservice.Proxy = WebRequest.DefaultWebProxy;
                webservice.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials;
            } 
            catch (Exception e) {
                MessageBox.Show("Erro ao configurar o Proxy: " + e.Message);
            }
        }

        /// <summary>
        /// Quando der dois cliques em uma c�lula preenchida, consulta a cota��o
        /// </summary>
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //N�o atualiza cota��o se o clique duplo for na primeira coluna (onde digita o papel)
            if (e.ColumnIndex == 0)
                return;

            //Se a linha estir preenchida, atualiza
            if (dataGridView1.Rows[e.RowIndex].Cells["CodNeg"].Value != null && ((string)dataGridView1.Rows[e.RowIndex].Cells["CodNeg"].Value).Trim() != "")
            {
                atualizarCotacao((string)dataGridView1.Rows[e.RowIndex].Cells["CodNeg"].Value, e.RowIndex);
            }
        }

        /// <summary>
        /// Consulta uma cota��o no webservice
        /// </summary>
        /// <param name="papel">C�digo de Negocia��o da A��o</param>
        /// <param name="linha">Linha no DataGrid</param>
        private void atualizarCotacao(String papel, int linha)
        {
            System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
            DateTime startTime = DateTime.Now;

            DadosCotacao Cotacao = webservice.ObterCotacao(papel.Trim());

            atualizarDataGrid(linha, Cotacao);
            
            lblSegundos.Text = (DateTime.Now - startTime).TotalSeconds.ToString("N2") + 's';
            System.Windows.Forms.Cursor.Current = Cursors.Default;
        }

        /// <summary>
        /// Atualiza os dados de uma linha do DataGrid com a cota��o
        /// </summary>
        /// <param name="linha">Linha do DataGrid que ser� atualizada</param>
        /// <param name="Cotacao">Dados da cota��o da a��o</param>
        private void atualizarDataGrid(int linha, DadosCotacao Cotacao)
        {
            dataGridView1.Rows[linha].Cells["CodNeg"].Value = Cotacao.codAtivo;
            dataGridView1.Rows[linha].Cells["CodNeg"].ToolTipText = Cotacao.descAtivo;
            dataGridView1.Rows[linha].Cells["ValFechamento"].Value = Cotacao.valFechamento.ToString();
            dataGridView1.Rows[linha].Cells["QtdNegocios"].Value = Cotacao.numNegocios;
            dataGridView1.Rows[linha].Cells["QtdNegocios"].ToolTipText = "R$ " + Cotacao.qtdVolume;
            dataGridView1.Rows[linha].Cells["Hora"].Value = Cotacao.datPregao + ' ' + Cotacao.horCotacao;

            dataGridView1.Rows[linha].Cells["ValAbertura"].Value = Cotacao.valAbertura.ToString();
            dataGridView1.Rows[linha].Cells["ValAbertura"].Style.ForeColor = (Cotacao.valAbertura < Cotacao.valFechamento ? Color.Red : Color.Blue);

            dataGridView1.Rows[linha].Cells["ValMinimo"].Value = Cotacao.valMinimo.ToString();
            dataGridView1.Rows[linha].Cells["ValMinimo"].Style.ForeColor = (Cotacao.valMinimo < Cotacao.valFechamento ? Color.Red : Color.Blue);

            dataGridView1.Rows[linha].Cells["ValMaximo"].Value = Cotacao.valMaximo.ToString();
            dataGridView1.Rows[linha].Cells["ValMaximo"].Style.ForeColor = (Cotacao.valMaximo < Cotacao.valFechamento ? Color.Red : Color.Blue);

            dataGridView1.Rows[linha].Cells["ValAtualCompra"].Value = Cotacao.valOfertaCompra[0].ToString();
            dataGridView1.Rows[linha].Cells["ValAtualCompra"].Style.ForeColor = (Cotacao.valOfertaCompra[0] < Cotacao.valFechamento ? Color.Red : Color.Blue);

            dataGridView1.Rows[linha].Cells["ValAtualVenda"].Value = Cotacao.valOfertaVenda[0].ToString();
            dataGridView1.Rows[linha].Cells["ValAtualVenda"].Style.ForeColor = (Cotacao.valOfertaVenda[0] < Cotacao.valFechamento ? Color.Red : Color.Blue);

            //Se tiver pre�o atual e de fechamento, calcula a porcentagem em tempo real
            if (Cotacao.valFechamento > 0 && Cotacao.valOfertaVenda[0] != 0)
            {
                dataGridView1.Rows[linha].Cells["PerVariacao"].Value = Convert.ToDecimal(((Cotacao.valOfertaCompra[0] / Cotacao.valFechamento - 1) )).ToString("P2");
                dataGridView1.Rows[linha].Cells["PerVariacao"].Style.ForeColor = (((Cotacao.valOfertaCompra[0] / Cotacao.valFechamento - 1) * 100) < 0 ? Color.Red : Color.Blue);
            }
            else
            {
                dataGridView1.Rows[linha].Cells["PerVariacao"].Value = decimal.Divide(Cotacao.qtdVariacao, 100).ToString("P2");
                dataGridView1.Rows[linha].Cells["PerVariacao"].Style.ForeColor = (Cotacao.qtdVariacao < 0 ? Color.Red : Color.Blue);
            }
            dataGridView1.Rows[linha].Cells["CodNeg"].Style.ForeColor = dataGridView1.Rows[linha].Cells["PerVariacao"].Style.ForeColor;

            if (Cotacao.valFechamento > 0)
            {
                dataGridView1.Rows[linha].Cells["ValAbertura"].ToolTipText = Convert.ToDecimal(((Cotacao.valAbertura / Cotacao.valFechamento - 1))).ToString("P2");
                dataGridView1.Rows[linha].Cells["ValMinimo"].ToolTipText = Convert.ToDecimal(((Cotacao.valMinimo / Cotacao.valFechamento - 1))).ToString("P2");
                dataGridView1.Rows[linha].Cells["ValMaximo"].ToolTipText = Convert.ToDecimal(((Cotacao.valMaximo / Cotacao.valFechamento - 1))).ToString("P2");
                dataGridView1.Rows[linha].Cells["ValAtualCompra"].ToolTipText = Convert.ToDecimal(((Cotacao.valOfertaCompra[0] / Cotacao.valFechamento - 1))).ToString("P2");
                dataGridView1.Rows[linha].Cells["ValAtualVenda"].ToolTipText = Convert.ToDecimal(((Cotacao.valOfertaVenda[0] / Cotacao.valFechamento - 1))).ToString("P2");
            }
        }

        /// <summary>
        /// Consulta cota��o de todos os papeis no webservice
        /// </summary>
        private void atualizarCotacoes() {
            System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
            DateTime startTime = DateTime.Now;

            string[] cotacoes = new string[dataGridView1.Rows.Count-1]; //a �ltima linha est� em branco
            int numRegistrosValidos = 0;

            for (int i = 0; i < dataGridView1.Rows.Count-1; i++)
            {
                cotacoes[i] = (string)dataGridView1.Rows[i].Cells["CodNeg"].Value;
                if (dataGridView1.Rows[i].Cells["CodNeg"].Value != null && ((string)dataGridView1.Rows[i].Cells["CodNeg"].Value).Trim() != "")
                    numRegistrosValidos++;
            }

            if (numRegistrosValidos > 0)
            {
                DadosCotacao[] arrayCotacao = webservice.ObterCotacoes(cotacoes);

                for (int i = 0; i < arrayCotacao.Length; i++)
                    atualizarDataGrid(i, arrayCotacao[i]);
            }
            else
            {
                dataGridView1.Rows.Clear();
            }

            lblSegundos.Text = (DateTime.Now - startTime).TotalSeconds.ToString("N2") + 's';
            System.Windows.Forms.Cursor.Current = Cursors.Default;
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            atualizarCotacoes();
        }

        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            int numRegistrosValidos = 0;

            //Abre a chave do registro e, se n�o encontrar, cria
            RegistryKey key = Registry.CurrentUser.OpenSubKey("Software\\Cintra\\ConsultaCotacaoClient");
            if (key == null)
            {
                key = Registry.CurrentUser.CreateSubKey("Software\\Cintra\\ConsultaCotacaoClient");
            }

            //Se encontrou valor e ele for um array de string (n�o foi alterado na m�o)
            if (key.GetValue("ListaAcoes") != null && key.GetValue("ListaAcoes").GetType() == typeof(string[]))
            {
                string[] ListaAcoes = (string[])key.GetValue("ListaAcoes");
                for (int i = 0; i < ListaAcoes.Length; i++)
                {
                    //Se o conte�do n�o estiver vazio
                    if ( ListaAcoes[i] != "" ) 
                    {
                        dataGridView1.Rows.Add(1);
                        numRegistrosValidos++;
                        dataGridView1.Rows[numRegistrosValidos-1].Cells["CodNeg"].Value = ListaAcoes[i];
                    }
                }
                atualizarCotacoes();
            }
        }

        private void frmPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Abre o registro pra gravar as a��es
            RegistryKey key = Registry.CurrentUser.OpenSubKey("Software\\Cintra\\ConsultaCotacaoClient", true);

            //Se tiver mais que a �ltima linha em branco
            if (dataGridView1.Rows.Count > 1)
            {
                string[] ListaAcoes = new string[dataGridView1.Rows.Count - 1];
                for (int i = 0; i < dataGridView1.Rows.Count-1; i++)
                {
                    ListaAcoes[i] = (string)dataGridView1.Rows[i].Cells["CodNeg"].Value == null ? "" : ((string)dataGridView1.Rows[i].Cells["CodNeg"].Value).Trim();
                }
                key.SetValue("ListaAcoes", ListaAcoes);
            }
            else
            {
                key.SetValue("ListaAcoes", "");
            }
        }

        private void frmPrincipal_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                Hide();
            }
        }

        private void notifyIcon1_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                Show();
                WindowState = FormWindowState.Normal;
                Activate();
            }
            else
            {
                Hide();
                WindowState = FormWindowState.Minimized;
            }
        }
       
    }
}