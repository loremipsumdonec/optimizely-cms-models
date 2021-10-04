import { Link } from 'react-router-dom';

interface Article {
    url:string,
    heading:string,
    preamble:string
}

interface Props {
    articles: Article[]
}

export const Articles:React.FC<Props> = ({articles}) => {
    return (
        <div>
            {articles.map((article) =>
                <Link to={article.url}> 
                    <div>
                        <h2>{article.heading}</h2>
                        <p>{article.preamble}</p>
                    </div>
                </Link>
            )}
        </div>
    );
}

export default Articles;