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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Management
{
    public partial class Form1 : Form
    {
        DataTable table = new DataTable();
        public Form1()
        {
            InitializeComponent();

            table.Columns.Add("Name", typeof(string));
            table.Columns.Add("Price", typeof(string));
            table.Columns.Add("Count", typeof(string));
            table.Columns.Add("Total", typeof(string));

            dataview.DataSource = table;
            countUpDown.Value = 1;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MySqlConnection connection = new MySqlConnection("datasource=localhost;port=3306;username=root;password=1234;");
            connection.Open();
            if (connection.State == ConnectionState.Open)
            {
                SeverState.Text = "Connected";
                SeverState.ForeColor = Color.Green;
            }
            else
            {
                SeverState.Text = "DisConnected";
                SeverState.ForeColor = Color.Red;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (NameTxt.Text == "" || PriceTxt.Text == "")
            {
                MessageBox.Show("항목을 정확히 입력해주세요");
                NameTxt.Clear();
                PriceTxt.Clear();
            }
            else
            {
                //합계를 구하기 위해 품목명과 가격을 정의하고 total로 합침
                decimal price = decimal.Parse(PriceTxt.Text);
                decimal count = countUpDown.Value;
                decimal total = price * count;

                //text박스내의 정보를 표에 삽입
                table.Rows.Add(NameTxt.Text, PriceTxt.Text, countUpDown.Value, total);
                dataview.DataSource = table;

                //text박스의 정보 초기화
                NameTxt.Clear();
                PriceTxt.Clear();
                countUpDown.Value = 1;

                //합계
                decimal all = 0;
                for (int i = 0; i < dataview.Rows.Count; ++i)
                {
                    all += Convert.ToDecimal(dataview.Rows[i].Cells[3].Value);
                }
                TotalTxt.Text = all.ToString();
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            //행 지우기
            foreach (DataGridViewRow item in this.dataview.SelectedRows)
            {
                dataview.Rows.RemoveAt(item.Index);
            }

            //합계창에 수정된 값 넣기
            decimal all = 0;
            for (int i = 0; i < dataview.Rows.Count; ++i)
            {
                all += Convert.ToDecimal(dataview.Rows[i].Cells[3].Value);
            }

            TotalTxt.Text = all.ToString();
        }

        private void btnCal_Click(object sender, EventArgs e)
        {

            //DB연결 후 데이터 전송
            using (MySqlConnection conn = new MySqlConnection("Server=localhost;Port=3306;Database=pos_dataset;Uid=root;Pwd=1234"))
            {
                conn.Open();
                //각 행의 정보를 반복문으로 불러온다
                for (int i = 0; i < dataview.Rows.Count - 1; i++)
                {
                    String Name = dataview.Rows[i].Cells[0].Value.ToString();
                    String Price = dataview.Rows[i].Cells[1].Value.ToString();
                    String Count = dataview.Rows[i].Cells[2].Value.ToString();
                    String Total = dataview.Rows[i].Cells[3].Value.ToString();

                    //INSERT INTO 쿼리문으로 받아온 정보를 DB에 전송한다. 
                    string sql = string.Format("INSERT INTO sales_tb(name,price,count,total,c_num) VALUES  ('{0}',{1},{2},{3},{4}) ON DUPLICATE KEY UPDATE count = count + {5}, total = total + {6}", @Name, @Price, @Count, @Total, @i, @Count, @Total);

                    //INSERT INTO 쿼리문으로 이름으로 찾고 i_count의 수량에서 판매된 개수만큼 빼준다
                    string sql_count = string.Format("update item_tb set i_count = i_count - {0} where i_name = '{1}'", @Count, @Name);

                    //DB전송을 진행하고 실패시 에러메세지 출력
                    try
                    {
                        MySqlCommand command = new MySqlCommand(sql, conn);
                        command.ExecuteNonQuery();

                        MySqlCommand c_command = new MySqlCommand(sql_count, conn);
                        c_command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }

            MessageBox.Show("계산되었습니다.");

            //데이터 그리드뷰 초기화
            int rowCount = dataview.Rows.Count;
            for (int n = 0; n < rowCount; n++)
            {
                if (dataview.Rows[0].IsNewRow == false)
                    dataview.Rows.RemoveAt(0);
            }

            //합계창 초기화
            TotalTxt.Text = "0";
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Form3 dlg3 = new Form3();
            dlg3.ShowDialog();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            Form2 dlg2 = new Form2();
            dlg2.ShowDialog();
        }

    }
}
