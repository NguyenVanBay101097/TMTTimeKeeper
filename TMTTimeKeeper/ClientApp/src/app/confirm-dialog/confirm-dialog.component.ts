import { Component, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-confirm-dialog',
  templateUrl: './confirm-dialog.component.html',
  styleUrls: ['./confirm-dialog.component.css']
})
export class ConfirmDialogComponent implements OnInit {
  title: string = 'Xóa máy chấm công';
  body: string = 'Bạn có muốn xóa máy chấm công không ?';
  body2: string;
  confirmText = 'Xác nhận';
  closeText = 'Đóng';
  closeClass = '';

  constructor(
    public activeModal: NgbActiveModal,

  ) { }

  ngOnInit() {
  }

  onConfirm() {
    this.activeModal.close(true);
  }

  onCancel() {
    this.activeModal.dismiss();
  }

}
