import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ButtonComponent } from '../../atoms/button/button.component';
import { InputComponent } from '../../atoms/input/input.component';
import { ProductoFiltro } from '../../../core/models';

@Component({
  selector: 'app-search-bar',
  standalone: true,
  imports: [CommonModule, FormsModule, ButtonComponent, InputComponent],
  templateUrl: './search-bar.component.html',
  styleUrls: ['./search-bar.component.scss']
})
export class SearchBarComponent {
  @Input() loading: boolean = false;
  @Output() search = new EventEmitter<ProductoFiltro>();
  @Output() clear = new EventEmitter<void>();

  searchTerm: string = '';
  searchId: string = '';

  onSearch(): void {
    const filtro: ProductoFiltro = {
      search: this.searchTerm || undefined,
      id: this.searchId ? parseInt(this.searchId, 10) : undefined
    };
    this.search.emit(filtro);
  }

  onClear(): void {
    this.searchTerm = '';
    this.searchId = '';
    this.clear.emit();
  }

  onEnter(): void {
    this.onSearch();
  }
}
