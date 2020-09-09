using CRUD_Basico.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRUD_Basico
{
    public partial class FormPrincipal : Form
    {
        private List<Aluno> _aluno;
        private Aluno _alunoselecionado;


        public FormPrincipal()
        {
            InitializeComponent();
            _aluno = new Aluno().obterAlunos();
        }

        private void configuradgvaluno()
        {
            Dgvaluno.Columns.Add("id", "id");
            Dgvaluno.Columns["id"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            Dgvaluno.Columns["id"].Visible = false;
            Dgvaluno.Columns.Add("Nome", "Nome do Aluno");
            Dgvaluno.Columns["nome"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            Dgvaluno.Columns.Add("dtnasc", "Data de Nacimento");
            Dgvaluno.Columns["dtnasc"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;


        }

        private void carregaDgvalunos()
        {
            Dgvaluno.Rows.Clear();
            //ordesando a lista de alunos pelo nome 
            _aluno = _aluno.OrderBy(a => a.Nome).ToList();

            foreach (Aluno aluno in _aluno)
            {
                Dgvaluno.Rows.Add(aluno .Id, aluno.Nome, aluno.DtNascimento.ToString("dd/MM/yyyy"));
            }



        }




        private void FormPrincipal_Load(object sender, EventArgs e)
        {
            try
            {

                //List<Aluno> alunos = new Aluno().obterAlunos();
                //Dgvaluno.DataSource = alunos.Where (a => a.Ativo).ToList();

                configuradgvaluno();
                carregaDgvalunos();

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);

            }
        }

        private void Dgvaluno_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;


            _alunoselecionado = _aluno.Find(a => a.Id == (int)Dgvaluno.Rows[e.RowIndex].Cells["id"].Value);


            Txbnome.Text = _alunoselecionado.Nome;
            DtpDtnascimento.Value = _alunoselecionado.DtNascimento;
            CkbAtivo.Checked = _alunoselecionado.Ativo;


            configurabotoesecampos(3);

        }

        private void configurabotoesecampos(int estilo) 
        {
            if (estilo == 1)
            {
                Tsbsalvar.Enabled = false;
                TsbEditar.Enabled = false;
                TsbExcluir.Enabled = false;
                TsbNovo.Enabled = true;


                Txbnome.Clear();
                Txbnome.Enabled = true;
                CkbAtivo.Enabled = true;
                CkbAtivo.Checked = true;
                DtpDtnascimento.Enabled = true;


                _alunoselecionado = null;
            }
            
             else if (estilo == 2)
             {
                    Tsbsalvar.Enabled = true;
                    TsbEditar.Enabled = false;
                    TsbExcluir.Enabled = false;
                    TsbNovo.Enabled = true;

             }
            else if (estilo == 3)
            {
                TsbNovo.Enabled = true;
                Tsbsalvar.Enabled = false;
                TsbEditar.Enabled = true;
                TsbExcluir.Enabled = false;


                Txbnome.Enabled = false;
                DtpDtnascimento.Enabled = false;
                CkbAtivo.Enabled = false;
            }
            else if (estilo == 4)
            {
                TsbNovo.Enabled = true;
                Tsbsalvar.Enabled = true;
                TsbExcluir.Enabled = true;
                TsbEditar.Enabled = false;

                Txbnome.Enabled = true;
                DtpDtnascimento.Enabled = true;
                CkbAtivo.Enabled = true;
            }
            
        
        }


        private void TsbNovo_Click(object sender, EventArgs e)
        {

            configurabotoesecampos(1);


        }

        private void Txbnome_KeyDown(object sender, KeyEventArgs e)
        {
            if (Txbnome.Text.Trim().Length >= 2)
            {
                configurabotoesecampos(2);
            }
        }

        private void TsbEditar_Click(object sender, EventArgs e)
        {
            configurabotoesecampos(4);
        }

        private void Tsbsalvar_Click(object sender, EventArgs e)
        {
            if (_alunoselecionado == null)
            {
                try
                {
                    Aluno novoAluno = new Aluno(Txbnome.Text, DtpDtnascimento.Value, CkbAtivo.Checked);

                    novoAluno.Cadastrar();
                    _aluno.Add(novoAluno);
                    carregaDgvalunos();
                    configurabotoesecampos(1);
                    MessageBox.Show($"Aluno cadastrado com sucesso:\n {novoAluno.Nome}\nid inserido pelo banco:{novoAluno.Id}");
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                _aluno.Remove(_alunoselecionado);



                _alunoselecionado.Nome = Txbnome.Text;
                _alunoselecionado.DtNascimento = DtpDtnascimento.Value;
                _alunoselecionado.Ativo = CkbAtivo.Checked;
                try
                {
                    string retornoBD = _alunoselecionado.atualizar();
                    _aluno.Add(_alunoselecionado);
                    carregaDgvalunos();
                    configurabotoesecampos(1);
                    MessageBox.Show(retornoBD);

                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
               
            }
        }

        private void TsbExcluir_Click(object sender, EventArgs e)
        {

            try
            {
                //remover o aluno selecionado do branco e o retorno da frase sendo armazenado 
                string retornoBD = _alunoselecionado.excluir();
                //remover o aluno de lista de memoria
                _aluno.Remove(_alunoselecionado);
                carregaDgvalunos();
                configurabotoesecampos(1);
                MessageBox.Show(retornoBD);

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }
    }
}
