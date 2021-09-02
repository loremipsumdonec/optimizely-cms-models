import { useEffect } from 'react';
import { useParams } from 'react-router-dom';
import { useDispatch, useSelector } from 'react-redux';
import { selectApiUrl, selectContent, selectModel } from '../ApplicationState';
import { getModelStart, getModelSuccess, getModelFailed } from '../ApplicationState';
import Spinner from '../Spinner/Spinner';
import ContentFactory from '../ContentFactory';
import Alert from '../Alert/Alert';

const suppressFirstLoadWhenHydrated = () => {

  if(typeof window !== 'undefined' && window.__model) {
    delete window.__model;
    return true
  }

  return false;

}

export function RouteHandler({getModel}) {

    const dispatch = useDispatch();
    const apiUrl = useSelector(selectApiUrl);
    const { loading, error } = useSelector(selectModel);
    const content = useSelector(selectContent);
    let { route } = useParams()

    useEffect(() => {
      async function fetchData() {

        dispatch(getModelStart())

        try {
          const model = await getModel(apiUrl, route);
          dispatch(getModelSuccess(model))
        }catch(ex) {
          dispatch(getModelFailed(ex.message));
        }
      }

      if(suppressFirstLoadWhenHydrated()) {
        return;
      }

      fetchData();

    }, [apiUrl, route, getModel, dispatch]);

    return(
      <>
        {loading &&
          <Spinner /> 
        }
        
        {error && 
              <Alert 
                type="error" 
                heading="Failed load data" 
                message={error}/>
        }

        {(content && !error) &&
          <ContentFactory content={content}/>
        }
      </>
    );
}
  