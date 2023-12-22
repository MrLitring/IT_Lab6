using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;

namespace WindowsFormsApp6
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            button1.Text = "Матрица 1";
            button2.Text = "Матрица 2";
            button3.Text = "Произведение матриц";

            dataGridView3.ReadOnly = true;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.ColumnHeadersVisible = false;
            dataGridView2.RowHeadersVisible = false;
            dataGridView2.ColumnHeadersVisible = false;
            dataGridView3.RowHeadersVisible = false;
            dataGridView3.ColumnHeadersVisible = false;

            dataGridView1.AllowUserToAddRows = false;
            dataGridView2.AllowUserToAddRows = false;
            dataGridView3.AllowUserToAddRows = false;

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView2.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            dataGridView3.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView3.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            dataGridView1.ColumnCount = 1;
            dataGridView1.RowCount = 1;
            dataGridView2.ColumnCount = 1;
            dataGridView2.RowCount = 1;


            dataGridView1.CellValueChanged += (s, e) =>
            {
                CellChangeValue(s, e);
            };
            dataGridView2.CellValueChanged += (s, e) =>
            {
                CellChangeValue(s, e);
            };
        }


        private bool isCellNotNull(DataGridView dataElem)
        {
            for (int i = 0; i < dataElem.Rows.Count; i++)
            {
                for (int j = 0; j < dataElem.Columns.Count; j++)
                {
                    try
                    {
                        if (dataElem[j, i].Value == null) return false;
                        else if (dataElem[j, i].Value.ToString() == " " || dataElem[j, i].Value.ToString() == "") return false;
                    }
                    catch
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private void NumValuewChange(object sender, EventArgs e)
        {
            if ((sender as NumericUpDown).Name == "numericUpDown1")
            {
                dataGridView1.ColumnCount = (int)numericUpDown1.Value;
                dataGridView2.RowCount = (int)numericUpDown1.Value;
            }

            if ((sender as NumericUpDown).Name == "numericUpDown2")
            {
                dataGridView1.RowCount = (int)numericUpDown2.Value;
            }

            if ((sender as NumericUpDown).Name == "numericUpDown3")
            {
                dataGridView2.ColumnCount = (int)numericUpDown3.Value;
            }
        }

        private void DataUpdate(int i)
        {
            if (i == 1)
            {
                dataGridView1.ColumnCount = (int)numericUpDown1.Value;
                dataGridView1.RowCount = (int)numericUpDown2.Value;
            }
            else if (i == 2)
            {
                dataGridView2.ColumnCount = (int)numericUpDown3.Value;
                dataGridView2.RowCount = (int)numericUpDown2.Value;
            }
        }


        private void PanelShow_UnShow(object sender, EventArgs e)
        {
            Panel panel = null;
            if (panel == null)
            {
                panel1.Height = panel1.MinimumSize.Height;
                panel2.Height = panel2.MinimumSize.Height;
                panel3.Height = panel3.MinimumSize.Height;

                button1.Visible = true;
                button2.Visible = true;
                button3.Visible = true;

                if ((sender as Button).Name == "button1")
                {
                    panel = panel1;
                    ((Button)sender).Visible = false;
                }
                if ((sender as Button).Name == "button2")
                {
                    panel = panel2;
                    ((Button)sender).Visible = false;
                }
                if ((sender as Button).Name == "button3")
                {
                    panel = panel3;
                    ((Button)sender).Visible = false;
                }

                panel.Height = panel.MaximumSize.Height;
            }
        }

        private void ClearData(object sender, EventArgs e)
        {
            if ((sender as Button).Name == "button5")
            {
                dataGridView1.Rows.Clear();
                DataUpdate(1);
            }
            if ((sender as Button).Name == "button6")
            {
                dataGridView2.Rows.Clear();
                DataUpdate(2);
            }
            if ((sender as Button).Name == "button7")
            {
                dataGridView3.Rows.Clear();
            }

        }

        private void CalculateAndShow()
        {
            dataGridView3.RowCount = (int)(dataGridView1.Rows.Count);
            dataGridView3.ColumnCount = (int)(dataGridView2.Columns.Count);

            for (int i = 0; i < dataGridView3.Rows.Count; i++)
            {
                for (int j = 0; j < dataGridView3.Columns.Count; j++)
                {

                    double x = 0;
                    for (int k = 0; k < dataGridView1.Columns.Count; k++)
                    {
                        double c = Convert.ToDouble(dataGridView1.Rows[i].Cells[k].Value);
                        double v = Convert.ToDouble(dataGridView2.Rows[k].Cells[j].Value);
                        x += c * v;

                    }
                    dataGridView3.Rows[i].Cells[j].Value = x;

                }
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (isCellNotNull(dataGridView1) == true && isCellNotNull(dataGridView2) == true)
                CalculateAndShow();
            else
                MessageBox.Show("Заполните все матрицы", "Owubka");
        }

        private void CellChangeValue(object sender, DataGridViewCellEventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                for (int j = 0; j < dataGridView1.Columns.Count; j++)
                {

                    string s = Convert.ToString(dataGridView1.Rows[i].Cells[j].Value);
                    string num = ".0123456789";
                    foreach (char n in s)
                    {
                        if (!num.Contains(n))
                        {
                            dataGridView1.Rows[i].Cells[j].Value = "";
                            MessageBox.Show("Введите положительное число", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                }
            }

        }
    }
}
