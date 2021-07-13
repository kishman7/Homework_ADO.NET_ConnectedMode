using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ADO.NET_ConnectedMode
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //SqlConnection GetConnection()
        //{
        //    string connectionString = $"Data Source=DESKTOP-48R2R1B\\SQLEXPRESS;User ID=DodUser;Password=123;Integrated Security=false;Initial catalog=master;";
        //    var connection = new SqlConnection(connectionString);
        //    return connection;
        //}

        private void button1_Click(object sender, EventArgs e)
        {
            //створюємо зєднання
            //string connectionString = $"Data Source=DESKTOP-48R2R1B\\SQLEXPRESS;User ID=DodUser;Password=123;Integrated Security=false;Initial catalog=master;";
            string connectionString = $"Data Source={textBox1.Text};User ID={textBox2.Text};Password={textBox3.Text};Integrated Security=false;Initial catalog=master;";
            // для textBox використовуємо властивість UserSystemPasswordChar (true) - дозволяє шифрувати символи для пароля
            //створюємо обєкт зєднання
            var connection = new SqlConnection(connectionString);
            //GetConnection();

            //відкриваємо зєднання
            connection.StateChange += Program_StateChange;
            using (connection)
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandText = "select * from sys.databases";
                    //listBox1.Items.Add(command.ExecuteReader());
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                            //string value = "";
                            //for (int i = 0; i < reader.FieldCount; i++)
                            //{
                            //    value += reader.GetValue(i) + " ";
                            //}
                            //listBox1.Items.Add(value);
                            while (reader.Read())
                            {
                                //object id = reader[0];
                                //object name = reader[1];

                                object name = reader["Name"]; // считуємо конкретну колонку name
                                listBox1.Items.Add(name); // записуємо цю колонку в listBox1
                            }
                    }
                    reader.Close();

                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

        }

        private void Program_StateChange(object sender, StateChangeEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //створюємо зєднання
            //string connectionString = $"Data Source=DESKTOP-48R2R1B\\SQLEXPRESS;User ID=DodUser;Password=123;Integrated Security=false;Initial catalog=master;";
            string connectionString = $"Data Source={textBox1.Text};User ID={textBox2.Text};Password={textBox3.Text};Integrated Security=false;Initial catalog=master;";

            //створюємо обєкт зєднання
            var connection = new SqlConnection(connectionString);

            using (connection)
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    string dataBase = textBox4.Text;
                    connection.ChangeDatabase(dataBase); //міняємо базу, на ту що ввели

                    command.CommandText = "select * from sys.tables";
                    //listBox1.Items.Add(command.ExecuteReader());
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                            //string value = "";
                            //for (int i = 0; i < reader.FieldCount; i++)
                            //{
                            //    value += reader.GetValue(i) + " ";
                            //}
                            //listBox1.Items.Add(value);
                            while (reader.Read())
                            {
                                //object id = reader[0];
                                //object name = reader[1];

                                object name = reader["Name"]; // считуємо конкретну колонку name
                                listBox2.Items.Add(name); // записуємо цю колонку в listBox2
                            }
                    }
                    reader.Close();
                    
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            listBox2.Items.Clear(); // очищаємо вікно з таблицями
        }

        private void button4_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear(); // очищаємо вікго з базами даних
        }

        
    }
}
