import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-event-details',
  templateUrl: './event-details.component.html',
  styleUrls: ['./event-details.component.css']
})
export class EventDetailsComponent implements OnInit {

  form!: FormGroup;

  get f(): any {
    return this.form.controls;
  }


  constructor(private fb: FormBuilder) { }

  ngOnInit() {
    this.validation();
  }

  public validation(): void {
    this.form = this.fb.group({
      subject: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(50)]],
      city: ['', Validators.required],
      eventDate: ['', [Validators.required]],
      qtyGuests: ['', [Validators.required, Validators.max(1000)]],
      phone: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      imageURL: ['', Validators.required]
  });
  }

  public resetForm(): void{
    this.form.reset();
  }

}
