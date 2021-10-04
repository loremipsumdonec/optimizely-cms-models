import { Articles as Component } from './Articles';
import { gql } from '@apollo/client';
import { useGetArticlesQuery } from '../../generated/graphql';

export const GET_ARTICLES = gql`
    query getArticles($parentId:Int!) {
        articles(parentId: $parentId) {
            url,
            heading,
            preamble
        }
    }
`;

interface Props {
    pageId:number
}

const Articles: React.FC<Props> = ({pageId}) => {

    const { data, error, loading } = useGetArticlesQuery({variables: { parentId:pageId}});

    if (loading) {
        return <h1>Loading...</h1>
    }

    return (
        <Component articles={data!.articles} />
    );
}

export default Articles;