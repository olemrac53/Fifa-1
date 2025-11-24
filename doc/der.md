erDiagram
    %% Entidades Principales
    EQUIPO {
        int id_equipo PK
        string nombre
        decimal presupuesto
    }

    TIPO {
        int id_tipo PK
        string nombre
    }

    FUTBOLISTA {
        int id_futbolista PK
        string nombre
        string apellido
        string num_camisa
        string apodo
        date fecha_nacimiento
        decimal cotizacion
        int id_equipo FK
        int id_tipo FK
    }

    USUARIO {
        int id_usuario PK
        string nombre
        string apellido
        string email
        date fecha_nacimiento
        char contrasenia
    }

    ADMINISTRADOR {
        int id_administrador PK
        string nombre
        string apellido
        string email
        date fecha_nacimiento
        char contrasenia
    }

    PLANTILLA {
        int id_plantilla PK
        int id_usuario FK
        int id_administrador FK
        decimal presupuesto_max
        int cant_max_futbolistas
    }

    PUNTUACION_FUTBOLISTA {
        int id_puntuacion PK
        int id_futbolista FK
        int fecha
        decimal puntuacion
    }

    %% Tablas Intermedias (Muchos a Muchos)
    PLANTILLA_TITULAR {
        int id_plantilla FK "PK compuesto"
        int id_futbolista FK "PK compuesto"
    }

    PLANTILLA_SUPLENTE {
        int id_plantilla FK "PK compuesto"
        int id_futbolista FK "PK compuesto"
    }

    %% Relaciones
    EQUIPO ||--o{ FUTBOLISTA : "tiene"
    TIPO ||--o{ FUTBOLISTA : "define rol"
    
    USUARIO ||--o{ PLANTILLA : "gestiona"
    ADMINISTRADOR ||--o{ PLANTILLA : "supervisa (opcional)"

    FUTBOLISTA ||--o{ PUNTUACION_FUTBOLISTA : "recibe"

    %% Relaciones Plantilla - Futbolista (Titulares)
    PLANTILLA ||--o{ PLANTILLA_TITULAR : "tiene titulares"
    FUTBOLISTA ||--o{ PLANTILLA_TITULAR : "es titular en"

    %% Relaciones Plantilla - Futbolista (Suplentes)
    PLANTILLA ||--o{ PLANTILLA_SUPLENTE : "tiene suplentes"
    FUTBOLISTA ||--o{ PLANTILLA_SUPLENTE : "es suplente en"