// Predstavlja model osobe koja radi na projektu - recimo kao kada na Github-u share-ujemo projekat medjusobno.
export class Contributor {
    id: number; // ID nam je ovde bitan cisto kao identifikaciono obelezje, ali ga u krajnjoj tabeli necemo prikazivati.
    firstName: string;
    lastName: string;
    userName: string;
}