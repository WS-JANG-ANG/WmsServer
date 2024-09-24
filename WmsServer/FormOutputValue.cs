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
    public partial class FormOutputValue : Form
    {
        public string selectIo = "";
        public decimal selectValue = 0;
        public string type = "";
        public FormOutputValue()
        {
            InitializeComponent();
        }

        private void FormOutputValue_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < Data.IoDatas.Length; i++)
            {
                if (Data.IoDatas[i].Type == "AO" && (type == "" || type == "AO")) cbIO.Items.Add(Data.IoDatas[i].Name);
                if (Data.IoDatas[i].Type == "DO" && (type == "" || type == "DO")) cbIO.Items.Add(Data.IoDatas[i].Name);
            }
            if (selectIo != "") cbIO.Text = selectIo;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            selectIo = cbIO.Text;
            selectValue = nudValue.Value;
        }
    }
}
