using System;
using System.Collections.Generic;
using System.Drawing;
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

        public Form1()
        {
            InitializeComponent();
            AssignIconsToSquares();

            // Initialize and set the timer
            elapsedTime = 0;
            score = 0;
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

            // Configure the label to show the time
            labelTime.AutoSize = false;
            labelTime.Width = 200;
            labelTime.Font = new Font("Arial", 16, FontStyle.Bold);
            labelTime.TextAlign = ContentAlignment.MiddleCenter;
            labelTime.ForeColor = Color.Black;

            // Set a solid color background
            this.BackColor = Color.LightGray; 
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

            CheckForWinner();

            if (firstClicked.Text == secondClicked.Text)
            {
                score++; // Increase score
                lblScore.Text = $"Score: {score}"; // Update the Score Label

                firstClicked = null;
                secondClicked = null;
            }
            else
                timer1.Start();
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
            MessageBox.Show($"Felicidades, te ganaste un cantonés. Tiempo: {elapsedTime} segundos. Puntuación final: {score}");
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

            for (int i = 0; i < tableLayoutPanel1.Controls.Count; i++)
            {
                if (tableLayoutPanel1.Controls[i] is Label)
                    label = (Label)tableLayoutPanel1.Controls[i];
                else
                    continue;

                randomNumber = random.Next(0, icons.Count);
                label.Text = icons[randomNumber];

                icons.RemoveAt(randomNumber);
            }
        }
    }
}

