using Library.Models;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Library.DataAccess.Impl
{
    public class SqliteBookRepository : IRepository<Book>
    {
        private readonly IDbConnection _connection;

        public SqliteBookRepository(IDbConnection connection)
        {
            _connection = connection;
        }


        public int Add(Book entity)
        {
            int newId = -1;

            using (_connection)
            {
                _connection.Open();

                using (IDbTransaction transaction = _connection.BeginTransaction())
                {
                    try
                    {
                        string insertQuery = "INSERT INTO Book (Title, Author, Description) VALUES (@Title, @Author, @Description); SELECT last_insert_rowid();";

                        using (SqliteCommand command = new SqliteCommand(insertQuery, _connection as SqliteConnection, (SqliteTransaction)transaction))
                        {
                            command.Parameters.AddWithValue("@Title", entity.Title);
                            command.Parameters.AddWithValue("@Author", entity.Author);

                            if (entity.Description != null)
                            {
                                command.Parameters.AddWithValue("@Description", entity.Description);
                            }

                         
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

        public Book Find(int id)
        {
            Book book = new Book();

            using (_connection)
            {
                _connection.Open();

                var command = new SqliteCommand("SELECT * FROM Book WHERE id = @id", _connection as SqliteConnection);
                command.Parameters.Add(new SqliteParameter("id", id));

                var reader = command.ExecuteReader();

                book = reader.HasRows ? GetRecord(reader) : null;
            }
            return book;
        }

        public IEnumerable<Book> GetAll()
        {
            List<Book> books = new List<Book>();

            using (_connection)
            {
                _connection.Open();

                var command = new SqliteCommand("SELECT * FROM Book", _connection as SqliteConnection);
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    books.Add(GetRecord(reader));
                }
          }
            return books.AsEnumerable();
        }


        public void Update(Book entity)
        {
            using (_connection)
            {
                _connection.Open();

                using (IDbTransaction transaction = _connection.BeginTransaction())
                {
                    try
                    {
                        string query = "UPDATE Book SET Title = @Title, Author = @Author, Description = @Description, Status = @Status  WHERE Id = @Id";

                        using (SqliteCommand command = new SqliteCommand(query, _connection as SqliteConnection, (SqliteTransaction)transaction))
                        {
                            command.Parameters.AddWithValue("@Title", entity.Title);
                            command.Parameters.AddWithValue("@Author", entity.Author);
                            command.Parameters.AddWithValue("@Description", entity.Description);
                            command.Parameters.AddWithValue("@Status", entity.Status);
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

        private Book GetRecord(SqliteDataReader reader)
        {
            return new Book()
            {
                Id = reader.GetInt32(0),
                Title = reader.GetString(1),
                Description = reader.GetString(2),
                Author = reader.GetString(3),
                Status = (Const.BookStatus)reader.GetInt32(4)
            };
        }
    }
}