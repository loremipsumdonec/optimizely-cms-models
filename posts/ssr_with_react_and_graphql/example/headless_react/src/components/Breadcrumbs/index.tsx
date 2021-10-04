import { Breadcrumbs as Component } from './Breadcrumbs';
import { gql } from '@apollo/client';
import { useGetBreadcrumbsQuery } from '../../generated/graphql';

export const GET_BREADCRUMBS = gql`
    query getBreadcrumbs($forPageId:Int!) {
        breadcrumbs(forPageId:$forPageId) {
            breadcrumbs{
              text,
              url
            }
          }
    }
`;

interface Props {
    pageId:number
}

const StartPage: React.FC<Props> = ({pageId}) => {

    const { data, error, loading } = useGetBreadcrumbsQuery({variables: { forPageId:pageId}});

    if (loading) {
        return <h1>Loading...</h1>
    }

    return (
        <Component {...data!.breadcrumbs!}/>
    );
}

export default StartPage;