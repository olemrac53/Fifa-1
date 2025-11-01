DROP USER IF EXISTS 'admin1'@'%';
DROP USER IF EXISTS 'usuario1'@'%';
DROP ROLE IF EXISTS rol_admin;
DROP ROLE IF EXISTS rol_usuario;

-- Crear roles
CREATE ROLE rol_admin;
CREATE ROLE rol_usuario;

-- Privilegios para rol_admin
GRANT ALL PRIVILEGES ON GranET12.* TO rol_admin;

-- Privilegios para rol_usuario - CORREGIDOS
GRANT SELECT ON GranET12.Equipo TO rol_usuario;
GRANT SELECT ON GranET12.Tipo TO rol_usuario;
GRANT SELECT ON GranET12.Futbolista TO rol_usuario;
GRANT SELECT ON GranET12.PuntuacionFutbolista TO rol_usuario;
GRANT SELECT ON GranET12.Plantilla TO rol_usuario;
GRANT SELECT, INSERT, UPDATE, DELETE ON GranET12.Plantilla TO rol_usuario;
GRANT SELECT, INSERT, DELETE ON GranET12.PlantillaTitular TO rol_usuario;
GRANT SELECT, INSERT, DELETE ON GranET12.PlantillaSuplente TO rol_usuario;
GRANT SELECT ON GranET12.Bitacora TO rol_usuario;

-- Permisos de ejecución para funciones
GRANT EXECUTE ON FUNCTION GranET12.PresupuestoPlantilla TO rol_usuario;
GRANT EXECUTE ON FUNCTION GranET12.CantidadFutbolistasPlantilla TO rol_usuario;
GRANT EXECUTE ON FUNCTION GranET12.PlantillaEsValida TO rol_usuario;
GRANT EXECUTE ON FUNCTION GranET12.PuntajePlantillaFecha TO rol_usuario;

-- Permisos de ejecución para procedimientos
GRANT EXECUTE ON PROCEDURE GranET12.CrearPlantilla TO rol_usuario;
GRANT EXECUTE ON PROCEDURE GranET12.ModificarPlantilla TO rol_usuario;
GRANT EXECUTE ON PROCEDURE GranET12.EliminarPlantilla TO rol_usuario;
GRANT EXECUTE ON PROCEDURE GranET12.AltaTitular TO rol_usuario;
GRANT EXECUTE ON PROCEDURE GranET12.EliminarTitular TO rol_usuario;
GRANT EXECUTE ON PROCEDURE GranET12.AltaSuplente TO rol_usuario;
GRANT EXECUTE ON PROCEDURE GranET12.EliminarSuplente TO rol_usuario;

-- Crear usuarios de ejemplo
CREATE USER IF NOT EXISTS 'admin1'@'%' IDENTIFIED BY 'adminpass';
GRANT rol_admin TO 'admin1'@'%';
SET DEFAULT ROLE rol_admin TO 'admin1'@'%';

CREATE USER IF NOT EXISTS 'usuario1'@'%' IDENTIFIED BY 'userpass';
GRANT rol_usuario TO 'usuario1'@'%';
SET DEFAULT ROLE rol_usuario TO 'usuario1'@'%';

FLUSH PRIVILEGES;