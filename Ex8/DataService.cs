using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using System.Configuration;
using System.Windows;
using System.Diagnostics.Eventing.Reader;


namespace Ex8
{
    class DataService
    {
        private readonly string _connectionString = "Data Source=;Database=company;Persist Security Info=True;User ID=; Password=;Pooling=False;Encrypt=True;TrustServerCertificate=True;";
        
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
                                PartnerType = reader.IsDBNull(1) ? null : reader.GetString(1).TrimEnd(),
                                PartnerName = reader.IsDBNull(2) ? null : reader.GetString(2).TrimEnd(), 
                                director = reader.IsDBNull(3) ? null : reader.GetString(3).TrimEnd(), 
                                email = reader.IsDBNull(4) ? null : reader.GetString(4).TrimEnd(), 
                                phoneNumber = reader.IsDBNull(5) ? null : reader.GetString(5).TrimEnd(), 
                                address = reader.IsDBNull(6) ? null : reader.GetString(6).TrimEnd(), 
                                INN = reader.IsDBNull(7) ? null : reader.GetString(7).TrimEnd(), 
                                rating = reader.IsDBNull(8) ? 0 : reader.GetInt32(8),
                                discont = reader.IsDBNull(9) ? 0 : reader.GetInt32(9)
                            };
                            partners.Add(partner);
                        }
                    }
                }
                connection.Close();
            }

            return partners;
        }

        // Добавляет данные партнера в БД
        public bool AddPartner(Partner partner)
        {
            string query = @"
    INSERT INTO partners (partnerType, partnerName, director, email, phoneNumber, address, INN, rating)
    VALUES (@PartnerType, @PartnerName, @director, @email, @phoneNumber, @address, @INN, @rating)";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                
                    try
                    {
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@PartnerType", partner.PartnerType);
                            command.Parameters.AddWithValue("@PartnerName", partner.PartnerName);
                            command.Parameters.AddWithValue("@director", partner.director);
                            command.Parameters.AddWithValue("@email", partner.email);
                            command.Parameters.AddWithValue("@phoneNumber", partner.phoneNumber);
                            command.Parameters.AddWithValue("@address", partner.address);
                            command.Parameters.AddWithValue("@INN", "testINN");
                            command.Parameters.AddWithValue("@rating", partner.rating);

                            command.ExecuteNonQuery();
                        }
                 
                        return true;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        return false;
                    }
                    finally
                    {
                        connection.Close();
                    }
                
            }
        }


        public bool UpdatePartner(Partner partner)
        {
            string query = @"
                            UPDATE partners 
                            SET 
                                partnerType = @PartnerType,
                                partnerName = @PartnerName,
                                director = @director,
                                email = @email,
                                phoneNumber = @PhoneNumber,
                                address = @address,
                                INN = @INN,
                                rating = @rating
                            WHERE id = @PartnerID";

            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                connection.Open();
                
                    try
                    {
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@PartnerType", partner.PartnerType);
                            command.Parameters.AddWithValue("@PartnerName", partner.PartnerName);
                            command.Parameters.AddWithValue("@director", partner.director);
                            command.Parameters.AddWithValue("@email", partner.email);
                            command.Parameters.AddWithValue("@phoneNumber", partner.phoneNumber);
                            command.Parameters.AddWithValue("@address", partner.address);
                            command.Parameters.AddWithValue("@INN", "testINN");
                            command.Parameters.AddWithValue("@rating", partner.rating);
                            command.Parameters.AddWithValue("@PartnerID", partner.PartnerID);

                            command.ExecuteNonQuery();
                        }

                        
                        return true;
                    }
                    catch (Exception ex) 
                    { 
                        MessageBox.Show(ex.ToString());
                        return false;
                    }
                    
                    finally { connection.Close(); }
                
            }
        }

        public List<Sale> GetSales()
        {
            List<Sale> sales = new List<Sale>();
            string query = "SELECT * FROM sales";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Sale sale = new Sale
                            {
                                ID = reader.GetInt32(0),
                                ProductName = reader.GetString(1).TrimEnd(),
                                PartnerName = reader.GetString(2).TrimEnd(),
                                ProductAmount = reader.GetInt32(3),
                                DateOfSale = reader.GetDateTime(4)

                            };
                            sales.Add(sale);
                        }
                    }
                }
                connection.Close();
            }

            return sales;
        }

        public List<string> GetPartnersTypes()
        {
            List<string> types = new List<string> { "ЗАО", "ООО", "ПАО", "ОАО"};
            return types;
        }

    }
    
}
