
export class BacklogItem {
    backlogItemId: string
    backlogItemName: string
    userId: string
    backlogId: string

    constructor(backlogItemId: string, backlogItemName: string, userId: string, idBacklog: string) {
        this.backlogItemId = backlogItemId;
        this.backlogItemName = backlogItemName;
        this.userId = userId;
        this.backlogId = idBacklog;
    }
}

export class BacklogItemDto {
    backlogItemName: string
    userId: string
    backlogId: string

    constructor(backlogItemName: string, userId: string, backlogId: string) {
        this.backlogItemName = backlogItemName;
        this.userId = userId;
        this.backlogId = backlogId;
    }
}

    
