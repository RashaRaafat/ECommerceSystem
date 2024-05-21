// role-list.component.ts
import { Component, OnInit } from '@angular/core';
import { RoleService } from 'src/app/services/Role.service';

@Component({
  selector: 'app-role-list',
  templateUrl: './role-list.component.html',
  styleUrls: ['./role-list.component.css']
})
export class RoleListComponent implements OnInit {
  roles: any[] = [];

  constructor(private roleService: RoleService) { }

  ngOnInit(): void {
    this.roleService.getRoles().subscribe(data => {
      this.roles = data;
    });
  }
}
