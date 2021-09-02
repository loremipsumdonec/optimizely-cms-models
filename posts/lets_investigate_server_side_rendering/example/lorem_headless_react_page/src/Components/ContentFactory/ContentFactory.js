import Alert from '../Alert';
import StartPage from '../StartPage';
import ArticlePage from '../ArticlePage';

function ContentFactory({content}) {

    switch(content.modelType) {
        case "StartPage":
            return <StartPage {...content} />
        case "ArticlePage":
            return <ArticlePage {...content} />
        default:
        return <Alert 
            type="error" 
            heading="Could not find content component" 
            message={`Could not find content component with modelType "${content.modelType}", add it in the ContentFactory component`}/>
    }
}

export default ContentFactory;