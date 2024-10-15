import { Contributor } from "./contributor";

export class Task {
    id: number; // ID task-a, jako bitno za pracenje kroz vreme. Task nikako ne sme da se izgubi.
    title: string;
    description: string;
    assignee: Contributor; // Naziv osobe koja je preuzela da radi na task-u.
}