namespace PongGame
{
    partial class PongForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PongForm));
            this.ball = new System.Windows.Forms.PictureBox();
            this.playerScoreLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.computerScoreLabel = new System.Windows.Forms.Label();
            this.startButton = new System.Windows.Forms.Button();
            this.humanPaddleTimer = new System.Windows.Forms.Timer(this.components);
            this.ballTimer = new System.Windows.Forms.Timer(this.components);
            this.computerPaddleTimer = new System.Windows.Forms.Timer(this.components);
            this.humanPaddle = new System.Windows.Forms.PictureBox();
            this.computerPaddle = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.ball)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.humanPaddle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.computerPaddle)).BeginInit();
            this.SuspendLayout();

            // Ball
            this.ball.BackColor = System.Drawing.Color.Black;
            this.ball.Location = new System.Drawing.Point(227, 120);
            this.ball.Name = "Ball";
            this.ball.Size = new System.Drawing.Size(15, 15);
            this.ball.TabIndex = 2;
            this.ball.TabStop = false;

            // playerScoreLabel
            this.playerScoreLabel.AutoSize = true;
            this.playerScoreLabel.Font = new System.Drawing.Font("MS Reference Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.playerScoreLabel.Location = new System.Drawing.Point(163, 9);
            this.playerScoreLabel.Name = "playerScoreLabel";
            this.playerScoreLabel.Size = new System.Drawing.Size(20, 20);
            this.playerScoreLabel.TabIndex = 3;
            this.playerScoreLabel.Text = "0";

            // label2
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("MS Reference Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(226, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = ":";

            // computerScoreLabel
            this.computerScoreLabel.AutoSize = true;
            this.computerScoreLabel.Font = new System.Drawing.Font("MS Reference Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.computerScoreLabel.Location = new System.Drawing.Point(287, 9);
            this.computerScoreLabel.Name = "computerScoreLabel";
            this.computerScoreLabel.Size = new System.Drawing.Size(20, 20);
            this.computerScoreLabel.TabIndex = 5;
            this.computerScoreLabel.Text = "0";

            // startButton
            this.startButton.Location = new System.Drawing.Point(196, 209);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(80, 40);
            this.startButton.TabIndex = 6;
            this.startButton.Text = "Start Game";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.StartButtonClick);
            
            // timer1
            this.humanPaddleTimer.Interval = 5;
            this.humanPaddleTimer.Tick += new System.EventHandler(this.MovePaddle);
            
            // timer2
            this.ballTimer.Interval = 5;
            this.ballTimer.Tick += new System.EventHandler(this.MoveBall);

            // timer3
            this.computerPaddleTimer.Interval = 5;
            this.computerPaddleTimer.Tick += new System.EventHandler(this.Computer);

            // Player
            this.humanPaddle.BackColor = System.Drawing.Color.Aqua;
            this.humanPaddle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.humanPaddle.Location = new System.Drawing.Point(0, 75);
            this.humanPaddle.Name = "Player";
            this.humanPaddle.Size = new System.Drawing.Size(30, 90);
            this.humanPaddle.TabIndex = 7;
            this.humanPaddle.TabStop = false;            
            
            // Computer player             
            this.computerPaddle.BackColor = System.Drawing.Color.Red;
            this.computerPaddle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.computerPaddle.Location = new System.Drawing.Point(454, 75);
            this.computerPaddle.Name = "PC";
            this.computerPaddle.Size = new System.Drawing.Size(30, 90);
            this.computerPaddle.TabIndex = 8;
            this.computerPaddle.TabStop = false;
            
            // PongForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(484, 261);
            this.Controls.Add(this.computerPaddle);
            this.Controls.Add(this.humanPaddle);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.computerScoreLabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.playerScoreLabel);
            this.Controls.Add(this.ball);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PongForm";
            this.Text = "Pong";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyPressed);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyReleased);
            ((System.ComponentModel.ISupportInitialize)(this.ball)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.humanPaddle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.computerPaddle)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox ball;
        private System.Windows.Forms.Label playerScoreLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label computerScoreLabel;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Timer humanPaddleTimer;
        private System.Windows.Forms.Timer ballTimer;
        private System.Windows.Forms.Timer computerPaddleTimer;
        private System.Windows.Forms.PictureBox humanPaddle;
        private System.Windows.Forms.PictureBox computerPaddle;
    }
}

