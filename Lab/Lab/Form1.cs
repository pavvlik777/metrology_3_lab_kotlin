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
            Dictionary<string, int> spensData = metrics1.FindSpen(FileName);
            int summarySpen = 0;

            DataTable spensTableData = new DataTable();
            spensTableData.Columns.Add("Идентификатор");
            spensTableData.Columns.Add("Спен");

            foreach (var cur in spensData)
            {
                DataRow r = spensTableData.NewRow();
                r["Идентификатор"] = cur.Key;
                r["Спен"] = cur.Value;
                summarySpen += cur.Value;
                spensTableData.Rows.Add(r);
            }
            SpenTable.DataSource = spensTableData;
            OutputSpen.Text = $"Суммарный спен - {summarySpen}";

            ChepinFullMetrics metrics2 = new ChepinFullMetrics();
            ChepinFullData fullChepinData = metrics2.FindChepin(FileName);
            DataTable fullChepinTable = new DataTable();
            fullChepinTable.Columns.Add("P");
            fullChepinTable.Columns.Add("M");
            fullChepinTable.Columns.Add("C");
            fullChepinTable.Columns.Add("T");
            int fullP = fullChepinData.PVars.Count;
            int fullM = fullChepinData.MVars.Count;
            int fullC = fullChepinData.CVars.Count;
            int fullT = fullChepinData.TVars.Count;
            double fullChepinCalc = fullP + 2 * fullM + 3 * fullC + 0.5 * fullT;
            FullChebinInfo.Text = $"P = {fullP}\r\nM = {fullM}\r\nC = {fullC}\r\nT = {fullT}\r\nQ = 1 * {fullP} + 2 * {fullM} + 3 * {fullC} + 0,5 * {fullT} = {fullChepinCalc}";

            int maxCount = -1;
            if (fullChepinData.PVars.Count > maxCount) maxCount = fullChepinData.PVars.Count;
            if (fullChepinData.MVars.Count > maxCount) maxCount = fullChepinData.MVars.Count;
            if (fullChepinData.CVars.Count > maxCount) maxCount = fullChepinData.CVars.Count;
            if (fullChepinData.TVars.Count > maxCount) maxCount = fullChepinData.TVars.Count;

            int j = maxCount - fullChepinData.PVars.Count;
            while(j > 0)
            {
                fullChepinData.PVars.Add("");
                j--;
            }
            j = maxCount - fullChepinData.MVars.Count;
            while (j > 0)
            {
                fullChepinData.MVars.Add("");
                j--;
            }
            j = maxCount - fullChepinData.CVars.Count;
            while (j > 0)
            {
                fullChepinData.CVars.Add("");
                j--;
            }
            j = maxCount - fullChepinData.TVars.Count;
            while (j > 0)
            {
                fullChepinData.TVars.Add("");
                j--;
            }

            for(int i = 0; i < maxCount; i++)
            {
                DataRow r = fullChepinTable.NewRow();
                r["P"] = fullChepinData.PVars[i];
                r["M"] = fullChepinData.MVars[i];
                r["C"] = fullChepinData.CVars[i];
                r["T"] = fullChepinData.TVars[i];
                fullChepinTable.Rows.Add(r);
            }
            FullChebinTable.DataSource = fullChepinTable;

            ChepinIOMetrics metrics3 = new ChepinIOMetrics();
            ChepinIOData ioChepinData = metrics3.FindChepin(FileName);
            DataTable ioChepinTable = new DataTable();
            ioChepinTable.Columns.Add("P");
            ioChepinTable.Columns.Add("M");
            ioChepinTable.Columns.Add("C");
            ioChepinTable.Columns.Add("T");
            int ioP = ioChepinData.PVars.Count;
            int ioM = ioChepinData.MVars.Count;
            int ioC = ioChepinData.CVars.Count;
            int ioT = ioChepinData.TVars.Count;
            double ioChepinCalc = ioP + 2 * ioM + 3 * ioC + 0.5 * ioT;
            IOChepinInfo.Text = $"P = {ioP}\r\nM = {ioM}\r\nC = {ioC}\r\nT = {ioT}\r\nQ = 1 * {ioP} + 2 * {ioM} + 3 * {ioC} + 0,5 * {ioT} = {ioChepinCalc}";

            maxCount = -1;
            if (ioChepinData.PVars.Count > maxCount) maxCount = ioChepinData.PVars.Count;
            if (ioChepinData.MVars.Count > maxCount) maxCount = ioChepinData.MVars.Count;
            if (ioChepinData.CVars.Count > maxCount) maxCount = ioChepinData.CVars.Count;
            if (ioChepinData.TVars.Count > maxCount) maxCount = ioChepinData.TVars.Count;

            j = maxCount - ioChepinData.PVars.Count;
            while (j > 0)
            {
                ioChepinData.PVars.Add("");
                j--;
            }
            j = maxCount - ioChepinData.MVars.Count;
            while (j > 0)
            {
                ioChepinData.MVars.Add("");
                j--;
            }
            j = maxCount - ioChepinData.CVars.Count;
            while (j > 0)
            {
                ioChepinData.CVars.Add("");
                j--;
            }
            j = maxCount - ioChepinData.TVars.Count;
            while (j > 0)
            {
                ioChepinData.TVars.Add("");
                j--;
            }

            for (int i = 0; i < maxCount; i++)
            {
                DataRow r = ioChepinTable.NewRow();
                r["P"] = ioChepinData.PVars[i];
                r["M"] = ioChepinData.MVars[i];
                r["C"] = ioChepinData.CVars[i];
                r["T"] = ioChepinData.TVars[i];
                ioChepinTable.Rows.Add(r);
            }
            chepinIOTable.DataSource = ioChepinTable;
        }
    }
}
