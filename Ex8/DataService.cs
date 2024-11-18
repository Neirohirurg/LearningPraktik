using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using System.Configuration;


namespace Ex8
{
    class DataService
    {
        private readonly string _connectionString = "Data Source=89.179.146.97, 1433;Database=company;Persist Security Info=True;User ID=sa; Password=;Pooling=False;Encrypt=True;TrustServerCertificate=True;";
        
        public List<string> PartnerTypes = new List<string>();
        public DataService() { }

        // Возвращает список партнеров с БД
        public List<Partner> GetPartners()
        {
            List<Partner> partners = new List<Partner>();
            string query = "SELECT * FROM partners";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Partner partner = new Partner
                            {
                                PartnerID = reader.GetInt32(0),
                                PartnerType = reader.GetString(1),
                                PartnerName = reader.GetString(2),
                                director = reader.GetString(3),
                                email = reader.GetString(4),
                                phoneNumber = reader.GetString(5),
                                address = reader.GetString(6),
                                INN = reader.GetString(7),
                                rating = reader.GetInt32(8),
                            };
                            this.PartnerTypes.Add(reader.GetString(1));
                            partners.Add(partner);
                        }
                    }
                }
            }

            return partners;
        }

        // Добавляет данные в БД
        public bool AddPartner(Partner partner)
        {
            string query = @"
        INSERT INTO partners (PartnerType, PartnerName, director, email, phoneNumber, address, INN, rating)
        VALUES (@PartnerType, @PartnerName, @director, @email, @phoneNumber, @address, @INN, @rating)";

            // Используем транзакцию для добавления всех партнеров в базу данных
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                            using (SqlCommand command = new SqlCommand(query, connection, transaction))
                            {
                                command.Parameters.AddWithValue("@PartnerType", " ");
                                command.Parameters.AddWithValue("@PartnerName", partner.PartnerName);
                                command.Parameters.AddWithValue("@director", partner.director);
                                command.Parameters.AddWithValue("@email", partner.email);
                                command.Parameters.AddWithValue("@phoneNumber", partner.phoneNumber);
                                command.Parameters.AddWithValue("@address", partner.address);
                                command.Parameters.AddWithValue("@INN", " ");
                                command.Parameters.AddWithValue("@rating", partner.rating);

                                command.ExecuteNonQuery();
                            }      

                        transaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        Console.WriteLine(ex.Message);
                        return false;
                    }
                }
            }
        }
    }
}
