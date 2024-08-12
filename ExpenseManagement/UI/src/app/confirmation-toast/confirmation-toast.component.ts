import { Component } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-confirmation-toast',
  templateUrl: './confirmation-toast.component.html',
  styleUrls: ['./confirmation-toast.component.css']
})
export class ConfirmationToastComponent {
  public message!: string;
  public title!: string;
  public onConfirm!: () => void;
  public onCancel!: () => void;

  constructor(private toastr: ToastrService) { }

  confirm(): void {
    this.onConfirm();
    this.toastr.clear();
  }

  cancel(): void {
    this.onCancel();
    this.toastr.clear();
  }
}
