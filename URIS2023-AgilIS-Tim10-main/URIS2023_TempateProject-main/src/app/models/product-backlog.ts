export class ProductBacklog{
    idBacklog: string
    opisZadatak: string
    vremeTrajanja: string
    angazovaniRadnici: string
    opis: string
    naslov: string
    projekatId: string
    korisnikId:string

    
    constructor(idBacklog: string, opisZadatak: string, vremeTrajanja: string, angazovaniRadnici: string, opis: string, naslov: string, projekatId:string, korisnikId:string) {
        this.idBacklog = idBacklog;
        this.opisZadatak = opisZadatak;
        this.vremeTrajanja = vremeTrajanja;
        this.angazovaniRadnici = angazovaniRadnici;
        this.opis = opis;
        this.naslov = naslov;
        this.projekatId=projekatId;
        this.korisnikId=korisnikId
    }
}

export class ProductBacklogCreation{
    opisZadatak: string
    vremeTrajanja: string
    angazovaniRadnici: string
    opis: string
    naslov: string
    projekatId: string
    korisnikId:string

    
    constructor( opisZadatak: string, vremeTrajanja: string, angazovaniRadnici: string, opis: string, naslov: string,projekatId:string, korisnikId:string) {
        this.opisZadatak = opisZadatak;
        this.vremeTrajanja = vremeTrajanja;
        this.angazovaniRadnici = angazovaniRadnici;
        this.opis = opis;
        this.naslov = naslov;
        this.projekatId=projekatId;
        this.korisnikId=korisnikId
    }
}