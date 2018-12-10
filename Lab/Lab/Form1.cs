using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Lab
{
    public partial class Form1 : Form
    {
        public static string FileName = "None";

        public Form1()
        {
            InitializeComponent();
        }

        private void ChooseFileButton_Click(object sender, EventArgs e)
        {
            try
            {
                using (openFileDialog1)
                {
                    openFileDialog1.FileName = String.Empty;
                    if (openFileDialog1.ShowDialog() != DialogResult.OK)
                    {
                        return;
                    }

                    try
                    {
                        using (var fileStream = File.OpenRead(openFileDialog1.FileName))
                        {
                            TextReader textReader = new StreamReader(fileStream);



                            textReader.Close();
                            fileStream.Close();
                        }
                    }
                    catch (Exception) { }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
            string temp = "";
            for (int i = openFileDialog1.FileName.Length - 1; i > 0; i--)
            {
                if (openFileDialog1.FileName[i] == '\\')
                {
                    temp = openFileDialog1.FileName.Substring(i + 1);
                    break;
                }
            }
            bool isKotlin = false;
            for (int i = temp.Length - 1; i > 0; i--)
            {
                if (temp[i] == '.')
                    if (temp.Substring(i + 1) == "kt")
                    {
                        isKotlin = true;
                        break;
                    }
            }
            if (isKotlin)
            {
                MessageBox.Show("Файл выбран");
                FileName = openFileDialog1.FileName;
            }
            else
            {
                MessageBox.Show("Неверный формат файла");
                FileName = "None";
            }
        }

        private void SolveTaskButton_Click(object sender, EventArgs e)
        {
            if(FileName == "None")
            {
                MessageBox.Show("Вы не указали файл");
                return;
            }

            SpenMetrics metrics1 = new SpenMetrics();
            Dictionary<string, int>  spensData = metrics1.FindSpen(FileName);
            int summarySpen = 0;

            DataTable outputOperatorsData = new DataTable();
            outputOperatorsData.Columns.Add("Идентификатор");
            outputOperatorsData.Columns.Add("Спен");

            foreach (var cur in spensData)
            {
                DataRow r = outputOperatorsData.NewRow();
                r["Идентификатор"] = cur.Key;
                r["Спен"] = cur.Value;
                summarySpen += cur.Value;
                outputOperatorsData.Rows.Add(r);
            }

            SpenTable.DataSource = outputOperatorsData;
            OutputSpen.Text = $"Суммарный спен - {summarySpen}";
        }
    }
}
