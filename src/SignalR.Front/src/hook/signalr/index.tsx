import React from "react";
import { SignalRConnectionService, SignalRService } from "../../services/signalr";


interface SignalRContextInterfcae {
    Connection: SignalRConnectionService | null,
}

const SignalRContext = React.createContext<SignalRContextInterfcae | null>(null);

type Props = {
    children: React.ReactNode;
};

const SignalProvider: React.FC<Props> = ({ children }) => {

    const [hubConnection, setHubConnection] = React.useState<SignalRConnectionService | null>(null);

    React.useEffect(() => {
        setHubConnection(new SignalRService().CreateSignalRConnection(process.env.REACT_APP_API_PATH as string))
    }, [])

    return (<SignalRContext.Provider value={{ Connection: hubConnection }}>{children} </SignalRContext.Provider>)

}


const useSignalR = () => {
    var context = React.useContext(SignalRContext);
    if (!context) throw new Error();

    return context;
};

export {
    SignalProvider,
    useSignalR
}