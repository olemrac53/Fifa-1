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
