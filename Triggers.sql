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


DELIMITER $$
DROP TRIGGER IF EXISTS TR_ValidarPresupuestoPlantilla $$
CREATE TRIGGER TR_ValidarPresupuestoPlantilla
BEFORE INSERT ON PlantillaFutbolista
FOR EACH ROW
BEGIN
    DECLARE total DECIMAL(11,2);

    -- Calcular el presupuesto actual de la plantilla
    SELECT COALESCE(SUM(f.cotizacion),0)
    INTO total
    FROM PlantillaFutbolista pf
    JOIN Futbolista f ON f.id_futbolista = pf.id_futbolista
    WHERE pf.id_plantilla = NEW.id_plantilla;

    -- Validar que no supere el máximo
    IF (total + (SELECT cotizacion FROM Futbolista WHERE id_futbolista = NEW.id_futbolista)) > 99999999.99 THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'El presupuesto de la plantilla no puede superar los $99.999.999,99.';
    END IF;
END $$
DELIMITER ;



DELIMITER $$
DROP TRIGGER IF EXISTS TR_ValidarCantidadPlantilla $$
CREATE TRIGGER TR_ValidarCantidadPlantilla
BEFORE INSERT ON PlantillaFutbolista
FOR EACH ROW
BEGIN
    DECLARE cantidad INT;

    SELECT COUNT(*) INTO cantidad
    FROM PlantillaFutbolista
    WHERE id_plantilla = NEW.id_plantilla;

    IF cantidad >= 20 THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'No se pueden agregar más de 20 jugadores en la plantilla.';
    END IF;
END $$
DELIMITER ;


DELIMITER $$
DROP TRIGGER IF EXISTS TR_ValidarEmailUsuario $$
CREATE TRIGGER TR_ValidarEmailUsuario
BEFORE INSERT ON Usuario
FOR EACH ROW
BEGIN
    IF EXISTS (
        SELECT 1
        FROM Usuario u
        WHERE u.email = NEW.email
    ) THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'El email ya está registrado.';
    END IF;
END $$
DELIMITER ;
   
   