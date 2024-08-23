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

            // Configure the Start button
            btnStartGame.Text = "START";
            btnStartGame.Font = new Font("Times New Roman", 12, FontStyle.Bold);
            btnStartGame.ForeColor = Color.Black;
            btnStartGame.BackColor = Color.White;
            btnStartGame.FlatStyle = FlatStyle.Flat;

            // Configure the Instructions button
            btnInstructions.Text = "Instructions";
            btnInstructions.Font = new Font("Times New Roman", 12, FontStyle.Bold);
            btnInstructions.ForeColor = Color.Black;
            btnInstructions.BackColor = Color.White;
            btnInstructions.FlatStyle = FlatStyle.Flat;
        }

        private void btnStartGame_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 gameForm = new Form1();
            gameForm.ShowDialog();
            this.Close();
        }
        // Button that displays a message box that shows instructions
        private void btnInstructions_Click(object sender, EventArgs e)
        {
            // Display instructions in a message box for the user
            MessageBox.Show("Welcome to Memorama!\n" +
                "Find the pairs of icons by clicking on the cards to reveal them.\n" +
                "For each correct pair found, you earn 2 points.\n" +
                "Each incorrect attempt costs you 1 point. You cannot have negative points.\n" +
                "Try to score as high as possible In the shortest time possible!\n\n" +
                "Good luck!");
        }
    }
}