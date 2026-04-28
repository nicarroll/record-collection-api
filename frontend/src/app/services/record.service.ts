import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Record } from '../models/record';

@Injectable({
  providedIn: 'root',
})
export class RecordService {
  private apiUrl = 'http://localhost:5093/api/recordcollection';
  
  constructor(private http: HttpClient) {}

  getAll(): Observable<Record[]> {
    return this.http.get<Record[]>(this.apiUrl);
  }
}
