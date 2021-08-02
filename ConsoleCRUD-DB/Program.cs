using System;
using System.Data.SqlClient;

namespace ConsoleCRUD_DB
{
    class Program
    {

        static string cadenaDeConexion = string.Empty;
        static SqlConnection conexion = null;

        static SqlCommand mySqlCommand = null;
        static SqlDataReader mySqlDataReader = null;

        static void Main(string[] args)//Siempre se debe llamar a los metodos creados mediante el Main principal
        {
            Console.WriteLine("Trabajando con BBDD en C#");
            ConectarASQL();
            MostrarDatosDeEmpleado();
            //InsertarNuevoEmpleado("David", 34, 600.00M);
            //ActualizarEmpleado("Mati", 900.00M);
            EliminarEmpleado("Mati");
            CerrarConexion();
        }

        private static void ConectarASQL()
        {
            try
            {
                cadenaDeConexion = @"Server=DESKTOP-4CUGQV7;Database=Test;User Id=sa;Password=contravlady"; //conexion a BBDD
                conexion = new SqlConnection(cadenaDeConexion);
                conexion.Open();
                Console.WriteLine("Conexion exitosa a SQL Server");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problema al tratar de conectar a BD. Detalles.");
                Console.WriteLine(ex.Message);
            }
        }

        private static void MostrarDatosDeEmpleado()
        {
            try
            {
                string sqlQuery = "SELECT * FROM Empleado";
                mySqlCommand = new SqlCommand(sqlQuery, conexion); //El primer parametro es la consulta, el segundo la conexion a BBDD.
                mySqlDataReader = mySqlCommand.ExecuteReader(); // da acceso a la BBDD.
                Console.WriteLine("Id Empleado\t\t Nombre\t\t Edad\t\t Salario");
                Console.WriteLine("_________________________________________________");
                while (mySqlDataReader.Read())
                {
                    Console.WriteLine($"{mySqlDataReader["Id"]}\t {mySqlDataReader["Nombre"]}\t {mySqlDataReader["Edad"]}\t {mySqlDataReader["Salario"]}"); //Podemos acceder de varias formas (columnas y filas)
                }
                mySqlDataReader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problema al tratar de conectar a BD. Detalles.");
                Console.WriteLine(ex.Message);
            }
        }

        private static void InsertarNuevoEmpleado(string nombre, byte edad, decimal salario)
        {
            try
            {
                string sqlQuery = "INSERT INTO Empleado(Nombre, Edad, Salario) VALUES (@nombre, @edad, @salario)";
                mySqlCommand = new SqlCommand(sqlQuery, conexion);
                mySqlCommand.Parameters.AddWithValue("nombre", nombre);
                mySqlCommand.Parameters.AddWithValue("edad", edad);
                mySqlCommand.Parameters.AddWithValue("salario", salario); //Agregamos los parametros a la query que llenara las tablas.

                int resultado = mySqlCommand.ExecuteNonQuery(); //Ejecuta la peticion y nos devuelve la cantidad de filas modificadas.
                Console.WriteLine($"{resultado} Registro insertado correctamente");
                Console.WriteLine("Datos actuales de las tablas");
                MostrarDatosDeEmpleado(); //Volvemos a llamar al metodo para verificar 
             }
            catch (SqlException ex)
            {
                Console.WriteLine("Problema al tratar de cargar la BD. Detalles.");
                Console.WriteLine(ex.Message);
            }
        }

        private static void ActualizarEmpleado(string nombre, decimal aumento)
        {
            try
            {
                Console.WriteLine($"Actualizar salario del empleado {nombre}");
                string sqlQuery = "UPDATE Empleado SET Salario = @salario WHERE Nombre = @nombre";

                mySqlCommand = new SqlCommand(sqlQuery, conexion); //Damos parametros para hacer el update
                mySqlCommand.Parameters.AddWithValue("salario", aumento);
                mySqlCommand.Parameters.AddWithValue("nombre", nombre);
                int resultado = mySqlCommand.ExecuteNonQuery();
                Console.WriteLine($"{resultado} Registro se actualizo en la BBDD."); //imprimmimos la cantidad de filas afectadas.
                Console.WriteLine($"{nombre} ahora tiene un aumento de {aumento}"); //imprimimos fila actualizada.
                Console.WriteLine("Datos actuales de BBDD");
                MostrarDatosDeEmpleado();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problema al tratar de conectar a BD. Detalles.");
                Console.WriteLine(ex.Message);
            }
        }

        private static void EliminarEmpleado(string nombre)
        {
            try
            {
            //    Console.WriteLine("ingrese el nombre del empleado a Eliminar");
            //    string empleadoEliminar = Console.ReadLine();

                string sqlQuery = "DELETE FROM Empleado WHERE Nombre = @nombre";
                mySqlCommand = new SqlCommand(sqlQuery, conexion);
                mySqlCommand.Parameters.AddWithValue("nombre", nombre); //empleadoEliminar
                int resultado = mySqlCommand.ExecuteNonQuery();
                Console.WriteLine($"Datos de {nombre} eliminado con exito.");
                MostrarDatosDeEmpleado();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Problema al tratar de conectar a BD. Detalles.");
                Console.WriteLine(ex.Message);
            }
        }
        private static void CerrarConexion()
        {
            try
            {
                conexion.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problema al tratar de conectar a BD. Detalles.");
                Console.WriteLine(ex.Message);
            }
        }
    }
}
