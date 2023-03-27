# Conexión con la base de datos
1. Ejecutar el archivo database.sql en SQL Server. Se creará tanto la estructura completa de la base de datos como algunos registros para probar el sistema.
2. En Visual Studio, abrir el 'Explorador de servidores' y en el apartado 'Conexiones de datos' clickear la base de datos recién creada y abrir el directorio 'Tables'.
3. En el 'Explorador de la solución', ir al proyecto 'Classes' y abrir el archivo 'Tables.dbml'.
4. Seleccionar todas las tablas presentes en el archivo y eliminarlas. El archivo tiene que quedar vacío.
5. Seleccionar todos los items (que vienen a ser las tablas de la BDD) que están dentro del directorio 'Tables' abierto en el punto 2, y arrastrarlos hacia cualquier lugar dentro del archivo abierto 'Tables.dbml'. Al hacer ésto, le estamos dando las credenciales nuestras y de nuestra base de datos a la solución para que así pueda acceder a la misma sin pedir ningún dato.
6. Guardar los cambios.

# Creación de nuevos usuarios
1. Insertar manualmente un nuevo registro a la tabla *afiliados* de la base de datos.
2. Realizar la creación del usuario dentro de la app, ingresando los datos recién insertados en la tabla.

# Estructura del proyecto
- En la carpeta AppWeb se encuentra el proyecto web de la aplicación.
- En la carpeta principal se encuentra el proyecto de escritorio de la aplicación.
- La carpeta Classes contiene una biblioteca de clases.
- El archivo database.sql contiene tanto la estructura como algunos registros fundamentales de la base de datos.

# Posibles errores
Al correr el proyecto web, puede ser que aparezca un error con la frase 'Could not find a part of the path 'C:\Users\<user>\<directorio>\TurnosMedicos\AppWeb\bin\roslyn\csc.exe'. Esto sucede ya que el compilador Roslyn proviene de un paquete NuGet y suele haber un error en algunas versiones de ese paquete (no sé exactamente cuáles). La solución es reinstalar/actualizar ese paquete a una versión libre de errores. Para ello, debemos hacer lo siguiente:
1. Finalizar la ejecución del proyecto.
2. Abrir la Consola del Administrador de Paquetes en Visual Studio, y ejecutar el siguiente comando `Update-Package Microsoft.CodeDom.Providers.DotNetCompilerPlatform -r`.
3. Volver a correr el proyecto.
