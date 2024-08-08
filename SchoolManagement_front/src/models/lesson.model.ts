import { Subject } from "./subject.model";

export interface Lesson {
  id: number;
  subjectId?: number;
  subject?: Subject;
}