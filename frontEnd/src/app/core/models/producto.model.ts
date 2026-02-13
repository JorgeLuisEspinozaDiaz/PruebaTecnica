export interface Producto {
  id: number;
  nombre: string;
  precio: number;
  fechaCreacion: Date;
}

export interface ProductoCreateDTO {
  nombre: string;
  precio: number;
}

export interface ProductoUpdateDTO {
  id: number;
  nombre: string;
  precio: number;
}

export interface BaseResponseDTO {
  statusCode: number;
  mensaje: string;
  confirmacion: boolean;
  data: any;
}

export interface PaginatedResponseDTO extends BaseResponseDTO {
  total: number;
  page: number;
  pages: number;
}

export interface ProductoFiltro {
  skip?: number;
  take?: number;
  search?: string;
  id?: number;
}
