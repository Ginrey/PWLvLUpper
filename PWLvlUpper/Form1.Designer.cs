using System.Drawing;

namespace PWLvlUpper
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.GroupBox groupBox1;

        private System.Windows.Forms.TextBox textBoxProxyPassword;

        private System.Windows.Forms.TextBox textBoxProxyLogin;

        private System.Windows.Forms.Label label3;

        private System.Windows.Forms.Button buttonOpenProxy;

        private System.Windows.Forms.Label label2;

        private System.Windows.Forms.TextBox textBoxProxyPath;

        private System.Windows.Forms.RadioButton radioButtonProxySocks5;

        private System.Windows.Forms.RadioButton radioButtonProxySocks4;

        private System.Windows.Forms.Label label1;

        private System.Windows.Forms.RadioButton radioButtonProxyNone;

        private System.Windows.Forms.GroupBox groupBox2;

        private System.Windows.Forms.Button buttonOpenAccs;

        private System.Windows.Forms.Label label7;

        private System.Windows.Forms.TextBox textBoxAccsPath;

        private System.Windows.Forms.Button buttonSaveDb;

        private System.Windows.Forms.Label label9;

        private System.Windows.Forms.GroupBox groupBox3;

        public System.Windows.Forms.TextBox textBoxPathSave;

        private System.Windows.Forms.RadioButton radioButtonProxyHTTP;

        private System.Windows.Forms.CheckBox checkBoxProxyUrl;

        private System.Windows.Forms.Label label4;

        private System.Windows.Forms.TextBox textBoxProxyUrl;

        private System.Windows.Forms.Label label5;

        private System.Windows.Forms.NumericUpDown numericThreadsCount;

        private System.Windows.Forms.Label labelInvalids;

        private System.Windows.Forms.Label labelValids;

        private System.Windows.Forms.Label label8;

        private System.Windows.Forms.Label label11;

        private System.Windows.Forms.Label labelProgress;

        private System.Windows.Forms.Label labelProxyCount;

        private System.Windows.Forms.Label label16;

        private System.Windows.Forms.Label label17;

        private System.Windows.Forms.ProgressBar progressBar1;

        private System.Windows.Forms.Button buttonStart;

        //  private VisualStyler visualStyler1;

        private System.Windows.Forms.Label labelMeditation;

        private System.Windows.Forms.Label label10;

        private System.Windows.Forms.Label label19;

        private System.Windows.Forms.NumericUpDown numericMinLvl;

        private System.Windows.Forms.Label label18;

        private System.Windows.Forms.ComboBox comboBoxServer;


        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager componentResourceManager = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.labelMeditation = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.numericMinLvl = new System.Windows.Forms.NumericUpDown();
            this.label18 = new System.Windows.Forms.Label();
            this.comboBoxServer = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.numericThreadsCount = new System.Windows.Forms.NumericUpDown();
            this.labelInvalids = new System.Windows.Forms.Label();
            this.labelValids = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.labelProgress = new System.Windows.Forms.Label();
            this.labelProxyCount = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.buttonStart = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.buttonSaveDb = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.textBoxPathSave = new System.Windows.Forms.TextBox();
            this.buttonOpenAccs = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxAccsPath = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBoxProxyUrl = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxProxyUrl = new System.Windows.Forms.TextBox();
            this.radioButtonProxyHTTP = new System.Windows.Forms.RadioButton();
            this.textBoxProxyPassword = new System.Windows.Forms.TextBox();
            this.textBoxProxyLogin = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonOpenProxy = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxProxyPath = new System.Windows.Forms.TextBox();
            this.radioButtonProxySocks5 = new System.Windows.Forms.RadioButton();
            this.radioButtonProxySocks4 = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.radioButtonProxyNone = new System.Windows.Forms.RadioButton();
          //  this.visualStyler1 = new VisualStyler(this.components);
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)this.numericMinLvl).BeginInit();
            ((System.ComponentModel.ISupportInitialize)this.numericThreadsCount).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
         //   this.visualStyler1.BeginInit();
            base.SuspendLayout();
            this.groupBox3.Controls.Add(this.labelMeditation);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.label19);
            this.groupBox3.Controls.Add(this.numericMinLvl);
            this.groupBox3.Controls.Add(this.label18);
            this.groupBox3.Controls.Add(this.comboBoxServer);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.numericThreadsCount);
            this.groupBox3.Controls.Add(this.labelInvalids);
            this.groupBox3.Controls.Add(this.labelValids);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.labelProgress);
            this.groupBox3.Controls.Add(this.labelProxyCount);
            this.groupBox3.Controls.Add(this.label16);
            this.groupBox3.Controls.Add(this.label17);
            this.groupBox3.Controls.Add(this.progressBar1);
            this.groupBox3.Controls.Add(this.buttonStart);
            this.groupBox3.Location = new Point(12, 230);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new Size(590, 179);
            this.groupBox3.TabIndex = 15;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Запуск";
            this.labelMeditation.AutoSize = true;
            this.labelMeditation.Location = new Point(445, 89);
            this.labelMeditation.Name = "labelMeditation";
            this.labelMeditation.Size = new Size(13, 13);
            this.labelMeditation.TabIndex = 35;
            this.labelMeditation.Text = "0";
            this.label10.AutoSize = true;
            this.label10.Location = new Point(305, 63);
            this.label10.Name = "label10";
            this.label10.Size = new Size(68, 13);
            this.label10.TabIndex = 34;
            this.label10.Text = "Обработано";
            this.label19.AutoSize = true;
            this.label19.Location = new Point(21, 113);
            this.label19.Name = "label19";
            this.label19.Size = new Size(52, 13);
            this.label19.TabIndex = 33;
            this.label19.Text = "Мин. лвл";
            this.numericMinLvl.Location = new Point(193, 111);
            System.Windows.Forms.NumericUpDown arg_588_0 = this.numericMinLvl;
            int[] array = new int[4];
            array[0] = 9999;
          
            System.Windows.Forms.NumericUpDown arg_5A5_0 = this.numericMinLvl;
            int[] array2 = new int[4];
            array2[0] = 31;
         
            this.numericMinLvl.Name = "numericMinLvl";
            this.numericMinLvl.Size = new Size(57, 20);
            this.numericMinLvl.TabIndex = 32;
            this.numericMinLvl.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            System.Windows.Forms.NumericUpDown arg_5FF_0 = this.numericMinLvl;
            int[] array3 = new int[4];
            array3[0] = 31;
         
            this.label18.AutoSize = true;
            this.label18.Location = new Point(305, 33);
            this.label18.Name = "label18";
            this.label18.Size = new Size(44, 13);
            this.label18.TabIndex = 31;
            this.label18.Text = "Сервер";
            this.comboBoxServer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxServer.FormattingEnabled = true;
            this.comboBoxServer.Location = new Point(448, 31);
            this.comboBoxServer.Name = "comboBoxServer";
            this.comboBoxServer.Size = new Size(121, 21);
            this.comboBoxServer.TabIndex = 30;
            this.label5.AutoSize = true;
            this.label5.Location = new Point(16, 33);
            this.label5.Name = "label5";
            this.label5.Size = new Size(110, 13);
            this.label5.TabIndex = 29;
            this.label5.Text = "Количество потоков";
            this.numericThreadsCount.Location = new Point(194, 31);
            System.Windows.Forms.NumericUpDown arg_75E_0 = this.numericThreadsCount;
            int[] array4 = new int[4];
            array4[0] = 4000;
           
            System.Windows.Forms.NumericUpDown arg_77D_0 = this.numericThreadsCount;
            int[] array5 = new int[4];
            array5[0] = 1;
         
            this.numericThreadsCount.Name = "numericThreadsCount";
            this.numericThreadsCount.Size = new Size(56, 20);
            this.numericThreadsCount.TabIndex = 28;
            this.numericThreadsCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            System.Windows.Forms.NumericUpDown arg_7D9_0 = this.numericThreadsCount;
            int[] array6 = new int[4];
            array6[0] = 1;
          
            this.labelInvalids.AutoSize = true;
            this.labelInvalids.Location = new Point(445, 115);
            this.labelInvalids.Name = "labelInvalids";
            this.labelInvalids.Size = new Size(13, 13);
            this.labelInvalids.TabIndex = 27;
            this.labelInvalids.Text = "0";
            this.labelValids.AutoSize = true;
            this.labelValids.Location = new Point(196, 89);
            this.labelValids.Name = "labelValids";
            this.labelValids.Size = new Size(13, 13);
            this.labelValids.TabIndex = 26;
            this.labelValids.Text = "0";
            this.label8.AutoSize = true;
            this.label8.Location = new Point(305, 115);
            this.label8.Name = "label8";
            this.label8.Size = new Size(47, 13);
            this.label8.TabIndex = 25;
            this.label8.Text = "Ошибка";
            this.label11.AutoSize = true;
            this.label11.Location = new Point(21, 89);
            this.label11.Name = "label11";
            this.label11.Size = new Size(80, 13);
            this.label11.TabIndex = 24;
            this.label11.Text = "Успешно (40+)";
            this.labelProgress.AutoSize = true;
            this.labelProgress.Location = new Point(445, 63);
            this.labelProgress.Name = "labelProgress";
            this.labelProgress.Size = new Size(13, 13);
            this.labelProgress.TabIndex = 23;
            this.labelProgress.Text = "0";
            this.labelProxyCount.AutoSize = true;
            this.labelProxyCount.Location = new Point(196, 63);
            this.labelProxyCount.Name = "labelProxyCount";
            this.labelProxyCount.Size = new Size(13, 13);
            this.labelProxyCount.TabIndex = 22;
            this.labelProxyCount.Text = "0";
            this.label16.AutoSize = true;
            this.label16.Location = new Point(305, 89);
            this.label16.Name = "label16";
            this.label16.Size = new Size(69, 13);
            this.label16.TabIndex = 21;
            this.label16.Text = "Медитируют";
            this.label17.AutoSize = true;
            this.label17.Location = new Point(16, 63);
            this.label17.Name = "label17";
            this.label17.Size = new Size(105, 13);
            this.label17.TabIndex = 20;
            this.label17.Text = "Количество прокси";
            this.progressBar1.Location = new Point(19, 142);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new Size(464, 23);
            this.progressBar1.TabIndex = 19;
            this.buttonStart.Location = new Point(490, 142);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new Size(79, 23);
            this.buttonStart.TabIndex = 18;
            this.buttonStart.Text = "Начать";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            this.groupBox2.Controls.Add(this.buttonSaveDb);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.textBoxPathSave);
            this.groupBox2.Controls.Add(this.buttonOpenAccs);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.textBoxAccsPath);
            this.groupBox2.Location = new Point(12, 141);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(590, 83);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Учетные записи";
            this.buttonSaveDb.Location = new Point(487, 47);
            this.buttonSaveDb.Name = "buttonSaveDb";
            this.buttonSaveDb.Size = new Size(75, 23);
            this.buttonSaveDb.TabIndex = 9;
            this.buttonSaveDb.Text = "Выбрать";
            this.buttonSaveDb.UseVisualStyleBackColor = true;
            this.buttonSaveDb.Click += new System.EventHandler(this.buttonSettings_Click);
            this.label9.AutoSize = true;
            this.label9.Location = new Point(14, 52);
            this.label9.Name = "label9";
            this.label9.Size = new Size(161, 13);
            this.label9.TabIndex = 8;
            this.label9.Text = "Папка сохранения результата";
            this.textBoxPathSave.Location = new Point(191, 49);
            this.textBoxPathSave.Name = "textBoxPathSave";
            this.textBoxPathSave.Size = new Size(290, 20);
            this.textBoxPathSave.TabIndex = 7;
            this.buttonOpenAccs.Location = new Point(487, 21);
            this.buttonOpenAccs.Name = "buttonOpenAccs";
            this.buttonOpenAccs.Size = new Size(75, 23);
            this.buttonOpenAccs.TabIndex = 6;
            this.buttonOpenAccs.Text = "Выбрать";
            this.buttonOpenAccs.UseVisualStyleBackColor = true;
            this.buttonOpenAccs.Click += new System.EventHandler(this.buttonSettings_Click);
            this.label7.AutoSize = true;
            this.label7.Location = new Point(13, 26);
            this.label7.Name = "label7";
            this.label7.Size = new Size(99, 13);
            this.label7.TabIndex = 5;
            this.label7.Text = "Список аккаунтов";
            this.textBoxAccsPath.Location = new Point(191, 23);
            this.textBoxAccsPath.Name = "textBoxAccsPath";
            this.textBoxAccsPath.Size = new Size(290, 20);
            this.textBoxAccsPath.TabIndex = 4;
            this.groupBox1.Controls.Add(this.checkBoxProxyUrl);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.textBoxProxyUrl);
            this.groupBox1.Controls.Add(this.radioButtonProxyHTTP);
            this.groupBox1.Controls.Add(this.textBoxProxyPassword);
            this.groupBox1.Controls.Add(this.textBoxProxyLogin);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.buttonOpenProxy);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.textBoxProxyPath);
            this.groupBox1.Controls.Add(this.radioButtonProxySocks5);
            this.groupBox1.Controls.Add(this.radioButtonProxySocks4);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.radioButtonProxyNone);
            this.groupBox1.Location = new Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(590, 123);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Прокси";
            this.checkBoxProxyUrl.AutoSize = true;
            this.checkBoxProxyUrl.Location = new Point(489, 94);
            this.checkBoxProxyUrl.Name = "checkBoxProxyUrl";
            this.checkBoxProxyUrl.Size = new Size(77, 17);
            this.checkBoxProxyUrl.TabIndex = 19;
            this.checkBoxProxyUrl.Text = "Вкл/Выкл";
            this.checkBoxProxyUrl.UseVisualStyleBackColor = true;
            this.checkBoxProxyUrl.CheckedChanged += new System.EventHandler(this.checkBox_Trigger);
            this.label4.AutoSize = true;
            this.label4.Location = new Point(16, 95);
            this.label4.Name = "label4";
            this.label4.Size = new Size(139, 13);
            this.label4.TabIndex = 18;
            this.label4.Text = "Список прокси по ссылке";
            this.textBoxProxyUrl.AllowDrop = true;
            this.textBoxProxyUrl.Location = new Point(193, 92);
            this.textBoxProxyUrl.Name = "textBoxProxyUrl";
            this.textBoxProxyUrl.Size = new Size(290, 20);
            this.textBoxProxyUrl.TabIndex = 17;
            this.radioButtonProxyHTTP.AutoSize = true;
            this.radioButtonProxyHTTP.Location = new Point(426, 17);
            this.radioButtonProxyHTTP.Name = "radioButtonProxyHTTP";
            this.radioButtonProxyHTTP.Size = new Size(54, 17);
            this.radioButtonProxyHTTP.TabIndex = 13;
            this.radioButtonProxyHTTP.Text = "HTTP";
            this.radioButtonProxyHTTP.UseVisualStyleBackColor = true;
            this.radioButtonProxyHTTP.Click += new System.EventHandler(this.radioButton_Click);
            this.textBoxProxyPassword.Location = new Point(308, 40);
            this.textBoxProxyPassword.Name = "textBoxProxyPassword";
            this.textBoxProxyPassword.Size = new Size(108, 20);
            this.textBoxProxyPassword.TabIndex = 9;
            this.textBoxProxyLogin.Location = new Point(193, 40);
            this.textBoxProxyLogin.Name = "textBoxProxyLogin";
            this.textBoxProxyLogin.Size = new Size(109, 20);
            this.textBoxProxyLogin.TabIndex = 8;
            this.label3.AutoSize = true;
            this.label3.Location = new Point(16, 43);
            this.label3.Name = "label3";
            this.label3.Size = new Size(150, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Авторизация [логин пароль]";
            this.buttonOpenProxy.Location = new Point(489, 64);
            this.buttonOpenProxy.Name = "buttonOpenProxy";
            this.buttonOpenProxy.Size = new Size(75, 23);
            this.buttonOpenProxy.TabIndex = 6;
            this.buttonOpenProxy.Text = "Выбрать";
            this.buttonOpenProxy.UseVisualStyleBackColor = true;
            this.buttonOpenProxy.Click += new System.EventHandler(this.buttonSettings_Click);
            this.label2.AutoSize = true;
            this.label2.Location = new Point(16, 69);
            this.label2.Name = "label2";
            this.label2.Size = new Size(44, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Список";
            this.textBoxProxyPath.Location = new Point(193, 66);
            this.textBoxProxyPath.Name = "textBoxProxyPath";
            this.textBoxProxyPath.Size = new Size(290, 20);
            this.textBoxProxyPath.TabIndex = 4;
            this.radioButtonProxySocks5.AutoSize = true;
            this.radioButtonProxySocks5.Location = new Point(353, 17);
            this.radioButtonProxySocks5.Name = "radioButtonProxySocks5";
            this.radioButtonProxySocks5.Size = new Size(67, 17);
            this.radioButtonProxySocks5.TabIndex = 3;
            this.radioButtonProxySocks5.Text = "SOCKS5";
            this.radioButtonProxySocks5.UseVisualStyleBackColor = true;
            this.radioButtonProxySocks5.Click += new System.EventHandler(this.radioButton_Click);
            this.radioButtonProxySocks4.AutoSize = true;
            this.radioButtonProxySocks4.Location = new Point(280, 17);
            this.radioButtonProxySocks4.Name = "radioButtonProxySocks4";
            this.radioButtonProxySocks4.Size = new Size(67, 17);
            this.radioButtonProxySocks4.TabIndex = 2;
            this.radioButtonProxySocks4.Text = "SOCKS4";
            this.radioButtonProxySocks4.UseVisualStyleBackColor = true;
            this.radioButtonProxySocks4.Click += new System.EventHandler(this.radioButton_Click);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(16, 21);
            this.label1.Name = "label1";
            this.label1.Size = new Size(26, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Тип";
            this.radioButtonProxyNone.AutoSize = true;
            this.radioButtonProxyNone.Location = new Point(193, 17);
            this.radioButtonProxyNone.Name = "radioButtonProxyNone";
            this.radioButtonProxyNone.Size = new Size(81, 17);
            this.radioButtonProxyNone.TabIndex = 0;
            this.radioButtonProxyNone.TabStop = true;
            this.radioButtonProxyNone.Tag = "";
            this.radioButtonProxyNone.Text = "Отключено";
            this.radioButtonProxyNone.UseVisualStyleBackColor = true;
            this.radioButtonProxyNone.Click += new System.EventHandler(this.radioButton_Click);
        //    this.visualStyler1.set_HostForm(this);
        //    this.visualStyler1.set_License((VisualStylerLicense)componentResourceManager.GetObject("visualStyler1.License"));
         //   this.visualStyler1.set_ShowFocusCues(false);
        //    this.visualStyler1.LoadVisualStyle(null, "ConcaveD (Normal).vssf");
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.ClientSize = new Size(614, 415);
            base.Controls.Add(this.groupBox3);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
          
            base.MaximizeBox = false;
            base.Name = "Form1";
            base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PW Lvl Upper";
            base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)this.numericMinLvl).EndInit();
            ((System.ComponentModel.ISupportInitialize)this.numericThreadsCount).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
          //  this.visualStyler1.EndInit();
            base.ResumeLayout(false);
        }

        #endregion
    }
}

