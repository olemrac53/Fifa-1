USE GranET12;

-- PASO 1: Limpiar SOLO el usuario de prueba temporal
DELETE FROM Usuario WHERE email = 'nuevo@mail.com';

-- PASO 2: Verificar si existen juan y maria
SELECT id_usuario, email FROM Usuario WHERE email IN ('juan@mail.com', 'maria@mail.com');

-- PASO 3A: Si NO existen (la consulta anterior devuelve vacío), ejecutar esto:
-- Primero borra todos los usuarios para resetear el auto_increment
DELETE FROM Usuario;

-- Resetear el auto_increment
ALTER TABLE Usuario AUTO_INCREMENT = 1;

-- Insertar con IDs específicos
INSERT INTO Usuario (id_usuario, nombre, apellido, email, contrasenia, fecha_nacimiento)
VALUES 
(1, 'Juan', 'Perez', 'juan@mail.com', SHA2('pass123', 256), '1990-01-01'),
(2, 'Maria', 'Lopez', 'maria@mail.com', SHA2('pass456', 256), '1985-05-15');

-- PASO 3B: Si YA existen pero con contraseña incorrecta, ejecutar esto:
UPDATE Usuario 
SET contrasenia = SHA2('pass123', 256)
WHERE email = 'juan@mail.com';

UPDATE Usuario 
SET contrasenia = SHA2('pass456', 256)
WHERE email = 'maria@mail.com';

-- PASO 4: Verificar que quedaron con los IDs correctos
SELECT 
    id_usuario, 
    nombre, 
    apellido, 
    email,
    LENGTH(contrasenia) as longitud_hash
FROM Usuario 
WHERE email IN ('juan@mail.com', 'maria@mail.com')
ORDER BY id_usuario;

-- PASO 5: Verificar que las contraseñas funcionan
SELECT  
    id_usuario,
    nombre,
    email
FROM Usuario
WHERE email = 'juan@mail.com'
AND contrasenia = SHA2('pass123', 256);

SELECT  
    id_usuario,
    nombre,
    email
FROM Usuario
WHERE email = 'maria@mail.com'
AND contrasenia = SHA2('pass456', 256);