import Articles from '../Articles';

interface Props {
    pageId:number,
    heading:string,
    preamble:string
}

export const StartPage:React.FC<Props> = ({pageId, heading, preamble}) => {
    return (
        <div>
            <h1>{heading}</h1>
            <p>{preamble}</p>
            <Articles pageId={pageId}/>
        </div>
    );
}

export default StartPage;