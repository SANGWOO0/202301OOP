using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Management
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            try
            {
                MySqlConnection connection = new MySqlConnection("Server = localhost;Database=pos_dataset;Uid=root;Pwd=1234;");

                connection.Open();
                // SQL 서버 연결

                int login_status = 0;
                // 로그인 상태 변수 선언, 비로그인 상태는 0

                string loginid = txtbox_id.Text;
                // 문자열 loginid 변수는 txtbox_id 의 텍스트값
                string loginpwd = txtbox_pwd.Text;
                // 문자열 loginpwd 변수는 txtbox_pwd 의 텍스트값

                string selectQuery = "SELECT * FROM account_info WHERE id = \'" + loginid + "\' ";

                MySqlCommand Selectcommand = new MySqlCommand(selectQuery, connection);
                // MySqlCommand는 MySQL로 명령어를 전송하기 위한 클래스.
                // MySQL에 selectQuery 값을 보내고, connection 값을 보내 연결을 시도한다.
                // 위 정보를 Selectcommand 변수에 저장한다.

                MySqlDataReader userAccount = Selectcommand.ExecuteReader();
                // MySqlDataReader은 입력값을 받기 위함.
                // Selectcommand 변수에 ExecuteReader() 객체를 통해 입력값을 받고,
                // 해당 정보를 userAccount 변수에 저장한다.

                while (userAccount.Read())
                // userAccount가 Read 되고 있을 동안
                {
                    if (loginid == (string)userAccount["id"] && loginpwd == (string)userAccount["pwd"])
                    // 만약 loginid변수의 값이 account_info 테이블 값의 id 정보와,
                    // loginpwd변수의 값이 account_info 테이블 값의 pwd 정보와 일치한다면
                    {
                        login_status = 1;
                        // 해당 변수 상태를 1로 바꾼다.
                    }
                }
                connection.Close();
                // MySQL과 연결을 끊는다.

                if (login_status == 1)
                // 만약 해당 변수 상태가 1이라면,
                {
                    MessageBox.Show("로그인 완료");
                    // 로그인 완료 메시지박스를 띄운다.
                    Form3 dlg3 = new Form3();
                    dlg3.ShowDialog();
                }
                else
                // 아니라면,
                {
                    MessageBox.Show("회원 정보를 확인해 주세요.");
                    // 오류 메시지박스를 띄운다.
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                // 예외값 발생 시 해당 정보와 관련된 메시지박스를 띄운다.
            }
        }

        private void btn_newmember_Click(object sender, EventArgs e)
        {
            Form5 showform5 = new Form5();
            showform5.ShowDialog();
        }
    }
}
