using Library.Models;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Library.DataAccess.Impl
{
    public class SqliteBookAuditRepository : IRepository<BookAudit>
    {
        private readonly IDbConnection _connection;

        public SqliteBookAuditRepository(IDbConnection connection)
        {
            _connection = connection;
        }


        public int Add(BookAudit entity)
        {
            int newId = -1;

            using (_connection)
            {
                _connection.Open();

                using (IDbTransaction transaction = _connection.BeginTransaction())
                {
                    try
                    {
                        string insertQuery = "INSERT INTO BookAudit (BookId, ClientId) VALUES (@BookId, @ClientId); SELECT last_insert_rowid();";

                        using (SqliteCommand command = new SqliteCommand(insertQuery, _connection as SqliteConnection, (SqliteTransaction)transaction))
                        {
                            command.Parameters.AddWithValue("@BookId", entity.BookId);
                            command.Parameters.AddWithValue("@ClientId", entity.ClientId);

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
            using (_connection)
            {
                _connection.Open();

                var command = new SqliteCommand("DELETE FROM BookAudit WHERE id = @id", _connection as SqliteConnection);
                command.Parameters.Add(new SqliteParameter("id", id));

                var reader = command.ExecuteNonQuery();
            }
        }

        public void Dispose()
        {
            _connection.Dispose();
        }

        public BookAudit Find(int id)
        {
            BookAudit book = new BookAudit();

            using (_connection)
            {
                _connection.Open();

                var command = new SqliteCommand("SELECT BookAudit.Id, BookId, ClientId, Book.Title, Client.Name, Client.Family FROM BookAudit " +
                                                "JOIN Book ON BookAudit.BookId = Book.Id " +
                                                "JOIN Client ON BookAudit.ClientId = Client.Id WHERE BookAudit.Id = @id", _connection as SqliteConnection);
                command.Parameters.Add(new SqliteParameter("id", id));

                var reader = command.ExecuteReader();

                book = reader.HasRows ? GetRecord(reader) : null;
            }
            return book;
        }

        public IEnumerable<BookAudit> GetAll()
        {
            List<BookAudit> books = new List<BookAudit>();

            using (_connection)
            {
                _connection.Open();

                var command = new SqliteCommand("SELECT BookAudit.Id, BookId, ClientId, Book.Title, Client.Name, Client.Family FROM BookAudit " +
                                                "JOIN Book ON BookAudit.BookId = Book.Id " +
                                                "JOIN Client ON BookAudit.ClientId = Client.Id", _connection as SqliteConnection);
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    books.Add(GetRecord(reader));
                }
            }
            return books.AsEnumerable();
        }



        public void Update(BookAudit entity)
        {
            using (_connection)
            {
                _connection.Open();

                using (IDbTransaction transaction = _connection.BeginTransaction())
                {
                    try
                    {
                        string query = "UPDATE BookAudit SET BookId = @BookId, ClientId = @ClientId  WHERE Id = @Id";

                        using (SqliteCommand command = new SqliteCommand(query, _connection as SqliteConnection, (SqliteTransaction)transaction))
                        {
                            command.Parameters.AddWithValue("@BookId", entity.BookId);
                            command.Parameters.AddWithValue("@ClientId", entity.ClientId);

                            command.Parameters.AddWithValue("@Id", entity.Id);
                            command.ExecuteNonQuery();
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
        }

        private BookAudit GetRecord(SqliteDataReader reader)
        {
            return new BookAudit()
            {
                Id = reader.GetInt32(0),
                BookId = reader.GetInt32(1),
                ClientId = reader.GetInt32(2),
                Book = reader.GetString(3),
                Client = reader.GetString(4) + " " + reader.GetString(5),
            };
        }
    }
}