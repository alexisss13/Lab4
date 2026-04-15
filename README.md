# Laboratorio 4 - Serialización y Deserialización JSON en ASP.NET Core MVC

## Descripción
Proyecto ASP.NET Core MVC que implementa tres ejercicios prácticos sobre serialización y deserialización JSON, demostrando diferentes técnicas de persistencia de datos.

## Ejercicios Implementados

### 📦 Ejercicio 1: Sistema de Delivery
**Ruta:** `/Pedidos`

Gestión de pedidos con serialización JSON automática:
- Crear nuevos pedidos con cliente, productos, dirección y estado
- Visualizar historial completo de pedidos
- Actualizar estado de entrega (Pendiente → En camino → Entregado)
- Persistencia automática en `Data/pedidos.json`

**Características técnicas:**
- Serialización con `System.Text.Json`
- Control de concurrencia con `SemaphoreSlim`
- Deserialización automática al iniciar la aplicación

### 🎓 Ejercicio 2: Control de Acceso Educativo
**Ruta:** `/Usuarios`

Sistema de gestión de usuarios institucionales:
- Registro de docentes, alumnos y administrativos
- Edición de roles y estado de usuarios
- Estadísticas por tipo de usuario
- Persistencia con `StreamWriter/StreamReader`

**Características técnicas:**
- Uso explícito de streams para I/O
- Control de concurrencia para integridad de datos
- Actualización automática del archivo JSON
- Archivo: `Data/usuarios.json`

### 🌡️ Ejercicio 3: Sensores IoT
**Ruta:** `/Sensores`

Monitoreo ambiental con rotación de logs:
- Registro de lecturas de temperatura y humedad
- Rotación automática de archivos (100 KB)
- Visualización con gráficas interactivas
- Simulación de lecturas para pruebas

**Características técnicas:**
- Rotación automática de archivos JSON
- Múltiples archivos con timestamp
- Deserialización de múltiples archivos
- Visualización con Chart.js
- Archivos: `Data/Sensores/sensores_*.json`

## Requisitos
- .NET 10.0 o superior
- Navegador web moderno

## Instalación y Ejecución

1. Clonar o descargar el proyecto

2. Restaurar dependencias:
```bash
dotnet restore
```

3. Ejecutar la aplicación:
```bash
dotnet run --project Lab4
```

4. Abrir en el navegador:
```
https://localhost:5001
```

## Estructura del Proyecto

```
Lab4/
├── Controllers/
│   ├── HomeController.cs
│   ├── PedidosController.cs
│   ├── UsuariosController.cs
│   └── SensoresController.cs
├── Models/
│   ├── Pedido.cs
│   ├── Usuario.cs
│   └── LecturaSensor.cs
├── Services/
│   ├── PedidoService.cs
│   ├── UsuarioService.cs
│   └── SensorService.cs
├── Views/
│   ├── Home/
│   ├── Pedidos/
│   ├── Usuarios/
│   └── Sensores/
├── Data/                    (se crea automáticamente)
│   ├── pedidos.json
│   ├── usuarios.json
│   └── Sensores/
│       └── sensores_*.json
└── README.md
```

## Uso de la Aplicación

### Sistema de Delivery
1. Ir a "Pedidos" desde la página principal
2. Hacer clic en "Nuevo Pedido"
3. Completar datos del cliente, productos y dirección
4. El pedido se guarda automáticamente en JSON
5. Actualizar estado desde la lista de pedidos

### Control de Acceso
1. Ir a "Usuarios" desde la página principal
2. Hacer clic en "Registrar Usuario"
3. Completar nombre, email y seleccionar rol
4. Editar usuarios existentes para cambiar roles
5. Ver estadísticas por tipo de usuario

### Sensores IoT
1. Ir a "Sensores" desde la página principal
2. Opción 1: Registrar lectura manual
3. Opción 2: Simular 10 lecturas automáticas
4. Ver gráficas de evolución temporal
5. Observar rotación automática de archivos

## Características Técnicas Destacadas

### Serialización JSON
- Uso de `System.Text.Json` (nativo de .NET)
- Opciones de formato indentado para legibilidad
- Serialización/deserialización de colecciones

### Control de Concurrencia
- `SemaphoreSlim` para acceso exclusivo
- Patrón async/await para operaciones I/O
- Prevención de condiciones de carrera

### Manejo de Archivos
- `StreamWriter/StreamReader` para control granular
- Rotación automática de logs
- Creación automática de directorios

### Arquitectura
- Patrón Repository con servicios
- Dependency Injection
- Separación de responsabilidades (MVC)

## Archivos de Datos

Los archivos JSON se crean automáticamente en la carpeta `Data/`:

**pedidos.json:**
```json
[
  {
    "Id": 1,
    "Cliente": "Juan Pérez",
    "Productos": ["Pizza", "Coca Cola"],
    "Direccion": "Av. Principal 123",
    "Estado": "Entregado",
    "FechaPedido": "2026-04-14T10:30:00"
  }
]
```

**usuarios.json:**
```json
[
  {
    "Id": 1,
    "Nombre": "María García",
    "Email": "maria@universidad.edu",
    "Rol": "Docente",
    "FechaRegistro": "2026-04-14T09:00:00",
    "Activo": true
  }
]
```

**sensores_*.json:**
```json
[
  {
    "Id": 1,
    "SensorId": "SENSOR-001",
    "Temperatura": 23.5,
    "Humedad": 65.2,
    "FechaLectura": "2026-04-14T14:15:00",
    "Ubicacion": "Sala A"
  }
]
```

## Observaciones y Conclusiones

Ver el archivo `OBSERVACIONES_Y_CONCLUSIONES.md` para un análisis detallado de:
- Ventajas y desventajas de JSON
- Patrones implementados
- Consideraciones de performance
- Recomendaciones para producción

## Autor
Laboratorio desarrollado para el curso de Programación Web

## Licencia
Proyecto educativo - Uso libre para fines académicos
