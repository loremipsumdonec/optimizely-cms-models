import { Route, Switch } from 'react-router-dom';
import { RouteHandler } from './Components/RouteHandler';
import { useSelector } from 'react-redux';
import { selectContent } from './Components/ApplicationState';
import "@fontsource/material-icons";

const getLocation = (content) => {
	  let location = '';

    if (content?.url) {
      location = content?.url;
    } else if (typeof window !== 'undefined') {
      location = window.location.pathname;
    }

	return location;
}

function App({Router}) {

  const content = useSelector(selectContent);
  const location = getLocation(content);

  return (
      <Router location={location}>
        <Switch>
          <Route key="/:route*" path="/:route*">
            <RouteHandler/>
          </Route>
        </Switch>
      </Router>
  );
}

export default App;
