using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace w13
{
    public partial class Form2 : Form
    {
        string connectionString = "server=localhost; uid=root; pwd=; database=premier_league";
        MySqlConnection sqlConnect;
        MySqlCommand sqlCommand;
        MySqlDataAdapter sqlAdapter;
        string sqlQuery;
        DataTable dtTeam = new DataTable();
        DataTable dtNat = new DataTable();
        DataTable dtA = new DataTable();

        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

            sqlConnect = new MySqlConnection(connectionString);
            date1.CustomFormat = "yyyy-MM-dd";

            sqlQuery = "SELECT p.nationality_id as 'Nationality ID', n.nation as 'Nationality Name'\r\nFROM premier_league.nationality n, premier_league.player p\r\nWHERE n.nationality_id = p.nationality_id\r\n GROUP BY p.nationality_id;";
            sqlCommand = new MySqlCommand(sqlQuery, sqlConnect);
            sqlAdapter = new MySqlDataAdapter(sqlCommand);
            sqlAdapter.Fill(dtTeam);
            comboaddnat.DataSource = dtTeam;
            comboaddnat.DisplayMember= "Nationality Name";
            comboaddnat.ValueMember = "Nationality ID";

            sqlQuery = "SELECT p.team_id as 'Team ID', t.team_name as 'Team Name'\r\nFROM premier_league.player p, premier_league.team t\r\nWHERE p.team_id = t.team_id;";
            sqlCommand = new MySqlCommand(sqlQuery, sqlConnect);
            sqlAdapter = new MySqlDataAdapter(sqlCommand);
            sqlAdapter.Fill(dtNat);
            comboaddteam.DataSource = dtNat;
            comboaddteam.DisplayMember = "Team Name";
            comboaddteam.ValueMember = "Team ID";

        }


        private void buttonadd_Click(object sender, EventArgs e)
        {
            string hurufawal = tbname.Text[0].ToString();
            sqlQuery = "SELECT * \r\nFROM premier_league.player p\r\nWHERE player_id LIKE '"+ hurufawal +"%'\r\nORDER BY 1 desc;";
            sqlCommand = new MySqlCommand(sqlQuery, sqlConnect);
            sqlAdapter = new MySqlDataAdapter(sqlCommand);
            sqlAdapter.Fill(dtA);
            combo1.DataSource = dtNat;
            combo1.DisplayMember = "player_id";
            string playerid = (Int32.Parse(combo1.DisplayMember)+1).ToString();
            
            string insertQuery = "INSERT INTO premier_league.player p(player_id,team_number,player_name,nationality_id,playing_pos,height,weight,birthdate,team_id,status,delete)" + "VALUES('" + playerid + "','"+ tbnum.Text + "','" + tbname.Text + "'," + comboaddnat.ValueMember + tbpos.Text + "'," + tbheight.Text + "'," + tbweight.Text + "'," + date1.Text + "','1','0'"  + ")";
            sqlConnect.Open();
            sqlCommand = new MySqlCommand(insertQuery, sqlConnect);

            try
            {
                if (sqlCommand.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Player Added");
                }
                else
                {
                    MessageBox.Show("Player Not Added");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            sqlConnect.Close();

        }
    }
}
