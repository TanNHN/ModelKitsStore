namespace ToysMagement
{
    partial class AdminForm
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
            this.btnOrderManagement = new System.Windows.Forms.Button();
            this.btnProductManagement = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnAccountManagement = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lb = new System.Windows.Forms.Label();
            this.lbAdminName = new System.Windows.Forms.Label();
            this.btnLogout = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOrderManagement
            // 
            this.btnOrderManagement.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnOrderManagement.Location = new System.Drawing.Point(214, 214);
            this.btnOrderManagement.Name = "btnOrderManagement";
            this.btnOrderManagement.Size = new System.Drawing.Size(317, 63);
            this.btnOrderManagement.TabIndex = 0;
            this.btnOrderManagement.Text = "Order Management";
            this.btnOrderManagement.UseVisualStyleBackColor = true;
            this.btnOrderManagement.Click += new System.EventHandler(this.btnOrderManagement_Click);
            // 
            // btnProductManagement
            // 
            this.btnProductManagement.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.btnProductManagement.Location = new System.Drawing.Point(214, 111);
            this.btnProductManagement.Name = "btnProductManagement";
            this.btnProductManagement.Size = new System.Drawing.Size(317, 63);
            this.btnProductManagement.TabIndex = 1;
            this.btnProductManagement.Text = "Product Management";
            this.btnProductManagement.UseVisualStyleBackColor = true;
            this.btnProductManagement.Click += new System.EventHandler(this.btnProductManagement_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnAccountManagement);
            this.groupBox1.Controls.Add(this.btnProductManagement);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnOrderManagement);
            this.groupBox1.Location = new System.Drawing.Point(21, 98);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(767, 407);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            // 
            // btnAccountManagement
            // 
            this.btnAccountManagement.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnAccountManagement.Location = new System.Drawing.Point(214, 321);
            this.btnAccountManagement.Name = "btnAccountManagement";
            this.btnAccountManagement.Size = new System.Drawing.Size(317, 63);
            this.btnAccountManagement.TabIndex = 2;
            this.btnAccountManagement.Text = "Account Management";
            this.btnAccountManagement.UseVisualStyleBackColor = true;
            this.btnAccountManagement.Click += new System.EventHandler(this.btnAccountManagement_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label1.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label1.Location = new System.Drawing.Point(147, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(435, 58);
            this.label1.TabIndex = 0;
            this.label1.Text = "Manage Resource";
            // 
            // lb
            // 
            this.lb.AutoSize = true;
            this.lb.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lb.Location = new System.Drawing.Point(16, 21);
            this.lb.Name = "lb";
            this.lb.Size = new System.Drawing.Size(127, 29);
            this.lb.TabIndex = 6;
            this.lb.Text = "Welcome:";
            // 
            // lbAdminName
            // 
            this.lbAdminName.AutoSize = true;
            this.lbAdminName.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lbAdminName.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lbAdminName.Location = new System.Drawing.Point(149, 21);
            this.lbAdminName.Name = "lbAdminName";
            this.lbAdminName.Size = new System.Drawing.Size(0, 29);
            this.lbAdminName.TabIndex = 7;
            // 
            // btnLogout
            // 
            this.btnLogout.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnLogout.Location = new System.Drawing.Point(657, 17);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(131, 38);
            this.btnLogout.TabIndex = 3;
            this.btnLogout.Text = "Log out";
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // AdminForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 517);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.lbAdminName);
            this.Controls.Add(this.lb);
            this.Controls.Add(this.groupBox1);
            this.Name = "AdminForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AdminForm";
            this.Load += new System.EventHandler(this.AdminForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOrderManagement;
        private System.Windows.Forms.Button btnProductManagement;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAccountManagement;
        private System.Windows.Forms.Label lb;
        private System.Windows.Forms.Button btnLogout;
        public System.Windows.Forms.Label lbAdminName;
    }
}