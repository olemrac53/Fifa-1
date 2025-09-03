
DELIMITER //
DROP FUNCTION IF EXISTS PresupuestoPlantilla //
CREATE FUNCTION PresupuestoPlantilla(p_id_plantilla INT)
RETURNS DECIMAL(10,2)
DETERMINISTIC
BEGIN
    DECLARE v_presupuesto DECIMAL(10,2);

    SELECT IFNULL(SUM(f.cotizacion), 0)
    INTO v_presupuesto
    FROM PlantillaFutbolista pf
    JOIN Futbolista f ON pf.id_futbolista = f.id_futbolista
    WHERE pf.id_plantilla = p_id_plantilla;

    RETURN v_presupuesto;
END //
DELIMITER ;


DELIMITER //
DROP FUNCTION IF EXISTS CantidadFutbolistasPlantilla //
CREATE FUNCTION CantidadFutbolistasPlantilla(p_id_plantilla INT)
RETURNS INT
DETERMINISTIC
BEGIN
    DECLARE v_cantidad INT;

    SELECT COUNT(*)
    INTO v_cantidad
    FROM PlantillaFutbolista
    WHERE id_plantilla = p_id_plantilla;

    RETURN v_cantidad;
END //
DELIMITER ;


DELIMITER //
DROP FUNCTION IF EXISTS PuntajeFutbolistaFecha //
CREATE FUNCTION PuntajeFutbolistaFecha(p_id_futbolista INT, p_fecha INT)
RETURNS DECIMAL(3,1)
DETERMINISTIC
BEGIN
    DECLARE v_puntaje DECIMAL(3,1);

    SELECT IFNULL(pf.puntuacion, 0)
    INTO v_puntaje
    FROM PuntuacionFutbolista pf
    WHERE pf.id_futbolista = p_id_futbolista
      AND pf.fecha = p_fecha;

    RETURN v_puntaje;
END //
DELIMITER ;


DELIMITER //
DROP FUNCTION IF EXISTS PuntajePlantillaFecha //
CREATE FUNCTION PuntajePlantillaFecha(p_id_plantilla INT, p_fecha INT)
RETURNS DECIMAL(5,1)
DETERMINISTIC
BEGIN
    DECLARE v_total DECIMAL(5,1);

    SELECT IFNULL(SUM(PuntajeFutbolistaFecha(f.id_futbolista, p_fecha)), 0)
    INTO v_total
    FROM PlantillaFutbolista pf
    JOIN Futbolista f ON pf.id_futbolista = f.id_futbolista
    WHERE pf.id_plantilla = p_id_plantilla
      AND pf.es_titular = TRUE;

    RETURN v_total;
END //
DELIMITER ;

