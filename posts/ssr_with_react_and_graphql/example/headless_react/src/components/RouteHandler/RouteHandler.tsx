import React from 'react';
import { useParams } from 'react-router-dom';
import { gql } from '@apollo/client';
import { useGetContextModelQuery } from '../../generated/graphql';
import ContentFactory from '../ContentFactory';

export const GET_MODEL_CONTEXT = gql`
    query getContextModel($url:String!) {
        contextModel(url: $url) {
            pageId,
            modelType,
            state
        }
    }
`;

const RouteHandler: React.FC = () => {

    let { route } = useParams<any>();

    if(route) {
        route = '/' + route;
    } else {
        route = '/';
    }

    const { data, error, loading } = useGetContextModelQuery({variables: { url: route }});

    if (loading) {
        return <h1>Loading...</h1>
    }

    return(
        <ContentFactory 
            pageId={data!.contextModel!.pageId} 
            modelType={data!.contextModel!.modelType} />
    )
}

export default RouteHandler;