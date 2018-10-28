using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AddDatabase
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string name = this.textBox1.Text.ToString();
            int count = Convert.ToInt32(this.textBox2.Text.ToString());
            LRS.OA.Model.UserInfo user = new LRS.OA.Model.UserInfo();
            LRS.OA.BLL.UserInfoService userBLL = new LRS.OA.BLL.UserInfoService();
            for (int i = 0; i < count; i++)
            {
                user.UName = name + i.ToString();
                user.UPwd = "123456789";
                user.SubTime = DateTime.Now.ToLocalTime();
                user.DelFlag = 0;
                user.ModifiedOn = DateTime.Now.ToLocalTime();
                user.Remark = "批量添加";
                user.Sort = i.ToString();
                userBLL.AddUserInfo50(user);
            }
            userBLL.saveUser50();
            MessageBox.Show("添加完成");
        }
    }
}
