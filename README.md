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


