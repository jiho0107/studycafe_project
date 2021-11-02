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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Text = "스터디 카페";
            label7.Text = DataManager.Seats.Count.ToString();
            label8.Text = DataManager.Users.Count.ToString();
            label9.Text = DataManager.Seats.Where(x => x.Used).Count().ToString();

            dataGridView1.DataSource = DataManager.Seats;
        }

        private void button1_Click(object sender, EventArgs e) //좌석선택
        {
            if (textBox1.Text.Trim() == "")
            {

            }
            else if (textBox2.Text.Trim() == "")
            {

            }
            else
            {
                try
                {
                    Seat seat = DataManager.Seats.Single((x) => x.SeatNo == int.Parse(textBox1.Text));
                    if (seat.Used)
                    {
                        MessageBox.Show("이미 사용 중인 좌석입니다.");
                    }
                    else
                    {
                        User user = DataManager.Users.Single((x) => x.Id.ToString() == textBox2.Text);
                        if (seat.SeatPrice <= user.Charge)
                        {
                            seat.UserId = user.Id;
                            seat.UserName = user.Name;
                            seat.Used = true;
                            seat.UsedAt = DateTime.Now;
                            user.Charge -= seat.SeatPrice;
                        }
                        else
                        {
                            MessageBox.Show("잔액이 부족하여 좌석을 사용할 수 없습니다.");
                        }

                        dataGridView1.DataSource = null;
                        dataGridView1.DataSource = DataManager.Seats;
                        DataManager.Save();
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show("존재하지 않는 좌석입니다.");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e) //좌석반납
        {
            try
            {
                Seat seat = DataManager.Seats.Single((x) => x.SeatNo.ToString() == textBox1.Text);
                if (seat.Used)
                {
                    User user = DataManager.Users.Single((x) => x.Id.ToString() == textBox2.Text);
                    seat.UserId = 0;
                    seat.UserName = "";
                    seat.Used = false;
                    seat.UsedAt = new DateTime();

                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = DataManager.Seats;
                    DataManager.Save();
                }
                else
                {
                    MessageBox.Show("사용되고 있지 않은 좌석입니다.");
                }
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
                if (seat.Used)
                {
                    textBox3.Text = seat.UserName;
                    textBox2.Text = seat.UserId.ToString();
                }
                else
                {
                    textBox3.Text = "";
                    textBox2.Text = "";
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void 좌석관리ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Form2().ShowDialog();
        }

        private void 사용자관리ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Form3().ShowDialog();
        }
    }
}
