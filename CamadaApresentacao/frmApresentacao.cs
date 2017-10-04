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
    public partial class frmApresentacao : Form
    {

        private bool Novo = false;
        private bool Editar = false;

        public frmApresentacao()
        {
            InitializeComponent();
            this.ttMensagem.SetToolTip(this.txtNome, "Insira o nome da Apresentação!");
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
            if (this.Novo || this.Editar)
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

        private void OcultarColunas()
        {
            this.dataLista.Columns[0].Visible = false;
            this.dataLista.Columns[1].Visible = false;
        }

        // Mostrar no Data Grid
        private void Consultar()
        {
            this.dataLista.DataSource = NApresentacao.Consultar();
            // para não mostrar as duas primeiras colunas
            this.OcultarColunas();
            // label para mostrar o total de registros
            lblTotal.Text = "Total de Registros: " + dataLista.Rows.Count.ToString(); // ou convert.tostring(datalista.rows.count);
        }

        private void ConsultarPorNome()
        {
            this.dataLista.DataSource = NApresentacao.ConsultarPorNome(this.txtBuscar.Text);
            // para não mostrar as duas primeiras colunas
            this.OcultarColunas();
            // label para mostrar o total de registros
            lblTotal.Text = "Total de Registros: " + dataLista.Rows.Count.ToString(); // ou convert.tostring(datalista.rows.count);
        }

        private void frmApresentacao_Load(object sender, EventArgs e)
        {
            this.Top = 0;
            this.Left = 0;
            this.Consultar();
            this.HabilitarTextBox(false);
            this.HabilitarButton();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            this.ConsultarPorNome();
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            this.ConsultarPorNome();
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

                if (txtNome.Text == string.Empty)
                {
                    MensagemErro("Preencha todos os campos...");
                    errorIcone.SetError(txtNome, "Insira o nome!");
                }
                else
                {
                    if (this.Novo)
                    {
                        // Trim ignora espaços vazios existentes na caixa de texto
                        resp = NApresentacao.Inserir(txtNome.Text.Trim().ToUpper(), txtDescricao.Text.Trim());
                    }
                    else
                    {
                        resp = NApresentacao.Editar(Convert.ToInt32(this.txtIdCategoria.Text),
                            this.txtNome.Text.Trim().ToUpper(),
                            this.txtDescricao.Text.Trim());
                    }

                    if (resp.Equals("OK"))
                    {
                        if (this.Novo)
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
                    this.Consultar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void dataLista_DoubleClick(object sender, EventArgs e)
        {
            // trago para a caixa de texto a linha referente a célula id categoria... converto para string pois caixa de texto pois o valor de retorno é objeto
            this.txtIdCategoria.Text = this.dataLista.CurrentRow.Cells["idapresentacao"].Value.ToString();
            this.txtNome.Text = this.dataLista.CurrentRow.Cells["nome"].Value.ToString();
            this.txtDescricao.Text = this.dataLista.CurrentRow.Cells["descricao"].Value.ToString();
            // após trazer os resultados, aponto para a tab de configurações -> 0 = listar -> 1 = configurações
            this.tabControl1.SelectedIndex = 1;
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (txtIdCategoria.Text.Equals(""))
            {
                this.MensagemErro("Selecione um registro para inserir.");
            }
            else
            {
                this.Editar = true;
                this.HabilitarButton();
                this.HabilitarTextBox(true);
                this.txtIdCategoria.ReadOnly = true;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Novo = false;
            this.Editar = false;
            this.HabilitarButton();
            this.HabilitarTextBox(false);
            this.Limpar();
        }

        private void chkDeletar_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDeletar.Checked)
            {
                // coluna com id fica visível.
                this.dataLista.Columns[0].Visible = true;
            }
            else
            {
                // coluna com id fica invisível.
                this.dataLista.Columns[0].Visible = false;
            }
        }

        private void dataLista_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataLista.Columns["Deletar"].Index)
            {
                DataGridViewCheckBoxCell ChkDeletar = (DataGridViewCheckBoxCell)dataLista.Rows[e.RowIndex].Cells["Deletar"];
                // quando for true, ele aparece... quando não for, ele apaga. A negativa é pra ele começar apagado.
                ChkDeletar.Value = !Convert.ToBoolean(ChkDeletar.Value);
            }
        }

        private void btnDeletar_Click(object sender, EventArgs e)
        {
            try
            {
                // verificar uma caixa de diálogo
                DialogResult opcao;
                opcao = MessageBox.Show("Deseja apagar os registros? ", "Sistema Comércio", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (opcao == DialogResult.Yes)
                {
                    string codigo;
                    string resp = "";

                    foreach (DataGridViewRow row in dataLista.Rows)
                    {
                        // se estiver marcado, eu quero que exclua.
                        if (Convert.ToBoolean(row.Cells[0].Value))
                        {
                            // célula 1 = ID
                            codigo = Convert.ToString(row.Cells[1].Value);
                            // converto o ID recebido como string pela variavel código para int
                            resp = NApresentacao.Excluir(Convert.ToInt32(codigo));

                            if (resp.Equals("OK"))
                            {
                                this.MensagemOk("Registro excluído com sucesso!");
                            }
                            else
                            {
                                this.MensagemErro(resp);
                            }

                        }
                    }

                    // se o registro for excluído, me mostre os dados atualizados no grid.
                    this.Consultar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }
    }
}
