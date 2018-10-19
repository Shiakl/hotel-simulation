namespace HotelSimulationSE5
{
    partial class HotelForm
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
            this.GuestButton = new System.Windows.Forms.Button();
            this.PauseButton = new System.Windows.Forms.Button();
            this.EventButton = new System.Windows.Forms.Button();
            this.PathButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // GuestButton
            // 
            this.GuestButton.Location = new System.Drawing.Point(28, 30);
            this.GuestButton.Name = "GuestButton";
            this.GuestButton.Size = new System.Drawing.Size(75, 23);
            this.GuestButton.TabIndex = 0;
            this.GuestButton.Text = "Create Guest";
            this.GuestButton.UseVisualStyleBackColor = true;
            this.GuestButton.Click += new System.EventHandler(this.GuestButton_Click);
            // 
            // PauseButton
            // 
            this.PauseButton.Location = new System.Drawing.Point(131, 30);
            this.PauseButton.Name = "PauseButton";
            this.PauseButton.Size = new System.Drawing.Size(75, 23);
            this.PauseButton.TabIndex = 1;
            this.PauseButton.Text = "Pause";
            this.PauseButton.UseVisualStyleBackColor = true;
            this.PauseButton.Click += new System.EventHandler(this.PauseButton_Click);
            // 
            // EventButton
            // 
            this.EventButton.Location = new System.Drawing.Point(254, 30);
            this.EventButton.Name = "EventButton";
            this.EventButton.Size = new System.Drawing.Size(75, 23);
            this.EventButton.TabIndex = 2;
            this.EventButton.Text = "Event Start";
            this.EventButton.UseVisualStyleBackColor = true;
            this.EventButton.Click += new System.EventHandler(this.EventButton_Click);
            // 
            // PathButton
            // 
            this.PathButton.Location = new System.Drawing.Point(356, 30);
            this.PathButton.Name = "PathButton";
            this.PathButton.Size = new System.Drawing.Size(75, 23);
            this.PathButton.TabIndex = 3;
            this.PathButton.Text = "Set Path";
            this.PathButton.UseVisualStyleBackColor = true;
            this.PathButton.Click += new System.EventHandler(this.PathButton_Click);
            // 
            // HotelForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.PathButton);
            this.Controls.Add(this.EventButton);
            this.Controls.Add(this.PauseButton);
            this.Controls.Add(this.GuestButton);
            this.DoubleBuffered = true;
            this.Name = "HotelForm";
            this.Text = "HotelForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button GuestButton;
        private System.Windows.Forms.Button PauseButton;
        private System.Windows.Forms.Button EventButton;
        private System.Windows.Forms.Button PathButton;
    }
}