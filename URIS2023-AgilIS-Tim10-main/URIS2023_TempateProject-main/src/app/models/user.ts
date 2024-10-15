import { v4 as uuidv4 } from 'uuid';
export class User {
    userId: string
    name: string; 
    surname: string; 
    username: string;
    email: string; 
    password: string;
    role: number;
}

export class UserDto {
    name: string; 
    surname: string; 
    username: string;
    email: string; 
    role: number;

    constructor(name, surname, username, email, role){
        this.name=name;
        this.surname=surname;
        this.username = username;
        this.email = email;
        this.role=role;
    }
}