import * as signalR from "@microsoft/signalr";

export class SignalRService {

    public CreateSignalRConnection(url: string): SignalRConnectionService {
        const hubConnection = new signalR.HubConnectionBuilder()
            .withUrl(url)
            .withAutomaticReconnect()
            .configureLogging(signalR.LogLevel.Information)
            .build();

        return new SignalRConnectionService(hubConnection);
    }
};

export class SignalRConnectionService {

    private hubConnection: signalR.HubConnection;

    constructor(hubConnection: signalR.HubConnection) {
        this.hubConnection = hubConnection;
    }

    public Subcribe(method: string, callBack: SignalRServiceCallBack): void {

        if (this.hubConnection.state === signalR.HubConnectionState.Connected) {
            this.hubConnection.on(method, callBack);
            return;
        }

        this.hubConnection.start().then(() => {
            this.hubConnection.on(method, callBack);
        })
    }
}

interface SignalRServiceCallBack {
    (...args: any[]): void
}




