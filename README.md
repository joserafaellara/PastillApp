# PastillApp - Backend

Bienvenido al backend de PastillApp, una API que gestiona recordatorios de medicamentos, estados de salud diarios y envía alertas mediante Firebase Cloud Messaging.

## Tecnologías Utilizadas

- **.NET 6:** Plataforma de desarrollo para la construcción de aplicaciones.
- **Entity Framework:** Framework de mapeo objeto-relacional para acceder y gestionar datos en la base de datos.
- **Swagger:** Herramienta para documentar y probar APIs.
- **Firebase Cloud Messaging (FCM):** Servicio de mensajería en la nube para enviar notificaciones push.
- **Worker Service:** Servicio que ejecuta en segundo plano para gestionar tareas programadas.
- **Visual Studio 2022:** Entorno de desarrollo integrado utilizado para construir la aplicación.

## Instalación

1. Clona el repositorio:
   ```bash
   git clone https://github.com/joserafaellara/PastillApp.git
2. Abre el proyecto en Visual Studio 2022.
3. Ejecutar. Esto creará una base de datos con medicamentos precargados.

## Estructura del Proyecto
API.PastillaApp: Proyecto principal con los controladores de la API.
API.PastillaApp.Domain: Capa con las entidades del dominio.
API.PastillaApp.Services: Capa con la lógica de negocio.
API.PastillaApp.Repository: Capa con las consultas y búsquedas a la base de datos.

## Uso
La documentación completa de la API se encuentra en Swagger.
Utiliza las operaciones CRUD en los controladores para gestionar usuarios, recordatorios, medicamentos y estados diarios.
El servicio de trabajador (WorkerService) maneja las notificaciones de medicamentos y alertas de emergencia.

## Contribución
1. Realiza un fork del repositorio.
2. Crea una rama para tu función/bugfix: git checkout -b feature-nueva.
3. Realiza tus cambios y haz commits: git commit -m 'Añade nueva funcionalidad'.
4. Sube tus cambios a tu fork: git push origin feature-nueva.
5. Crea un Pull Request en GitHub.

## Equipo
Estudiantes del Instituto ORT Argentina, en la carrera de Analistas en Sistemas:
Agustina Boto, Joaquin Herreros, Tomas Guerchicoff Adamo, Camila Szesko, Camila Ingberg,
Francisco Veron, Paola Quinonez, Federico Marty, Patricia Berkovics, Pia Potasznik, Jose Lara
