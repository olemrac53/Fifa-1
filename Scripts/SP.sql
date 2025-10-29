-- 01_SP.sql
USE GranET12;
DELIMITER $$

-- === Alta / Modificación / Baja Equipo ===
DROP PROCEDURE IF EXISTS AltaEquipo $$
CREATE PROCEDURE AltaEquipo(IN p_nombre VARCHAR(100))
BEGIN
    INSERT INTO Equipo (nombre) VALUES (p_nombre);
END $$

DROP PROCEDURE IF EXISTS ModificarEquipo $$
CREATE PROCEDURE ModificarEquipo(IN p_id_equipo INT, IN p_nombre VARCHAR(100))
BEGIN
    UPDATE Equipo SET nombre = p_nombre WHERE id_equipo = p_id_equipo;
END $$

DROP PROCEDURE IF EXISTS EliminarEquipo $$
CREATE PROCEDURE EliminarEquipo(IN p_id_equipo INT)
BEGIN
    DELETE FROM Equipo WHERE id_equipo = p_id_equipo;
END $$
-- === Alta / Modificación / Baja Tipo ===
DROP PROCEDURE IF EXISTS AltaTipo $$
CREATE PROCEDURE AltaTipo(IN p_nombre VARCHAR(50))
BEGIN
    INSERT INTO Tipo (nombre) VALUES (p_nombre);
END $$

DROP PROCEDURE IF EXISTS EliminarTipo $$
CREATE PROCEDURE EliminarTipo(IN p_id_tipo INT)
BEGIN
    DELETE FROM Tipo WHERE id_tipo = p_id_tipo;
END $$
-- === Alta / Modificación / Baja Futbolista ===
DROP PROCEDURE IF EXISTS AltaFutbolista $$
CREATE PROCEDURE AltaFutbolista(
    IN p_nombre VARCHAR(100),
    IN p_apellido VARCHAR(100),
    IN p_apodo VARCHAR(45),
    IN p_num_camisa VARCHAR(45),
    IN p_fecha_nacimiento DATE,
    IN p_cotizacion DECIMAL(10,2),
    IN p_id_tipo INT,
    IN p_id_equipo INT
)
BEGIN
    INSERT INTO Futbolista (nombre, apellido, apodo, num_camisa, fecha_nacimiento, cotizacion, id_tipo, id_equipo)
    VALUES (p_nombre, p_apellido, p_apodo, p_num_camisa, p_fecha_nacimiento, p_cotizacion, p_id_tipo, p_id_equipo);
END $$

DROP PROCEDURE IF EXISTS ModificarFutbolista $$
CREATE PROCEDURE ModificarFutbolista(
    IN p_id_futbolista INT,
    IN p_nombre VARCHAR(100),
    IN p_apellido VARCHAR(100),
    IN p_apodo VARCHAR(45),
    IN p_num_camisa VARCHAR(45),
    IN p_fecha_nacimiento DATE,
    IN p_cotizacion DECIMAL(10,2),
    IN p_id_tipo INT,
    IN p_id_equipo INT
)
BEGIN
    UPDATE Futbolista
    SET nombre = p_nombre,
        apellido = p_apellido,
        apodo = p_apodo,
        num_camisa = p_num_camisa,
        fecha_nacimiento = p_fecha_nacimiento,
        cotizacion = p_cotizacion,
        id_tipo = p_id_tipo,
        id_equipo = p_id_equipo
    WHERE id_futbolista = p_id_futbolista;
END $$

DROP PROCEDURE IF EXISTS EliminarFutbolista $$
CREATE PROCEDURE EliminarFutbolista(IN p_id_futbolista INT)
BEGIN
    DELETE FROM Futbolista WHERE id_futbolista = p_id_futbolista;
END $$

-- === CORRECCIÓN: Alta Usuario con SHA2 ===
DROP PROCEDURE IF EXISTS AltaUsuario $$
CREATE PROCEDURE AltaUsuario(
    OUT p_idUsuario INT,
    IN p_nombre VARCHAR(100),
    IN p_apellido VARCHAR(100),
    IN p_email VARCHAR(150),
    IN p_fecha_nacimiento DATE,
    IN p_contrasenia VARCHAR(100)
)
BEGIN
    INSERT INTO Usuario (nombre, apellido, email, fecha_nacimiento, contrasenia)
    VALUES (p_nombre, p_apellido, p_email, p_fecha_nacimiento, SHA2(p_contrasenia, 256));
    SET p_idUsuario = LAST_INSERT_ID();
END $$

DROP PROCEDURE IF EXISTS ModificarUsuario $$
CREATE PROCEDURE ModificarUsuario(
    IN p_id_usuario INT,
    IN p_nombre VARCHAR(100),
    IN p_apellido VARCHAR(100),
    IN p_email VARCHAR(150),
    IN p_fecha_nacimiento DATE,
    IN p_contrasenia VARCHAR(100),
    IN p_rol VARCHAR(50)
)
BEGIN
    UPDATE Usuario
    SET nombre = p_nombre,
        apellido = p_apellido,
        email = p_email,
        fecha_nacimiento = p_fecha_nacimiento,
        contrasenia = SHA2(p_contrasenia, 256)
    WHERE id_usuario = p_id_usuario;
END $$

DROP PROCEDURE IF EXISTS EliminarUsuario $$
CREATE PROCEDURE EliminarUsuario(IN p_id_usuario INT)
BEGIN
    DELETE FROM Usuario WHERE id_usuario = p_id_usuario;
END $$


-- === Alta / Modificación / Baja Administrador
DROP PROCEDURE IF EXISTS AltaAdministrador $$
CREATE PROCEDURE AltaAdministrador(
    IN p_nombre VARCHAR(100),
    IN p_apellido VARCHAR(100),
    IN p_email VARCHAR(150),
    IN p_fecha_nacimiento DATE,
    IN p_contrasenia VARCHAR(100)
)
BEGIN
    INSERT INTO Administrador (nombre, apellido, email, fecha_nacimiento, contrasenia)
    VALUES (p_nombre, p_apellido, p_email, p_fecha_nacimiento, SHA2(p_contrasenia, 256));
END $$

DROP PROCEDURE IF EXISTS ModificarAdministrador $$
CREATE PROCEDURE ModificarAdministrador(
    IN p_id_administrador INT,
    IN p_nombre VARCHAR(100),
    IN p_apellido VARCHAR(100),
    IN p_email VARCHAR(150),
    IN p_fecha_nacimiento DATE,
    IN p_contrasenia VARCHAR(100)
)
BEGIN
    UPDATE Administrador
    SET nombre = p_nombre,
        apellido = p_apellido,
        email = p_email,
        fecha_nacimiento = p_fecha_nacimiento,
        contrasenia = SHA2(p_contrasenia, 256)
    WHERE id_administrador = p_id_administrador;
END $$

DROP PROCEDURE IF EXISTS EliminarAdministrador $$
CREATE PROCEDURE EliminarAdministrador(IN p_id_administrador INT)
BEGIN
    DELETE FROM Administrador WHERE id_administrador = p_id_administrador;
END $$
-- === Plantilla ===
DROP PROCEDURE IF EXISTS CrearPlantilla $$
CREATE PROCEDURE CrearPlantilla(
    IN p_id_usuario INT,
    IN p_presupuesto_max DECIMAL(10,2),
    IN p_cant_max_futbolistas INT
)
BEGIN
    INSERT INTO Plantilla (id_usuario, presupuesto_max, cant_max_futbolistas)
    VALUES (p_id_usuario, p_presupuesto_max, p_cant_max_futbolistas);
END $$

DROP PROCEDURE IF EXISTS ModificarPlantilla $$
CREATE PROCEDURE ModificarPlantilla(
    IN p_id_plantilla INT,
    IN p_presupuesto_max DECIMAL(10,2),
    IN p_cant_max_futbolistas INT
)
BEGIN
    UPDATE Plantilla
    SET presupuesto_max = p_presupuesto_max,
        cant_max_futbolistas = p_cant_max_futbolistas
    WHERE id_plantilla = p_id_plantilla;
END $$

DROP PROCEDURE IF EXISTS EliminarPlantilla $$
CREATE PROCEDURE EliminarPlantilla(IN p_id_plantilla INT)
BEGIN
    DELETE FROM Plantilla WHERE id_plantilla = p_id_plantilla;
END $$


-- === PlantillaTitular / PlantillaSuplente SPs ===
DROP PROCEDURE IF EXISTS AltaTitular $$
CREATE PROCEDURE AltaTitular(IN p_id_plantilla INT, IN p_id_futbolista INT)
BEGIN
    IF EXISTS (SELECT 1 FROM PlantillaSuplente WHERE id_plantilla = p_id_plantilla AND id_futbolista = p_id_futbolista) THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'El futbolista ya está en suplentes de esa plantilla.';
    END IF;
    INSERT INTO PlantillaTitular (id_plantilla, id_futbolista) VALUES (p_id_plantilla, p_id_futbolista);
END $$

DROP PROCEDURE IF EXISTS EliminarTitular $$
CREATE PROCEDURE EliminarTitular(IN p_id_plantilla INT, IN p_id_futbolista INT)
BEGIN
    DELETE FROM PlantillaTitular WHERE id_plantilla = p_id_plantilla AND id_futbolista = p_id_futbolista;
END $$

DROP PROCEDURE IF EXISTS AltaSuplente $$
CREATE PROCEDURE AltaSuplente(IN p_id_plantilla INT, IN p_id_futbolista INT)
BEGIN
    IF EXISTS (SELECT 1 FROM PlantillaTitular WHERE id_plantilla = p_id_plantilla AND id_futbolista = p_id_futbolista) THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'El futbolista ya está en titulares de esa plantilla.';
    END IF;
    INSERT INTO PlantillaSuplente (id_plantilla, id_futbolista) VALUES (p_id_plantilla, p_id_futbolista);
END $$

DROP PROCEDURE IF EXISTS EliminarSuplente $$
CREATE PROCEDURE EliminarSuplente(IN p_id_plantilla INT, IN p_id_futbolista INT)
BEGIN
    DELETE FROM PlantillaSuplente WHERE id_plantilla = p_id_plantilla AND id_futbolista = p_id_futbolista;
END $$

-- === Puntuaciones ===
DROP PROCEDURE IF EXISTS AltaPuntuacion $$
CREATE PROCEDURE AltaPuntuacion(IN p_id_futbolista INT, IN p_fecha INT, IN p_puntuacion DECIMAL(3,1))
BEGIN
    INSERT INTO PuntuacionFutbolista (id_futbolista, fecha, puntuacion) 
    VALUES (p_id_futbolista, p_fecha, p_puntuacion);
END $$

DROP PROCEDURE IF EXISTS ModificarPuntuacion $$
CREATE PROCEDURE ModificarPuntuacion(IN p_id_puntuacion INT, IN p_puntuacion DECIMAL(3,1))
BEGIN
    UPDATE PuntuacionFutbolista SET puntuacion = p_puntuacion WHERE id_puntuacion = p_id_puntuacion;
END $$

DROP PROCEDURE IF EXISTS EliminarPuntuacion $$
CREATE PROCEDURE EliminarPuntuacion(IN p_id_puntuacion INT)
BEGIN
    DELETE FROM PuntuacionFutbolista WHERE id_puntuacion = p_id_puntuacion;
END $$


DELIMITER ;
