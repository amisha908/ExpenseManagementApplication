import { Component } from '@angular/core';
import { GroupService } from '../../services/group.service';
import { Group } from '../../models/group.model';

@Component({
  selector: 'app-all-groups',
  templateUrl: './all-groups.component.html',
  styleUrl: './all-groups.component.css'
})
export class AllGroupsComponent {
groups:Group[]=[];
  constructor(private groupService:GroupService)
  {

  }
  ngOnInit(): void {
    this.groupService.getAllGroup().subscribe({
      next:(groups)=>{
        this.groups=groups
        console.log(groups)
      }
    })
  }

}
