using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp3
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            Text = "좌석관리";
            dataGridView1.DataSource = DataManager.Seats;
        }

        private void button1_Click(object sender, EventArgs e) //좌석추가
        {
            try
            {
                if (DataManager.Seats.Exists((x) => x.SeatNo.ToString() == textBox1.Text))
                {
                    MessageBox.Show("이미 존재하는 좌석입니다.");
                }
                else
                {
                    Seat seat = new Seat()
                    {
                        SeatNo = int.Parse(textBox1.Text),
                        SeatKind = textBox2.Text,
                        SeatPrice = int.Parse(textBox3.Text)
                    };
                    DataManager.Seats.Add(seat);
                }
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = DataManager.Seats;
                DataManager.Save();
            }
            catch (Exception ex)
            {

            }
        }

        private void button2_Click(object sender, EventArgs e) //좌석변경
        {
            try
            {
                Seat seat = DataManager.Seats.Single((x) => x.SeatNo.ToString() == textBox1.Text);
                seat.SeatKind = textBox2.Text;
                seat.SeatPrice = int.Parse(textBox3.Text);

                dataGridView1.DataSource = null;
                dataGridView1.DataSource = DataManager.Seats;
                DataManager.Save();
            }
            catch (Exception ex)
            {

            }
        }

        private void button3_Click(object sender, EventArgs e) //좌석삭제
        {
            try
            {
                Seat seat = DataManager.Seats.Single((x) => x.SeatNo.ToString() == textBox1.Text);
                DataManager.Seats.Remove(seat);

                dataGridView1.DataSource = null;
                dataGridView1.DataSource = DataManager.Seats;
                DataManager.Save();
            }
            catch (Exception ex)
            {

            }
        }

        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            try
            {
                Seat seat = dataGridView1.CurrentRow.DataBoundItem as Seat;
                textBox1.Text = seat.SeatNo.ToString();
                textBox2.Text = seat.SeatKind.ToString();
                textBox3.Text = seat.SeatPrice.ToString();
            }
            catch (Exception ex)
            {

            }
        }
    }
}
