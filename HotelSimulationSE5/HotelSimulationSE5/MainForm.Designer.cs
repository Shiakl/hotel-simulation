namespace HotelSimulationSE5
{
    partial class MainForm
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
            this.StopButton = new System.Windows.Forms.Button();
            this.EventButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // GuestButton
            // 
            this.GuestButton.Location = new System.Drawing.Point(12, 12);
            this.GuestButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.GuestButton.Name = "GuestButton";
            this.GuestButton.Size = new System.Drawing.Size(100, 23);
            this.GuestButton.TabIndex = 0;
            this.GuestButton.Text = "Spawn Guest";
            this.GuestButton.UseVisualStyleBackColor = true;
            this.GuestButton.Click += new System.EventHandler(this.GuestButton_Click);
            // 
            // StopButton
            // 
            this.StopButton.Location = new System.Drawing.Point(131, 11);
            this.StopButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.StopButton.Name = "StopButton";
            this.StopButton.Size = new System.Drawing.Size(75, 23);
            this.StopButton.TabIndex = 2;
            this.StopButton.Text = "Pause";
            this.StopButton.UseVisualStyleBackColor = true;
            this.StopButton.Click += new System.EventHandler(this.Stop_Click);
            // 
            // EventButton
            // 
            this.EventButton.Location = new System.Drawing.Point(236, 11);
            this.EventButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.EventButton.Name = "EventButton";
            this.EventButton.Size = new System.Drawing.Size(85, 23);
            this.EventButton.TabIndex = 3;
            this.EventButton.Text = "Event start";
            this.EventButton.UseVisualStyleBackColor = true;
            this.EventButton.Click += new System.EventHandler(this.EventButton_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(393, 20);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.Black;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.EventButton);
            this.Controls.Add(this.StopButton);
            this.Controls.Add(this.GuestButton);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "MainForm";
            this.Text = "Alternatieve Hilton";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button GuestButton;
        private System.Windows.Forms.Button StopButton;
        private System.Windows.Forms.Button EventButton;
        private System.Windows.Forms.Button button1;
    }
}

