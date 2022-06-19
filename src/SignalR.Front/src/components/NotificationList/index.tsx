
import { List, ListItem, ListItemIcon, ListItemText } from "@mui/material";
import { useEffect, useState } from "react";
import { useSignalR } from "../../hook/signalr";
import StarIcon from '@material-ui/icons/Star';
import Snackbar from '@material-ui/core/Snackbar';

export const NotificationList: React.FC = () => {

    const signalR = useSignalR();
    const [messages, setMessages] = useState<string[]>([]);
    const [open, setOpen] = useState(false);
    const [currentNotification, setCurrentNotification] = useState("");

    useEffect(() => {
        if (signalR.Connection == null) return;

        signalR.Connection.Subcribe("SendNewNotification", (message) => {
            setMessages(messageState => [...messageState, message]);
            setCurrentNotification(message);
            setOpen(true);
        });
    }, [signalR]);

    return (
        <>
            <List>
                {messages.map((message, key) => (
                    <ListItem key={`${key}-${message}`}>
                        <ListItemIcon>
                            <StarIcon />
                        </ListItemIcon>
                        <ListItemText> {message} </ListItemText>
                    </ListItem>
                ))}
            </List>
            <Snackbar 
                anchorOrigin={{ vertical: 'top', horizontal: 'right' }} 
                message={currentNotification} 
                open={open} 
                autoHideDuration={6000} 
                onClose={() => setOpen(false)} 
            />
        </>
    );
}

