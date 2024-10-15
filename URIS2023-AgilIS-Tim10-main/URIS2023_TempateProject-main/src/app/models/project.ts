export class Project{
    projekatID: string
    nazivProjekta: string
    opisProjekta: string
    datumProjekta: string
    timID: string

    constructor(projekatId, naziv, opis, datum, timId) {
        this.projekatID=projekatId;
        this.nazivProjekta=naziv;
        this.opisProjekta=opis;
        this.datumProjekta=datum;
        this.timID=timId
        
    }
}