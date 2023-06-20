using System;
using System.Data.SqlClient;//Using statement for automatic disposal of resources

namespace ConsoleCrudApp // Note: actual namespace depends on the project name.
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlConnection sqlConnection;
            string connectionString = @"Data Source=LTIN408556\SQLEXPRESS;Integrated Security=SSPI;Initial Catalog=ConsoleDB";
            sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            try
            {
                string ans;
                Console.WriteLine("DataBase Connection successfully established MsS200!");
                do
                {
                    Console.WriteLine("Enter the below option that you want to perform \n1.Creation\n2.Retrieve\n3.Update\n4.Delete\n");
                    int choice = int.Parse(Console.ReadLine() ?? "0");
                    switch (choice)
                    {
                        case 1:   //CREATE
                            Console.WriteLine("Enter your name");
                            string Username = Console.ReadLine() ?? "0";
                            Console.WriteLine("Enter your Age");
                            int Userage = int.Parse(Console.ReadLine() ?? "0");
                            Console.WriteLine("Enter PhoneNo");
                            int Phoneno = int.Parse(Console.ReadLine() ?? "0");
                            //string query = $"SELECT * FROM Users WHERE Name = '{name}' AND Age = {age} AND Phoneno = {Phoneno}";
                            //Interpolated strings are a feature of C#,  for creating readable and maintainable code( $"" and{}).
                            //Parameterized queries to avoid SQL injection attacks and simplify the code
                            string insertQuery = "INSERT INTO Details(User_name,User_age,Phone_no) Values (@Username,@Userage,@Phoneno)";
                            SqlCommand insertcommand = new SqlCommand(insertQuery, sqlConnection);
                            insertcommand.Parameters.AddWithValue("@Username", Username);
                            insertcommand.Parameters.AddWithValue("@Userage", Userage);
                            insertcommand.Parameters.AddWithValue("@Phoneno", Phoneno);
                            insertcommand.ExecuteNonQuery();
                            Console.WriteLine("Data Inserted Successfully");
                            break;

                        case 2:  //RETRIEVE
                            string displayQuery = "SELECT * FROM Details";
                            SqlCommand displayCommand = new SqlCommand(displayQuery, sqlConnection);
                            SqlDataReader reader = displayCommand.ExecuteReader();
                            while (reader.Read())
                            {
                                Console.WriteLine("ID: " + reader.GetValue(0).ToString());
                                Console.WriteLine("Name: " + reader.GetValue(1).ToString());
                                Console.WriteLine("Age: " + reader.GetValue(2).ToString());
                                Console.WriteLine("PhoneNo: " + reader.GetValue(3).ToString());
                            }
                            reader.Close();
                            break;
                        case 3:  //UPDATE
                            int u_id;
                            int u_age;
                            Console.WriteLine("Enter user ID that you want to update");
                            u_id = int.Parse(Console.ReadLine() ?? "0");
                            Console.WriteLine("Enter user age that want to update");
                            u_age = int.Parse(Console.ReadLine() ?? "0");
                            string updateQuery = "UPDATE DETAILS SET User_age =" + u_age + "  WHERE User_Id =" + u_id + "";
                            SqlCommand updateCommand = new SqlCommand(updateQuery, sqlConnection);
                            updateCommand.ExecuteNonQuery();
                            Console.WriteLine("Data Updated successfully");
                            break;
                        case 4:   //DELETE
                            int d_id;
                            Console.WriteLine("Enter user Id that you want to delete record");
                            d_id = int.Parse(Console.ReadLine() ?? "0");
                            string deleteQuery = "DELETE FROM DETAILS WHERE User_id = " + d_id + "";
                            SqlCommand deleteCommand = new SqlCommand(deleteQuery, sqlConnection);
                            deleteCommand.ExecuteNonQuery();
                            Console.WriteLine("Deletion is successful!");
                            break;
                        default:
                            Console.WriteLine("Invalid Input");
                            break;
                    }
                    Console.WriteLine("Do you want to continue?");
                    ans = Console.ReadLine()??"0";
                } while (ans != "No");
            }       
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                
            }
            finally
            {
                sqlConnection.Close();
            }
            
        }
    }
}
