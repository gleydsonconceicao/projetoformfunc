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
    public partial class CadastroFucForm : Form
    {
        FUNCIONARIO funcionario = new FUNCIONARIO();
        public CadastroFucForm()
        {
            InitializeComponent();
            cbSexo.Items.Add("Masculino");
            cbSexo.Items.Add("Feminino");
            cbSexo.Items.Add("Outros");
            cbCargo.Items.Add("Gerente");
            cbCargo.Items.Add("Supervisor");
            cbCargo.Items.Add("Operacional");
        }
        public bool FecharForm()
        {
            this.Close();
            return true;
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
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
                funcionario.NOME = txtNome.Text;
                funcionario.DATANASCIMENTO = dtpDataNascimento.Value;
                funcionario.SEXO = sexo.ToString();
                funcionario.TELEFONE = mtxtTelefone.Text;
                funcionario.FILHOS = int.Parse(txtFilhos.Text);
                funcionario.SALARIO = decimal.Parse(txtSalario.Text);
                funcionario.CARGO = cbCargo.Text;
                funcionario.CEP = mtxtCep.Text;
                funcionario.ENDERECO = txtEndereco.Text;
                funcionario.BAIRRO = txtBairro.Text;
                funcionario.CIDADE = txtCidade.Text;
                funcionario.UF = txtUF.Text;
                funcionario.NUMERO = int.Parse(txtNumero.Text);
                try
                {
                    using (EMPRESAEntities1 ctx = new EMPRESAEntities1())
                    {
                        ctx.FUNCIONARIOs.Add(funcionario);
                        ctx.SaveChanges();
                        MessageBox.Show("Cadastro Realizado com sucesso", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
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
                    MessageBox.Show(er.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
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
        private void txtSalario_KeyPress(object sender, KeyPressEventArgs e)
        {
            Program.txtNumerico(sender, e);
        }

        

        private void txtNumero_KeyPress(object sender, KeyPressEventArgs e)
        {
            Program.txtNumerico(sender, e);
        }
    }
}
