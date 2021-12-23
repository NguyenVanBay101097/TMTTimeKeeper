import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-time-keeper-create-dialog',
  templateUrl: './time-keeper-create-dialog.component.html',
  styleUrls: ['./time-keeper-create-dialog.component.css']
})
export class TimeKeeperCreateDialogComponent implements OnInit {
  title: string;
  form: FormGroup;
  submitted = false;
  constructor(
    public activeModal: NgbActiveModal,
    private fb: FormBuilder
  ) { }

  ngOnInit() {
    this.form = this.fb.group({
      ipAddress: [null, Validators.required],
      tcpPort: [null, Validators.required]
    })
  }

  onSave() {
    this.submitted = true;
    if (this.form.invalid) {
      return;
    }

    let formValue = this.form.value;
    this.activeModal.close(formValue);
  }

  get f() {
    return this.form.controls;
  }

}
