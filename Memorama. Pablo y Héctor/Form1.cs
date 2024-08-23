using System;
using System.Collections.Generic;
using System.Drawing;
using System.Media;
using System.Windows.Forms;

namespace Memorama.Pablo_y_Héctor
{
    public partial class Form1 : Form
    {
        // Create a list with all the icons, and desings them randomly to each grid
        Random random = new Random(); 
        List<string> icons = new List<string>()
        {
            "!", "!", "N", "N", ",", ",", "w", "w", "d", "d", "b", "b",
            "p", "p", "v", "v", "ñ", "ñ", "j", "j", "f", "f", "o", "o",
            "l", "l", "z", "z", "k","k", "a", "a", "q", "q", "h", "h"
        };

        Label firstClicked, secondClicked; // Labels to keep track of the first and second clicked labels
        private Timer gameTimer; // Timer to keep track of elapsed time
        private int elapsedTime; // Elapsed time in seconds
        private int score;  // Variable for scoring
        private int attempts; // Variable for attempts
        private SoundPlayer player; // Variable for the music 

        public Form1()
        {
            InitializeComponent();
            AssignIconsToSquares();

            // Initialize game state
            elapsedTime = 0; // Reset elapsed time
            score = 0; // Reset score
            attempts = 0; // Reset attempts

            // Initialize and set the timer
            gameTimer = new Timer();
            gameTimer.Interval = 1000; 
            gameTimer.Tick += GameTimer_Tick; 
            gameTimer.Start(); // Start the timer

            // Set up the visual properties of the score label
            lblScore.AutoSize = false;
            lblScore.Width = 200;
            lblScore.Height = 50;
            lblScore.Font = new Font("Arial", 16, FontStyle.Bold);
            lblScore.TextAlign = ContentAlignment.MiddleCenter;
            lblScore.ForeColor = Color.Black;
            lblScore.Text = $"Score: {score}"; // Display the initial score

            // Set up the visual properties of the attempts label
            lblAttempts.AutoSize = false;
            lblAttempts.Width = 200;
            lblAttempts.Height = 50;
            lblAttempts.Font = new Font("Arial", 16, FontStyle.Bold);
            lblAttempts.TextAlign = ContentAlignment.MiddleCenter;
            lblAttempts.ForeColor = Color.Black;
            lblAttempts.Text = $"Attempts: {attempts}"; // Display the initial attempts

            // Set up the visual properties of the time label
            labelTime.AutoSize = false;
            labelTime.Width = 200;
            labelTime.Font = new Font("Arial", 16, FontStyle.Bold);
            labelTime.TextAlign = ContentAlignment.MiddleCenter;
            labelTime.ForeColor = Color.Black;

            // Set a solid color background for the form
            this.BackColor = Color.LightGray;

            // Initialize and start playing background music
            player = new SoundPlayer("Audio/Megaman 3 Theme.wav"); // Load the music file
            player.PlayLooping(); // Play the music on a loop
        }

        private void label_Click(object sender, EventArgs e)
        {
            // when the player click two card and they dont match both will hide again
            if (firstClicked != null && secondClicked != null)
                return;
            // the as keyword is triying to convert ther center into a Label but if it cannot do that, "clickedLabel" just will be null
            Label clickedLabel = sender as Label; // Cast sender to Label
            if (clickedLabel == null)
                return;

            // if an already pressed button, is pressed againd, it will be ignored
            if (clickedLabel.ForeColor == Color.Black)
                return;

            // Handle the first label click
            if (firstClicked == null)
            {
                //if a label is first time pressed, the color will turn to black, so it will be viseble
                firstClicked = clickedLabel;
                firstClicked.ForeColor = Color.Black;
                return;
            }

            // Handle the second label click
            secondClicked = clickedLabel;
            secondClicked.ForeColor = Color.Black;

            // Increment attempts and update the label
            attempts++;
            lblAttempts.Text = $"Attempts: {attempts}";

            CheckForWinner(); // Check if the game is won

            // Whe changed the poitns system. This way not everyone will ed up with the same score at the end of the game
            if (firstClicked.Text == secondClicked.Text)
            {
                score += 2; // Increase score for a correct match
            }
            else
            {
                score--; // Decrease score for an incorrect match
                if (score < 0)
                {
                    score = 0; // Prevent negative score
                }
            }

            // Update the Score Label
            lblScore.Text = $"Score: {score}";

            //when two images are the same, both will be freezed in place
            if (firstClicked.Text == secondClicked.Text)
            {
                firstClicked = null;
                secondClicked = null;
            }
            else
            {
                timer1.Start(); // Start the timer to hide non-matching labels
            }
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            elapsedTime++;
            // Update the Label with elapsed time
            labelTime.Text = $"Time: {elapsedTime} s";
        }

        // Check for winner and type a message with time, score and attemps
        private void CheckForWinner()
        {
            Label label;
            for (int i = 0; i < tableLayoutPanel1.Controls.Count; i++)
            {
                label = tableLayoutPanel1.Controls[i] as Label;
                if (label != null && label.ForeColor == label.BackColor)
                    return; // Game is not yet won
            }

            // If all labels are matched
            gameTimer.Stop(); // Stop the timer
            MessageBox.Show($"Felicidades, te ganaste un cantonés. Tiempo: {elapsedTime} segundos. Puntuación final: {score}. Intentos: {attempts}");
        }
        //set a timmer to click the pair of images. The will be freezed in place or they will hide again until you click two cards
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop(); // Stop the timer

            // Hide the non-matching labels
            firstClicked.ForeColor = firstClicked.BackColor;
            secondClicked.ForeColor = secondClicked.BackColor;

            // Reset clicked labels
            firstClicked = null;
            secondClicked = null;
        }
        // Method to assign icons to the squares on the game board
        private void AssignIconsToSquares()
        {
            Label label;
            int randomNumber;

            
            if (icons.Count != tableLayoutPanel1.Controls.Count)
            {
                MessageBox.Show("The list of icons does not match the number of labels on the board.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            List<string> iconsCopy = new List<string>(icons); // Create a copy of the icon list

            for (int i = 0; i < tableLayoutPanel1.Controls.Count; i++)
            {
                if (tableLayoutPanel1.Controls[i] is Label)
                {
                    label = (Label)tableLayoutPanel1.Controls[i];

                    // Select a random icon from the copied list
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

                // Reinitialize the icons on the game board
                AssignIconsToSquares();
            }
            catch (Exception ex)
            {
                // Handle any errors that occur during restart
                MessageBox.Show($"errors occur during restart: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
