USE GranET12;

DELIMITER //

-- Alta Equipo
DROP PROCEDURE IF EXISTS AltaEquipo //
CREATE PROCEDURE AltaEquipo(
    IN p_nombre VARCHAR(100)
)
BEGIN
    INSERT INTO Equipo (nombre)
    VALUES (p_nombre);
END //

-- Alta Futbolista
DROP PROCEDURE IF EXISTS AltaFutbolista //
CREATE PROCEDURE AltaFutbolista(
    IN p_nombre VARCHAR(100),
    IN p_apellido VARCHAR(100),
    IN p_apodo VARCHAR(45),
    IN p_fecha_nacimiento DATE,
    IN p_cotizacion DECIMAL(10,2),
    IN p_tipo VARCHAR(45),
    IN p_id_equipo INT
)
BEGIN
    INSERT INTO Futbolista (nombre, apellido, apodo, fecha_nacimiento, cotizacion, tipo, id_equipo)
    VALUES (p_nombre, p_apellido, p_apodo, p_fecha_nacimiento, p_cotizacion, p_tipo, p_id_equipo);
END //

-- Alta Usuario
DROP PROCEDURE IF EXISTS AltaUsuario //
CREATE PROCEDURE AltaUsuario(
    IN p_nombre VARCHAR(100),
    IN p_apellido VARCHAR(100),
    IN p_email VARCHAR(150),
    IN p_fecha_nacimiento DATE,
    IN p_contrasenia CHAR(64)
)
BEGIN
    INSERT INTO Usuario (nombre, apellido, email, fecha_nacimiento, contrasenia)
    VALUES (p_nombre, p_apellido, p_email, p_fecha_nacimiento, p_contrasenia);
END //

-- Crear Plantilla
DROP PROCEDURE IF EXISTS CrearPlantilla //
CREATE PROCEDURE CrearPlantilla(
    IN p_id_usuario INT,
    IN p_presupuesto_max DECIMAL(10,2),
    IN p_cant_max_futbolistas INT
)
BEGIN
    INSERT INTO Plantilla (id_usuario, presupuesto_max, cant_max_futbolistas)
    VALUES (p_id_usuario, p_presupuesto_max, p_cant_max_futbolistas);
END //

-- Alta Puntuacion
DROP PROCEDURE IF EXISTS AltaPuntuacion //
CREATE PROCEDURE AltaPuntuacion(
    IN p_id_futbolista INT,
    IN p_fecha INT,
    IN p_puntuacion DECIMAL(3,1)
)
BEGIN
    INSERT INTO PuntuacionFutbolista (id_futbolista, fecha, puntuacion)
    VALUES (p_id_futbolista, p_fecha, p_puntuacion);
END //

DELIMITER ;


DELIMITER //
DROP PROCEDURE IF EXISTS ModificarEquipo //
CREATE PROCEDURE ModificarEquipo(
    IN p_id_equipo INT,
    IN p_nombre VARCHAR(100)
)
BEGIN
    UPDATE Equipo
    SET nombre = p_nombre
    WHERE id_equipo = p_id_equipo;
END //
DELIMITER ;


DELIMITER //
DROP PROCEDURE IF EXISTS EliminarEquipo //
CREATE PROCEDURE EliminarEquipo(
    IN p_id_equipo INT
)
BEGIN
    DELETE FROM Equipo
    WHERE id_equipo = p_id_equipo;
END //
DELIMITER ;


DELIMITER //
DROP PROCEDURE IF EXISTS EliminarFutbolista //
CREATE PROCEDURE EliminarFutbolista(
    IN p_id_futbolista INT
)
BEGIN
    DELETE FROM Futbolista
    WHERE id_futbolista = p_id_futbolista;
END //
DELIMITER ;


DELIMITER //
DROP PROCEDURE IF EXISTS ModificarUsuario //
CREATE PROCEDURE ModificarUsuario(
    IN p_id_usuario INT,
    IN p_nombre VARCHAR(100),
    IN p_apellido VARCHAR(100),
    IN p_email VARCHAR(150),
    IN p_fecha_nacimiento DATE,
    IN p_contrasenia CHAR(64)
)
BEGIN
    UPDATE Usuario
    SET nombre = p_nombre,
        apellido = p_apellido,
        email = p_email,
        fecha_nacimiento = p_fecha_nacimiento,
        contrasenia = p_contrasenia
    WHERE id_usuario = p_id_usuario;
END //
DELIMITER ;


DELIMITER //
DROP PROCEDURE IF EXISTS EliminarUsuario //
CREATE PROCEDURE EliminarUsuario(
    IN p_id_usuario INT
)
BEGIN
    DELETE FROM Usuario
    WHERE id_usuario = p_id_usuario;
END //
DELIMITER ;


DELIMITER //
DROP PROCEDURE IF EXISTS AgregarFutbolistaPlantilla //
CREATE PROCEDURE AgregarFutbolistaPlantilla(
    IN p_id_plantilla INT,
    IN p_id_futbolista INT,
    IN p_es_titular BOOLEAN
)
BEGIN
    INSERT INTO PlantillaFutbolista (id_plantilla, id_futbolista, es_titular)
    VALUES (p_id_plantilla, p_id_futbolista, p_es_titular);
END //
DELIMITER ;


DELIMITER //
DROP PROCEDURE IF EXISTS EliminarFutbolistaPlantilla //
CREATE PROCEDURE EliminarFutbolistaPlantilla(
    IN p_id_plantilla INT,
    IN p_id_futbolista INT
)
BEGIN
    DELETE FROM PlantillaFutbolista
    WHERE id_plantilla = p_id_plantilla
      AND id_futbolista = p_id_futbolista;
END //
DELIMITER ;


DELIMITER //
DROP PROCEDURE IF EXISTS ModificarPuntuacion //
CREATE PROCEDURE ModificarPuntuacion(
    IN p_id_puntuacion INT,
    IN p_puntuacion DECIMAL(3,1)
)
BEGIN
    UPDATE PuntuacionFutbolista
    SET puntuacion = p_puntuacion
    WHERE id_puntuacion = p_id_puntuacion;
END //
DELIMITER ;


DELIMITER //
DROP PROCEDURE IF EXISTS EliminarPuntuacion //
CREATE PROCEDURE EliminarPuntuacion(
    IN p_id_puntuacion INT
)
BEGIN
    DELETE FROM PuntuacionFutbolista
    WHERE id_puntuacion = p_id_puntuacion;
END //
DELIMITER ;
