import { NotificationList } from './components/NotificationList';
import { GlobalStyle } from '../src/styles/global';
import { SignalProvider } from './hook/signalr';

function App() {

  return (
    <SignalProvider>
      <h1>Notifications:</h1>
      <NotificationList />
      <GlobalStyle />
    </SignalProvider>
  );
}

export default App;
