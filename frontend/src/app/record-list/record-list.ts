import { AsyncPipe } from '@angular/common';
import { Component } from '@angular/core';
import { RecordService } from '../services/record.service';

@Component({
  selector: 'app-record-list',
  imports: [AsyncPipe],
  templateUrl: './record-list.html',
  styleUrl: './record-list.css',
})
export class RecordListComponent {
  records$;

  constructor(private recordService: RecordService) {
    this.records$ = this.recordService.getAll();
  }
}