using MySql.Data.MySqlClient;
using System;

namespace barcito.Persistencia
{
    public class GestorBaseDeDatos
    {
        private ConexiónMySQL conexiónMySQL;

        // Constructor para inicializar la conexión
        public GestorBaseDeDatos()
        {
            this.conexiónMySQL = new ConexiónMySQL();
        }

        public void cargarBaseDeDatos()
        {
            //aca insertamos la query de creacion de la base de datos

            string QUERY = @"
                -- Tabla de Usuarios
                CREATE TABLE IF NOT EXISTS Usuarios (
                    usuario_id INT PRIMARY KEY AUTO_INCREMENT,
                    nombre_usuario VARCHAR(50) NOT NULL UNIQUE,
                    contraseña_hash VARCHAR(255) NOT NULL,
                    email VARCHAR(100) UNIQUE,
                    fecha_creacion DATETIME DEFAULT CURRENT_TIMESTAMP,
                    ultimo_acceso DATETIME,
                    estado ENUM('activo', 'inactivo', 'suspendido') DEFAULT 'activo'
                );

                -- Tabla de Proveedores
                CREATE TABLE IF NOT EXISTS Proveedores (
                    proveedor_id INT PRIMARY KEY AUTO_INCREMENT,
                    nombre VARCHAR(100) NOT NULL,
                    telefono VARCHAR(20),
                    email VARCHAR(100)
                );

                -- Tabla de Artículos
                CREATE TABLE IF NOT EXISTS Articulos (
                    articulo_id INT PRIMARY KEY AUTO_INCREMENT,
                    nombre VARCHAR(100) NOT NULL,
                    descripcion TEXT,
                    precio DECIMAL(10, 2) NOT NULL,
                    stock INT NOT NULL,
                    imagen LONGBLOB
                );

                -- Tabla intermedia Artículo-Proveedor (muchos a muchos)
                CREATE TABLE IF NOT EXISTS Articulo_Proveedor (
                    articulo_id INT NOT NULL,
                    proveedor_id INT NOT NULL,
                    PRIMARY KEY (articulo_id, proveedor_id),
                    FOREIGN KEY (articulo_id) REFERENCES Articulos(articulo_id),
                    FOREIGN KEY (proveedor_id) REFERENCES Proveedores(proveedor_id)
                );

                -- Tabla de Ventas
                CREATE TABLE IF NOT EXISTS Ventas (
                    venta_id INT PRIMARY KEY AUTO_INCREMENT,
                    fecha DATETIME NOT NULL,
                    cliente_nombre VARCHAR(100),
                    total DECIMAL(10, 2)
                );

                -- Tabla de Detalles de Ventas
                CREATE TABLE IF NOT EXISTS DetallesVenta (
                    detalle_id INT PRIMARY KEY AUTO_INCREMENT,
                    venta_id INT NOT NULL,
                    articulo_id INT NOT NULL,
                    cantidad INT NOT NULL,
                    precio_unitario DECIMAL(10, 2) NOT NULL,
                    FOREIGN KEY (venta_id) REFERENCES Ventas(venta_id),
                    FOREIGN KEY (articulo_id) REFERENCES Articulos(articulo_id)
                );

            ";

            try
            {
                using (MySqlConnection connection = conexiónMySQL.GetConnection())
                {
                    if (connection.State != System.Data.ConnectionState.Open)
                    {
                        connection.Open();
                    }

                    MySqlCommand miComando = new MySqlCommand(QUERY, connection);
                    miComando.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al crear la base de datos y las tablas: " + ex.Message);
                throw;
            }
        }
    }
}
