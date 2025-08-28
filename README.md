# Gran ET12 ⚽ - Simulador de Torneo Estilo Gran DT

## 📌 Descripción
Este proyecto implementa el **backend y gestión de datos** para una aplicación de simulación de un torneo ficticio con jugadores reales, similar a **Gran DT**.  
Incluye:
- Base de datos SQL con tablas, relaciones y triggers.
- Librería en C# para la lógica de negocio.
- Interfaz gráfica (Windows Forms).
- Documentación completa: DER, diagrama de clases y bitácora.

---

## 🛠️ Requerimientos principales
- **Jugadores**: nombre, apellido, apodo (opcional), fecha de nacimiento, equipo, cotización (máx. $99.999.999,99), tipo (Arquero, Defensor, Mediocampista, Delantero).
- **Equipos**: nombre único, máx. 32 equipos.
- **Usuarios**: nombre, apellido, email (único), fecha de nacimiento, contraseña (64 caracteres encriptados), máx. 2000 usuarios.
- **Plantillas**: asociadas a un usuario, con presupuesto máximo y cantidad máxima de jugadores (p. ej. 20).
    - Requisitos del equipo titular:
        - 1 Arquero
        - 4 Defensores
        - 4 Mediocampistas
        - 2 Delanteros
- **Puntuaciones**: para cada jugador y fecha (1 a 49), nota decimal entre 1 y 10.  
    - Un jugador no puede tener dos puntuaciones en la misma fecha.
- **Cálculos requeridos**:
    - Validar presupuesto y composición.
    - Sumar puntaje de titulares por fecha (`Plantilla.PuntajeFecha(n)`).

---

## 📂 Estructura del repositorio
