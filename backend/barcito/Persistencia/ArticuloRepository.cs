using barcito.Logica;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace barcito.Persistencia
{
  public class ArticuloRepository : IRepository<Articulo>
  {
    private ConexiónMySQL conexiónMySQL;

    public ArticuloRepository()
    {
      conexiónMySQL = new ConexiónMySQL();
    }

    public void Save(Articulo articulo)
    {
      string QUERY = "INSERT INTO articulo (Nombre, Descripcion, Precio, CategoriaID) " +
                     "VALUES (@nombre, @descripcion, @precio, @categoriaID)";
      MySqlCommand miComando = new MySqlCommand(QUERY);
      miComando.Connection = conexiónMySQL.GetConnection();

      miComando.Parameters.AddWithValue("@nombre", articulo.Nombre);
      miComando.Parameters.AddWithValue("@urlImagen", articulo.UrlImagen);
      miComando.Parameters.AddWithValue("@descripcion", articulo.Descripcion);
      miComando.Parameters.AddWithValue("@precio", articulo.Precio);
      miComando.Parameters.AddWithValue("@categoriaID", articulo.CategoriaID);

      miComando.ExecuteNonQuery();
    }

    public Articulo FindById(int id)
    {
      string QUERY = "SELECT * FROM articulo WHERE ArticuloID = " + id;
      MySqlCommand miComando = new MySqlCommand(QUERY);
      miComando.Connection = conexiónMySQL.GetConnection();

      MySqlDataReader mReader = miComando.ExecuteReader();
      Articulo articulo = null;
      CategoriaRepository cateRepo = null;

      if (mReader.Read())
      {
        articulo = new Articulo
        {
          ArticuloID = mReader.GetInt32("ArticuloID"),
          Nombre = mReader.GetString("Nombre"),
          UrlImagen = mReader.GetString("urlImagen"),
          Descripcion = mReader.GetString("Descripcion"),
          Precio = mReader.GetDecimal("Precio"),
          CategoriaID = mReader.GetInt32("CategoriaID"),
          Categoria = cateRepo.FindById(mReader.GetInt32("CategoriaID"))
        };
      }

      mReader.Close();
      return articulo;
    }

    public List<Articulo> FindAll()
    {
      string QUERY = "SELECT * FROM articulo";
      MySqlCommand miComando = new MySqlCommand(QUERY);
      miComando.Connection = conexiónMySQL.GetConnection();

      MySqlDataReader mReader = miComando.ExecuteReader();
      List<Articulo> articulos = new List<Articulo>();
      CategoriaRepository cateRepo = new CategoriaRepository();

      while (mReader.Read())
      {
        Articulo articulo = new Articulo
        {
          ArticuloID = mReader.GetInt32("ArticuloID"),
          Nombre = mReader.GetString("Nombre"),
          UrlImagen = mReader.GetString("urlImagen"),
          Descripcion = mReader.GetString("Descripcion"),
          Precio = mReader.GetDecimal("Precio"),
          CategoriaID = mReader.GetInt32("CategoriaID"),
          Categoria = cateRepo.FindById(mReader.GetInt32("CategoriaID"))
        };
        articulos.Add(articulo);
      }

      mReader.Close();
      return articulos;
    }

    public bool Update(Articulo articulo)
    {
      string QUERY = "UPDATE articulo SET Nombre = @nombre, Descripcion = @descripcion, " +
                     "Precio = @precio, CategoriaID = @categoriaID WHERE ArticuloID = @id";
      MySqlCommand miComando = new MySqlCommand(QUERY);
      miComando.Connection = conexiónMySQL.GetConnection();

      miComando.Parameters.AddWithValue("@nombre", articulo.Nombre);
      miComando.Parameters.AddWithValue("@urlImagen", articulo.UrlImagen);  
      miComando.Parameters.AddWithValue("@descripcion", articulo.Descripcion);
      miComando.Parameters.AddWithValue("@precio", articulo.Precio);
      miComando.Parameters.AddWithValue("@categoriaID", articulo.CategoriaID);
      miComando.Parameters.AddWithValue("@id", articulo.ArticuloID);

      int filasAfectadas = miComando.ExecuteNonQuery();
      return filasAfectadas > 0;
    }

    public void Delete(int id)
    {
      string QUERY = "DELETE FROM articulo WHERE ArticuloID = @id";
      MySqlCommand miComando = new MySqlCommand(QUERY);
      miComando.Connection = conexiónMySQL.GetConnection();

      miComando.Parameters.AddWithValue("@id", id);
      miComando.ExecuteNonQuery();
    }

    public List<Articulo> FindByCategoriaId(int categoriaID)
    {
      string QUERY = "SELECT * FROM articulo WHERE categoriaID = " + categoriaID;
      MySqlCommand miComando = new MySqlCommand(QUERY);
      miComando.Connection = conexiónMySQL.GetConnection();

      MySqlDataReader mReader = miComando.ExecuteReader();
      List<Articulo> articulos = new List<Articulo>();
      CategoriaRepository cateRepo = new CategoriaRepository();

      while (mReader.Read())
      {
        Articulo articulo = new Articulo
        {
          ArticuloID = mReader.GetInt32("ArticuloID"),
          Nombre = mReader.GetString("Nombre"),
          UrlImagen = mReader.GetString("urlImagen"),
          Descripcion = mReader.GetString("Descripcion"),
          Precio = mReader.GetDecimal("Precio"),
          CategoriaID = mReader.GetInt32("CategoriaID"),
          Categoria = cateRepo.FindById(mReader.GetInt32("CategoriaID"))
        };
        articulos.Add(articulo);
      }

      mReader.Close();
      return articulos;
    }
  }
}
