import { BacklogItem } from "./backog-item"

export class AcceptanceCriteria {
   acceptanceCriteriaId: string
   acceptanceCriteriaText: string
   backlogItemId: string
   backlogItem: BacklogItem
}

export class AcceptanceCriteriaCreation {
   acceptanceCriteriaText: string
   backlogItemId: string

   constructor(acceptanceCriteriaText,backlogItemId ){
    this.acceptanceCriteriaText=acceptanceCriteriaText;
    this.backlogItemId =backlogItemId
   }
}

export class AcceptanceCriteriaUpdate {
    acceptanceCriteriaId: string
    acceptanceCriteriaText: string

    constructor(acceptanceCriteriaId, acceptanceCriteriaText ){
      this.acceptanceCriteriaId =acceptanceCriteriaId
      this.acceptanceCriteriaText=acceptanceCriteriaText;
       }

 }