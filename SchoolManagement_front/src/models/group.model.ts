import { Classroom } from "./classroom.model";
import { Lesson } from "./lesson.model";
import { Person } from "./person.model";

export interface Group {
    id: number;
    name?: string;
    classroomId: number;
    classroom?: Classroom;
    students?: Person[];
    lessons?: Lesson[];
}