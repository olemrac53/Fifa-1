DELIMITER $$
DROP TRIGGER IF EXISTS ValidarPuntuacion $$
CREATE TRIGGER ValidarPuntuacion
BEFORE INSERT ON PuntuacionFutbolista
FOR EACH ROW
BEGIN
    -- Validar que el jugador exista en alguna plantilla
    IF NOT EXISTS (
        SELECT 1
        FROM PlantillaFutbolista pf
        WHERE pf.id_futbolista = NEW.id_futbolista
    ) THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'El futbolista no pertenece a ninguna plantilla. No se puede asignar puntaje.';
    END IF;
END $$
DELIMITER ;



SELECT PuntajePlantillaFecha(1, 4);


DELIMITER $$
DROP TRIGGER IF EXISTS TR_ValidarPuntuacionUnica $$
CREATE TRIGGER TR_ValidarPuntuacionUnica
BEFORE INSERT ON PuntuacionFutbolista
FOR EACH ROW
BEGIN
    IF EXISTS (
        SELECT 1
        FROM PuntuacionFutbolista pf
        WHERE pf.id_futbolista = NEW.id_futbolista
          AND pf.fecha = NEW.fecha
    ) THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'El futbolista ya tiene una puntuación para esa fecha.';
    END IF;
END $$
DELIMITER ;


DELIMITER $$
DROP TRIGGER IF EXISTS TR_ValidarTipoFutbolista $$
CREATE TRIGGER TR_ValidarTipoFutbolista
BEFORE INSERT ON Futbolista
FOR EACH ROW
BEGIN
    IF NEW.id_tipo IS NULL THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Todo futbolista debe tener asignado un tipo válido (Arquero, Defensor, etc).';
    END IF;
END $$
DELIMITER ;
