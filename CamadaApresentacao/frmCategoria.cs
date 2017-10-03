using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CamadaNegocio;

namespace CamadaApresentacao
{
    public partial class frmCategoria : Form
    {

        private bool Novo = false;
        private bool Editar = false;

        public frmCategoria()
        {
            InitializeComponent();
            this.ttMensagem.SetToolTip(this.txtNome, "Insira o nome da Categoria!");
        }

        // Mostrar mensagem de confirmação
        private void MensagemOk(string mensagem)
        {
            MessageBox.Show(mensagem, "Sistema Comércio", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Mostrar mensagem de erro
        private void MensagemErro(string mensagem)
        {
            MessageBox.Show(mensagem, "Sistema Comércio", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        // Limpar Campos
        private void Limpar()
        {
            this.txtIdCategoria.Text = string.Empty;
            this.txtNome.Text = string.Empty;
            this.txtDescricao.Text = string.Empty;
        }

        // Habilitar os text box
        private void HabilitarTextBox(bool valor)
        {
            this.txtIdCategoria.ReadOnly = !valor;
            this.txtNome.ReadOnly = !valor;
            this.txtDescricao.ReadOnly = !valor;
        }

        // Habilitar os botões
        private void HabilitarButton()
        {
            if(this.Novo || this.Editar)
            {
                this.HabilitarTextBox(true);
                this.btnNovo.Enabled = false;
                this.btnSalvar.Enabled = true;
                this.btnEditar.Enabled = false;
                this.btnCancelar.Enabled = true;
            }
            else
            {
                this.HabilitarTextBox(false);
                this.btnNovo.Enabled = true;
                this.btnSalvar.Enabled = false;
                this.btnEditar.Enabled = true;
                this.btnCancelar.Enabled = false;
            }
        }

        // Ocultar colunas do grid
        private void OcultarColunas()
        {
            this.dataLista.Columns[0].Visible = false;
            //this.dataLista.Columns[1].Visible = false;
        }

        // Mostrar no Data Grid
        private void Mostrar()
        {
            this.dataLista.DataSource = NCategoria.Mostrar();
            // para não mostrar as duas primeiras colunas
            this.OcultarColunas();
            // label para mostrar o total de registros
            lblTotal.Text = "Total de Registros: " + dataLista.Rows.Count.ToString(); // ou convert.tostring(datalista.rows.count);
        }

        private void BuscarNome()
        {
            this.dataLista.DataSource = NCategoria.BuscarNome(this.txtBuscar.Text);
            // para não mostrar as duas primeiras colunas
            this.OcultarColunas();
            // label para mostrar o total de registros
            lblTotal.Text = "Total de Registros: " + dataLista.Rows.Count.ToString(); // ou convert.tostring(datalista.rows.count);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void frmCategoria_Load(object sender, EventArgs e)
        {
            this.Top = 0;
            this.Left = 0;
            this.Mostrar();
            this.HabilitarTextBox(false);
            this.HabilitarButton();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            this.BuscarNome();
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            this.BuscarNome();
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            this.Novo = true;
            this.Editar = false;
            this.HabilitarButton();
            this.Limpar();
            this.HabilitarTextBox(true);
            this.txtNome.Focus();
            this.txtIdCategoria.Enabled = false;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                string resp = "";

                if(txtNome.Text == string.Empty)
                {
                    MensagemErro("Preencha todos os campos...");
                    errorIcone.SetError(txtNome, "Insira o nome!");
                }
                else
                {
                    if(this.Novo)
                    {
                        // Trim ignora espaços vazios existentes na caixa de texto
                        resp = NCategoria.Inserir(txtNome.Text.Trim().ToUpper(), txtDescricao.Text.Trim());
                    }
                    else
                    {
                        resp = NCategoria.Editar(Convert.ToInt32(this.txtIdCategoria.Text), 
                            this.txtNome.Text.Trim().ToUpper(), 
                            this.txtDescricao.Text.Trim());
                    }

                    if(resp.Equals("OK"))
                    {
                        if(this.Novo)
                        {
                            this.MensagemOk("Registro efetuado com sucesso!");
                        }
                        else
                        {
                            this.MensagemOk("Edição efetuada com sucesso!");
                        }
                    }
                    else
                    {
                        this.MensagemErro(resp);
                    }

                    this.Novo = false;
                    this.Editar = false;
                    this.HabilitarButton();
                    this.Limpar();
                    this.Mostrar();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }
    }
}
