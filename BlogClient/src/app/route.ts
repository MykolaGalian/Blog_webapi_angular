import { Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { ProfileComponent } from './components/profile/profile.component';
import { AuthGuard } from './auth.guard';
import { BlogEditorComponent } from './components/blog-editor/blog-editor.component';
import { HomeComponent } from './components/home/home.component';
import { ReadPostComponent } from './components/home/read-post/read-post.component';
import  {UserPostComponent}  from './components/profile/user-post/user-post.component';
import { AdminComponent } from './components/admin/admin.component';
import {ModeratorComponent} from './components/moderator/moderator.component';
import { PostBytegComponent } from './components/home/post-byteg/post-byteg.component';
import { EditProfileComponent } from './components/profile/edit-profile/edit-profile.component';




export const appRoutes: Routes = [

    {path:'',redirectTo:'/home',pathMatch:'full'},   
    {path: 'home', component: HomeComponent }, 
    {path: 'read-post', component: ReadPostComponent }, 
    {path: 'registration', component: RegisterComponent }, 
    {path: 'login', component: LoginComponent},
    {path: 'profile', component: ProfileComponent, canActivate:[AuthGuard]},
    {path: 'blog-editor', component: BlogEditorComponent, canActivate:[AuthGuard]}, 
    {path: 'user-post', component: UserPostComponent, canActivate:[AuthGuard]},  //'user-post/:login/:id'
    {path: 'admin', component: AdminComponent, canActivate:[AuthGuard]},  
    {path: 'moder', component: ModeratorComponent, canActivate:[AuthGuard]},
    {path: 'post-byteg', component: PostBytegComponent}, 
    {path: 'profile-edit', component: EditProfileComponent, canActivate:[AuthGuard]},
    
    {path: '**', redirectTo: '/login'}
    

];
