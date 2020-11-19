// based on src: https://www.codeproject.com/Articles/5250284/Small-WinForm-Pong-Game-Csharp
// TODO: add sound
// TODO: change color of background when goal scores and display work GOAL
// TODO: start ball slow and move it faster after each score
// TODO: choose color of pong ball and other items
using System;
using System.Drawing;
using System.Windows.Forms;

namespace PongGame
{
    public partial class PongForm : Form
    {
        public PongForm()
        {
            InitializeComponent();
            this.KeyPreview = true;
        }
        const int limitPad = 170;
        const int limitBall = 245;
        const int x = 277;  // x ball starting posistion
        const int y = 120;  // y ball starting position

        int computerScore = 0;  // tracks computer's score
        int playerScore = 0;  // tracks player's score

        int yBallPosition;  // tracks ball's vertical position (up and down)
        int xBallPosition; // tracks ball's horizontal position (left and right)

        bool moveUp = false;  //  tracks if player wants to move paddle up
        bool moveDown = false;  // tracks if player wants to move paddle down
        bool gameInProgress = false;  // tracks if game is in progress

        readonly Random randomPosition = new Random(Guid.NewGuid().GetHashCode());

        // toggles paddle movement to true when up or down keys are press
        private void KeyPressed(object sender, KeyEventArgs e)
        {
            if (gameInProgress)
            {
                if (e.KeyCode == Keys.Up || e.KeyCode == Keys.W)
                {
                    moveUp = true;
                }
                else if (e.KeyCode == Keys.Down || e.KeyCode == Keys.S)
                {
                    moveDown = true;
                }
                humanPaddleTimer.Start(); 
            }
        }

        // toggles paddle movement to false when up or down keys are press
        private void KeyReleased(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.W)
            {
                moveUp = false;
            }
            else if (e.KeyCode == Keys.Up || e.KeyCode == Keys.S)
            {
                moveDown = false;
            }
            humanPaddleTimer.Stop();
        }
        
        // moves paddle to new position
        private void MovePaddle(object sender, EventArgs e)
        {
            if (moveUp && humanPaddle.Location.Y > 0)
            {
                humanPaddle.Top -= 3; 
            }
            else if (moveDown && humanPaddle.Location.Y < limitPad)
            {
                humanPaddle.Top += 3;
            }
        }

        // moves ball to new position
        private void MoveBall(object sender, EventArgs e)
        {
            if (ball.Bounds.IntersectsWith(humanPaddle.Bounds))
            {
                Collision(humanPaddle);
            }
            else if (ball.Bounds.IntersectsWith(computerPaddle.Bounds))
            {
                Collision(computerPaddle);
            }
            CheckIfHitBorder();
            CheckIfBallLeftField();
            BallMoves();
        }

        // tracks collision of ball with player paddles
        private void Collision(PictureBox Paddle) 
        {
            switch (true)
            {
                case true when Upper(Paddle):
                    yBallPosition = Negative(4, 6);
                    xBallPosition = AdjustCoordinates(5, 6);
                    break;
                case true when High(Paddle):
                    yBallPosition = Negative(2, 3);
                    xBallPosition = AdjustCoordinates(6, 7);
                    break;
                case true when Middle(Paddle):
                    yBallPosition = 0;
                    xBallPosition = AdjustCoordinates(5, 5);
                    break;
                case true when Low(Paddle):
                    yBallPosition = randomPosition.Next(2, 3);
                    xBallPosition = AdjustCoordinates(6, 7);
                    break;
                case true when Bot(Paddle):
                    yBallPosition = randomPosition.Next(4, 6);
                    xBallPosition = AdjustCoordinates(5, 6);
                    break;                   
            }
            PaddleImpact(); 
        }

        // Adjusts ball x axis movement after a collision
        private int AdjustCoordinates(int i,int n)
        {
            int res = 0; 

            if (ball.Location.X < this.Width / 2)
            {
                res = randomPosition.Next(i, n);
            }
            else if (ball.Location.X > this.Width / 2)
            {
                res = Negative(i, n);
            }
            return res;
        }

        private int Negative(int i,int n)
        {
            int myval = randomPosition.Next(i, n) * -1;
            return myval; 
        }

        private bool Upper(PictureBox Pad)
        {
            return ball.Location.Y >= Pad.Top - ball.Height && ball.Location.Y <= Pad.Top + ball.Height;
        }

        private bool High(PictureBox Pad)
        {
            return ball.Location.Y > Pad.Top + ball.Height && ball.Location.Y <= Pad.Top + 2 * ball.Height;
        }

        private bool Middle(PictureBox Pad)
        {
            return ball.Location.Y > Pad.Top + 2 * ball.Height && ball.Location.Y <= Pad.Top + 3 * ball.Height;
        }

        private bool Low(PictureBox Pad)
        {
            return ball.Location.Y > Pad.Top + 3 * ball.Height && ball.Location.Y <= Pad.Top + 4 * ball.Height;
        }

        private bool Bot(PictureBox Pad)
        {
            return ball.Location.Y > Pad.Top + 4 * ball.Height && ball.Location.Y <= Pad.Bottom + ball.Height;
        }

        // checks if ball hits a boarder
        private void CheckIfHitBorder()
        {
            if (ball.Location.Y <= 0 || ball.Location.Y >= limitBall)
            {
                yBallPosition *= -1; 
            }
        }

        // checks to see ball left the field of play
        private void CheckIfBallLeftField()
        {
            // end game when a score is 10
            if (playerScore == 10 || computerScore == 10)
            {
                EndGame();
            }

            // increase computer score when ball crosses player goal
            if (ball.Location.X < 0 - humanPaddle.Width && ball.Location.X < this.Width / 2)
            {
                RespawnBall(5);
                ComputerPlayerScored(); 
            }
            // increase player score when ball crosses computer goal
            else if (ball.Location.X > computerPaddle.Location.X + computerPaddle.Width && ball.Location.X > this.Width / 2)
            {
                RespawnBall(-5);
                HumanPlayerScored(); 
            }
        }

        // determine what happens when ball impacts a paddle
        private void PaddleImpact()
        {
            if (ball.Location.X < this.Width / 2)
            {
                if (ball.Location.X < 0 + ball.Height / 3)
                {
                    xBallPosition *= -1; 
                }
            }
            else if (ball.Location.X > this.Width / 2)
            {
                if (ball.Location.X > computerPaddle.Location.X + (ball.Width /3))
                {
                    xBallPosition *= -1;
                }
            }
        }

        // respawns ball at starting position after score takes place
        private void RespawnBall(int _horizontalSpeed)
        { 
            ball.Location = new Point(x, y);
            yBallPosition = 0;
            xBallPosition = _horizontalSpeed;
        }

        private void StartValues()
        {
            yBallPosition = 0;
            xBallPosition = -5; 
        }
        private void BallMoves()
        {
            ball.Top += yBallPosition;
            ball.Left += xBallPosition; 
        }
        private void Computer(object sender, EventArgs e)
        {
            if (computerPaddle.Location.Y <= 0)
            {
                computerPaddle.Location = new Point(computerPaddle.Location.X, 0); 
            }
            else if (computerPaddle.Location.Y >= limitPad)
            {
                computerPaddle.Location = new Point(computerPaddle.Location.X, limitPad);
            }
            if (ball.Location.Y < computerPaddle.Top + (computerPaddle.Height / 2))
            {
                computerPaddle.Top -= 3;
            }
            else if (ball.Location.Y > computerPaddle.Top + (computerPaddle.Height / 2))
            {
                computerPaddle.Top += 3;
            }
        }

        private void HumanPlayerScored()
        {
            playerScore++;
            playerScoreLabel.Text = playerScore.ToString();
        }

        // increments ComputerPlayer's score and updates the scoreboard
        private void ComputerPlayerScored()
        {
            computerScore++;
            computerScoreLabel.Text = computerScore.ToString(); 
        }
        private void StartButtonClick(object sender, EventArgs e)
        {
            StartValues(); 
            gameInProgress = true;
            startButton.Visible = false;
            humanPaddleTimer.Start();
            ballTimer.Start();
            computerPaddleTimer.Start(); 
        }
        private void EndGame()
        {
            humanPaddle.Location = new Point(0, 75);
            computerPaddle.Location = new Point(454, 75);
            gameInProgress = false;
            playerScore = 0;
            computerScore = 0;
            playerScoreLabel.Text = playerScore.ToString();
            computerScoreLabel.Text = computerScore.ToString();
            humanPaddleTimer.Stop();
            ballTimer.Stop();
            computerPaddleTimer.Stop();
            startButton.Visible = true; 
        }
    }
}
