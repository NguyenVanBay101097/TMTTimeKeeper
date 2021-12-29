import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { AppService } from '../app.service';
import { ConfirmDialogComponent } from '../confirm-dialog/confirm-dialog.component';
import { TimeKeeperCreateDialogComponent } from '../time-keeper-create-dialog/time-keeper-create-dialog.component';

@Component({
  selector: 'app-timekeeper-list',
  templateUrl: './timekeeper-list.component.html',
  styleUrls: ['./timekeeper-list.component.css']
})
export class TimekeeperListComponent implements OnInit {
  isConnect = false;
  timeKeepers = [
    // {
    //   name: '',
    //   model: '',
    //   ipAddress: '172.29.7.201',
    //   tcpPort: '4370',
    //   company: '',
    //   state: '',
    //   isConnect: false
    // }
  ]
  
  constructor(
    private service: AppService,
    private modalService: NgbModal
  ) { }

  ngOnInit() {
    this.getTimeKeepers();
  }

  connect(item) {
  }

  getTimeKeepers() {
    this.service.getTimeKeepers().subscribe((result: any) => {
      this.timeKeepers = result;
    })
  }

  addTimeKeeper() {
    const modalRef = this.modalService.open(TimeKeeperCreateDialogComponent, {backdrop: 'static', keyboard: false});
    modalRef.componentInstance.title = 'Thêm mới máy chấm công';
    modalRef.result.then(result => {
      this.service.connect(result).subscribe(() => {
        alert('Đã kết nối máy chấm công');
        this.getTimeKeepers();
        this.isConnect = true;
      }, () => {
        alert('Kết nối thất bại');
        this.isConnect = false;
      })
      
    })
  }

  update(id) {
    const modalRef = this.modalService.open(TimeKeeperCreateDialogComponent, {backdrop: 'static', keyboard: false});
    modalRef.componentInstance.title = 'Cập nhật máy chấm công';
    modalRef.componentInstance.id = id;

    modalRef.result.then(result => {
      this.service.update(id,result).subscribe(() => {
        alert('Lưu thành công');
        this.getTimeKeepers();
      }, () => {
        alert('Không thể lưu');
      })
      
    })
  }

  delete(id) {
    const modalRef = this.modalService.open(ConfirmDialogComponent, {backdrop: 'static', keyboard: false});
    modalRef.result.then(result => {
      this.service.delete(id).subscribe(res => {
        alert('Đã xóa máy chấm công');
        this.getTimeKeepers();
      },error => {
        alert('Không thể xóa máy chấm công');
      })
      
    })
  }

}
