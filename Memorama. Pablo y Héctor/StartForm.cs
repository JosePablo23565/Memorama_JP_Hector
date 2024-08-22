using System;
using System.Drawing;
using System.Windows.Forms;

namespace Memorama.Pablo_y_Héctor
{
    public partial class StartForm : Form
    {
        public StartForm()
        {
            InitializeComponent();
            
            btnStartGame.Text = "INICIAR";
            btnStartGame.Font = new Font("Times New Roman", 12, FontStyle.Bold);
            btnStartGame.ForeColor = Color.Black;
            btnStartGame.BackColor = Color.White;
            btnStartGame.FlatStyle = FlatStyle.Flat;
        }

        private void StartForm_Load(object sender, EventArgs e)
        {
           
        }

        private void btnStartGame_Click(object sender, EventArgs e)
        {
            this.Hide(); 
            Form1 gameForm = new Form1(); 
            gameForm.ShowDialog(); 
            this.Close(); 
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
