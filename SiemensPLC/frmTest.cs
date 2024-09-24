using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SiemensPLC
{
    public partial class frmTest : Form
    {
        Timer m_tmrMonitor;
        ushort[] PLC_ReadData;
        ushort[] PLC_WriteData;

        bool isHex = true;

        public frmTest()
        {
            InitializeComponent();

            rbHex.Checked = isHex;
            rbInteger.Checked = !isHex;

            m_tmrMonitor = new Timer();
            m_tmrMonitor.Tick += new EventHandler(m_tmrMonitor_Tick);
            m_tmrMonitor.Interval = 50;
            m_tmrMonitor.Stop();
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            SiemensPLC.Initialize();
            SiemensPLC.Start();
            m_tmrMonitor.Start();

            PLC_ReadData = new ushort[SiemensPLC.m_nReadNum];
            PLC_WriteData = new ushort[SiemensPLC.m_nWriteNum];

            cbAdd.Items.Clear();

            dgvRead.RowCount = SiemensPLC.m_nReadNum;
            dgvWrite.RowCount = SiemensPLC.m_nWriteNum;

            for (int i = 0; i < SiemensPLC.m_nReadNum; i++)
            {
                dgvRead.Rows[i].Cells[0].Value = (object)SiemensPLC.DescriptionofChannel[i];
            }

            for (int i = 0; i < SiemensPLC.m_nWriteNum; i++)
            {
                dgvWrite.Rows[i].Cells[0].Value = (object)SiemensPLC.DescriptionofChannel[i + SiemensPLC.m_nReadNum]; 
                cbAdd.Items.Add(String.Format("{0}", SiemensPLC.DescriptionofChannel[i + SiemensPLC.m_nReadNum]));
            }
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            SiemensPLC.Stop();
            m_tmrMonitor.Stop();
        }

        private void m_tmrMonitor_Tick(object sender, EventArgs e)
        {

            try
            {
                PLC_ReadData = SiemensPLC.ReadData();

                for (int i = 0; i < PLC_ReadData.Length; i++)
                {
                    if(isHex)
                        dgvRead.Rows[i].Cells[1].Value = (object)String.Format("{0:X2}", PLC_ReadData[i]);
                    else
                        dgvRead.Rows[i].Cells[1].Value = (object)String.Format("{0:D}", PLC_ReadData[i]);
                }

                for (int i = 0; i < PLC_WriteData.Length; i++)
                {
                    if (isHex) 
                        dgvWrite.Rows[i].Cells[1].Value = (object)String.Format("{0:X2}", PLC_WriteData[i]);
                    else
                        dgvWrite.Rows[i].Cells[1].Value = (object)String.Format("{0:D}", PLC_WriteData[i]);
                }
            }
            //catch (Exception exep)
            catch (Exception)
            {
            }
        }

        private void buttonWrite_Click(object sender, EventArgs e)
        {
            try
            {
                int nData;
                if (isHex)
                    nData = Convert.ToUInt16(tbData.Text, 16);
                else
                    nData = Convert.ToUInt16(tbData.Text);

                PLC_WriteData[cbAdd.SelectedIndex] = (ushort)nData;

                SiemensPLC.WriteAOData(cbAdd.Text,nData);
            }
            catch (Exception exep)
            {
                MessageBox.Show(exep.Message);
            }
        }

        private void rbHex_CheckedChanged(object sender, EventArgs e)
        {
            isHex = rbHex.Checked;
            rbInteger.Checked = !isHex;
        }

        private void rbInteger_CheckedChanged(object sender, EventArgs e)
        {
            isHex = !rbInteger.Checked;
            rbHex.Checked = isHex;
        }

        private void dgvWrite_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int idx = e.RowIndex;
            cbAdd.SelectedItem = (object)(String.Format("{0}",idx));
        }

        private void dgvWrite_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int row = e.RowIndex;
            if (row < 0) return;
            string sel = dgvWrite.Rows[row].Cells[0].Value.ToString();
            int index = 0;
            for (int i = 0; i < cbAdd.Items.Count; i++)
            {
                if (cbAdd.Items[i].ToString() == sel) index = i;
            }

            cbAdd.SelectedIndex = index;
        }

    }
}
