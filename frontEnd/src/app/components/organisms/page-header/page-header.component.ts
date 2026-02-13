import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ButtonComponent } from '../../atoms/button/button.component';

@Component({
  selector: 'app-page-header',
  standalone: true,
  imports: [CommonModule, ButtonComponent],
  templateUrl: './page-header.component.html',
  styleUrls: ['./page-header.component.scss']
})
export class PageHeaderComponent {
  @Input() title: string = '';
  @Input() subtitle?: string;
  @Input() showAddButton: boolean = true;
  @Input() addButtonText: string = 'Nuevo';
  @Input() addButtonIcon: string = 'fas fa-plus';

  @Output() add = new EventEmitter<void>();

  onAdd(): void {
    this.add.emit();
  }
}
