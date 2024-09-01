


using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Crud
{
    public class Student
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public double Marks { get; set; }
        public string Batch { get; set; }
        public string Course { get; set; }
    }

    public class Email
    {
        public virtual bool SendMail(string toAddress, string subject, string body)
        {
            Console.WriteLine($"Sending email to {toAddress} with subject '{subject}' and body '{body}'");
            return true;
        }
    }

    internal class Program
    {
        static SqlConnection connection;
        static SqlCommand command;

        static void Main(string[] args)
        {
            int choice = Menu();
            switch (choice)
            {
                case 1: GetRecords(); break;
                case 2: AddStudent(); break;
                case 3: DeleteRecord(); break;
                case 4: EditRecord(); break;
                default:
                    Console.WriteLine("Invalid choice"); break;
            }
        }

        static SqlConnection GetConnection()
        {
            string connectionString = "Data Source=PRASAD3759; Initial Catalog=PracticeDb; Integrated Security=True;";
            connection = new SqlConnection(connectionString);
            return connection;
        }

        static int Menu()
        {
            Console.WriteLine("1. Get Records");
            Console.WriteLine("2. Add Student");
            Console.WriteLine("3. Delete Record");
            Console.WriteLine("4. Edit Record");
            Console.WriteLine("Enter your choice:");
            int choice = int.Parse(Console.ReadLine());
            return choice;
        }

        static void GetRecords()
        {
            string connectionString = "Data Source=PRASAD3759; Initial Catalog=PracticeDb; Integrated Security=True;";
            connection = new SqlConnection(connectionString);
            command = new SqlCommand("SELECT * FROM Student", connection);

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Console.WriteLine($"{reader["ID"]} {reader["Name"]} {reader["DateOfBirth"]:yyyy-MM-dd} {reader["Marks"]} {reader["Batch"]} {reader["Course"]}");
                }
            }
            else
            {
                Console.WriteLine("No records found.");
            }

            reader.Close();
            connection.Close();
        }

        static void AddStudent()
        {
            Console.WriteLine("Enter ID:");
            int id = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter Name:");
            string name = Console.ReadLine();

            Console.WriteLine("Enter Date of Birth (yyyy-MM-dd):");
            DateTime dateOfBirth = DateTime.Parse(Console.ReadLine());

            Console.WriteLine("Enter Marks:");
            double marks = double.Parse(Console.ReadLine());

            Console.WriteLine("Enter Batch:");
            string batch = Console.ReadLine();

            Console.WriteLine("Enter Course:");
            string course = Console.ReadLine();

            string connectionString = "Data Source=PRASAD3759; Initial Catalog=PracticeDb; Integrated Security=True;";
            connection = new SqlConnection(connectionString);
            command = new SqlCommand("INSERT INTO Student (ID, Name, DateOfBirth, Marks, Batch, Course) VALUES (@ID, @Name, @DateOfBirth, @Marks, @Batch, @Course)", connection);

            command.Parameters.AddWithValue("@ID", id);
            command.Parameters.AddWithValue("@Name", name);
            command.Parameters.AddWithValue("@DateOfBirth", dateOfBirth);
            command.Parameters.AddWithValue("@Marks", marks);
            command.Parameters.AddWithValue("@Batch", batch);
            command.Parameters.AddWithValue("@Course", course);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();

            Email email = new Email();
            email.SendMail("student@example.com", "Welcome", $"Dear {name}, you have been added to the Student database.");
        }

        static void DeleteRecord()
        {
            Console.WriteLine("Enter ID of the student to delete:");
            int id = int.Parse(Console.ReadLine());

            string connectionString = "Data Source=PRASAD3759; Initial Catalog=PracticeDb; Integrated Security=True;";
            connection = new SqlConnection(connectionString);
            command = new SqlCommand("DELETE FROM Student WHERE ID = @ID", connection);

            command.Parameters.AddWithValue("@ID", id);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        static void EditRecord()
        {
            Console.WriteLine("Enter ID of the student to edit:");
            int id = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter new Name:");
            string name = Console.ReadLine();

            Console.WriteLine("Enter new Date of Birth (yyyy-MM-dd):");
            DateTime dateOfBirth = DateTime.Parse(Console.ReadLine());

            Console.WriteLine("Enter new Marks:");
            double marks = double.Parse(Console.ReadLine());

            Console.WriteLine("Enter new Batch:");
            string batch = Console.ReadLine();

            Console.WriteLine("Enter new Course:");
            string course = Console.ReadLine();

            string connectionString = "Data Source=PRASAD3759; Initial Catalog=PracticeDb; Integrated Security=True;";
            connection = new SqlConnection(connectionString);
            command = new SqlCommand("UPDATE Student SET Name = @Name, DateOfBirth = @DateOfBirth, Marks = @Marks, Batch = @Batch, Course = @Course WHERE ID = @ID", connection);

            command.Parameters.AddWithValue("@ID", id);
            command.Parameters.AddWithValue("@Name", name);
            command.Parameters.AddWithValue("@DateOfBirth", dateOfBirth);
            command.Parameters.AddWithValue("@Marks", marks);
            command.Parameters.AddWithValue("@Batch", batch);
            command.Parameters.AddWithValue("@Course", course);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
}
