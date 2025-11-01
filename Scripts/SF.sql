USE GranET12;

DELIMITER $$

-- Presupuesto total: suma cotizaciones de titulares + suplentes en la plantilla
DROP FUNCTION IF EXISTS PresupuestoPlantilla $$
CREATE FUNCTION PresupuestoPlantilla(p_id_plantilla INT)
RETURNS DECIMAL(10,2)
DETERMINISTIC
READS SQL DATA
BEGIN
    DECLARE v_pres DECIMAL(10,2);
    SELECT IFNULL(SUM(f.cotizacion),0) INTO v_pres
    FROM (
        SELECT id_futbolista FROM PlantillaTitular WHERE id_plantilla = p_id_plantilla
        UNION ALL
        SELECT id_futbolista FROM PlantillaSuplente WHERE id_plantilla = p_id_plantilla
    ) t
    JOIN Futbolista f ON f.id_futbolista = t.id_futbolista;
    RETURN IFNULL(v_pres,0);
END $$

-- Cantidad de futbolistas en plantilla (titulares + suplentes)
DROP FUNCTION IF EXISTS CantidadFutbolistasPlantilla $$
CREATE FUNCTION CantidadFutbolistasPlantilla(p_id_plantilla INT)
RETURNS INT
DETERMINISTIC
READS SQL DATA
BEGIN
    DECLARE v_cnt INT;
    SELECT COUNT(*) INTO v_cnt FROM (
        SELECT id_futbolista FROM PlantillaTitular WHERE id_plantilla = p_id_plantilla
        UNION
        SELECT id_futbolista FROM PlantillaSuplente WHERE id_plantilla = p_id_plantilla
    ) x;
    RETURN IFNULL(v_cnt,0);
END $$

-- Validar composición de titulares (1-4-4-2)
DROP FUNCTION IF EXISTS PlantillaEsValida $$
CREATE FUNCTION PlantillaEsValida(p_id_plantilla INT)
RETURNS BOOLEAN
DETERMINISTIC
READS SQL DATA
BEGIN
    DECLARE v_arq INT; 
    DECLARE v_def INT; 
    DECLARE v_med INT; 
    DECLARE v_del INT;

    -- Arquero
    SELECT COUNT(*) INTO v_arq
    FROM PlantillaTitular pt
    JOIN Futbolista f ON pt.id_futbolista = f.id_futbolista
    JOIN Tipo t ON f.id_tipo = t.id_tipo
    WHERE pt.id_plantilla = p_id_plantilla AND t.nombre = 'Arquero';

    -- Defensa
    SELECT COUNT(*) INTO v_def
    FROM PlantillaTitular pt
    JOIN Futbolista f ON pt.id_futbolista = f.id_futbolista
    JOIN Tipo t ON f.id_tipo = t.id_tipo
    WHERE pt.id_plantilla = p_id_plantilla AND t.nombre = 'Defensa';

    -- Mediocampista
    SELECT COUNT(*) INTO v_med
    FROM PlantillaTitular pt
    JOIN Futbolista f ON pt.id_futbolista = f.id_futbolista
    JOIN Tipo t ON f.id_tipo = t.id_tipo
    WHERE pt.id_plantilla = p_id_plantilla AND t.nombre = 'Mediocampista';

    -- Delantero
    SELECT COUNT(*) INTO v_del
    FROM PlantillaTitular pt
    JOIN Futbolista f ON pt.id_futbolista = f.id_futbolista
    JOIN Tipo t ON f.id_tipo = t.id_tipo
    WHERE pt.id_plantilla = p_id_plantilla AND t.nombre = 'Delantero';

    -- Validar formación
    IF v_arq = 1 AND v_def = 4 AND v_med = 4 AND v_del = 2 THEN
        RETURN TRUE;
    ELSE
        RETURN FALSE;
    END IF;
END $$

-- Puntaje de un futbolista en una fecha
DROP FUNCTION IF EXISTS PuntajeFutbolistaFecha $$
CREATE FUNCTION PuntajeFutbolistaFecha(p_id_futbolista INT, p_fecha INT)
RETURNS DECIMAL(3,1)
DETERMINISTIC
READS SQL DATA
BEGIN
    DECLARE v_p DECIMAL(3,1);
    SELECT IFNULL(puntuacion,0) INTO v_p
    FROM PuntuacionFutbolista
    WHERE id_futbolista = p_id_futbolista AND fecha = p_fecha;
    RETURN IFNULL(v_p,0);
END $$

-- Puntaje total de la plantilla en una fecha
DROP FUNCTION IF EXISTS PuntajePlantillaFecha $$
CREATE FUNCTION PuntajePlantillaFecha(p_id_plantilla INT, p_fecha INT)
RETURNS DECIMAL(6,1)
DETERMINISTIC
READS SQL DATA
BEGIN
    DECLARE v_total DECIMAL(6,1);
    SELECT IFNULL(SUM(PuntajeFutbolistaFecha(pt.id_futbolista, p_fecha)),0) INTO v_total
    FROM PlantillaTitular pt
    WHERE pt.id_plantilla = p_id_plantilla;
    RETURN IFNULL(v_total,0);
END $$

DELIMITER ;
