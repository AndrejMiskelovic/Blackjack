namespace Blackjack
{
    partial class Form1
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
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.Bet = new System.Windows.Forms.Button();
            this.Hit = new System.Windows.Forms.Button();
            this.Stand = new System.Windows.Forms.Button();
            this.Surrender = new System.Windows.Forms.Button();
            this.Double = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(12, 12);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(276, 225);
            this.listBox1.TabIndex = 0;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(294, 12);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 2;
            // 
            // Bet
            // 
            this.Bet.Location = new System.Drawing.Point(294, 38);
            this.Bet.Name = "Bet";
            this.Bet.Size = new System.Drawing.Size(100, 23);
            this.Bet.TabIndex = 3;
            this.Bet.Text = "Bet";
            this.Bet.UseVisualStyleBackColor = true;
            this.Bet.Click += new System.EventHandler(this.Bet_Click);
            // 
            // Hit
            // 
            this.Hit.Location = new System.Drawing.Point(12, 261);
            this.Hit.Name = "Hit";
            this.Hit.Size = new System.Drawing.Size(75, 23);
            this.Hit.TabIndex = 4;
            this.Hit.Text = "Hit";
            this.Hit.UseVisualStyleBackColor = true;
            this.Hit.Click += new System.EventHandler(this.Hit_Click);
            // 
            // Stand
            // 
            this.Stand.Location = new System.Drawing.Point(93, 261);
            this.Stand.Name = "Stand";
            this.Stand.Size = new System.Drawing.Size(75, 23);
            this.Stand.TabIndex = 5;
            this.Stand.Text = "Stand";
            this.Stand.UseVisualStyleBackColor = true;
            this.Stand.Click += new System.EventHandler(this.Stand_Click);
            // 
            // Surrender
            // 
            this.Surrender.Location = new System.Drawing.Point(174, 261);
            this.Surrender.Name = "Surrender";
            this.Surrender.Size = new System.Drawing.Size(75, 23);
            this.Surrender.TabIndex = 6;
            this.Surrender.Text = "Surrender";
            this.Surrender.UseVisualStyleBackColor = true;
            this.Surrender.Click += new System.EventHandler(this.Surrender_Click);
            // 
            // Double
            // 
            this.Double.Location = new System.Drawing.Point(255, 261);
            this.Double.Name = "Double";
            this.Double.Size = new System.Drawing.Size(75, 23);
            this.Double.TabIndex = 7;
            this.Double.Text = "Double";
            this.Double.UseVisualStyleBackColor = true;
            this.Double.Click += new System.EventHandler(this.Double_Click);
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(15, 303);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(515, 112);
            this.panel1.TabIndex = 14;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 287);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "Player Hand";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 418);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 16;
            this.label2.Text = "Dealer Hand";
            // 
            // panel3
            // 
            this.panel3.Location = new System.Drawing.Point(15, 434);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(515, 112);
            this.panel3.TabIndex = 15;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 240);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 17;
            this.label3.Text = "Help";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(536, 551);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.Double);
            this.Controls.Add(this.Surrender);
            this.Controls.Add(this.Stand);
            this.Controls.Add(this.Hit);
            this.Controls.Add(this.Bet);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.listBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button Bet;
        private System.Windows.Forms.Button Hit;
        private System.Windows.Forms.Button Stand;
        private System.Windows.Forms.Button Surrender;
        private System.Windows.Forms.Button Double;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label3;
    }
}

