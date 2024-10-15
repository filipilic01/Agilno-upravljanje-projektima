import { BacklogItem } from "./backog-item"

export class StoryPoint {
   storyPointId: string
   storyPointValue: number
   backlogItemId: string
   backlogItem: BacklogItem
}

export class StoryPointCreation {
    storyPointValue: number
     backlogItemId: string

   constructor(storyPointValue, backlogItemId ){
    this.storyPointValue=storyPointValue;
    this.backlogItemId =backlogItemId
   }
}

export class StoryPointUpdate {
    storyPointId: string
    storyPointValue: number

    constructor(storyPointId, storyPointValue ){
      this.storyPointId =storyPointId
      this.storyPointValue=storyPointValue;
       }

 }