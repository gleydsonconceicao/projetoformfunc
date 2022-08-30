using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CadFuncionarioApp
{
    public partial class AtualizarForm : Form
    {
        FUNCIONARIO func = new FUNCIONARIO();
        EMPRESAEntities1 ctx = new EMPRESAEntities1();
        public AtualizarForm()
        {
            InitializeComponent();
            cbSexo.Items.Add("Masculino");
            cbSexo.Items.Add("Feminino");
            cbSexo.Items.Add("Outros");
            cbCargo.Items.Add("Gerente");
            cbCargo.Items.Add("Supervisor");
            cbCargo.Items.Add("Operacional");
            CarregarFunc();
        }
        public void CarregarFunc()
        {
            func = ctx.FUNCIONARIOs.Find(Program.id);
            txtNome.Text = func.NOME;
            mtxtTelefone.Text = func.TELEFONE;
            dtpDataNascimento.Value = func.DATANASCIMENTO == null ? DateTime.Now : func.DATANASCIMENTO.Value;
            if (func.SEXO == "M") cbSexo.Text = "Masculino";
            else if (func.SEXO == "F") cbSexo.Text = "Feminino";
            else cbSexo.Text = "Outros";
            txtSalario.Text = func.SALARIO.ToString();
            cbCargo.Text = func.CARGO;
            mtxtCep.Text = func.CEP;
            txtEndereco.Text = func.ENDERECO;
            txtBairro.Text = func.BAIRRO;
            txtCidade.Text = func.CIDADE;
            txtUF.Text = func.UF;
            txtNumero.Text = func.NUMERO.ToString();
        }
        public bool FecharForm()
        {
            this.Close();
            return true;
        }
        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            if (ValidaForm())
            {
                char sexo = ' ';
                if (cbSexo.Text == "Masculino")
                {
                    sexo = 'M';
                }
                else if (cbSexo.Text == "Feminino")
                {
                    sexo = 'F';
                }
                else
                {
                    sexo = 'O';
                }
                func.NOME = txtNome.Text;
                func.DATANASCIMENTO = dtpDataNascimento.Value;
                func.SEXO = sexo.ToString();
                func.TELEFONE = mtxtTelefone.Text;
                func.FILHOS = int.Parse(txtFilhos.Text);
                func.SALARIO = decimal.Parse(txtSalario.Text);
                func.CARGO = cbCargo.Text;
                func.CEP = mtxtCep.Text;
                func.ENDERECO = txtEndereco.Text;
                func.BAIRRO = txtBairro.Text;
                func.CIDADE = txtCidade.Text;
                func.UF = txtUF.Text;
                func.NUMERO = int.Parse(txtNumero.Text);
                ctx.SaveChanges();
                MessageBox.Show("Cadastro atualizado com sucesso!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnPesquisaCEP_Click(object sender, EventArgs e)
        {
            using (var ws = new WSCorreios.AtendeClienteClient())
            {
                try
                {
                    var consulta = ws.consultaCEP(mtxtCep.Text);
                    txtEndereco.Text = consulta.end;
                    txtBairro.Text = consulta.bairro;
                    txtCidade.Text = consulta.cidade;
                    txtUF.Text = consulta.uf;
                }
                catch (Exception er)
                {

                    MessageBox.Show(er.Message);
                }
            }
        }

        private void txtSalario_KeyPress(object sender, KeyPressEventArgs e)
        {
            Program.txtNumerico(sender, e);
        }

        private void txtNumero_KeyPress(object sender, KeyPressEventArgs e)
        {
            Program.txtNumerico(sender, e);
        }
        public bool ValidaForm()
        {
            if (string.IsNullOrEmpty(txtNome.Text))
            {
                errorProvider1.SetError(txtNome, "O campo é obrigatório");
                return false;
            }
            else errorProvider1.SetError(txtNome, "");
            if (string.IsNullOrEmpty(cbSexo.Text))
            {
                errorProvider1.SetError(cbSexo, "O campo é obrigatório");
                return false;
            }
            else errorProvider1.SetError(cbSexo, "");
            if (mtxtTelefone.Text == "(  )     -")
            {
                errorProvider1.SetError(mtxtTelefone, "O campo é obrigatório");
                return false;
            }
            else errorProvider1.SetError(mtxtTelefone, "");
            if (string.IsNullOrEmpty(txtSalario.Text))
            {
                errorProvider1.SetError(txtSalario, "O campo é obrigatório");
                return false;
            }
            else errorProvider1.SetError(txtSalario, "");
            if (string.IsNullOrEmpty(cbCargo.Text))
            {
                errorProvider1.SetError(cbCargo, "O campo é obrigatório");
                return false;
            }
            else errorProvider1.SetError(cbCargo, "");
            if (mtxtCep.Text == "     -")
            {
                errorProvider1.SetError(mtxtCep, "O campo é obrigatório");
                return false;
            }
            else errorProvider1.SetError(mtxtCep, "");
            if (string.IsNullOrEmpty(txtEndereco.Text))
            {
                errorProvider1.SetError(txtEndereco, "O campo é obrigatório");
                return false;
            }
            else
            {
                errorProvider1.SetError(txtEndereco, "");
                return true;
            }
        }
    }
}
