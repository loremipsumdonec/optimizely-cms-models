import StartPage from '../StartPage';
import ArticlePage from '../ArticlePage';
import React from "react"

interface Props {
    pageId:number,
    modelType:string
}

const ContentFactory: React.FC<Props> = ({pageId, modelType}) => {
    
    switch(modelType) {
        case 'StartPage':
            return <StartPage pageId={pageId} />
        case 'ArticlePage':
            return <ArticlePage pageId={pageId} />
        default:
            return <div>{`Could not find content component with modelType "${modelType}", add it in the ContentFactory component`}</div>
    }
}

export default ContentFactory