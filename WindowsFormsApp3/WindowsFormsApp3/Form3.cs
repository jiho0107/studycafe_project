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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            Text = "사용자 관리";
            dataGridView1.DataSource = DataManager.Users;
        }

        private void button1_Click(object sender, EventArgs e) //사용자 추가
        {
            try
            {
                if (DataManager.Users.Exists((x) => x.Id == int.Parse(textBox1.Text)))
                {
                    MessageBox.Show("사용자 ID가 이미 존재합니다.");
                }
                else
                {
                    User user = new User()
                    {
                        Id = int.Parse(textBox1.Text),
                        Name = textBox2.Text,
                        Charge = int.Parse(textBox3.Text)
                    };
                    DataManager.Users.Add(user);
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = DataManager.Users;
                    DataManager.Save();
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void button2_Click(object sender, EventArgs e) //사용자 수정
        {
            try
            {
                User user = DataManager.Users.Single((x) => x.Id == int.Parse(textBox1.Text));
                user.Name = textBox2.Text;
                user.Charge = int.Parse(textBox3.Text);

                dataGridView1.DataSource = null;
                dataGridView1.DataSource = DataManager.Users;
                DataManager.Save();
            }
            catch (Exception ex)
            {
                MessageBox.Show("존재하지 않는 사용자입니다.");
            }
        }

        private void button3_Click(object sender, EventArgs e) //사용자 탈퇴
        {
            try
            {
                User user = DataManager.Users.Single((x) => x.Id == int.Parse(textBox1.Text));
                DataManager.Users.Remove(user);

                dataGridView1.DataSource = null;
                dataGridView1.DataSource = DataManager.Users;
                DataManager.Save();
            }
            catch (Exception ex)
            {
                MessageBox.Show("존재하지 않는 사용자입니다.");
            }
        }

        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            try
            {
                User user = dataGridView1.CurrentRow.DataBoundItem as User;
                textBox1.Text = user.Id.ToString();
                textBox2.Text = user.Name;
                textBox3.Text = user.Charge.ToString();
            }
            catch (Exception ex)
            {

            }
        }
    }
}
