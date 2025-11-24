Fecha
Integrante / Responsable
Actividad realizada
Observaciones
21/08/2025
Ruben
Abstracción de requerimientos del sistema.
Se definieron entidades principales (jugadores, equipos, usuarios, plantillas, puntuaciones) y reglas de negocio.
21/08/2025
(Ruben/Carmelo)
Diseño de casos de usos
Se realizó el diseño y los casos de uso.
21/08/2025
Ruben
Diseño del DER LÓGICO.
Entidades, atributos clave, relaciones y cardinalidades. Se incluyó diagrama en formato Mermaid.
21/08/2025
Ruben
Creación del archivo 00.DDL.
Definición de tablas, PK, FK, UNIQUE y CHECK (cotización ≤ 99M; puntuación 1–10; fecha 1–49).
26/08/2025
Ruben / Carmelo
Creación del diseño de los formularios.
Se definieron múltiples formularios y cómo interactuarían.
26/08/2025
Carmelo
Adaptación del diseño (UI).
Inclusión de Buttons, ListBox, GroupBox, etc. y navegación entre formularios.
26/08/2025
Carmelo
Primer diseño de animación (WinForms).
Se realizó una animación de intro para el menú.
26/08/2025
Ruben / Carmelo
Diseño del DER (Diagrama Entidad Relación).
Se generó la plantilla gráfica del DER.
28/08/2025
Ruben / Carmelo
Implementación de Stored Procedures (Altas, Modificar, Eliminar).
Procedimientos de gestión iniciales sobre tablas principales.
28/08/2025
Ruben / Carmelo
Implementación de tablas Suplente, Titular y Puntaje.
Se optó por separar en tablas para simplificar consultas.
02/09/2025
Ruben / Carmelo
Terminación de diseño e implementación de animación y login.
Se modificó menú, login e intro, agregando animación y formulario de inicio de sesión.
03/09/2025
Ruben / Carmelo
Agregación y modificaciones en backend (SF, Triggers, DDL).
Se hizo un overhaul completo en la BD (Work In Progress).
04/09/2025
Carmelo
Capacitación sobre conexión (WinForms y MySQL).
El profesor Durán explicó cómo vincular la base de datos.
09/09/2025
Carmelo / Ruben
Implementación de audio y animación.
Se integró sonido en la intro y se personalizó iconografía.
18/09/2025
Ruben
Corrección del DDL.
Integración de PlantillaTitular/Suplente, ajustes de FK y constraints.
20/09/2025
Ruben / Carmelo
Corrección de Stored Procedures.
Se detectó y corrigió error de incompatibilidad en AltaFutbolista (num_camisa).
22/09/2025
Ruben/Carmelo
Implementación de Stored Functions avanzadas.
PlantillaEsValida, PuntajePlantillaFecha. Permiten validar composición y calcular puntajes.
24/09/2025
Ruben/ Carmelo
Implementación de triggers de validación.
Garantizan reglas de negocio (presupuesto, cantidad máxima) automáticamente.
24/09/2025
Ruben / Carmelo
Implementación de triggers de auditoría (Bitácora).
Se registra operación, usuario y fecha en tabla Bitacora.
25/09/2025
Ruben/Carmelo
Integración final de bitácora de BD.
Permite seguimiento automático de operaciones CRUD en la base.
25/09/2025
Carmelo/Ruben
Diagrama de Gantt.
Se realizó el diagrama de Gantt y se puso en la bitácora.
26/09/2025
Ruben/Carmelo
Implementación de roles y GRANTs.
Se definieron roles rol_admin y rol_usuario con permisos diferenciados.
27/09/2025
Ruben
Creación de la estructura de proyectos C# (.sln).
Se definieron 3 proyectos: Fifa.Core, Fifa.Dapper y Fifa-1 (Windows Forms).
28/09/2025
Ruben
Implementación de Entidades (POCOs) en Fifa.Core.
Creadas las clases Usuario, Equipo, Futbolista, Plantilla y Tipo.
29/09/2025
Ruben / Carmelo
Definición de Interfaces de Repositorios en Fifa.Core/Repos.
Se crearon todos los contratos (IRepoUsuario, IRepoEquipo, IRepoFutbolista, IRepoPlantilla, IRepoPuntuacion).
02/10/2025
Ruben
Implementación de Fifa.Dapper (Capa de Datos).
Se implementaron todas las clases de repositorio (RepoUsuario, RepoFutbolista, Repo.Plantilla, etc.) usando Dapper.
03/10/2025
Ruben
Implementación de Repo.cs y ConexionDB.
Se centralizó la cadena de conexión y la lógica base del repositorio.
05/10/2025
Ruben
Creación del proyecto ReposDapperTests con xUnit.
Configuración de la clase TestRepo.cs para manejar la conexión y Dapper.
06/10/2025
Ruben / Carmelo
Implementación de TestRepoUsuario y TestRepoAdministrador.
Pruebas exitosas para Login (Usuario/Admin) y Alta/Modificación.
07/10/2025
Ruben / Carmelo
Implementación de TestRepoFutbolista y TestRepoPuntuacion.
Se validó el CRUD de futbolistas y el cálculo de puntajes.
09/10/2025
Ruben / Carmelo
Implementación de TestRepoPlantilla.
Pruebas exitosas para GetPlantillaCompleta, AgregarTitular y AgregarSuplente.
15/10/2025
Carmelo
Conexión del Backend a Formularios (Login y Registro).
Se implementó la lógica en Inicio sesion.cs y Registro.cs para llamar a RepoUsuario.
09/11/2025
Ruben / Carmelo
Pausa en el desarrollo de UI (Front-end).
El backend está 100% completo y testeado, pero falta la implementación de las interfaces de plantilla.cs y Jugador.cs.
10/11/2025
Ruben / Carmelo
Generación de código para plantilla.cs (Designer y Lógica).
Se generó la interfaz completa para la gestión de plantilla (fichajes, grillas, presupuesto).
11/11/2025
Ruben / Carmelo
Generación de código para Jugador.cs (Designer y Lógica).
Se generó la interfaz completa del CRUD de Futbolistas (Admin).
12/11/2025
Ruben / Carmelo
Corrección de errores de compilación (CS1061) en Fifa.Dapper.
Se detectaron y corrigieron inconsistencias de mayúsculas (IdEquipo vs idEquipo) entre Fifa.Core y Fifa.Dapper.
13/11/2025
Ruben / Carmelo
Implementación de TestRepoEquipo.cs.
Se creó el archivo de pruebas unitarias para RepoEquipo, completando la cobertura de tests del backend.

