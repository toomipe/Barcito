using barcito.Logica;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace barcito.Persistencia
{
  public class CategoriaRepository : IRepository<Categoria>
  {
    private ConexiónMySQL conexiónMySQL;

    public CategoriaRepository()
    {
      conexiónMySQL = new ConexiónMySQL();
    }

    public void Save(Categoria categoria)
    {
      string QUERY = "INSERT INTO categoria (Nombre, urlImagen) VALUES (@nombre, @urlImagen)";
      MySqlCommand miComando = new MySqlCommand(QUERY);
      miComando.Connection = conexiónMySQL.GetConnection();
      miComando.Parameters.AddWithValue("@nombre", categoria.Nombre);
      miComando.Parameters.AddWithValue("@urlImagen", categoria.UrlImagen);

      miComando.ExecuteNonQuery();
    }

    public Categoria FindById(int id)
    {
      string QUERY = "SELECT * FROM categoria WHERE CategoriaID = @id";
      MySqlCommand miComando = new MySqlCommand(QUERY);
      miComando.Connection = conexiónMySQL.GetConnection();
      miComando.Parameters.AddWithValue("@id", id);

      MySqlDataReader mReader = miComando.ExecuteReader();
      Categoria unaCategoria = null;

      if (mReader.Read())
      {
        unaCategoria = new Categoria
        {
          CategoriaID = mReader.GetInt32("CategoriaID"),
          Nombre = mReader.GetString("Nombre"),
          UrlImagen = mReader.GetString("urlImagen")
        };
      }

      mReader.Close();
      return unaCategoria;
    }

    public List<Categoria> FindAll()
    {
      string QUERY = "SELECT * FROM categoria";
      List<Categoria> listaCategoria = new List<Categoria>();

      MySqlCommand miComando = new MySqlCommand(QUERY);
      miComando.Connection = conexiónMySQL.GetConnection();

      MySqlDataReader mReader = miComando.ExecuteReader();

      while (mReader.Read())
      {
        Categoria unaCategoria = new Categoria
        {
          CategoriaID = mReader.GetInt32("CategoriaID"),
          Nombre = mReader.GetString("Nombre"),
          UrlImagen = mReader.GetString("urlImagen")
        };
        listaCategoria.Add(unaCategoria);
      }

      mReader.Close();
      return listaCategoria;
    }

    public bool Update(Categoria categoria)
    {
      string QUERY = "UPDATE categoria SET Nombre = @nombre WHERE CategoriaID = @id";
      MySqlCommand miComando = new MySqlCommand(QUERY);
      miComando.Connection = conexiónMySQL.GetConnection();

      miComando.Parameters.AddWithValue("@nombre", categoria.Nombre);
      miComando.Parameters.AddWithValue("@id", categoria.CategoriaID);
      miComando.Parameters.AddWithValue("@urlImagen", categoria.UrlImagen);

      int filasAfectadas = miComando.ExecuteNonQuery();
      return filasAfectadas > 0;
    }

    public void Delete(int id)
    {
      string QUERY = "DELETE FROM categoria WHERE CategoriaID = @id";
      MySqlCommand miComando = new MySqlCommand(QUERY);
      miComando.Connection = conexiónMySQL.GetConnection();

      miComando.Parameters.AddWithValue("@id", id);
      miComando.ExecuteNonQuery();
    }
  }
}
