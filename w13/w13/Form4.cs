using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace w13
{
    public partial class Form4 : Form
    {
        string connectionString = "server=localhost; uid=root; pwd=; database=premier_league";
        MySqlConnection sqlConnect;
        MySqlCommand sqlCommand;
        MySqlDataAdapter sqlAdapter;
        string sqlQuery;
        DataTable dtTeam = new DataTable();
        DataTable dtNat = new DataTable();
        DataTable dtA = new DataTable();
        public Form4()
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            sqlConnect = new MySqlConnection(connectionString);

            sqlQuery = "SELECT p.team_id as 'Team ID', t.team_name as 'Team Name'\r\nFROM premier_league.player p, premier_league.team t\r\nWHERE p.team_id = t.team_id;";
            sqlCommand = new MySqlCommand(sqlQuery, sqlConnect);
            sqlAdapter = new MySqlDataAdapter(sqlCommand);
            sqlAdapter.Fill(dtTeam);
            comboteam.DataSource = dtTeam;
            comboteam.DisplayMember = "Team Name";
            comboteam.ValueMember = "Team ID";

        }

        private void comboteam_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dtB = new DataTable();
            sqlQuery = "SELECT p.player_name as 'Name', n.nation as 'Nationality',p.playing_pos as 'Playing Position',p.team_number as 'Number',p.height as 'Height' ,p.weight as 'Weight',p.birthdate as 'Birthdate'\r\nFROM premier_league.nationality n, premier_league.player p, premier_league.team t\r\nWHERE n.nationality_id = p.nationality_id AND p.team_id = '" + comboteam.ValueMember.ToString() + "'\r\nGROUP BY p.player_name;";
            sqlCommand = new MySqlCommand(sqlQuery, sqlConnect);
            sqlAdapter = new MySqlDataAdapter(sqlCommand);
            sqlAdapter.Fill(dtB);
            dataGridView1.DataSource = dtB;
        }

        private void buttondelete_Click(object sender, EventArgs e)
        {
            try
            {
                //string deleteQuery = "DELETE FROM test_db.users WHERE id = " + textBoxID.Text;
                sqlConnect.Open();
                //sqlCommand = new MySqlCommand(deleteQuery, sqlConnect);

                if (sqlCommand.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("PLAYER DELETED");
                }
                else
                {
                    MessageBox.Show("PLAYER NOT DELETED");
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
