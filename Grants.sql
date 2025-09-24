-- 05_Grants.sql
-- NOTA: ejecutar como root o con un usuario con privilegios suficientes

USE GranET12;

-- ===========================================================
-- CREACIÓN DE ROLES (MySQL 8+)
-- ===========================================================
DROP ROLE IF EXISTS rol_admin;
DROP ROLE IF EXISTS rol_usuario;

CREATE ROLE rol_admin;
CREATE ROLE rol_usuario;

-- ===========================================================
-- PRIVILEGIOS PARA rol_admin
-- ===========================================================
-- Acceso completo a todas las tablas y SP/SF
GRANT ALL PRIVILEGES ON GranET12.* TO rol_admin;

-- ===========================================================
-- PRIVILEGIOS PARA rol_usuario
-- ===========================================================
-- Usuarios normales:
-- - pueden seleccionar información (consultas)
-- - pueden ejecutar funciones definidas
-- - pueden modificar SOLO sus plantillas, titulares/suplentes
-- - NO pueden alterar tablas maestras (Equipo, Futbolista, Tipo, Usuario, Puntuacion ajena)

-- Lectura de tablas básicas
GRANT SELECT ON GranET12.Equipo TO rol_usuario;
GRANT SELECT ON GranET12.Tipo TO rol_usuario;
GRANT SELECT ON GranET12.Futbolista TO rol_usuario;
GRANT SELECT ON GranET12.PuntuacionFutbolista TO rol_usuario;
GRANT SELECT ON GranET12.Puntaje TO rol_usuario;

-- Permisos de gestión de sus plantillas
GRANT SELECT, INSERT, UPDATE, DELETE ON GranET12.Plantilla TO rol_usuario;
GRANT SELECT, INSERT, DELETE ON GranET12.PlantillaTitular TO rol_usuario;
GRANT SELECT, INSERT, DELETE ON GranET12.PlantillaSuplente TO rol_usuario;
GRANT SELECT ON GranET12.PlantillaFutbolista TO rol_usuario;

-- Pueden consultar bitácora, pero NO modificar
GRANT SELECT ON GranET12.Bitacora TO rol_usuario;

-- ===========================================================
-- PERMISOS PARA EJECUTAR PROCEDURES Y FUNCTIONS
-- ===========================================================
-- Admin: acceso total a SP/SF
GRANT EXECUTE ON PROCEDURE GranET12.* TO rol_admin;
GRANT EXECUTE ON FUNCTION GranET12.* TO rol_admin;

-- Usuario: solo ejecución de funciones de consulta + SP de plantillas
GRANT EXECUTE ON FUNCTION GranET12.PresupuestoPlantilla TO rol_usuario;
GRANT EXECUTE ON FUNCTION GranET12.CantidadFutbolistasPlantilla TO rol_usuario;
GRANT EXECUTE ON FUNCTION GranET12.PlantillaEsValida TO rol_usuario;
GRANT EXECUTE ON FUNCTION GranET12.PuntajeFutbolistaFecha TO rol_usuario;
GRANT EXECUTE ON FUNCTION GranET12.PuntajePlantillaFecha TO rol_usuario;

GRANT EXECUTE ON PROCEDURE GranET12.CrearPlantilla TO rol_usuario;
GRANT EXECUTE ON PROCEDURE GranET12.ModificarPlantilla TO rol_usuario;
GRANT EXECUTE ON PROCEDURE GranET12.EliminarPlantilla TO rol_usuario;
GRANT EXECUTE ON PROCEDURE GranET12.AltaTitular TO rol_usuario;
GRANT EXECUTE ON PROCEDURE GranET12.EliminarTitular TO rol_usuario;
GRANT EXECUTE ON PROCEDURE GranET12.AltaSuplente TO rol_usuario;
GRANT EXECUTE ON PROCEDURE GranET12.EliminarSuplente TO rol_usuario;

-- ===========================================================
-- EJEMPLO: CREAR USUARIOS DE MYSQL Y ASIGNAR ROLES
-- ===========================================================
-- Crear un usuario administrador
CREATE USER IF NOT EXISTS 'admin1'@'%' IDENTIFIED BY 'adminpass';
GRANT rol_admin TO 'admin1'@'%';
SET DEFAULT ROLE rol_admin TO 'admin1'@'%';

-- Crear un usuario normal
CREATE USER IF NOT EXISTS 'usuario1'@'%' IDENTIFIED BY 'userpass';
GRANT rol_usuario TO 'usuario1'@'%';
SET DEFAULT ROLE rol_usuario TO 'usuario1'@'%';

-- ===========================================================
-- APLICAR CAMBIOS
-- ===========================================================
FLUSH PRIVILEGES;
