USE 5to_GranET12;

-- Corrección del delimitador (sin el +)
DELIMITER $$

-- ======================================================
-- 1. VALIDACIÓN: NO PERMITIR PUNTAJE SI NO ES TITULAR
-- ======================================================
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

-- ======================================================
-- 2. VALIDACIÓN: FUTBOLISTA DEBE TENER TIPO
-- ======================================================
DROP TRIGGER IF EXISTS TR_ValidarTipoFutbolista $$
CREATE TRIGGER TR_ValidarTipoFutbolista
BEFORE INSERT ON Futbolista
FOR EACH ROW
BEGIN
    IF NEW.id_tipo IS NULL OR NOT EXISTS (SELECT 1 FROM Tipo t WHERE t.id_tipo = NEW.id_tipo) THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Todo futbolista debe tener asignado un tipo válido.';
    END IF;
END $$

-- ======================================================
-- 3. VALIDACIÓN DE PRESUPUESTO (TITULARES)
-- ======================================================
DROP TRIGGER IF EXISTS TR_ValidarPresupuesto_AltaTitular $$
CREATE TRIGGER TR_ValidarPresupuesto_AltaTitular
BEFORE INSERT ON PlantillaTitular
FOR EACH ROW
BEGIN
    DECLARE v_presupuesto_actual DECIMAL(12,2);
    DECLARE v_cotizacion_nueva DECIMAL(12,2);
    DECLARE v_presupuesto_max DECIMAL(12,2);

    -- 1. Obtenemos el gastado actual (Suma Titulares + Suplentes usando tu Función)
    SELECT PresupuestoPlantilla(NEW.id_plantilla) INTO v_presupuesto_actual;

    -- 2. Obtenemos precio del jugador a ingresar
    SELECT cotizacion INTO v_cotizacion_nueva FROM Futbolista WHERE id_futbolista = NEW.id_futbolista;

    -- 3. Obtenemos el tope de la plantilla
    SELECT presupuesto_max INTO v_presupuesto_max FROM Plantilla WHERE id_plantilla = NEW.id_plantilla;

    -- 4. Validamos
    IF (v_presupuesto_actual + v_cotizacion_nueva) > v_presupuesto_max THEN
        SIGNAL SQLSTATE '45000' 
        SET MESSAGE_TEXT = 'Error: Presupuesto excedido al agregar Titular.';
    END IF;
END $$

-- ======================================================
-- 4. VALIDACIÓN DE PRESUPUESTO (SUPLENTES)
-- ======================================================
DROP TRIGGER IF EXISTS TR_ValidarPresupuesto_AltaSuplente $$
CREATE TRIGGER TR_ValidarPresupuesto_AltaSuplente
BEFORE INSERT ON PlantillaSuplente
FOR EACH ROW
BEGIN
    DECLARE v_presupuesto_actual DECIMAL(12,2);
    DECLARE v_cotizacion_nueva DECIMAL(12,2);
    DECLARE v_presupuesto_max DECIMAL(12,2);

    -- Misma lógica: Usamos la función que suma TODO
    SELECT PresupuestoPlantilla(NEW.id_plantilla) INTO v_presupuesto_actual;
    
    SELECT cotizacion INTO v_cotizacion_nueva FROM Futbolista WHERE id_futbolista = NEW.id_futbolista;
    
    SELECT presupuesto_max INTO v_presupuesto_max FROM Plantilla WHERE id_plantilla = NEW.id_plantilla;

    IF (v_presupuesto_actual + v_cotizacion_nueva) > v_presupuesto_max THEN
        SIGNAL SQLSTATE '45000' 
        SET MESSAGE_TEXT = 'Error: Presupuesto excedido al agregar Suplente.';
    END IF;
END $$

-- ======================================================
-- 5. LÍMITE DE JUGADORES (MAX 20)
-- ======================================================
-- Para Suplentes
DROP TRIGGER IF EXISTS TR_ValidarCantidadPlantilla_Suplente $$
CREATE TRIGGER TR_ValidarCantidadPlantilla_Suplente
BEFORE INSERT ON PlantillaSuplente
FOR EACH ROW
BEGIN
    DECLARE cnt INT;
    SELECT CantidadFutbolistasPlantilla(NEW.id_plantilla) INTO cnt;
    IF cnt >= (SELECT cant_max_futbolistas FROM Plantilla WHERE id_plantilla = NEW.id_plantilla) THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Límite de jugadores alcanzado (Suplente).';
    END IF;
END $$

-- Para Titulares (Agregado para consistencia)
DROP TRIGGER IF EXISTS TR_ValidarCantidadPlantilla_Titular $$
CREATE TRIGGER TR_ValidarCantidadPlantilla_Titular
BEFORE INSERT ON PlantillaTitular
FOR EACH ROW
BEGIN
    DECLARE cnt INT;
    SELECT CantidadFutbolistasPlantilla(NEW.id_plantilla) INTO cnt;
    IF cnt >= (SELECT cant_max_futbolistas FROM Plantilla WHERE id_plantilla = NEW.id_plantilla) THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Límite de jugadores alcanzado (Titular).';
    END IF;
END $$

-- ======================================================
-- 6. EVITAR DUPLICADOS CRUZADOS (TITULAR <-> SUPLENTE)
-- ======================================================
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

DELIMITER ;