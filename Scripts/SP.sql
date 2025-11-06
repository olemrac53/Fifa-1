-- === DATOS DE PRUEBA PARA TESTS (al final de SP.sql) ===
DELETE FROM PlantillaTitular WHERE id_plantilla IN (SELECT id_plantilla FROM Plantilla WHERE id_usuario IN (1, 2));
DELETE FROM PlantillaSuplente WHERE id_plantilla IN (SELECT id_plantilla FROM Plantilla WHERE id_usuario IN (1, 2));
DELETE FROM Plantilla WHERE id_usuario IN (1, 2);
DELETE FROM PuntuacionFutbolista;
DELETE FROM Futbolista;
DELETE FROM Tipo;
DELETE FROM Equipo WHERE id_equipo = 1;

-- SOLUCIÓN: Usar INSERT IGNORE en lugar de ON DUPLICATE KEY UPDATE
INSERT IGNORE INTO Equipo (id_equipo, nombre, presupuesto) 
VALUES (1, 'Equipo Test 1', 1000000.00);

INSERT IGNORE INTO Tipo (id_tipo, nombre) VALUES 
(1, 'Delantero'),
(2, 'Mediocampista'),
(3, 'Defensa'),
(4, 'Arquero');

SELECT '=== EQUIPOS DISPONIBLES PARA TESTS ===' as Info;
SELECT * FROM Equipo WHERE id_equipo = 1;

SELECT '=== TIPOS DISPONIBLES PARA TESTS ===' as Info;
SELECT * FROM Tipo ORDER BY id_tipo;

SELECT '=== SETUP COMPLETADO ===' as Info;
SELECT 'Base de datos lista para ejecutar tests' as Mensaje;