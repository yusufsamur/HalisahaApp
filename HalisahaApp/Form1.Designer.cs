namespace HalisahaApp
{
    partial class loginPanel
    {
        /// <summary>
        ///Gerekli tasarımcı değişkeni.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///Kullanılan tüm kaynakları temizleyin.
        /// </summary>
        ///<param name="disposing">yönetilen kaynaklar dispose edilmeliyse doğru; aksi halde yanlış.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer üretilen kod

        /// <summary>
        /// Tasarımcı desteği için gerekli metot - bu metodun 
        ///içeriğini kod düzenleyici ile değiştirmeyin.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(loginPanel));
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.girisyapbtn = new System.Windows.Forms.Button();
            this.kayitolbtn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.fileSystemWatcher1 = new System.IO.FileSystemWatcher();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox1.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBox1.Location = new System.Drawing.Point(473, 213);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(166, 20);
            this.textBox1.TabIndex = 0;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(473, 263);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(166, 20);
            this.textBox2.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 20F);
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label1.Location = new System.Drawing.Point(234, 122);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(485, 35);
            this.label1.TabIndex = 2;
            this.label1.Text = "Halısaha Rezervasyon Uygulaması";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // girisyapbtn
            // 
            this.girisyapbtn.BackColor = System.Drawing.Color.ForestGreen;
            this.girisyapbtn.FlatAppearance.BorderSize = 0;
            this.girisyapbtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.girisyapbtn.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.girisyapbtn.Location = new System.Drawing.Point(503, 323);
            this.girisyapbtn.Name = "girisyapbtn";
            this.girisyapbtn.Size = new System.Drawing.Size(136, 38);
            this.girisyapbtn.TabIndex = 4;
            this.girisyapbtn.Text = "Giriş Yap";
            this.girisyapbtn.UseVisualStyleBackColor = false;
            this.girisyapbtn.Click += new System.EventHandler(this.girisyapbtn_Click);
            this.girisyapbtn.MouseEnter += new System.EventHandler(this.girisyapbtn_MouseEnter);
            this.girisyapbtn.MouseLeave += new System.EventHandler(this.girisyapbtn_MouseLeave);
            this.girisyapbtn.MouseUp += new System.Windows.Forms.MouseEventHandler(this.girisyapbtn_MouseUp);
            // 
            // kayitolbtn
            // 
            this.kayitolbtn.BackColor = System.Drawing.Color.ForestGreen;
            this.kayitolbtn.FlatAppearance.BorderSize = 0;
            this.kayitolbtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.kayitolbtn.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.kayitolbtn.Location = new System.Drawing.Point(299, 323);
            this.kayitolbtn.Name = "kayitolbtn";
            this.kayitolbtn.Size = new System.Drawing.Size(136, 38);
            this.kayitolbtn.TabIndex = 5;
            this.kayitolbtn.Text = "Kayıt Ol";
            this.kayitolbtn.UseVisualStyleBackColor = false;
            this.kayitolbtn.MouseEnter += new System.EventHandler(this.kayitolbtn_MouseEnter);
            this.kayitolbtn.MouseLeave += new System.EventHandler(this.kayitolbtn_MouseLeave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Candara", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.ForeColor = System.Drawing.SystemColors.ActiveBorder;
            this.label2.Location = new System.Drawing.Point(295, 213);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 19);
            this.label2.TabIndex = 6;
            this.label2.Text = "Kullanıcı Adı";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Candara", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label4.ForeColor = System.Drawing.SystemColors.ActiveBorder;
            this.label4.Location = new System.Drawing.Point(295, 266);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 19);
            this.label4.TabIndex = 8;
            this.label4.Text = "Şifre";
            // 
            // fileSystemWatcher1
            // 
            this.fileSystemWatcher1.EnableRaisingEvents = true;
            this.fileSystemWatcher1.SynchronizingObject = this;
            // 
            // loginPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(110F, 110F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.ClientSize = new System.Drawing.Size(946, 482);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.kayitolbtn);
            this.Controls.Add(this.girisyapbtn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "loginPanel";
            this.Text = "Halısaha Rezervasyon";
            this.Load += new System.EventHandler(this.loginPanel_Load);
            this.MouseEnter += new System.EventHandler(this.Form1_MouseEnter);
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button girisyapbtn;
        private System.Windows.Forms.Button kayitolbtn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.IO.FileSystemWatcher fileSystemWatcher1;
    }
}

