using System;
using System.Collections.Generic;
using System.Drawing;
using System.Media;
using System.Windows.Forms;

namespace Memorama.Pablo_y_Héctor
{
    public partial class Form1 : Form
    {
        Random random = new Random();
        List<string> icons = new List<string>()
        {
            "!", "!", "N", "N", ",", ",", "w", "w", "d", "d", "b", "b",
            "p", "p", "v", "v", "ñ", "ñ", "j", "j", "f", "f", "o", "o",
            "l", "l", "z", "z", "k","k", "a", "a", "q", "q", "h", "h"
        };

        Label firstClicked, secondClicked;
        private Timer gameTimer;
        private int elapsedTime;
        private int score;  // Variable for scoring
        private int attempts; // Variable for attempts
        private SoundPlayer player; // Variable for the music 

        public Form1()
        {
            InitializeComponent();
            AssignIconsToSquares();

            // Initialize and set the timer
            elapsedTime = 0;
            score = 0;
            attempts = 0;
            gameTimer = new Timer();
            gameTimer.Interval = 1000;
            gameTimer.Tick += GameTimer_Tick;
            gameTimer.Start();

            // Configure the label to show the score
            lblScore.AutoSize = false;
            lblScore.Width = 200;
            lblScore.Font = new Font("Arial", 16, FontStyle.Bold);
            lblScore.TextAlign = ContentAlignment.MiddleCenter;
            lblScore.ForeColor = Color.Black;
            lblScore.Text = $"Score: {score}";

            // Configure the label to show the attempts
            lblAttempts.AutoSize = false;
            lblAttempts.Width = 200;
            lblAttempts.Font = new Font("Arial", 16, FontStyle.Bold);
            lblAttempts.TextAlign = ContentAlignment.MiddleCenter;
            lblAttempts.ForeColor = Color.Black;
            lblAttempts.Text = $"Attempts: {attempts}";

            // Configure the label to show the time
            labelTime.AutoSize = false;
            labelTime.Width = 200;
            labelTime.Font = new Font("Arial", 16, FontStyle.Bold);
            labelTime.TextAlign = ContentAlignment.MiddleCenter;
            labelTime.ForeColor = Color.Black;

            // Set a solid color background
            this.BackColor = Color.LightGray;

            // Initialize and start playing background music
            player = new SoundPlayer("Audio/Megaman 3 Theme.wav"); // Here it is to look for music in the right route 
            player.PlayLooping(); // The song ends and it plays again
        }

        private void label_Click(object sender, EventArgs e)
        {
            if (firstClicked != null && secondClicked != null)
                return;

            Label clickedLabel = sender as Label;
            if (clickedLabel == null)
                return;

            if (clickedLabel.ForeColor == Color.Black)
                return;

            if (firstClicked == null)
            {
                firstClicked = clickedLabel;
                firstClicked.ForeColor = Color.Black;
                return;
            }

            secondClicked = clickedLabel;
            secondClicked.ForeColor = Color.Black;

            attempts++; // Increment attempts
            lblAttempts.Text = $"Attempts: {attempts}"; // Update the Attempts Label

            CheckForWinner();

            // We changed the points system. This way, not everyone will end up with the same score at the end of the game.
            if (firstClicked.Text == secondClicked.Text)
            {
                score++; // Increase score
            }
            else
            {
                score--; // Decrease score
                if (score < 0)
                {
                    score = 0; // Prevent negative score
                }
            }

            // Update the Score Label
            lblScore.Text = $"Score: {score}";

            if (firstClicked.Text == secondClicked.Text)
            {
                firstClicked = null;
                secondClicked = null;
            }
            else
            {
                timer1.Start();
            }
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            elapsedTime++;
            // Update the Label with elapsed time
            labelTime.Text = $"Time: {elapsedTime} s";
        }

        private void CheckForWinner()
        {
            Label label;
            for (int i = 0; i < tableLayoutPanel1.Controls.Count; i++)
            {
                label = tableLayoutPanel1.Controls[i] as Label;
                if (label != null && label.ForeColor == label.BackColor)
                    return;
            }

            gameTimer.Stop(); // Stop the timer
            MessageBox.Show($"Felicidades, te ganaste un cantonés. Tiempo: {elapsedTime} segundos. Puntuación final: {score}. Intentos: {attempts}");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();

            firstClicked.ForeColor = firstClicked.BackColor;
            secondClicked.ForeColor = secondClicked.BackColor;

            firstClicked = null;
            secondClicked = null;
        }

        private void AssignIconsToSquares()
        {
            Label label;
            int randomNumber;

            // Ensure there are enough icons
            if (icons.Count != tableLayoutPanel1.Controls.Count)
            {
                MessageBox.Show("La lista de iconos no coincide con el número de etiquetas en el tablero.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            List<string> iconsCopy = new List<string>(icons); // Create a copy of the icon list

            for (int i = 0; i < tableLayoutPanel1.Controls.Count; i++)
            {
                if (tableLayoutPanel1.Controls[i] is Label)
                {
                    label = (Label)tableLayoutPanel1.Controls[i];

                    // Select a random index from the copied list
                    randomNumber = random.Next(iconsCopy.Count);
                    label.Text = iconsCopy[randomNumber];

                    // Remove the selected icon from the list
                    iconsCopy.RemoveAt(randomNumber);
                }
            }
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            try
            {
                // Reset the game state
                elapsedTime = 0;
                score = 0;
                attempts = 0;
                lblScore.Text = $"Score: {score}";
                lblAttempts.Text = $"Attempts: {attempts}";
                labelTime.Text = $"Time: {elapsedTime} s";

                // Restart the timer
                gameTimer.Start();

                // Hide and reset all labels
                foreach (Control control in tableLayoutPanel1.Controls)
                {
                    if (control is Label label)
                    {
                        label.ForeColor = label.BackColor;
                    }
                }

                // Reinitialize the icons
                AssignIconsToSquares();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al reiniciar el juego: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}