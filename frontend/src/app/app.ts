import { Component, signal } from '@angular/core';
import { RecordListComponent } from './record-list/record-list';


@Component({
  selector: 'app-root',
  imports: [RecordListComponent],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected readonly title = signal('frontend');
}
