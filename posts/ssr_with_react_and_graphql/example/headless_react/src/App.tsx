import { Switch, Route } from 'react-router';
import RouteHandler from './components/RouteHandler'
import { ApolloProvider } from '@apollo/client';

interface Model {
  content: {
    url?:string
  }
}

interface Props {
  client:any,
  Router:any,
  model: Model
}

const getLocation = (model:Model) => {
    let location = '';

    if (model.content.url) {
      location = model.content.url;
    } else if (typeof window !== 'undefined') {
      location = window.location.pathname;
    }

    return location;
}

const App: React.FC<Props> = ({client, Router, model}) => {

  const location = getLocation(model);

  return (
    <ApolloProvider client={client}>
      <Router location={location}>
        <Switch>
            <Route key="/:route*" path="/:route*">
              <RouteHandler/>
            </Route>
        </Switch>
      </Router>
    </ApolloProvider>
  );
}

export default App;
