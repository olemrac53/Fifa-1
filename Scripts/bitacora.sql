-- Active: 1758815312179@@127.0.0.1@3306@granet12
USE GranET12;

-- Tabla: Bitacora
CREATE TABLE IF NOT EXISTS Bitacora (
    id_bitacora INT AUTO_INCREMENT PRIMARY KEY,
    tabla VARCHAR(50) NOT NULL,
    operacion VARCHAR(10) NOT NULL,
    id_registro INT NOT NULL,
    fecha TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    usuario VARCHAR(100) DEFAULT CURRENT_USER
);

-- Triggers de Usuario
DELIMITER $$
DROP TRIGGER IF EXISTS TR_Usuario_Insert $$
CREATE TRIGGER TR_Usuario_Insert
AFTER INSERT ON Usuario
FOR EACH ROW
BEGIN
    INSERT INTO Bitacora(tabla, operacion, id_registro)
    VALUES ('Usuario', 'INSERT', NEW.id_usuario);
END $$

DROP TRIGGER IF EXISTS TR_Usuario_Update $$
CREATE TRIGGER TR_Usuario_Update
AFTER UPDATE ON Usuario
FOR EACH ROW
BEGIN
    INSERT INTO Bitacora(tabla, operacion, id_registro)
    VALUES ('Usuario', 'UPDATE', NEW.id_usuario);
END $$

DROP TRIGGER IF EXISTS TR_Usuario_Delete $$
CREATE TRIGGER TR_Usuario_Delete
AFTER DELETE ON Usuario
FOR EACH ROW
BEGIN
    INSERT INTO Bitacora(tabla, operacion, id_registro)
    VALUES ('Usuario', 'DELETE', OLD.id_usuario);
END $$

-- Triggers de Administrador
DROP TRIGGER IF EXISTS TR_Administrador_Insert $$
CREATE TRIGGER TR_Administrador_Insert
AFTER INSERT ON Administrador
FOR EACH ROW
BEGIN
    INSERT INTO Bitacora(tabla, operacion, id_registro)
    VALUES ('Administrador', 'INSERT', NEW.id_administrador);
END $$

DROP TRIGGER IF EXISTS TR_Administrador_Update $$
CREATE TRIGGER TR_Administrador_Update
AFTER UPDATE ON Administrador
FOR EACH ROW
BEGIN
    INSERT INTO Bitacora(tabla, operacion, id_registro)
    VALUES ('Administrador', 'UPDATE', NEW.id_administrador);
END $$

DROP TRIGGER IF EXISTS TR_Administrador_Delete $$
CREATE TRIGGER TR_Administrador_Delete
AFTER DELETE ON Administrador
FOR EACH ROW
BEGIN
    INSERT INTO Bitacora(tabla, operacion, id_registro)
    VALUES ('Administrador', 'DELETE', OLD.id_administrador);
END $$

DELIMITER ;