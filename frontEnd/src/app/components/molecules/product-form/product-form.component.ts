import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule, FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ButtonComponent } from '../../atoms/button/button.component';
import { InputComponent } from '../../atoms/input/input.component';
import { Producto, ProductoCreateDTO } from '../../../core/models';

@Component({
  selector: 'app-product-form',
  standalone: true,
  imports: [CommonModule, FormsModule, ReactiveFormsModule, ButtonComponent, InputComponent],
  templateUrl: './product-form.component.html',
  styleUrls: ['./product-form.component.scss']
})
export class ProductFormComponent {
  @Input() set producto(value: Producto | null) {
    if (value) {
      this.isEditMode = true;
      this.form.patchValue({
        nombre: value.nombre,
        precio: value.precio
      });
      this.idActual = value.id;
    } else {
      this.resetForm();
    }
  }

  @Input() loading: boolean = false;
  @Output() save = new EventEmitter<ProductoCreateDTO>();
  @Output() cancel = new EventEmitter<void>();

  form: FormGroup;
  isEditMode: boolean = false;
  idActual: number | null = null;

  constructor(private fb: FormBuilder) {
    this.form = this.fb.group({
      nombre: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(100)]],
      precio: [0, [Validators.required, Validators.min(0.01)]]
    });
  }

  get nombreError(): string {
    const control = this.form.get('nombre');
    if (control?.touched && control?.errors) {
      if (control.errors['required']) return 'El nombre es requerido';
      if (control.errors['minlength']) return 'Mínimo 2 caracteres';
      if (control.errors['maxlength']) return 'Máximo 100 caracteres';
    }
    return '';
  }

  get precioError(): string {
    const control = this.form.get('precio');
    if (control?.touched && control?.errors) {
      if (control.errors['required']) return 'El precio es requerido';
      if (control.errors['min']) return 'El precio debe ser mayor a 0';
    }
    return '';
  }

  onSubmit(): void {
    if (this.form.valid) {
      this.save.emit(this.form.value);
    } else {
      this.markAllAsTouched();
    }
  }

  onCancel(): void {
    this.cancel.emit();
    this.resetForm();
  }

  public resetForm(): void {
    this.form.reset({
      nombre: '',
      precio: 0
    });
    this.form.markAsPristine();
    this.form.markAsUntouched();
    this.isEditMode = false;
    this.idActual = null;
  }

  private markAllAsTouched(): void {
    Object.keys(this.form.controls).forEach(key => {
      this.form.get(key)?.markAsTouched();
    });
  }
}
