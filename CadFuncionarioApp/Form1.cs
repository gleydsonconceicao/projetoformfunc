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
    public partial class TelaInicialForm : Form
    {
        EMPRESAEntities1 ctx;
        FUNCIONARIO func;
        public TelaInicialForm()
        {
            InitializeComponent();
            ListarFuncionarios();
            gridFuncionarios.Columns[0].Width = 50;
            gridFuncionarios.Columns[1].Width = 200;
            gridFuncionarios.Columns[3].Width = 40;
            gridFuncionarios.Columns[5].Width = 40;
            gridFuncionarios.Columns[9].Width = 200;
        }
        public void ListarFuncionarios()
        {
            ctx = new EMPRESAEntities1();
            var funcionarios = ctx.FUNCIONARIOs;
            gridFuncionarios.DataSource = funcionarios.ToList();
        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cadastrarClienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var func = new CadastroFucForm();
            func.ShowDialog();
            if (func.FecharForm())
            {
                ListarFuncionarios();
            }
        }
        private void txtPesquisa_TextChanged(object sender, EventArgs e)
        {
            Pesquisa();
        }
        public void Pesquisa()
        {
            var pesquisa = from func in ctx.FUNCIONARIOs
                           where func.NOME.Contains(txtPesquisa.Text)
                           select func;
            gridFuncionarios.DataSource = pesquisa.ToList();
        }

        private void gridFuncionarios_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Program.id = int.Parse(gridFuncionarios.Rows[e.RowIndex].Cells[0].Value.ToString());
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            if (Program.id > 0)
            {
                AtualizarForm atualizar = new AtualizarForm();
                atualizar.ShowDialog();
                if (atualizar.FecharForm()) ListarFuncionarios();
                
            }
            else
            {
                MessageBox.Show("Selecione um funcionário");
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            string mesnagem = "Tem certeza que Deseja excluir esse registro?";
            if (Program.id > 0)
            {
                if (MessageBox.Show(mesnagem, "Excluir", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    func = ctx.FUNCIONARIOs.Find(Program.id);
                    ctx.FUNCIONARIOs.Remove(func);
                    ctx.SaveChanges();
                    MessageBox.Show("Registro excluido com sucesso");
                    ListarFuncionarios();
                }
            }
            else MessageBox.Show("Selecione um registro para ser exlcuido!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
