-- Active: 1700068523370@@127.0.0.1@3306@GranET12
USE GranET12;

-- ============================================
-- SCRIPT DE INICIALIZACIÓN PARA TESTS
-- ============================================

-- 1. LIMPIAR DATOS DE PRUEBA
-- ============================================
DELETE FROM PlantillaTitular WHERE id_plantilla IN (SELECT id_plantilla FROM Plantilla WHERE id_usuario IN (1, 2));
DELETE FROM PlantillaSuplente WHERE id_plantilla IN (SELECT id_plantilla FROM Plantilla WHERE id_usuario IN (1, 2));
DELETE FROM Plantilla WHERE id_usuario IN (1, 2);
DELETE FROM Usuario WHERE id_usuario IN (1, 2);
DELETE FROM Administrador;

-- 2. RESETEAR AUTO_INCREMENT
-- ============================================
ALTER TABLE Usuario AUTO_INCREMENT = 1;
ALTER TABLE Administrador AUTO_INCREMENT = 1;

-- 3. INSERTAR USUARIOS DE PRUEBA
-- ============================================
INSERT INTO Usuario (id_usuario, nombre, apellido, email, contrasenia, fecha_nacimiento)
VALUES 
(1, 'Juan', 'Perez', 'juan@mail.com', SHA2('pass123', 256), '1990-01-01'),
(2, 'Maria', 'Lopez', 'maria@mail.com', SHA2('pass456', 256), '1985-05-15');

-- 4. INSERTAR ADMINISTRADOR DE PRUEBA
-- ============================================
INSERT INTO Administrador (nombre, apellido, email, contrasenia, fecha_nacimiento)
VALUES ('Admin', 'Test', 'admin@fifa.com', SHA2('123456', 256), '1990-01-01');

-- 5. VERIFICAR DATOS INSERTADOS
-- ============================================
SELECT '=== USUARIOS DISPONIBLES ===' as info;
SELECT 
    id_usuario, 
    nombre, 
    apellido, 
    email,
    fecha_nacimiento
FROM Usuario
WHERE id_usuario IN (1, 2)
ORDER BY id_usuario;

SELECT '=== ADMINISTRADORES DISPONIBLES ===' as info;
SELECT 
    id_admin,
    nombre, 
    apellido, 
    email,
    fecha_nacimiento
FROM Administrador;