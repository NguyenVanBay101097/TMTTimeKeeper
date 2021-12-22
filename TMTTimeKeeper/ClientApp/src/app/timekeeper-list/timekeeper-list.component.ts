import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AppService } from '../app.service';

@Component({
  selector: 'app-timekeeper-list',
  templateUrl: './timekeeper-list.component.html',
  styleUrls: ['./timekeeper-list.component.css']
})
export class TimekeeperListComponent implements OnInit {
  isConnect = false;
  timeKeepers = [
    {
      name: '',
      model: '',
      ipAddress: '172.29.7.201',
      tcpPort: '4370',
      company: '',
      state: '',
      isConnect: false
    }
  ]
  constructor(
    private service: AppService,
    private router: Router,
    private route: ActivatedRoute
  ) { }

  ngOnInit() {
    
  }

  connect(item) {
    if (item.ipAddress && item.tcpPort) {
      let val = {ipAddress: item.ipAddress, tcpPort: item.tcpPort};
      this.service.connect(val).subscribe(result => {
        item.isConnect = result;
        
      })
    }
  }

}
