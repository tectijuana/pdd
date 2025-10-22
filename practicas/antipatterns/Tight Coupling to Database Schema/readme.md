
Tema: Tight Coupling to Database Schema

1. Entendimiento del antipatrón

El antipatrón Cuando la lógica del sistema o el código de aplicación dependen directamente de la estructura interna de la base de datos, se produce lo que 
se conoce como Tight Coupling to Database Schema.
Esto quiere decir que el código fuente debe ser modificado también si se produce algún cambio en las tablas, los nombres de las columnas o las relaciones.

Un ejemplo de dependencia intensa:

* El código realiza consultas SQL utilizando nombres de columnas y tablas "hardcodeados".

* Los elementos del sistema son un fiel reflejo de las tablas del esquema, sin que exista una capa de abstracción.

* Si se altera el esquema de la base de datos (por ejemplo, si se modifica un nombre de campo), la aplicación no puede operar.

Por qué se considera una práctica negativa:

* Infringe el principio de bajo acoplamiento.

* Complica la migración a otras bases de datos y el desarrollo del sistema.

* Convierten el código en algo frágil y complicado de mantener.

2. Ejemplo Técnico 
Ejemplo de código con acoplamiento fuerte:
// Ejemplo en C#
string query = "SELECT nombre_cliente, direccion, telefono FROM clientes WHERE id_cliente = @id";
SqlCommand cmd = new SqlCommand(query, conexion);
cmd.Parameters.AddWithValue("@id", id);
SqlDataReader reader = cmd.ExecuteReader();

En este ejemplo:

Los nombres de columnas (nombre_cliente, direccion, telefono) están directamente en el código.

Si en la base de datos se cambia “telefono” por “num_telefono”, el código deja de funcionar.

Ejemplo corregido con abstracción (uso de ORM – Entity Framework):
// Capa de abstracción con Entity Framework
var cliente = db.Clientes.Find(id);
Console.WriteLine($"{cliente.Nombre} - {cliente.Telefono}");

Aquí, el ORM (Object Relational Mapper) maneja el mapeo entre objetos y tablas, evitando que el código dependa directamente del esquema físico de la base de datos.

3. Consecuencias
| Consecuencia                    | Descripción                                                                              |
| ------------------------------- | ---------------------------------------------------------------------------------------- |
| **Alta fragilidad**             | Cualquier cambio en la base de datos obliga a modificar el código fuente.                |
| **Dificultad en mantenimiento** | Requiere actualizaciones constantes en consultas SQL y estructuras de datos.             |
| **Falta de portabilidad**       | El sistema no puede migrarse fácilmente a otro motor de base de datos.                   |
| **Escalabilidad limitada**      | Complica la adaptación del sistema a nuevos módulos o microservicios.                    |
| **Pruebas más difíciles**       | Las pruebas unitarias dependen de una base de datos real, en lugar de objetos simulados. |

4. Remedio correctivo

Prácticas recomendadas:

1.Emplear una capa de abstracción (DAO, ORM o repositorio).
Por ejemplo: Dapper, Hibernate, Sequelize, Entity Framework, entre otros.

2.Implementar el principio de inversión de dependencias (DIP).
El código tiene que estar basado en interfaces, no en implementaciones específicas de la base de datos.

3.Disociar la lógica de acceso a datos de la lógica empresarial.
Por medio de la arquitectura en capas (Hexagonal, Clean Architecture, MVC).

4.Implementar migraciones controladas (scripts versionados).

5.Crear modelos o DTOs que no dependan del esquema físico.

