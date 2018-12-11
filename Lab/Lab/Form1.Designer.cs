namespace Lab
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.SpenName = new System.Windows.Forms.Label();
            this.SpenTable = new System.Windows.Forms.DataGridView();
            this.SolveTaskButton = new System.Windows.Forms.Button();
            this.ChooseFileButton = new System.Windows.Forms.Button();
            this.OutputSpen = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.FullChebinName = new System.Windows.Forms.Label();
            this.FullChebinTable = new System.Windows.Forms.DataGridView();
            this.FullChebinInfo = new System.Windows.Forms.TextBox();
            this.chepinIOTable = new System.Windows.Forms.DataGridView();
            this.IOChepinInfo = new System.Windows.Forms.TextBox();
            this.chepinIOName = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.SpenTable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FullChebinTable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chepinIOTable)).BeginInit();
            this.SuspendLayout();
            // 
            // SpenName
            // 
            this.SpenName.AutoSize = true;
            this.SpenName.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SpenName.Location = new System.Drawing.Point(68, 45);
            this.SpenName.Name = "SpenName";
            this.SpenName.Size = new System.Drawing.Size(163, 24);
            this.SpenName.TabIndex = 0;
            this.SpenName.Text = "Спен программы";
            // 
            // SpenTable
            // 
            this.SpenTable.AllowUserToAddRows = false;
            this.SpenTable.AllowUserToDeleteRows = false;
            this.SpenTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.SpenTable.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.SpenTable.Location = new System.Drawing.Point(52, 96);
            this.SpenTable.Name = "SpenTable";
            this.SpenTable.ReadOnly = true;
            this.SpenTable.Size = new System.Drawing.Size(341, 448);
            this.SpenTable.TabIndex = 1;
            // 
            // SolveTaskButton
            // 
            this.SolveTaskButton.Location = new System.Drawing.Point(804, 3);
            this.SolveTaskButton.Name = "SolveTaskButton";
            this.SolveTaskButton.Size = new System.Drawing.Size(136, 23);
            this.SolveTaskButton.TabIndex = 2;
            this.SolveTaskButton.Text = "Посчитать";
            this.SolveTaskButton.UseVisualStyleBackColor = true;
            this.SolveTaskButton.Click += new System.EventHandler(this.SolveTaskButton_Click);
            // 
            // ChooseFileButton
            // 
            this.ChooseFileButton.Location = new System.Drawing.Point(946, 3);
            this.ChooseFileButton.Name = "ChooseFileButton";
            this.ChooseFileButton.Size = new System.Drawing.Size(136, 23);
            this.ChooseFileButton.TabIndex = 3;
            this.ChooseFileButton.Text = "Выбрать файл";
            this.ChooseFileButton.UseVisualStyleBackColor = true;
            this.ChooseFileButton.Click += new System.EventHandler(this.ChooseFileButton_Click);
            // 
            // OutputSpen
            // 
            this.OutputSpen.AutoSize = true;
            this.OutputSpen.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.OutputSpen.Location = new System.Drawing.Point(68, 577);
            this.OutputSpen.Name = "OutputSpen";
            this.OutputSpen.Size = new System.Drawing.Size(173, 24);
            this.OutputSpen.TabIndex = 4;
            this.OutputSpen.Text = "Суммарный спен -";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // FullChebinName
            // 
            this.FullChebinName.AutoSize = true;
            this.FullChebinName.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FullChebinName.Location = new System.Drawing.Point(423, 47);
            this.FullChebinName.Name = "FullChebinName";
            this.FullChebinName.Size = new System.Drawing.Size(225, 24);
            this.FullChebinName.TabIndex = 5;
            this.FullChebinName.Text = "Полная метрика Чебина";
            // 
            // FullChebinTable
            // 
            this.FullChebinTable.AllowUserToAddRows = false;
            this.FullChebinTable.AllowUserToDeleteRows = false;
            this.FullChebinTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.FullChebinTable.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.FullChebinTable.Location = new System.Drawing.Point(427, 96);
            this.FullChebinTable.Name = "FullChebinTable";
            this.FullChebinTable.ReadOnly = true;
            this.FullChebinTable.Size = new System.Drawing.Size(471, 448);
            this.FullChebinTable.TabIndex = 6;
            // 
            // FullChebinInfo
            // 
            this.FullChebinInfo.Location = new System.Drawing.Point(427, 577);
            this.FullChebinInfo.Multiline = true;
            this.FullChebinInfo.Name = "FullChebinInfo";
            this.FullChebinInfo.ReadOnly = true;
            this.FullChebinInfo.Size = new System.Drawing.Size(471, 107);
            this.FullChebinInfo.TabIndex = 7;
            // 
            // chepinIOTable
            // 
            this.chepinIOTable.AllowUserToAddRows = false;
            this.chepinIOTable.AllowUserToDeleteRows = false;
            this.chepinIOTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.chepinIOTable.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.chepinIOTable.Location = new System.Drawing.Point(946, 96);
            this.chepinIOTable.Name = "chepinIOTable";
            this.chepinIOTable.ReadOnly = true;
            this.chepinIOTable.Size = new System.Drawing.Size(495, 448);
            this.chepinIOTable.TabIndex = 8;
            // 
            // IOChepinInfo
            // 
            this.IOChepinInfo.Location = new System.Drawing.Point(946, 577);
            this.IOChepinInfo.Multiline = true;
            this.IOChepinInfo.Name = "IOChepinInfo";
            this.IOChepinInfo.ReadOnly = true;
            this.IOChepinInfo.Size = new System.Drawing.Size(495, 107);
            this.IOChepinInfo.TabIndex = 9;
            // 
            // chepinIOName
            // 
            this.chepinIOName.AutoSize = true;
            this.chepinIOName.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.chepinIOName.Location = new System.Drawing.Point(978, 47);
            this.chepinIOName.Name = "chepinIOName";
            this.chepinIOName.Size = new System.Drawing.Size(291, 24);
            this.chepinIOName.TabIndex = 10;
            this.chepinIOName.Text = "Метрика Чепина ввода\\вывода";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1491, 803);
            this.Controls.Add(this.chepinIOName);
            this.Controls.Add(this.IOChepinInfo);
            this.Controls.Add(this.chepinIOTable);
            this.Controls.Add(this.FullChebinInfo);
            this.Controls.Add(this.FullChebinTable);
            this.Controls.Add(this.FullChebinName);
            this.Controls.Add(this.OutputSpen);
            this.Controls.Add(this.ChooseFileButton);
            this.Controls.Add(this.SolveTaskButton);
            this.Controls.Add(this.SpenTable);
            this.Controls.Add(this.SpenName);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.SpenTable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FullChebinTable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chepinIOTable)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label SpenName;
        private System.Windows.Forms.DataGridView SpenTable;
        private System.Windows.Forms.Button SolveTaskButton;
        private System.Windows.Forms.Button ChooseFileButton;
        private System.Windows.Forms.Label OutputSpen;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label FullChebinName;
        private System.Windows.Forms.DataGridView FullChebinTable;
        private System.Windows.Forms.TextBox FullChebinInfo;
        private System.Windows.Forms.DataGridView chepinIOTable;
        private System.Windows.Forms.TextBox IOChepinInfo;
        private System.Windows.Forms.Label chepinIOName;
    }
}

