using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.Common;
using System.Net.Http;
using Test.Models;
using Test.Controllers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Security.Policy;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Test
{
    public partial class Form1 : Form
    {
        //string connectionString = @"Data Source=DESKTOP-O4872J7;Initial Catalog=Ordering;User ID=test;Password=123456";
        //new conection
        static string connectionString = @"Data Source=DESKTOP-O4872J7;Initial Catalog=Ordering;Integrated Security=True;";
        SqlConnection con = new SqlConnection(connectionString);
        SqlCommand cmd;
        SqlDataAdapter dataAdabtor;
        DataTable dataTable;
        int Selected_catID;
        int Selected_itemID=0;
        int Selected_Note ;
        int? Selected_user;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
               loadAll();

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }


        private DataTable load_categories()
        {
            dataTable = new DataTable();
            string query = "SELECT * FROM categories";
            con.Open();
            cmd = new SqlCommand(query, con);
            dataAdabtor = new SqlDataAdapter(cmd);
            dataAdabtor.Fill(dataTable);
            con.Close();
            return dataTable;
        }

        private async void loadAll(){
            listCateogries.DataSource = load_categories();
            listCateogries.DisplayMember = "Name";
            listCateogries.ValueMember = "Id";
            List<InterstDto> iterests=await InterestsControler.GetAll();
            foreach (InterstDto inrest in iterests)
            {
                cboIntresets.Items.Add(inrest.intrestText);
            }
            List<UserDto> users = await userController.GetAll();
            foreach (UserDto user in users)
            {

                cboAssignToUser.Items.Insert(user.id ,user.username);

            }
            loadnotes();

        }
        private void btnCatInsert_Click(object sender, EventArgs e)
        {
            string query = "INSERT INTO categories (Name) VALUES (@name) ";
            if (textCategory.Text == "")
            {
                MessageBox.Show("please insert data");
                return;
            }
            try
            {
                con.Open();
                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@name", textCategory.Text.Trim());
                cmd.ExecuteNonQuery();
                con.Close();
                loadAll();

            }
            catch (Exception ex){
                MessageBox.Show(ex.Message);
            }

        }

        private void listCateogries_Click(object sender, EventArgs e)
        {
            var item = listCateogries.SelectedValue;
            if (item != null)
            {
                Selected_catID = (int)item;
                listItems.DataSource = load_items((int)item);
                listItems.DisplayMember = "Name";
                listItems.ValueMember = "Id";
            }
        }

        private DataTable load_items(int id)
        {
            dataTable = new DataTable();
            string query = "SELECT * FROM Items WHERE CategoryId="+id+" ";
            con.Open();
            cmd = new SqlCommand(query, con);
            dataAdabtor = new SqlDataAdapter(cmd);
            dataAdabtor.Fill(dataTable);
            con.Close();
            return dataTable;
        }
        private DataTable LoadOneItem(int id)
        {
            dataTable = new DataTable();
            string query = "SELECT Name,Price FROM Items WHERE Id=" + id + " ";
            con.Open();
            cmd = new SqlCommand(query, con);
            dataAdabtor = new SqlDataAdapter(cmd);
            dataAdabtor.Fill(dataTable);
            con.Close();
            return dataTable;
        }
        private void btninsert_Click(object sender, EventArgs e)
        {
            string query = "INSERT INTO Items (Name,Price,CategoryId) VALUES (@name,@price,@catId) ";
            if (textItemName.Text == "" || textItemPrice.Text=="")
            {
                MessageBox.Show("please insert data");
                return;
            }
            try
            {
                con.Open();
                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@name", textItemName.Text.Trim());
                cmd.Parameters.AddWithValue("@price", decimal.Parse(textItemPrice.Text.Trim()));
                cmd.Parameters.AddWithValue("@catId", Selected_catID);
                cmd.ExecuteNonQuery();
                con.Close();
                loadAll();
                textItemName.Clear();
                textItemPrice.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void listItems_DoubleClick(object sender, EventArgs e)
        {
            var id = listItems.SelectedValue;
            Selected_itemID = (int)id;
            if (id != null)
            {
                dataGridView1.Columns[0].DataPropertyName = "Name";
                dataGridView1.Columns[1].DataPropertyName = "Price";
                foreach (DataRow dr in LoadOneItem((int)id).Rows)
                {
                    dataGridView1.Rows.Add(dr.ItemArray);
                }
            }

        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            if(listPrinters.CheckedItems.Count == 0 )
            {
                MessageBox.Show("no printer has cheeked");
                return;
            }
            if (Selected_itemID == 0)
            {
                MessageBox.Show("no item has cheeked");
                return;
            }
            string query = "INSERT INTO ItemPrinters (ItemId,Printer) VALUES (@itemID,@printer) ";
            foreach(var item in listPrinters.CheckedItems)
            {
            
                try
                {
                    con.Open();
                    cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@itemID", Selected_itemID);
                    cmd.Parameters.AddWithValue("@printer", item);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    loadAll();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                //listPrinters.SetItemChecked(,false);
            }
            MessageBox.Show("inserted to priter successfully");
        }

        private async void btnClearAll_Click(object sender, EventArgs e)
        {

        }

        private async void btnClearAll_Click_1(object sender, EventArgs e)
        {

        }
        private async void loadnotes()
        {
            List<NotesDto> objNote = await NoteController.GetAll();
            List<UserDto> users = await userController.GetAll();
            var query = from note in objNote
                        join user in users
                        on note.UserId equals user.id into results1
                        from final in results1.DefaultIfEmpty(new UserDto { username = null })
                        select new
                        {
                            Id = note.Id,
                            UserId = note.UserId,
                            Text = note.Text,
                            PlaceDateTime = note.PlaceDateTime,
                            userName = final.username

                        };

            foreach (var q in query)
            {
                string[] row = { q.Text, q.userName};
                var item = new ListViewItem(row);
                item.Tag = q.Id;
                listViewNotes.Items.Add(item);
                
            }
        }
        private void listViewNotes_DoubleClick(object sender, EventArgs e)
        {
            tbNotes.Text = listViewNotes.SelectedItems[0].Text;
            Selected_Note = (int)listViewNotes.SelectedItems[0].Tag;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.ShowDialog();
        }

        private async void btnSaveNote_Click(object sender, EventArgs e)
        {
            if (cboAssignToUser.SelectedIndex < 0)
                MessageBox.Show("selest user");
            var newNote = new NotesDto
            {
                Id = Selected_Note,
                UserId = cboAssignToUser.SelectedIndex,
                Text = tbNotes.Text,
                PlaceDateTime = DateTime.Now.ToString()
            };
           var res = await NoteController.update(newNote);

        }

        private void cboAssignToUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            //var user = cboAssignToUser.SelectedIndex;
        }
    }

}



