-- Crear un usuario común:
CALL AltaUsuario('Juan', 'Perez', 'juan@mail.com', '1990-05-15', 'contraseña_encriptada');

-- Crear un administrador:
CALL AltaAdministrador('Maria', 'Lopez', 'maria@mail.com', '1985-03-20', 'contraseña_encriptada');