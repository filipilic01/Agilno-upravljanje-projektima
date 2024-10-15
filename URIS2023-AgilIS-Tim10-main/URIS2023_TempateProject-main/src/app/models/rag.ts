export class Rag{
    ragId: string
    ragValue: string
    backlogItemId: string
}


export class RagCreation {
    ragValue: string
    backlogItemId: string
 
    constructor(ragValue,backlogItemId ){
     this.ragValue=ragValue;
     this.backlogItemId =backlogItemId
    }
 }
 
 export class RagUpdate {
    ragId: string
     ragValue: string
 
     constructor(ragId, ragValue ){
       this.ragId =ragId;
       this.ragValue=ragValue;
        }

    }