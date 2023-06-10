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
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }

        private void btn_newmember_Click(object sender, EventArgs e)
        {
            try
            {
                MySqlConnection connection = new MySqlConnection("Server = localhost;Database=pos_dataset;Uid=root;Pwd=1234;");
                // SQL 서버와 연결.
                // Server = localhost : 로컬 호스트 (내 컴퓨터) 서버와 연결
                // Database = 스키마 이름
                // Uid = DB 로그인 아이디
                // Pwd = DB 로그인 비밀번호

                connection.Open();
                // SQL 서버 연결

                string insertQuery = "INSERT INTO account_info (name, id, pwd) VALUES ('" + txtbox_name.Text + "', '" + txtbox_id.Text + "', '" + txtbox_pwd.Text + "');";
                // 문자열 insertQuery 변수 선언.
                // MySQL에 전송할 명령어를 입력한다.
                // 실제로 MySQL에 전송될 명령어는 "" 사이의 값.
                // dbtest 스키마의 account_info 테이블에 값을 추가하기 위한 변수.

                MySqlCommand command = new MySqlCommand(insertQuery, connection);
                // MySqlCommand는 MySQL로 명령어를 전송하기 위한 클래스.
                // MySQL에 insertQuery 값을 보내고, connection 값을 보내 연결을 시도한다.
                // 위 정보를 command 변수에 저장한다.

                if (command.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show(txtbox_name.Text + "님 회원가입 완료, 사용할 아이디는 " + txtbox_id.Text + "입니다.");
                    // 정상 회원가입 안내 메시지박스를 띄운다.
                    connection.Close();
                    // SQL 연결을 끊는다.
                    Close();
                    // Form2를 닫는다. (Form1의 로그인 창으로 돌아가기 위함)
                }
                else
                // 아니라면,
                {
                    MessageBox.Show("비정상 입력 정보, 재확인 요망");
                    // 오류 메시지박스를 띄운다.
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                // 예외값 발생 시 해당 정보와 관련된 메시지박스를 띄운다.
            }
        }
    }


}
