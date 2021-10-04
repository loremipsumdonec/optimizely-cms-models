import Breadcrumbs from '../Breadcrumbs';

interface Props {
    pageId:number,
    heading:string,
    preamble:string,
    text:string
}

export const ArticlePage:React.FC<Props> = ({pageId, heading, preamble, text}) => {
    return (
        <div>
            <Breadcrumbs pageId={pageId}/>

            <h1>{heading}</h1>
            <p>{preamble}</p>
            <div dangerouslySetInnerHTML={{__html: text}}></div>
        </div>
    );
}

export default ArticlePage;