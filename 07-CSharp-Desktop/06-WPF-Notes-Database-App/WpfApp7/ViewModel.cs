using System;
using System.Collections.ObjectModel;
using System.Windows;
using MySql.Data.MySqlClient;

namespace WpfApp7
{
    public class ViewModel
    {
        private string connectionString = "server=localhost;user id=root;password=;database=zametki;SslMode=none;";

        public ObservableCollection<Note> Notes = new ObservableCollection<Note>();

        public Note SelectedNote;
        public string Title;
        public string Text;
        public DateTime? Deadline;

        public void LoadNotes()
        {
            Notes.Clear();
            try
            {
                using (var conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = "SELECT id, title, text, deadline FROM notes";
                    using (var cmd = new MySqlCommand(sql, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Note n = new Note();
                            n.Id = reader.GetInt32("id");
                            n.Title = reader.GetString("title");
                            n.Text = reader.GetString("text");
                            n.Deadline = reader.IsDBNull(reader.GetOrdinal("deadline")) ? (DateTime?)null : reader.GetDateTime("deadline");
                            Notes.Add(n);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки из БД: " + ex.Message);
            }
        }

        public void AddNote()
        {
            if (string.IsNullOrWhiteSpace(Title) || string.IsNullOrWhiteSpace(Text))
            {
                MessageBox.Show("Введите заголовок и текст!");
                return;
            }

            try
            {
                using (var conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = "INSERT INTO notes (title, text, deadline) VALUES (@t, @c, @d)";
                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@t", Title);
                        cmd.Parameters.AddWithValue("@c", Text);
                        cmd.Parameters.AddWithValue("@d", (object)Deadline ?? DBNull.Value);
                        cmd.ExecuteNonQuery();
                    }
                }

                LoadNotes();
                Title = "";
                Text = "";
                Deadline = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка добавления в БД: " + ex.Message);
            }
        }

        public void DeleteNote()
        {
            if (SelectedNote == null)
            {
                MessageBox.Show("Выберите заметку!");
                return;
            }

            try
            {
                using (var conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = "DELETE FROM notes WHERE id=@id";
                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", SelectedNote.Id);
                        cmd.ExecuteNonQuery();
                    }
                }

                LoadNotes();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка удаления из БД: " + ex.Message);
            }
        }
    }
}
