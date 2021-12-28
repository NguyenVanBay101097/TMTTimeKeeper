import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { AppService } from '../app.service';
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
    this.service.getTimeKeepers().subscribe(result => {
      console.log(result);
    })
  }

  addTimeKeeper() {
    const modalRef = this.modalService.open(TimeKeeperCreateDialogComponent, {backdrop: 'static', keyboard: false});
    modalRef.componentInstance.title = 'Thêm mới máy chấm công';
    modalRef.result.then(result => {
      this.service.connect(result).subscribe(() => {
        alert('Đã kết nối máy chấm công');
        this.timeKeepers.push(result);
        this.isConnect = true;
      }, () => {
        alert('Kết nối thất bại');
        this.isConnect = false;
      })
      
    })
  }

}
