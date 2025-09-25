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


-- Triggers de Jugador
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
DELIMITER ;


