
DROP USER IF EXISTS 'admin1'@'%';
DROP USER IF EXISTS 'usuario1'@'%';
DROP ROLE IF EXISTS rol_admin;
DROP ROLE IF EXISTS rol_usuario;

-- Crear roles
CREATE ROLE rol_admin;
CREATE ROLE rol_usuario;

-- Privilegios para rol_admin (CON BACKTICKS)
GRANT ALL PRIVILEGES ON 5to_GranET12.* TO rol_admin;

-- CRÍTICO: Otorgar permisos al usuario de pruebas
GRANT ALL PRIVILEGES ON 5to_GranET12.* TO 'root'@'localhost';

-- Privilegios para rol_usuario (CON BACKTICKS)
GRANT SELECT ON 5to_GranET12.Equipo TO rol_usuario;
GRANT SELECT ON 5to_GranET12.Tipo TO rol_usuario;
GRANT SELECT ON 5to_GranET12.Futbolista TO rol_usuario;
GRANT SELECT ON 5to_GranET12.PuntuacionFutbolista TO rol_usuario;
GRANT SELECT ON 5to_GranET12.Plantilla TO rol_usuario;
GRANT SELECT, INSERT, UPDATE, DELETE ON 5to_GranET12.Plantilla TO rol_usuario;
GRANT SELECT, INSERT, DELETE ON 5to_GranET12.PlantillaTitular TO rol_usuario;
GRANT SELECT, INSERT, DELETE ON 5to_GranET12.PlantillaSuplente TO rol_usuario;
-- Nota: Tabla Bitacora no existe, se omite el GRANT

-- Permisos de ejecución para funciones (CON BACKTICKS)
GRANT EXECUTE ON FUNCTION 5to_GranET12.PresupuestoPlantilla TO rol_usuario;
GRANT EXECUTE ON FUNCTION 5to_GranET12.CantidadFutbolistasPlantilla TO rol_usuario;
GRANT EXECUTE ON FUNCTION 5to_GranET12.PlantillaEsValida TO rol_usuario;
GRANT EXECUTE ON FUNCTION 5to_GranET12.PuntajePlantillaFecha TO rol_usuario;

-- Permisos de ejecución para procedimientos (CON BACKTICKS)
GRANT EXECUTE ON PROCEDURE 5to_GranET12.CrearPlantilla TO rol_usuario;
GRANT EXECUTE ON PROCEDURE 5to_GranET12.ModificarPlantilla TO rol_usuario;
GRANT EXECUTE ON PROCEDURE 5to_GranET12.EliminarPlantilla TO rol_usuario;
GRANT EXECUTE ON PROCEDURE 5to_GranET12.AltaTitular TO rol_usuario;
GRANT EXECUTE ON PROCEDURE 5to_GranET12.EliminarTitular TO rol_usuario;
GRANT EXECUTE ON PROCEDURE 5to_GranET12.AltaSuplente TO rol_usuario;
GRANT EXECUTE ON PROCEDURE 5to_GranET12.EliminarSuplente TO rol_usuario;

-- Crear usuarios de ejemplo (CON CONTRASEÑAS SEGURAS)
CREATE USER IF NOT EXISTS 'admin1'@'%' IDENTIFIED BY 'AdminPass123!';
GRANT rol_admin TO 'admin1'@'%';
SET DEFAULT ROLE rol_admin TO 'admin1'@'%';

CREATE USER IF NOT EXISTS 'usuario1'@'%' IDENTIFIED BY 'UserPass123!';
GRANT rol_usuario TO 'usuario1'@'%';
SET DEFAULT ROLE rol_usuario TO 'usuario1'@'%';

-- Aplicar cambios
FLUSH PRIVILEGES;