import { ArticlePage as Component } from './ArticlePage';
import { gql } from '@apollo/client';
import { useGetArticlePageQuery } from '../../generated/graphql';

export const GET_ARTICLE_PAGE = gql`
    query getArticlePage($id:Int!) {
        articlePage(id: $id) {
            heading,
            preamble,
            text
        }
    }
`;

interface Props {
    pageId:number
}

const ArticlePage: React.FC<Props> = ({pageId}) => {

    const { data, error, loading } = useGetArticlePageQuery({variables: { id:pageId}});

    if (loading) {
        return <h1>Loading...</h1>
    }

    return (
        <Component pageId={pageId} {...data!.articlePage!} />
    );
}

export default ArticlePage;