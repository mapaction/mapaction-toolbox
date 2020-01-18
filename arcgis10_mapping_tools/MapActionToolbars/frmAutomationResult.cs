using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MapActionToolbar_Core;

namespace MapActionToolbars
{
    public partial class frmAutomationResult : Form
    {
        public frmAutomationResult()
        {
            InitializeComponent();
        }

        public void SetContent(AutomationReport report)
        {
            // Populate Summary box:
            if (report.result == "Success")
            {
                this.pictureBox1.Image = global::MapActionToolbars.Properties.Resources.gen_result_tick_50;
            }
            else if (report.result == "Failure")
            {
                this.pictureBox1.Image = global::MapActionToolbars.Properties.Resources.gen_result_cross_50;
            }
            else
            {
                this.pictureBox1.Image = global::MapActionToolbars.Properties.Resources.gen_result_warning_50;
            }
            this.textBox1.Text = report.summary;

            // Populate detail:
            DataGridViewImageColumn dgvImage = new DataGridViewImageColumn();
            dgvImage.HeaderText = "";
            dgvImage.ImageLayout = DataGridViewImageCellLayout.Normal;
            automationResultGridView.Columns.Add(dgvImage);
            automationResultGridView.Columns.Add("Layer", "Layer");
            automationResultGridView.Columns.Add("Time Stamp", "Time Stamp");
            automationResultGridView.Columns.Add("Data Source", "Data Source");
            automationResultGridView.Columns.Add("Detail", "Detail");

            DataGridViewCellStyle columnHeaderStyle = new DataGridViewCellStyle();
            automationResultGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            columnHeaderStyle.BackColor = Color.Beige;
            columnHeaderStyle.Font = new Font(automationResultGridView.Font, FontStyle.Bold);
            automationResultGridView.ColumnHeadersDefaultCellStyle = columnHeaderStyle;

            int row = 0;
            foreach (var rowArray in report.results)
            {
                automationResultGridView.Rows.Add(new string[] { null, rowArray.layerName, rowArray.dateStamp, rowArray.dataSource, rowArray.message });
                if (rowArray.added)
                {
                    automationResultGridView.Rows[row].Cells[0].Value = Properties.Resources.tick_17px;
                }
                else
                {
                    automationResultGridView.Rows[row].Cells[0].Value = Properties.Resources.cross_17px;
                }
                row++;
            }
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void automationResultGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
