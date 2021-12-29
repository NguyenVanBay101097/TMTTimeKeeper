import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { AppService } from '../app.service';

@Component({
  selector: 'app-time-keeper-create-dialog',
  templateUrl: './time-keeper-create-dialog.component.html',
  styleUrls: ['./time-keeper-create-dialog.component.css']
})
export class TimeKeeperCreateDialogComponent implements OnInit {
  title: string;
  form: FormGroup;
  submitted = false;
  id: string;
  constructor(
    public activeModal: NgbActiveModal,
    private fb: FormBuilder,
    private service: AppService
  ) { }

  ngOnInit() {
    this.form = this.fb.group({
      name: [null, Validators.required],
      model: null,
      ipAddress: [null, Validators.required],
      tcpPort: [null, Validators.required],
      seriNumber: null
    })

    if (this.id) {
      this.getValueFormById(this.id);
    }
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

  getValueFormById(id) {
    this.service.getTimeKeeperById(id).subscribe(result => {
      this.form.patchValue(result);
    })
  }

}
