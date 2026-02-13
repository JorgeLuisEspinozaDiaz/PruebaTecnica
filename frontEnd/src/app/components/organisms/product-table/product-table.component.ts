import { Component, Input, Output, EventEmitter, ChangeDetectionStrategy, ChangeDetectorRef } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';
import { SpinnerComponent } from '../../atoms/spinner/spinner.component';
import { Producto } from '../../../core/models';

@Component({
  selector: 'app-product-table',
  standalone: true,
  imports: [CommonModule, SpinnerComponent, DatePipe],
  templateUrl: './product-table.component.html',
  styleUrls: ['./product-table.component.scss'],
  changeDetection: ChangeDetectionStrategy.Default
})
export class ProductTableComponent {
  private _productos: Producto[] = [];

  @Input()
  set productos(value: Producto[]) {
    this._productos = value || [];
    this.cdr.markForCheck();
  }
  get productos(): Producto[] {
    return this._productos;
  }

  @Input() loading: boolean = false;

  @Output() edit = new EventEmitter<Producto>();
  @Output() delete = new EventEmitter<Producto>();
  @Output() view = new EventEmitter<Producto>();

  constructor(private cdr: ChangeDetectorRef) {}

  onEdit(producto: Producto): void {
    this.edit.emit(producto);
  }

  onDelete(producto: Producto): void {
    this.delete.emit(producto);
  }

  onView(producto: Producto): void {
    this.view.emit(producto);
  }

  formatPrice(price: number): string {
    return new Intl.NumberFormat('es-CO', {
      style: 'currency',
      currency: 'COP',
      minimumFractionDigits: 0
    }).format(price);
  }

  trackByProducto(index: number, producto: Producto): number {
    return producto.id;
  }
}
