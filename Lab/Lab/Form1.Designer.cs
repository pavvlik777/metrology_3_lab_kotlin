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
            ((System.ComponentModel.ISupportInitialize)(this.SpenTable)).BeginInit();
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
            this.SolveTaskButton.Location = new System.Drawing.Point(1215, 48);
            this.SolveTaskButton.Name = "SolveTaskButton";
            this.SolveTaskButton.Size = new System.Drawing.Size(136, 23);
            this.SolveTaskButton.TabIndex = 2;
            this.SolveTaskButton.Text = "Посчитать";
            this.SolveTaskButton.UseVisualStyleBackColor = true;
            this.SolveTaskButton.Click += new System.EventHandler(this.SolveTaskButton_Click);
            // 
            // ChooseFileButton
            // 
            this.ChooseFileButton.Location = new System.Drawing.Point(1215, 96);
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
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1491, 803);
            this.Controls.Add(this.OutputSpen);
            this.Controls.Add(this.ChooseFileButton);
            this.Controls.Add(this.SolveTaskButton);
            this.Controls.Add(this.SpenTable);
            this.Controls.Add(this.SpenName);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.SpenTable)).EndInit();
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
    }
}

