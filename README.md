
Gran ET12 ⚽ - Simulador de Torneo
📌 Descripción / Relevamiento
Este proyecto consiste en el desarrollo de una solución integral para la gestión de un torneo de fútbol fantástico, con una modalidad similar al Gran DT.

El sistema permite gestionar:

Futbolistas: Datos personales, cotización, equipo y posición (Arquero, Defensor, Mediocampista, Delantero).

Equipos: Hasta 32 equipos únicos.

Usuarios: Gestión de cuentas con seguridad (contraseñas encriptadas SHA-256) y validación de emails únicos.

Plantillas: Creación y gestión de equipos por parte de los usuarios, respetando un presupuesto máximo y una formación táctica obligatoria (1-4-4-2).

🎯 Requisitos del Juego
Para que una plantilla sea válida, debe cumplir estrictamente con:

Presupuesto: La suma de las cotizaciones de titulares y suplentes no puede superar el tope establecido ($99.999.999,99).

Formación Titular:

1 Arquero

4 Defensores

4 Mediocampistas

2 Delanteros

Puntuación: El sistema permite cargar puntuaciones por fecha (1 a 49) y calcular el rendimiento de la plantilla de un usuario en una fecha específica.

🛠️ Tecnologías Utilizadas
Lenguaje: C# (.NET 8.0)

Base de Datos: MySQL

Persistencia: Dapper (Micro-ORM)

Interfaz Gráfica: Windows Forms

Arquitectura: N-Capas (Core, Dapper, GUI)

📂 Estructura del Repositorio
A continuación se detalla la organización de los archivos en el repositorio, correspondiendo a la solución Fifa-1.sln:



|
| README.md (Documentación general)
| Fifa-1.sln (Archivo de solución de Visual Studio)
|
├── Scripts (Base de datos)
│   ├── DDL.sql         # Creación de tablas y esquema (Usuario, Futbolista, Plantilla, etc.)
│   ├── SP.sql          # Procedimientos Almacenados (AltaUsuario, ModificarUsuario, etc.)
│   ├── Trig.sql        # Triggers de base de datos
│   ├── alta.sql        # Scripts de alta de datos
│   ├── Gran.sql        # Script general
│   └── source.sql      # Fuentes de datos adicionales
│
├── Fifa.Core (Lógica de Negocio y Entidades)
│   ├── Futbolista.cs
│   ├── Plantilla.cs    # Lógica de presupuesto y validación de titulares
│   ├── Usuario.cs
│   ├── Equipo.cs
│   └── ... (Otras entidades del dominio)
│
├── Fifa.Dapper (Capa de Acceso a Datos - DAL)
│   ├── RepoUsuario.cs      # ABM de usuarios y autenticación
│   ├── RepoPlantilla.cs    # Gestión de plantillas y relaciones muchos a muchos
│   ├── RepoFutbolista.cs   # Consultas de jugadores
│   └── Repo.cs             # Clase base de conexión
│
├── Fifa-1 (Interfaz Gráfica - Windows Forms)
│   ├── Inicio sesion.cs    # Formulario de Login
│   ├── Menu.cs             # Menú principal
│   ├── Plantilla.cs        # Gestión visual del equipo
│   ├── Jugador.cs          # ABM de jugadores (Admin)
│   └── ... (Recursos e imágenes)
│
├── ReposDapperTests (Pruebas Unitarias)
│   ├── TestRepoUsuario.cs
│   ├── TestRepoPlantilla.cs
│   └── ...
│
├── animacion fifa (Proyecto adicional)
│   └── Form1.cs            # Animaciones visuales del torneo
│
└── doc (Documentación)
    ├── bitacora.md         # Registro de cambios y evolución
    └── der.md              # Diagrama Entidad-Relación y detalles de diseño

    
🚀 Instalación y Puesta en Marcha
1. Base de Datos
Asegúrese de tener MySQL Server instalado.

Recuerde habilitar mysql en los servicios del task manager (administrador de tarreas)

Ejecute los scripts en la terminal de visual studio code de la carpeta Scripts en el siguiente orden para evitar errores de dependencias:

DDL.sql (Crea la estructura).

SP.sql (Crea los procedimientos).

Trig.sql (Crea los triggers).

(Opcional) Scripts de carga de datos (alta.sql o Inserts.sql).

2. Configuración del Proyecto
Abra el archivo Fifa-1.sln con Visual Studio.

Verifique la cadena de conexión en el proyecto Fifa.Dapper (generalmente en Repo.cs o archivo de configuración) para que apunte a su instancia local de MySQL.

Compile la solución para restaurar las dependencias de NuGet (Dapper, MySqlConnector).

3. Ejecución
Establezca el proyecto Fifa-1 (GUI) como proyecto de inicio.

Inicie la aplicación. Se presentará el formulario de Inicio de Sesión.

📏 Alcances del Proyecto
El estado actual del proyecto cubre los siguientes hitos de aprobación:

✅ Modelo de Dominio: Clases definidas en Fifa.Core.

✅ Persistencia: Implementación completa de repositorios con Dapper.

✅ Relaciones: Manejo de relaciones "Uno a Muchos" (Equipo-Futbolista) y "Muchos a Muchos" (Plantilla-Futbolista con tablas intermedias PlantillaTitular y PlantillaSuplente).

✅ Seguridad: Encriptación de contraseñas con SHA256 desde la base de datos.

✅ Interfaz: Formularios funcionales para Login, Menú y gestión de datos.

👥 Autores
Torren ruben y carmelo gonzalez