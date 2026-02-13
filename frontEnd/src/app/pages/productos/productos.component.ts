import { Component, OnInit, ViewChild, ChangeDetectorRef, NgZone } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MainLayoutComponent } from '../../components/templates/main-layout/main-layout.component';
import { PageHeaderComponent } from '../../components/organisms/page-header/page-header.component';
import { SearchBarComponent } from '../../components/molecules/search-bar/search-bar.component';
import { ProductTableComponent } from '../../components/organisms/product-table/product-table.component';
import { ModalComponent } from '../../components/organisms/modal/modal.component';
import { ProductFormComponent } from '../../components/molecules/product-form/product-form.component';
import { ProductoService, AlertService } from '../../core/services';
import { Producto, ProductoCreateDTO, ProductoFiltro, PaginatedResponseDTO } from '../../core/models';

@Component({
  selector: 'app-productos-page',
  standalone: true,
  imports: [
    CommonModule,
    MainLayoutComponent,
    PageHeaderComponent,
    SearchBarComponent,
    ProductTableComponent,
    ModalComponent,
    ProductFormComponent
  ],
  templateUrl: './productos.component.html',
  styleUrls: ['./productos.component.scss']
})
export class ProductosComponent implements OnInit {
  productos: Producto[] = [];
  loading: boolean = false;
  searchLoading: boolean = false;
  formLoading: boolean = false;

  // Paginación
  totalItems: number = 0;
  currentPage: number = 1;
  totalPages: number = 0;
  pageSize: number = 10;
  searchTerm: string = '';
  searchId: number | undefined = undefined;

  isModalOpen: boolean = false;
  modalTitle: string = 'Nuevo Producto';
  selectedProducto: Producto | null = null;

  isDetailModalOpen: boolean = false;
  detailProducto: Producto | null = null;

  @ViewChild(ProductFormComponent) productForm!: ProductFormComponent;

  constructor(
    private productoService: ProductoService,
    private alertService: AlertService,
    private cdr: ChangeDetectorRef,
    private ngZone: NgZone
  ) {}

  ngOnInit(): void {
    this.loadProductos();
  }

  loadProductos(): void {
    this.loading = true;
    const filtro: ProductoFiltro = {
      skip: (this.currentPage - 1) * this.pageSize,
      take: this.pageSize,
      search: this.searchTerm || undefined,
      id: this.searchId
    };

    this.productoService.getAll(filtro).subscribe({
      next: (response: PaginatedResponseDTO) => {
        this.ngZone.run(() => {
          if (response.confirmacion) {
            this.productos = [...(response.data as Producto[])];
            this.totalItems = response.total;
            this.totalPages = response.pages;
            this.currentPage = response.page;
          } else {
            this.alertService.error('Error', response.mensaje);
          }
          this.loading = false;
          this.cdr.detectChanges();
        });
      },
      error: (err) => {
        this.ngZone.run(() => {
          this.alertService.error('Error', err.message || 'No se pudieron cargar los productos');
          this.loading = false;
          this.cdr.detectChanges();
        });
      }
    });
  }

  onSearch(filtro: ProductoFiltro): void {
    this.ngZone.run(() => {
      this.searchTerm = filtro.search || '';
      this.searchId = filtro.id;
      this.currentPage = 1;
      this.cdr.detectChanges();
      this.loadProductos();
    });
  }

  onClearSearch(): void {
    this.ngZone.run(() => {
      this.searchTerm = '';
      this.searchId = undefined;
      this.currentPage = 1;
      this.cdr.detectChanges();
      this.loadProductos();
    });
  }

  onPageChange(page: number): void {
    this.currentPage = page;
    this.loadProductos();
  }

  openCreateModal(): void {
    this.selectedProducto = null;
    this.modalTitle = 'Nuevo Producto';
    this.isModalOpen = true;
  }

  openEditModal(producto: Producto): void {
    this.selectedProducto = producto;
    this.modalTitle = 'Editar Producto';
    this.isModalOpen = true;
  }

  closeModal(): void {
    this.isModalOpen = false;
    this.selectedProducto = null;
    // Resetear el formulario al cerrar el modal
    if (this.productForm) {
      this.productForm.resetForm();
    }
    this.cdr.detectChanges();
  }

  onSaveProducto(data: ProductoCreateDTO): void {
    this.formLoading = true;

    if (this.selectedProducto) {
      // Actualizar
      this.productoService.update({
        id: this.selectedProducto.id,
        ...data
      }).subscribe({
        next: (response) => {
          this.ngZone.run(() => {
            this.formLoading = false;
            if (response.confirmacion) {
              this.closeModal();
              this.loadProductos();
              this.alertService.success('¡Actualizado!', response.mensaje);
            } else {
              this.alertService.error('Error', response.mensaje);
            }
          });
        },
        error: (err) => {
          this.ngZone.run(() => {
            this.formLoading = false;
            this.alertService.error('Error', err.message || 'No se pudo actualizar el producto');
          });
        }
      });
    } else {
      // Crear
      this.productoService.create(data).subscribe({
        next: (response) => {
          this.ngZone.run(() => {
            this.formLoading = false;
            if (response.confirmacion) {
              this.closeModal();
              this.loadProductos();
              this.alertService.success('¡Creado!', response.mensaje);
            } else {
              this.alertService.error('Error', response.mensaje);
            }
          });
        },
        error: (err) => {
          this.ngZone.run(() => {
            this.formLoading = false;
            this.alertService.error('Error', err.message || 'No se pudo crear el producto');
          });
        }
      });
    }
  }

  onDeleteProducto(producto: Producto): void {
    this.alertService.confirm(
      '¿Eliminar producto?',
      `Estás a punto de eliminar "${producto.nombre}". Esta acción no se puede deshacer.`
    ).then((result) => {
      if (result.isConfirmed) {
        this.productoService.delete(producto.id).subscribe({
          next: (response) => {
            if (response.confirmacion) {
              this.alertService.success('¡Eliminado!', response.mensaje);
              this.loadProductos();
            } else {
              this.alertService.error('Error', response.mensaje);
            }
          },
          error: (err) => {
            this.alertService.error('Error', err.message || 'No se pudo eliminar el producto');
          }
        });
      }
    });
  }

  onViewProducto(producto: Producto): void {
    this.detailProducto = producto;
    this.isDetailModalOpen = true;
  }

  closeDetailModal(): void {
    this.isDetailModalOpen = false;
    this.detailProducto = null;
  }

  formatPrice(price: number): string {
    return new Intl.NumberFormat('es-CO', {
      style: 'currency',
      currency: 'COP',
      minimumFractionDigits: 0
    }).format(price);
  }
}
