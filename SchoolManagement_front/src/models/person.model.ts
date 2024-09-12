import { Role } from "./role.model";

export interface Person {
    id: number;
    firstName: string;
    lastName: string;
    roles: Role;
}
