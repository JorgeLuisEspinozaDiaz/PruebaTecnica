# 🛍️ Sistema de Gestión de Productos - Frontend

Aplicación Frontend desarrollada en **Angular 21** con arquitectura **Atomic Design** para la gestión completa de productos (CRUD).

## 📋 Requisitos Previos

- **Node.js**: v18 o superior
- **npm**: v10 o superior
- **Angular CLI**: v21.1.4 (se instala con las dependencias)

## 🚀 Instalación y Ejecución

### 1. Navegar al proyecto

```bash
cd "c:\Users\espin\Desktop\Prueba Tecnica\frontEnd"
```

### 2. Instalar dependencias

```bash
npm install
```

### 3. Iniciar servidor de desarrollo

```bash
npm start
```

La aplicación estará disponible en: **http://localhost:4200**

> **Importante**: El backend .NET debe estar corriendo en `http://localhost:5000` antes de usar la aplicación.

## ⚙️ Configuración de la API

El archivo de configuración se encuentra en `src/environments/environment.ts`:

```typescript
export const environment = {
  production: false,
  apiUrl: 'http://localhost:5000/api'  
};
```
 
## 🛠️ Tecnologías Utilizadas

| Dependencia | Versión | Uso |
|-------------|---------|-----|
| Angular | 21.1.0 | Framework principal |
| TypeScript | 5.9.2 | Lenguaje tipado |
| RxJS | 7.8.0 | Programación reactiva |
| Font Awesome | 7.2.0 | Iconos |
| SweetAlert2 | 11.26.18 | Alertas y notificaciones |
| SCSS | - | Estilos con preprocesador |

## 📦 Scripts Disponibles

```bash
npm start       # Inicia servidor de desarrollo (localhost:4200)
npm run build   # Compila para producción (carpeta dist/)
```
