using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WmsServer
{
    public partial class FormEngine_Ship : Form
    {
        private System.Windows.Forms.Timer timerUpdate = new System.Windows.Forms.Timer();

        DB.DBInterface db = new DB.DBInterface();
        DateTime convertDateTime;
        public FormEngine_Ship()
        {
            InitializeComponent();
        }

        private void FormEngine_Ship_Load(object sender, EventArgs e)
        {
            #region Display Upadate
            timerUpdate.Interval = 500;
            timerUpdate.Tick += TimerUpdate_Tick;
            timerUpdate.Start();
            #endregion
        }

        private void FormEngine_Ship_FormClosing(object sender, FormClosingEventArgs e)
        {
            timerUpdate.Stop();
            LogMsg.CMsg.Show("FENSH", "close engine_ship :", "", false, true);
        }

        private void TimerUpdate_Tick(object sender, EventArgs e)
        {

            UpdateGrid();
        }

        private void UpdateGrid()
        {
            try
            {
                List<string> data = db.GetShipList("[Date]; [Sequence]; [Model]; [Quantity]; [CurrentCount]; [ToPlant]",
                    $"Quantity <> [CurrentCount] OR [CurrentCount] IS NULL Order by [Key] ");

                if (data == null)
                {
                    Data.DB_Connected = false;
                    return;
                }
                Data.DB_Connected = true;

                dgvShippingList.Rows.Clear();

                if (data.Count == 0) dgvShippingList.RowCount = 1;
                else dgvShippingList.RowCount = data.Count;

                for (int i = 0; i < data.Count; i++)
                {
                    string[] row = data[i].Split(',');
                    if (DateTime.TryParse(row[0], out convertDateTime))
                    {
                        // Extract the time part and format it
                        row[0] = convertDateTime.ToString("yyyy-MM-dd");
                    }

                    // 데이터 그리드 뷰의 셀 값을 설정하기 전에 셀이 존재하는지 확인
                    for (int j = 0; j < row.Length; j++)
                    {
                        if (j < dgvShippingList.Columns.Count && i < dgvShippingList.Rows.Count)
                        {
                            dgvShippingList.Rows[i].Cells[j].Value = row[j];
                        }
                    }
                }


                data = db.GetList("[ID]; [Model]; [InDateTime]; [FromPlant]; [NoShip]",
                    $"[inDateTime] is not null and [Status] = 0 Order by [inDateTime] desc");

                if (data == null)
                {
                    Data.DB_Connected = false;
                    return;
                }
                Data.DB_Connected = true;

                dgvEngineList.Rows.Clear();

                if (data.Count == 0) dgvEngineList.RowCount = 1;
                else dgvEngineList.RowCount = data.Count;

                for (int i = 0; i < data.Count; i++)
                {
                    string[] row = data[i].Split(',');
                    if (DateTime.TryParse(row[2], out convertDateTime))
                    {
                        // Extract the time part and format it
                        row[2] = convertDateTime.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    if (DateTime.TryParse(row[3], out convertDateTime))
                    {
                        // Extract the time part and format it
                        row[3] = convertDateTime.ToString("yyyy-MM-dd HH:mm:ss");
                    }

                    // 데이터 그리드 뷰의 셀 값을 설정하기 전에 셀이 존재하는지 확인
                    for (int j = 0; j < row.Length; j++)
                    {
                        if (j < dgvEngineList.Columns.Count && i < dgvEngineList.Rows.Count)
                        {
                            dgvEngineList.Rows[i].Cells[j].Value = row[j];
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogMsg.CMsg.Show("FENSH", "refresh error : ", ex.ToString(), false, true);
            }

        }

        
    }
}
