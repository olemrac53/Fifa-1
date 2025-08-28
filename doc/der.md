```mermaid
erDiagram
  EQUIPO ||--o{ JUGADOR : "tiene"
  USUARIO ||--o{ PLANTILLA : "crea"
  PLANTILLA ||--o{ PLANTILLA_JUGADOR : "contiene"
  JUGADOR ||--o{ PLANTILLA_JUGADOR : "pertenece a"
  JUGADOR ||--o{ PUNTUACION_JUGADOR : "recibe"

  EQUIPO {
    int id_equipo PK
    string nombre
  }

  JUGADOR {
    int id_jugador PK
    string nombre
    string apellido
    string apodo
    date fecha_nacimiento
    decimal cotizacion
    string tipo
    int id_equipo FK
  }

  USUARIO {
    int id_usuario PK
    string nombre
    string apellido
    string email
    date fecha_nacimiento
    char contrasenia
  }

  PLANTILLA {
    int id_plantilla PK
    int id_usuario FK
    decimal presupuesto_max
    int cant_max_jugadores
  }

  PLANTILLA_JUGADOR {
    int id_plantilla_jugador PK
    int id_plantilla FK
    int id_jugador FK
    bool es_titular
  }

  PUNTUACION_JUGADOR {
    int id_puntuacion PK
    int id_jugador FK
    int fecha
    decimal puntuacion
  }
