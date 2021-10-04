import { StartPage as Component } from './StartPage';
import { gql } from '@apollo/client';
import { useGetStartPageQuery } from '../../generated/graphql';

export const GET_START_PAGE = gql`
    query getStartPage($id:Int!) {
        startPage(id: $id) {
            heading,
            preamble
        },
        articles(parentId: $id) {
            url,
            heading,
            preamble
        }
    }
`;

interface Props {
    pageId:number
}

const StartPage: React.FC<Props> = ({pageId}) => {

    const { data, error, loading } = useGetStartPageQuery({variables: { id:pageId}});

    if (loading) {
        return <h1>Loading...</h1>
    }

    return (
        <Component pageId={pageId} 
            heading={data!.startPage!.heading} 
            preamble={data!.startPage!.preamble} />
    );
}

export default StartPage;