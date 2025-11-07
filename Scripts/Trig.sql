-- 03_Triggers.sql
USE `5to_GranET12`;
DELIMITER $$

-- 1) Validación: no permitir puntaje si futbolista NO está en PlantillaTitular
DROP TRIGGER IF EXISTS TR_ValidarPuntuacionExistencia $$
CREATE TRIGGER TR_ValidarPuntuacionExistencia
BEFORE INSERT ON PuntuacionFutbolista
FOR EACH ROW
BEGIN
    IF NOT EXISTS (
        SELECT 1 FROM PlantillaTitular pt WHERE pt.id_futbolista = NEW.id_futbolista
    ) THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'El futbolista no es titular en ninguna plantilla; no se puede asignar puntaje.';
    END IF;
END $$

-- 2) No permitir 2 puntuaciones misma fecha
DROP TRIGGER IF EXISTS TR_ValidarPuntuacionUnica $$
CREATE TRIGGER TR_ValidarPuntuacionUnica
BEFORE INSERT ON PuntuacionFutbolista
FOR EACH ROW
BEGIN
    IF EXISTS (
        SELECT 1 FROM PuntuacionFutbolista pf WHERE pf.id_futbolista = NEW.id_futbolista AND pf.fecha = NEW.fecha
    ) THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'El futbolista ya tiene una puntuación para esa fecha.';
    END IF;
END $$

-- 3) Validar que Futbolista tenga Tipo al insert
DROP TRIGGER IF EXISTS TR_ValidarTipoFutbolista $$
CREATE TRIGGER TR_ValidarTipoFutbolista
BEFORE INSERT ON Futbolista
FOR EACH ROW
BEGIN
    IF NEW.id_tipo IS NULL OR NOT EXISTS (SELECT 1 FROM Tipo t WHERE t.id_tipo = NEW.id_tipo) THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Todo futbolista debe tener asignado un tipo válido.';
    END IF;
END $$

-- 4) Validar presupuesto al insertar Titular/Suplente (se usa PresupuestoPlantilla)


DROP TRIGGER IF EXISTS TR_ValidarPresupuesto_AltaSuplente $$
CREATE TRIGGER TR_ValidarPresupuesto_AltaSuplente
BEFORE INSERT ON PlantillaSuplente
FOR EACH ROW
BEGIN
    DECLARE presupuesto DECIMAL(10,2);
    SELECT PresupuestoPlantilla(NEW.id_plantilla) INTO presupuesto;
    IF (presupuesto + (SELECT cotizacion FROM Futbolista WHERE id_futbolista = NEW.id_futbolista)) > (SELECT presupuesto_max FROM Plantilla WHERE id_plantilla = NEW.id_plantilla) THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Agregar suplente excede presupuesto de la plantilla.';
    END IF;
END $$

-- 5) Validar cantidad máxima por plantilla en titulares+suplentes

-- Reutilizo para suplentes
DROP TRIGGER IF EXISTS TR_ValidarCantidadPlantilla_Suplente $$
CREATE TRIGGER TR_ValidarCantidadPlantilla_Suplente
BEFORE INSERT ON PlantillaSuplente
FOR EACH ROW
BEGIN
    DECLARE cnt INT;
    SELECT CantidadFutbolistasPlantilla(NEW.id_plantilla) INTO cnt;
    IF cnt >= (SELECT cant_max_futbolistas FROM Plantilla WHERE id_plantilla = NEW.id_plantilla) THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'No se pueden agregar más futbolistas a la plantilla (se alcanza el máximo).';
    END IF;
END $$

-- 6) Evitar que un futbolista esté en titulares Y suplentes al mismo tiempo
DROP TRIGGER IF EXISTS TR_NoDuplicarTitularSuplente_Titular $$
CREATE TRIGGER TR_NoDuplicarTitularSuplente_Titular
BEFORE INSERT ON PlantillaTitular
FOR EACH ROW
BEGIN
    IF EXISTS (SELECT 1 FROM PlantillaSuplente WHERE id_plantilla = NEW.id_plantilla AND id_futbolista = NEW.id_futbolista) THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'El futbolista ya está como suplente en esta plantilla.';
    END IF;
END $$

DROP TRIGGER IF EXISTS TR_NoDuplicarTitularSuplente_Suplente $$
CREATE TRIGGER TR_NoDuplicarTitularSuplente_Suplente
BEFORE INSERT ON PlantillaSuplente
FOR EACH ROW
BEGIN
    IF EXISTS (SELECT 1 FROM PlantillaTitular WHERE id_plantilla = NEW.id_plantilla AND id_futbolista = NEW.id_futbolista) THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'El futbolista ya está como titular en esta plantilla.';
    END IF;
END $$




CREATE TRIGGER TR_VerificarPresupuesto
BEFORE INSERT ON PlantillaTitular
FOR EACH ROW
BEGIN
    DECLARE total DECIMAL(12,2);
    DECLARE presupuesto_max DECIMAL(12,2) DEFAULT 99999999.99; -- según consigna

    SELECT IFNULL(SUM(f.cotizacion), 0)
    INTO total
    FROM PlantillaTitular pt
    JOIN Futbolista f ON f.id_futbolista = pt.id_futbolista
    WHERE pt.id_plantilla = NEW.id_plantilla;

    IF (total + (SELECT cotizacion FROM Futbolista WHERE id_futbolista = NEW.id_futbolista)) > presupuesto_max THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Error: Presupuesto máximo de la plantilla excedido';
    END IF;
END; $$ -- <-- ESTA LÍNEA FUE CORREGIDA


CREATE TRIGGER TR_LimiteJugadores
BEFORE INSERT ON PlantillaTitular
FOR EACH ROW
BEGIN
    DECLARE cantidad INT;
    DECLARE max_jugadores INT DEFAULT 20;

    SELECT COUNT(*) INTO cantidad
    FROM PlantillaTitular
    WHERE id_plantilla = NEW.id_plantilla;

    IF cantidad >= max_jugadores THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Error: La plantilla ya tiene el máximo de jugadores (20)';
    END IF;
END; $$