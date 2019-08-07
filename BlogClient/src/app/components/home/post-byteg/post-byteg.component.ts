import { Component, OnInit } from '@angular/core';
import { PostService } from '../../../shared/post.service';
import { Post } from '../../../model/post/post';
import { Router } from '@angular/router';

@Component({
  selector: 'app-post-byteg',
  templateUrl: './post-byteg.component.html',
  styleUrls: ['./post-byteg.component.css']
})
export class PostBytegComponent implements OnInit {

  constructor(private postService: PostService, private router: Router) { }

  ngOnInit() {
  }

  populateForm(pd: Post) {       // метод обновляет данные во временном обьекте (postService.post) типа Post на основании обьекта выделенного из списка pd в представлении
    this.postService.post = Object.assign({}, pd);   // Object.assign - предотвращает корректировку полей в
    this.postService.tempPostId = pd.Id;
    this.router.navigate(['/read-post']);
  } 

}
