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




DELETE FROM PlantillaTitular WHERE id_plantilla IN (SELECT id_plantilla FROM Plantilla WHERE id_usuario IN (1, 2));
DELETE FROM PlantillaSuplente WHERE id_plantilla IN (SELECT id_plantilla FROM Plantilla WHERE id_usuario IN (1, 2));
DELETE FROM Plantilla WHERE id_usuario IN (1, 2);

-- Eliminar usuarios
DELETE FROM Usuario WHERE email IN ('nuevo@mail.com', 'juan@mail.com', 'maria@mail.com');

-- Resetear auto_increment
ALTER TABLE Usuario AUTO_INCREMENT = 1;

-- Insertar usuarios de prueba CON HASH SHA2
INSERT INTO Usuario (id_usuario, nombre, apellido, email, contrasenia, fecha_nacimiento)
VALUES 
(1, 'Juan', 'Perez', 'juan@mail.com', SHA2('pass123', 256), '1990-01-01'),
(2, 'Maria', 'Lopez', 'maria@mail.com', SHA2('pass456', 256), '1985-05-15');

-- === Verificación ===
SELECT '=== VERIFICACIÓN DE USUARIOS ===' as mensaje;

SELECT 
    id_usuario,
    nombre,
    apellido,
    email,
    LENGTH(contrasenia) as longitud_hash
FROM Usuario
WHERE email IN ('juan@mail.com', 'maria@mail.com');

-- Verificar que las contraseñas coinciden con SHA2
SELECT 
    email,
    (contrasenia = SHA2('pass123', 256)) as juan_password_ok,
    (contrasenia = SHA2('pass456', 256)) as maria_password_ok
FROM Usuario
WHERE email IN ('juan@mail.com', 'maria@mail.com');

-- Probar el stored procedure AltaUsuario
CALL AltaUsuario(@new_id, 'Test', 'Usuario', 'test@mail.com', '1995-01-01', 'testpass');
SELECT @new_id as nuevo_id_usuario;

-- Verificar que se guardó con hash
SELECT 
    id_usuario,
    email,
    (contrasenia = SHA2('testpass', 256)) as password_hasheada_correctamente
FROM Usuario 
WHERE id_usuario = @new_id;

-- Limpiar el usuario de prueba
DELETE FROM Usuario WHERE id_usuario = @new_id;



USE GranET12;

DROP INSERT IF EXISTS  ON Administrador $$


-- PASO 1: Limpiar administradores de prueba
DELETE FROM Administrador WHERE email IN ('admin@fifa.com', 'test@fifa.com');

-- PASO 2: Insertar administrador para testing
INSERT INTO Administrador (nombre, apellido, email, contrasenia, fecha_nacimiento)
VALUES ('Adminn', 'Test', 'adminn@fifa.com', SHA2('123456', 256), '1990-01-01');

-- PASO 3: Verificar que se creó correctamente
SELECT 
    id_administrador,
    nombre,
    apellido,
    email,
    fecha_nacimiento,
    LENGTH(contrasenia) as longitud_hash
FROM Administrador
WHERE email = 'adminn@fifa.com';

-- PASO 4: Verificar que la contraseña funciona (debe devolver 1 fila)
SELECT 
    id_administrador,
    nombre,
    email
FROM Administrador
WHERE email = 'adminn@fifa.com'
AND contrasenia = SHA2('123456', 256);

-- Limpiar administradores de prueba
DELETE FROM Administrador WHERE email = 'adminn@fifa.com'