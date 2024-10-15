export class Status{
    idStatusa: string
    vrednostStatusa: string
    backlogItemId: string
}


export class StatusCreation {
    vrednostStatusa: string
    backlogItemId: string
 
    constructor(vrednostStatusa,backlogItemId ){
     this.vrednostStatusa=vrednostStatusa;
     this.backlogItemId =backlogItemId
    }
 }
 
 export class StatusUpdate {
    idStatusa: string
    vrednostStatusa: string
 
     constructor(idStatusa, vrednostStatusa ){
       this.idStatusa =idStatusa
       this.vrednostStatusa=vrednostStatusa;
        }
    }