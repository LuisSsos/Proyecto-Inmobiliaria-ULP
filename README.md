# Proyecto-Inmobiliaria-ULP

---

## Integrantes del grupo
* **Luis Ezequiel Sosa**
* **Iván Oscar Auriol López**
* **Florencia Magalí Castro**

---

## Instrucciones para ejecutar el proyecto
### 1. Clonar el repositorio
Abrí tu terminal y ejecutá los siguientes comandos:
```bash
git clone https://github.com/LuisSsos/Proyecto-Inmobiliaria-ULP
cd Proyecto-Inmobiliaria-ULP
```
También podés descargarlo comprimido, descomprimirlo y abrirlo carpeta en tu editor de confianza.

---

### 2.Levantá la db
Podés importar el archivo Inmobiliaria.sql en tu gestor de base de datos (por ejemplo phpMyAdmin con xampp) o copiar y pegar las líneas directamente del archivo y pegarlas en la caja de consultas de tu gestor para poder ejecutarlo.

---

### 3. Configurar conexión
Verificá que el archivo `appsettings.json` coincida con las credenciales de tu servidor MySQL. Por defecto está configurada así:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "server=localhost;port=3306;database=inmobiliaria;user=root;password=;"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```
Si usás DBeaver procurá poner el usuario y la contraseña de usuario en el appsettings.json o por el contrario, quitar la contraseña y cambiar  a usuario "root". Phpmyadmin suele venir por defecto con las configuraciones de user "root" y sin contraseña, pero si en algún momento lo has cambiado, tené en cuenta de modificarlo para poder correr este proyecto.

---

### 4. Ejecutar el proyecto
En una consola, sea del VS Code o gitbash, parate dentro del directorio del proyecto y ejecutá:
```bash
dotnet run
```

---

## 5. Algo muy importante la navegación (Rutas)

Al ejecutar el proyecto, la te va a indicar en qué URL y puerto se está alojando (por ejemplo, `http://localhost:5063`). Este puerto se define en el archivo `Properties/launchSettings.json` de cada entorno.

---

## Modelado

Se podrá ver el  diagrama relacional en la carpeta db.

### Esquema de Base de Datos

![Esquema de Base de Datos](db/Inmobiliaria.png)

---

### 6. Configurar credenciales de Cloudinary

El modulo de imagenes de inmuebles utiliza el servicio externo **Cloudinary** para almacenar las fotos. por seguridad, las credenciales (Cloud Name, API Key y API Secret) **no se incluyen en el repositorio** ni en 'appsettings.json'.

Para que el proyecto funcione en el equipo, hay que usar estos comandos en la carpeta del proyecto, reemplazando los valores por los que te fueron entregados por separado (no se suben a GitHub por motivos de seguridad):

```bash
dotnet user-secrets init
dotnet user-secrets set "Cloudinary:CloudName" "TU_CLOUD_NAME"
dotnet user-secrets set "Cloudinary:ApiKey" "TU_API_KEY"
dotnet user-secrets set "Cloudinary:ApiSecret" "TU_API_SECRET"
```

> **Nota para el docente:** las credenciales de Cloudinary se entregan por separado (fuera del repositorio) para no gastar el limite de la cuenta gratuita.

---

## ⚙️ Estado Actual del Desarrollo
* Configuración de la base de datos **MySQL** y de la inyección de dependencias mediante RepositorioBase.
* Desarrollo de **modelos, repositorios y controladores** para las entidades:
  * **Propietario**
  * **Inquilino**
  * **Inmueble**
  * **Reserva**
  * **TipoInmueble**
  * **Usuario**
* Implementación de vistas Razor con operaciones CRUD para permitir listar, crear, editar y eliminar registros de **Propietarios, Inquilinos, Inmuebles, Reservas y Usuarios.**
* Implementación de **autenticacion y autorizacion** mediante cookies:
  * Login con email y contraseña ('/auth/login').
  * Roles diferenciados: **Administrador** (acceso total, incluida la eliminación de registros) y **Empleado** (puede crear y modificar, pero no eliminar).
  * Cada usuario puede editar su propio perfil desde '/Usuario/MiPerfil', sin necesidad de ser Administrador.
  * La entidad Usuario ya es accesible desde la interfaz web, con navegación integrada en el menú principal.
*Implementación de las funcionalidades de **Editar y Eliminar** para la entidad Reserva.
* Implementación de validaciones de negocio para la entidad **Reserva**:
  * No se permite guardar una reserva con fecha de inicio posterior o igual a la fecha de fin.
  * No se permite cargar una reserva con fecha de inicio anterior al dia actual.
  * No se permite reservar un mismo inmueble en fechas que se superpongan con otra reserva ya existente.
* Implementación de un **middleware de manejo de excepciones** a nivel global, que captura cualquier error no controlado de la aplicación y redirige a una vista de error personalizada, evitando mostrar el detalle técnico al usuario.
* Desarrollo del módulo de **imagenes de inmuebles**, permitiendo subir, listar y eliminar fotos asociadas a cada propiedad. La carga de imagenes se realiza mediante integración con el servicio externo **Cloudinary**, guardando en la base de datos la referencia (URL) de cada imagen subida.
* Incorporación de la imagen de portada del inmueble en el listado principal de **Inmuebles**, mostrando la foto (si existe) junto al resto de los datos.