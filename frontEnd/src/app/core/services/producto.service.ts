import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable, catchError, throwError, map } from 'rxjs';
import { Producto, ProductoCreateDTO, ProductoUpdateDTO, ProductoFiltro, BaseResponseDTO, PaginatedResponseDTO } from '../models';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ProductoService {
  private readonly apiUrl = `${environment.apiUrl}/productos`;

  constructor(private http: HttpClient) {}


  getAll(filtro?: ProductoFiltro): Observable<PaginatedResponseDTO> {
    let params = new HttpParams();

    if (filtro) {
      if (filtro.skip !== undefined) {
        params = params.set('skip', filtro.skip.toString());
      }
      if (filtro.take !== undefined) {
        params = params.set('take', filtro.take.toString());
      }
      if (filtro.search) {
        params = params.set('search', filtro.search);
      }
      if (filtro.id !== undefined) {
        params = params.set('id', filtro.id.toString());
      }
    }

    return this.http.get<PaginatedResponseDTO>(this.apiUrl, { params }).pipe(
      catchError(this.handleError)
    );
  }


  getById(id: number): Observable<Producto> {
    return this.http.get<Producto>(`${this.apiUrl}/${id}`).pipe(
      catchError(this.handleError)
    );
  }

  create(producto: ProductoCreateDTO): Observable<BaseResponseDTO> {
    return this.http.post<BaseResponseDTO>(this.apiUrl, producto).pipe(
      catchError(this.handleError)
    );
  }


  update(producto: ProductoUpdateDTO): Observable<BaseResponseDTO> {
    return this.http.put<BaseResponseDTO>(this.apiUrl, producto).pipe(
      catchError(this.handleError)
    );
  }


  delete(id: number): Observable<BaseResponseDTO> {
    return this.http.delete<BaseResponseDTO>(`${this.apiUrl}/${id}`).pipe(
      catchError(this.handleError)
    );
  }


  private handleError(error: any) {
    let errorMessage = 'Ha ocurrido un error';

    if (error.error instanceof ErrorEvent) {
       errorMessage = `Error: ${error.error.message}`;
    } else {
       errorMessage = error.error?.mensaje || `Error del servidor: ${error.status}`;
    }

    console.error('ProductoService Error:', errorMessage);
    return throwError(() => new Error(errorMessage));
  }
}
