import { BacklogItem } from "./backog-item"

export class Description {
   descriptionId: string
   descriptionText: string
   backlogItemId: string
   backlogItem: BacklogItem
}

export class DescriptionCreation {
    descriptionText: string
     backlogItemId: string

   constructor(descriptionText, backlogItemId ){
    this.descriptionText=descriptionText;
    this.backlogItemId =backlogItemId
   }
}

export class DescriptionUpdate {
    descriptionId: string
    descriptionText: string

    constructor(descriptionId, descriptionText ){
      this.descriptionId =descriptionId
      this.descriptionText=descriptionText;
       }

 }