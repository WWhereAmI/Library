using Library.Models;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Library.DataAccess.Impl
{
    public class SqliteClientRepository : IRepository<Client>
    {
        private readonly IDbConnection _connection;

        public SqliteClientRepository(IDbConnection connection)
        {
            _connection = connection;
        }


        public int Add(Client entity)
        {
            int newId = -1;

            using (_connection)
            {
                _connection.Open();

                using (IDbTransaction transaction = _connection.BeginTransaction())
                {
                    try
                    {
                        string insertQuery = "INSERT INTO Client (Name, Family) VALUES (@Name, @Family); SELECT last_insert_rowid();";

                        using (SqliteCommand command = new SqliteCommand(insertQuery, _connection as SqliteConnection, (SqliteTransaction)transaction))
                        {
                            command.Parameters.AddWithValue("@Name", entity.Name);
                            command.Parameters.AddWithValue("@Family", entity.Family);

                            newId = Convert.ToInt32(command.ExecuteScalar());
                        }

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }
                }
            }

            return newId;
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _connection.Dispose();
        }

        public Client Find(int id)
        {
            Client client = new Client();

            using (_connection)
            {
                _connection.Open();

                var command = new SqliteCommand("SELECT * FROM Client WHERE id = @id", _connection as SqliteConnection);
                command.Parameters.Add(new SqliteParameter("id", id));

                var reader = command.ExecuteReader();

                client = reader.HasRows ? GetRecord(reader) : null;
            }
            return client;
        }

        public IEnumerable<Client> GetAll()
        {
            List<Client> books = new List<Client>();

            using (_connection)
            {
                _connection.Open();

                var command = new SqliteCommand("SELECT * FROM Client", _connection as SqliteConnection);
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    books.Add(GetRecord(reader));
                }
            }
            return books.AsEnumerable();
        }


        public void Update(Client entity)
        {
            throw new NotImplementedException();
        }

        private Client GetRecord(SqliteDataReader reader)
        {
            return new Client()
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1),
                Family = reader.GetString(2),
            };
        }
    }
}