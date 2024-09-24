using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Threading;
using System.IO;

namespace WmsServer
{
    public partial class FormPLC : Form
    {
        Thread threadUpdate;
        bool threadUpdateCheck = false;
        public FormPLC()
        {
            InitializeComponent();
        }

        private void FormPLC_Load(object sender, EventArgs e)
        {
            if (Data.IoDatas == null) return;

            dataGridView1.RowCount = Data.AiCount;
            dataGridView2.RowCount = Data.AoCount;
            dataGridView3.RowCount = Data.DiCount;
            dataGridView4.RowCount = Data.DoCount;

            // key <--> value 변환
            Dictionary<string, string> rdic = new Dictionary<string, string>();
            foreach (KeyValuePair<string, string> p in Data.PlcIoDic)
            {
                if (!rdic.ContainsKey(p.Value)) rdic.Add(p.Value, p.Key);
            }

            int start = 0;
            for (int i = start; i < Data.AiCount; i++)
            {
                dataGridView1.Rows[i].Cells[0].Value = Data.IoDatas[i].Name;
                if (rdic.ContainsKey(Data.IoDatas[i].Name))
                    dataGridView1.Rows[i].Cells[2].Value = rdic[Data.IoDatas[i].Name];
            }

            start = Data.AiCount;
            for (int i = 0; i < Data.AoCount; i++)
            {
                dataGridView2.Rows[i].Cells[0].Value = Data.IoDatas[i + start].Name;
                if (rdic.ContainsKey(Data.IoDatas[i + start].Name))
                    dataGridView2.Rows[i].Cells[2].Value = rdic[Data.IoDatas[i + start].Name];
            }

            start = Data.AiCount + Data.AoCount;
            for (int i = 0; i < Data.DiCount; i++)
            {
                dataGridView3.Rows[i].Cells[0].Value = Data.IoDatas[i + start].Name;
                if (rdic.ContainsKey(Data.IoDatas[i + start].Name))
                    dataGridView3.Rows[i].Cells[2].Value = rdic[Data.IoDatas[i + start].Name];
            }

            start = Data.AiCount + Data.AoCount + Data.DiCount;
            for (int i = 0; i < Data.DoCount; i++)
            {
                dataGridView4.Rows[i].Cells[0].Value = Data.IoDatas[i + start].Name;
                if (rdic.ContainsKey(Data.IoDatas[i + start].Name))
                    dataGridView4.Rows[i].Cells[2].Value = rdic[Data.IoDatas[i + start].Name];
            }

            //LoadDescription();

            threadUpdate = new Thread(new ThreadStart(UpdateValue));
            threadUpdateCheck = true;
            threadUpdate.Start();
        }

        private void FormPLC_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (threadUpdate != null)
            {
                threadUpdateCheck = false;
                threadUpdate.Abort();
            }
        }

        void UpdateValue()
        {
            while (threadUpdateCheck)
            {
                try
                {
                    int start = 0;
                    for (int i = start; i < Data.AiCount; i++)
                    {
                        dataGridView1.Rows[i].Cells[1].Value = Data.IoDatas[start + i].fValue;
                    }

                    start = Data.AiCount;
                    for (int i = 0; i < Data.AoCount; i++)
                    {
                        dataGridView2.Rows[i].Cells[1].Value = Data.IoDatas[start + i].fValue;
                    }

                    start = Data.AiCount + Data.AoCount;
                    for (int i = 0; i < Data.DiCount; i++)
                    {
                        dataGridView3.Rows[i].Cells[1].Value = Data.IoDatas[start + i].fValue;
                    }

                    start = Data.AiCount + Data.AoCount + Data.DiCount;
                    for (int i = 0; i < Data.DoCount; i++)
                    {
                        dataGridView4.Rows[i].Cells[1].Value = Data.IoDatas[start + i].fValue;
                    }
                }
                catch { }

                Thread.Sleep(500);
            }
        }


        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //FileStream fs = new FileStream(file, FileMode.Create, FileAccess.Write);
            //StreamWriter sw = new StreamWriter(fs, Encoding.Default);
            //sw.WriteLine("A&G Technology PLC Description File");
            //sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            //sw.WriteLine("-----------------------------------");
            //sw.Close();
            //sw.Dispose();


            //for (int i = 0; i < dataGridView1.RowCount; i++)
            //{
            //    if (dataGridView1.Rows[i].Cells[2].Value != null)
            //        INI.WriteValue(file, "AI", "AI" + i.ToString(), dataGridView1.Rows[i].Cells[2].Value.ToString());
            //}

            //for (int i = 0; i < dataGridView2.RowCount; i++)
            //{
            //    if (dataGridView2.Rows[i].Cells[2].Value != null)
            //        INI.WriteValue(file, "AO", "AO" + i.ToString(), dataGridView2.Rows[i].Cells[2].Value.ToString());
            //}
            //for (int i = 0; i < dataGridView3.RowCount; i++)
            //{
            //    if (dataGridView3.Rows[i].Cells[2].Value != null)
            //        INI.WriteValue(file, "DI", "DI" + i.ToString(), dataGridView3.Rows[i].Cells[2].Value.ToString());
            //}
            //for (int i = 0; i < dataGridView4.RowCount; i++)
            //{
            //    if (dataGridView4.Rows[i].Cells[2].Value != null)
            //        INI.WriteValue(file, "DO", "DO" + i.ToString(), dataGridView4.Rows[i].Cells[2].Value.ToString());
            //}
        }

        private void LoadDescription()
        {
            //foreach (KeyValuePair<string, string> p in Data.PlcIoDic)
            //{
            //    for (int i = 0; i < dataGridView1.RowCount; i++)
            //    {
            //        dataGridView1.Rows[i].Cells[2].Value = INI.ReadValue(file, "AI", "AI" + i.ToString(), "");
            //    }
            //    for (int i = 0; i < dataGridView2.RowCount; i++)
            //    {
            //        dataGridView2.Rows[i].Cells[2].Value = INI.ReadValue(file, "AO", "AO" + i.ToString(), "");
            //    }
            //    for (int i = 0; i < dataGridView3.RowCount; i++)
            //    {
            //        dataGridView3.Rows[i].Cells[2].Value = INI.ReadValue(file, "DI", "DI" + i.ToString(), "");
            //    }
            //    for (int i = 0; i < dataGridView4.RowCount; i++)
            //    {
            //        dataGridView4.Rows[i].Cells[2].Value = INI.ReadValue(file, "DO", "DO" + i.ToString(), "");
            //    }
            //}
        }

        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int ir = e.RowIndex;
            string sr = dataGridView2.Rows[ir].Cells[0].Value.ToString();

            FormOutputValue frm = new FormOutputValue();
            frm.selectIo = sr;
            frm.type = "AO";
            if (frm.ShowDialog() == DialogResult.OK)
            {
                string io = frm.selectIo;
                decimal val = frm.selectValue;
                WriteData(io, (float)val);
            }
        }

        private void dataGridView4_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int ir = e.RowIndex;
            string sr = dataGridView4.Rows[ir].Cells[0].Value.ToString();

            FormOutputValue frm = new FormOutputValue();
            frm.selectIo = sr;
            frm.type = "DO";
            if (frm.ShowDialog() == DialogResult.OK)
            {
                string io = frm.selectIo;
                decimal val = frm.selectValue;
                WriteData(io, (float)val);
            }
        }

       

        private void WriteData(string name, float value)
        {
            if (Data.IoTypeDic[name] == "AO") DevicePLC.WritePLC(name, (double)value);
            if (Data.IoTypeDic[name] == "DO") DevicePLC.WritePLC(name, ((value > 0) ? true : false));
        }

        private void outToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormOutputValue frm = new FormOutputValue();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                string io = frm.selectIo;
                decimal val = frm.selectValue;
                WriteData(io, (float)val);
            }
        }

    }
}
