// role-assign.component.ts
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { RoleService } from 'src/app/services/Role.service';

@Component({
  selector: 'app-role-assign',
  templateUrl: './role-assign.component.html',
  styleUrls: ['./role-assign.component.css']
})
export class RoleAssignComponent implements OnInit {
  users: any[] = [];
  roles: any[] = [];
  selectedUser: string = '';
  selectedRole: string = '';

  constructor(private roleService: RoleService, private router: Router) { }

  ngOnInit(): void {
    this.roleService.getUsers().subscribe(data => {
      this.users = data;
    });

    this.roleService.getRoles().subscribe(data => {
      this.roles = data;
    });
  }

  assignRole(): void {
    this.roleService.assignRole(this.selectedUser, this.selectedRole).subscribe(() => {
      alert('Role assigned successfully');
      this.router.navigate(['/admin/role-list']);

    });
  }

  removeRole(): void {
    this.roleService.removeRole(this.selectedUser, this.selectedRole).subscribe(() => {
      alert('Role removed successfully');
      this.router.navigate(['/admin/role-list']);

    });
  }
}
