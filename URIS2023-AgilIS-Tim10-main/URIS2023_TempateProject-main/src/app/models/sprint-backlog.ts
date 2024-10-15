export class SprintBacklog{
    idBacklog: string
    opis: string
    naslov: string
    korisnikId: string
    projekatId: string
    pocetak: string
    kraj: string
    cilj: string

    constructor(
        idBacklog: string,
        opis: string,
        naslov: string,
        korisnikId: string,
        projekatId: string,
        pocetak: string,
        kraj: string,
        cilj: string
    ) {
        this.idBacklog = idBacklog;
        this.opis = opis;
        this.naslov = naslov;
        this.korisnikId = korisnikId;
        this.projekatId = projekatId;
        this.pocetak = pocetak;
        this.kraj = kraj;
        this.cilj = cilj;
    }
}

export class SprintBacklogCreation{
    cilj: string
    pocetak: string
    kraj: string
    opis: string
    naslov: string
    projekatId: string
    korisnikId: string
    
    constructor(
        cilj: string,
        pocetak: string,
        kraj: string,
        opis: string,
        naslov: string,
        projekatId: string,
        korisnikId: string
    ) {
        this.cilj = cilj;
        this.pocetak = pocetak;
        this.kraj = kraj;
        this.opis = opis;
        this.naslov = naslov;
        this.projekatId = projekatId;
        this.korisnikId = korisnikId;
    }
}