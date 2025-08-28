USE GranET12;

-- Tabla: Bitacora
CREATE TABLE Bitacora (
    id_log INT AUTO_INCREMENT PRIMARY KEY,
    usuario_bd VARCHAR(100) NOT NULL,
    accion VARCHAR(20) NOT NULL,         -- Aqui es donde van los VALUES (la accion hecha en el trigger) de INSERT, UPDATE, DELETE
    tabla_afectada VARCHAR(50) NOT NULL,
    fecha TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    detalle TEXT
);

-- Triggers de Jugador

CREATE TRIGGER trg_jugador_insert
AFTER INSERT ON Jugador
FOR EACH ROW
INSERT INTO Bitacora(usuario_bd, accion, tabla_afectada, detalle)
VALUES (CURRENT_USER(), 'INSERT', 'Jugador',
        CONCAT('Nuevo jugador: ', NEW.nombre, ' ', NEW.apellido,
               ' - EquipoID=', NEW.id_equipo));

CREATE TRIGGER trg_jugador_update
AFTER UPDATE ON Jugador
FOR EACH ROW
INSERT INTO Bitacora(usuario_bd, accion, tabla_afectada, detalle)
VALUES (CURRENT_USER(), 'UPDATE', 'Jugador',
        CONCAT('Jugador actualizado ID=', NEW.id_jugador,
               ' (Antes: ', OLD.nombre, ' ', OLD.apellido,
               ' | Ahora: ', NEW.nombre, ' ', NEW.apellido, ')'));

CREATE TRIGGER trg_jugador_delete
AFTER DELETE ON Jugador
FOR EACH ROW
INSERT INTO Bitacora(usuario_bd, accion, tabla_afectada, detalle)
VALUES (CURRENT_USER(), 'DELETE', 'Jugador',
        CONCAT('Jugador eliminado: ', OLD.nombre, ' ', OLD.apellido));


-- Triggers de Equipo
CREATE TRIGGER trg_equipo_insert
AFTER INSERT ON Equipo
FOR EACH ROW
INSERT INTO Bitacora(usuario_bd, accion, tabla_afectada, detalle)
VALUES (CURRENT_USER(), 'INSERT', 'Equipo',
        CONCAT('Nuevo equipo: ', NEW.nombre));

CREATE TRIGGER trg_equipo_delete
AFTER DELETE ON Equipo
FOR EACH ROW
INSERT INTO Bitacora(usuario_bd, accion, tabla_afectada, detalle)
VALUES (CURRENT_USER(), 'DELETE', 'Equipo',
        CONCAT('Equipo eliminado: ', OLD.nombre));

-- Triggers de Usuario

CREATE TRIGGER trg_usuario_insert
AFTER INSERT ON Usuario
FOR EACH ROW
INSERT INTO Bitacora(usuario_bd, accion, tabla_afectada, detalle)
VALUES (CURRENT_USER(), 'INSERT', 'Usuario',
        CONCAT('Nuevo usuario: ', NEW.email));

CREATE TRIGGER trg_usuario_delete
AFTER DELETE ON Usuario
FOR EACH ROW
INSERT INTO Bitacora(usuario_bd, accion, tabla_afectada, detalle)
VALUES (CURRENT_USER(), 'DELETE', 'Usuario',
        CONCAT('Usuario eliminado: ', OLD.email));


