// role-create.component.ts
import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { RoleService } from 'src/app/services/Role.service';

@Component({
  selector: 'app-role-create',
  templateUrl: './role-create.component.html',
  styleUrls: ['./role-create.component.css']
})
export class RoleCreateComponent {
  roleName: string = '';

  constructor(private roleService: RoleService, private router: Router) { }

  createRole(): void {
    this.roleService.createRole(this.roleName).subscribe(() => {
      this.router.navigate(['/admin/role-list']);
    });
  }
}
