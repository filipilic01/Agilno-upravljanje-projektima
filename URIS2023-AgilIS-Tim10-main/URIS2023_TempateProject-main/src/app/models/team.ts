export class Team{
    timID: string
    nazivTima: string
    brojClanova: string
    userId: string

    constructor(timID, nazivTima, brojClanova, userId){
        this.timID=timID;
        this.nazivTima=nazivTima;
        this.brojClanova;
        this.userId=userId

    }
}

    export class TeamMember{
        clanTimaId: string
        userName: string
        timId: string

        constructor(clanTimaId, userName, timId){
            this.clanTimaId=clanTimaId;
            this.userName=userName;
            this.timId=timId
    
        }
    }
