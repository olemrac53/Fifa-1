USE GrantET12;
-- Inserta usuarios de prueba
INSERT INTO Usuario (nombre, apellido, email, contrasenia, fecha_nacimiento)
VALUES 
('pepe', 'Pezrez', 'juaan@mail.com', SHA2('pass1234', 256), '1990-02-01'),
('wea', 'Lopewz', 'mariaa@mail.com', SHA2('pass4256', 256), '1985-06-15');

-- Verifica que se insertaron
SELECT * FROM Usuario;