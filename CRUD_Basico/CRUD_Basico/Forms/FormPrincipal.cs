using CRUD_Basico.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRUD_Basico
{
    public partial class FormPrincipal : Form
    {
        public FormPrincipal()
        {
            InitializeComponent();
        }

        private void BtnCadastrar_Click(object sender, EventArgs e)
        {
           

            try
            {
                Aluno novoAluno = new Aluno(Txbnome.Text, DtpDtnascimento.Value, CkbAtivo.Checked);

                novoAluno.Cadastrar();
                MessageBox.Show($"Aluno cadastrado com sucesso:\n {novoAluno.Nome}\nid inserido pelo banco:{novoAluno.Id}");
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void FormPrincipal_Load(object sender, EventArgs e)
        {
            try
            {

                List<Aluno> alunos = new Aluno().obterAlunos();
                dataGridView1.DataSource = alunos;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);

            }
        }
    }
}
