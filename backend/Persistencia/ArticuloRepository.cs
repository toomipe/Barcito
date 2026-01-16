using barcito.Logica;
using MySql.Data.MySqlClient;
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

        // CREATE
        public void Save(Articulo articulo)
        {
            string QUERY = @"
                INSERT INTO articulo 
                (Nombre, UrlImagen, Descripcion, Precio, CategoriaID)
                VALUES 
                (@nombre, @urlImagen, @descripcion, @precio, @categoriaID);
            ";

            Console.WriteLine(QUERY);

            var conn = conexiónMySQL.GetConnection();
            MySqlCommand cmd = new MySqlCommand(QUERY, conn);

            cmd.Parameters.AddWithValue("@nombre", articulo.Nombre);
            cmd.Parameters.AddWithValue("@urlImagen", articulo.UrlImagen);
            cmd.Parameters.AddWithValue("@descripcion", articulo.Descripcion);
            cmd.Parameters.AddWithValue("@precio", articulo.Precio);
            cmd.Parameters.AddWithValue("@categoriaID", articulo.CategoriaID);

            articulo.ArticuloID = Convert.ToInt32(cmd.ExecuteScalar());
            conn.Close();
        }

        // READ BY ID
        public Articulo FindById(int id)
        {
            string QUERY = "SELECT * FROM articulo WHERE ArticuloID = @id";
            var conn = conexiónMySQL.GetConnection();
            MySqlCommand cmd = new MySqlCommand(QUERY, conn);
            cmd.Parameters.AddWithValue("@id", id);

            MySqlDataReader reader = cmd.ExecuteReader();
            Articulo articulo = null;

            if (reader.Read())
            {
                articulo = new Articulo
                {
                    ArticuloID = reader.GetInt32("ArticuloID"),
                    Nombre = reader.GetString("Nombre"),
                    UrlImagen = reader.GetString("UrlImagen"),
                    Descripcion = reader.GetString("Descripcion"),
                    Precio = reader.GetDecimal("Precio"),
                    CategoriaID = reader.GetInt32("CategoriaID"),
                    Categoria = null
                };
            }

            reader.Close();
            conn.Close();
            return articulo;
        }

        // READ ALL
        public List<Articulo> FindAll()
        {
            string QUERY = "SELECT * FROM articulo";
            var conn = conexiónMySQL.GetConnection();
            MySqlCommand cmd = new MySqlCommand(QUERY, conn);

            MySqlDataReader reader = cmd.ExecuteReader();
            List<Articulo> articulos = new();
            CategoriaRepository cateRepo = new CategoriaRepository();

            while (reader.Read())
            {
                articulos.Add(new Articulo
                {
                    ArticuloID = reader.GetInt32("ArticuloID"),
                    Nombre = reader.GetString("Nombre"),
                    UrlImagen = reader.GetString("UrlImagen"),
                    Descripcion = reader.GetString("Descripcion"),
                    Precio = reader.GetDecimal("Precio"),
                    CategoriaID = reader.GetInt32("CategoriaID"),
                    Categoria = cateRepo.FindById(reader.GetInt32("CategoriaID"))
                });
            }

            reader.Close();
            conn.Close();
            return articulos;
        }

        // UPDATE
        public bool Update(Articulo articulo)
        {
            string QUERY = @"
                UPDATE articulo 
                SET Nombre = @nombre,
                    UrlImagen = @urlImagen,
                    Descripcion = @descripcion,
                    Precio = @precio,
                    CategoriaID = @categoriaID
                WHERE ArticuloID = @id
            ";

            var conn = conexiónMySQL.GetConnection();
            MySqlCommand cmd = new MySqlCommand(QUERY, conn);

            cmd.Parameters.AddWithValue("@nombre", articulo.Nombre);
            cmd.Parameters.AddWithValue("@urlImagen", articulo.UrlImagen);
            cmd.Parameters.AddWithValue("@descripcion", articulo.Descripcion);
            cmd.Parameters.AddWithValue("@precio", articulo.Precio);
            cmd.Parameters.AddWithValue("@categoriaID", articulo.CategoriaID);
            cmd.Parameters.AddWithValue("@id", articulo.ArticuloID);

            int filas = cmd.ExecuteNonQuery();
            conn.Close();
            return filas > 0;
        }

        // DELETE
        public void Delete(int id)
        {
            string QUERY = "DELETE FROM articulo WHERE ArticuloID = @id";
            var conn = conexiónMySQL.GetConnection();
            MySqlCommand cmd = new MySqlCommand(QUERY, conn);
            cmd.Parameters.AddWithValue("@id", id);

            cmd.ExecuteNonQuery();
            conn.Close();
        }

        // READ BY CATEGORIA
        public List<Articulo> FindByCategoriaId(int categoriaID)
        {
            string QUERY = "SELECT * FROM articulo WHERE CategoriaID = @categoriaID";
            var conn = conexiónMySQL.GetConnection();
            MySqlCommand cmd = new MySqlCommand(QUERY, conn);
            cmd.Parameters.AddWithValue("@categoriaID", categoriaID);

            MySqlDataReader reader = cmd.ExecuteReader();
            List<Articulo> articulos = new();
            CategoriaRepository cateRepo = new CategoriaRepository();

            while (reader.Read())
            {
                articulos.Add(new Articulo
                {
                    ArticuloID = reader.GetInt32("ArticuloID"),
                    Nombre = reader.GetString("Nombre"),
                    UrlImagen = reader.GetString("UrlImagen"),
                    Descripcion = reader.GetString("Descripcion"),
                    Precio = reader.GetDecimal("Precio"),
                    CategoriaID = reader.GetInt32("CategoriaID"),
                    Categoria = cateRepo.FindById(reader.GetInt32("CategoriaID"))
                });
            }

            reader.Close();
            conn.Close();
            return articulos;
        }
    }
}
