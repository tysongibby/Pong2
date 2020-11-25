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
            InitializeComponent(); // Do not modify
            this.KeyPreview = true;
        }

        // ** Intialize major application components **
        const int limitPad = 170;
        const int limitBall = 245;
        const int x = 277;  // stores ball's x starting posistion?
        const int y = 120;  // stores ball's y starting position?
        int computerPlayerScore = 0;  // stores computer's score
        int humanPlayerScore = 0;  // stores player's score
        int humanPlayerPaddleSpeed = 3; // stores movment speed of human player paddle
        int yBallPosition;  // stores ball's vertical position (up and down)
        int xBallPosition; // stores ball's horizontal position (left and right)
        bool moveUp = false;  // stores if player wants to move paddle up
        bool moveDown = false;  // stores if player wants to move paddle down
        bool gameInProgress = false;  // tracks if game is in progress
        readonly Random randomPositionGenerator = new Random(Guid.NewGuid().GetHashCode()); // create random position generator


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

        // toggles paddle movement to false when up or down keys are released
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
        
        // moves the human paddle to a new position by the amount stored in humanPlayerPaddleSpeed
        private void MoveHumanPaddle(object sender, EventArgs e)
        {           
            if (moveUp && humanPlayerPaddle.Location.Y > 0)
            {
                humanPlayerPaddle.Top = humanPlayerPaddle.Top - humanPlayerPaddleSpeed; 
            }
            else if (moveDown && humanPlayerPaddle.Location.Y < limitPad)
            {
                humanPlayerPaddle.Top = humanPlayerPaddle.Top + humanPlayerPaddleSpeed;
            }
        }

        // is called evertime ball moves to determine new ball position and direction after a collision based on what it has collided with
        private void MoveBall(object sender, EventArgs e)
        {
            if (ball.Bounds.IntersectsWith(humanPlayerPaddle.Bounds))
            {
                Collision(humanPlayerPaddle);
            }
            else if (ball.Bounds.IntersectsWith(computerPlayerPaddle.Bounds))
            {
                Collision(computerPlayerPaddle);
            }
            CheckIfCollidedWithBorder();  // checks to see if ball has collided with border
            CheckIfBallLeftField();  // checks to see if the ball left the field
            NewBallPosition();
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
                    yBallPosition = randomPositionGenerator.Next(2, 3); // sets yBallPosition to a random integer from 2 to 3
                    xBallPosition = AdjustCoordinates(6, 7); 
                    break;
                case true when Bot(Paddle):
                    yBallPosition = randomPositionGenerator.Next(4, 6); // sets yBallPosition to a random integer from 4 to 6
                    xBallPosition = AdjustCoordinates(5, 6); // sets xBallPosition to a random integer from 5 to 6
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
                res = randomPositionGenerator.Next(i, n);
            }
            else if (ball.Location.X > this.Width / 2)
            {
                res = Negative(i, n);
            }
            return res;
        }

        private int Negative(int i,int n)
        {
            int myval = randomPositionGenerator.Next(i, n) * -1;
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

        // checks if ball collides with a border
        private void CheckIfCollidedWithBorder()
        {
            // if the ball collides with a border, change to the opposite the direction
            if (ball.Location.Y <= 0 || ball.Location.Y >= limitBall)
            {               
                yBallPosition = yBallPosition * -1;
            }
        }

        // checks to see ball left the field of play
        private void CheckIfBallLeftField()
        {
            // end game when a score is 10
            if (humanPlayerScore == 10 || computerPlayerScore == 10)
            {
                EndGame();  // initiates end of game processes
            }

            // check to see if the computer player has scored
            if (ball.Location.X < 0 - humanPlayerPaddle.Width && ball.Location.X < this.Width / 2)
            {
                RespawnBall(5);  // initiates ball respawn
                ComputerPlayerScored(); // initiates computer player score incrementation
            }
            // check to see if the computer player has scored
            else if (ball.Location.X > computerPlayerPaddle.Location.X + computerPlayerPaddle.Width && ball.Location.X > this.Width / 2)
            {
                RespawnBall(-5);  // initiates ball respawn
                HumanPlayerScored(); // initiates human player score incrementation
            }
        }

        // determine what happens when ball impacts a paddle
        private void PaddleImpact()
        {
            if (ball.Location.X < this.Width / 2)
            {
                if (ball.Location.X < 0 + ball.Height / 3)
                {                  
                    xBallPosition = xBallPosition * -1; // change ball direction to the opposite direction
                }
            }
            else if (ball.Location.X > this.Width / 2)
            {
                if (ball.Location.X > computerPlayerPaddle.Location.X + (ball.Width /3))
                {                   
                    xBallPosition = xBallPosition * -1; // change ball direction to the opposite direction
                }
            }
        }

        // respawns ball at starting position after score takes place
        private void RespawnBall(int _initialHorizontalMovement)
        { 
            ball.Location = new Point(x, y);
            yBallPosition = 0;
            xBallPosition = _initialHorizontalMovement;
        }

        // sets ball starting values
        private void StartValues()
        {
            yBallPosition = 0;
            xBallPosition = -5; 
        }
        
        // sets new ball position
        private void NewBallPosition()
        {
            ball.Top = ball.Top + yBallPosition;
            ball.Left = ball.Left + xBallPosition; 
        }


        private void Computer(object sender, EventArgs e)
        {
            if (computerPlayerPaddle.Location.Y <= 0)
            {
                computerPlayerPaddle.Location = new Point(computerPlayerPaddle.Location.X, 0); 
            }
            else if (computerPlayerPaddle.Location.Y >= limitPad)
            {
                computerPlayerPaddle.Location = new Point(computerPlayerPaddle.Location.X, limitPad);
            }
            if (ball.Location.Y < computerPlayerPaddle.Top + (computerPlayerPaddle.Height / 2))
            {
                computerPlayerPaddle.Top = computerPlayerPaddle.Top - 3;
            }
            else if (ball.Location.Y > computerPlayerPaddle.Top + (computerPlayerPaddle.Height / 2))
            {
                computerPlayerPaddle.Top = computerPlayerPaddle.Top + 3;
            }
        }

        // Increments the human player's score
        private void HumanPlayerScored()
        {
            humanPlayerScore = humanPlayerScore + 1; // increments human player score by 1
            humanPlayerScoreLabel.Text = humanPlayerScore.ToString(); // displays human player score
        }

        // increments ComputerPlayer's score and updates the scoreboard
        private void ComputerPlayerScored()
        {
            computerPlayerScore = computerPlayerScore + 1; // increments computer player score by 1
            computerPlayerScoreLabel.Text = computerPlayerScore.ToString(); // displays computer player score
        }

        // Starts the game
        private void StartButtonClick(object sender, EventArgs e)
        {
            StartValues(); //  sets the starting x and y values for the ball
            gameInProgress = true;  
            startButton.Visible = false;  // hides start button
            humanPaddleTimer.Start();  // starts human paddle movement
            ballTimer.Start(); // starts ball movement
            computerPaddleTimer.Start();  // starts computer paddle movement
        }

        // ends the game
        private void EndGame()
        {
            humanPlayerPaddle.Location = new Point(0, 75);
            computerPlayerPaddle.Location = new Point(454, 75);
            gameInProgress = false;
            humanPlayerScore = 0;
            computerPlayerScore = 0;
            humanPlayerScoreLabel.Text = humanPlayerScore.ToString();
            computerPlayerScoreLabel.Text = computerPlayerScore.ToString();
            humanPaddleTimer.Stop();  // stops human paddle movement
            ballTimer.Stop();  // stops ball movement
            computerPaddleTimer.Stop();  // stops computer paddle movement
            startButton.Visible = true; 
        }
    }
}
