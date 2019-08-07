import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ReactiveFormsModule } from "@angular/forms";
import {QuillModule} from 'ngx-quill';
import { RouterModule } from '@angular/router'; 

import { AppComponent } from './app.component';
import { NavMenuComponent } from './components/nav-menu/nav-menu.component'; 
import { LoginComponent } from './components/login/login.component'; 
import { RegisterComponent } from './components/register/register.component'; 
import { UserService } from './shared/user.service'; 
import { ToastrModule } from 'ngx-toastr';
import { ProfileComponent } from './components/profile/profile.component';
import { AuthGuard } from './auth.guard';

import { BlogEditorComponent } from './components/blog-editor/blog-editor.component';
import { HomeComponent } from './components/home/home.component';
import { PostService } from './shared/post.service';
import { ReadPostComponent } from './components/home/read-post/read-post.component';
import { appRoutes } from './route';
import  {UserPostComponent}  from './components/profile/user-post/user-post.component';
import {CommentService} from './shared/comment.service';
import  {AdminService}  from './shared/admin.service';
import { AdminComponent } from './components/admin/admin.component';
import { UserListComponent } from './components/admin/user-list/user-list.component';
import { AdminCommandsComponent } from './components/admin/admin-commands/admin-commands.component';
import {ModeratorService} from './shared/moderator.service';
import {ModeratorComponent} from './components/moderator/moderator.component';
import {PostListComponent} from './components/moderator/post-list/post-list.component';
import { PostBytegComponent } from './components/home/post-byteg/post-byteg.component';
import { EditProfileComponent } from './components/profile/edit-profile/edit-profile.component'; 

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    LoginComponent,
    RegisterComponent,
    ProfileComponent,
    BlogEditorComponent,
    HomeComponent,
    ReadPostComponent,
    UserPostComponent,
    AdminComponent,
    UserListComponent,
    AdminCommandsComponent,
    ModeratorComponent,   
    PostListComponent, PostBytegComponent, EditProfileComponent
   
  ],
  exports: [RouterModule],
  imports: [
    BrowserModule,
    //AppRoutingModule,   
    FormsModule,
    BrowserAnimationsModule,
    ReactiveFormsModule,
    HttpClientModule,
    ToastrModule.forRoot(),
    QuillModule.forRoot(),
    RouterModule.forRoot(appRoutes),
   
   
  ],
  providers: [UserService,AuthGuard,PostService,CommentService, AdminService, ModeratorService],
  bootstrap: [AppComponent]
})
export class AppModule { }
